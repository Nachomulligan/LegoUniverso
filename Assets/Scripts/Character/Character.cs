using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class WalkController : IMovementController
{
    private float movementSpeed;
    private Transform transform;
    private Rigidbody rb;
    private float jumpForce;
    private Transform groundCheck;
    private LayerMask groundLayer;
    private float groundRadius;
    private float jumpBuffer;
    private float jumpBufferCounter;

    public float JumpBufferCounter
    {
        get { return jumpBufferCounter; }
        set { jumpBufferCounter = value; }
    }

    public void SetMovementSpeed(float newSpeed)
    {
        movementSpeed = newSpeed;
    }

    public WalkController(float movementSpeed, Transform transform, Rigidbody rb, float jumpForce, Transform groundCheck, LayerMask groundLayer, float groundRadius, float jumpBuffer)
    {
        this.movementSpeed = movementSpeed;
        this.transform = transform;
        this.rb = rb;
        this.jumpForce = jumpForce;
        this.groundCheck = groundCheck;
        this.groundLayer = groundLayer;
        this.groundRadius = groundRadius;
        this.jumpBuffer = jumpBuffer;
        this.jumpBufferCounter = 0f;
    }

    public void Move(Vector3 playerInput)
    {
        Vector3 moveDirection = playerInput.normalized * movementSpeed * Time.deltaTime;
        transform.position += moveDirection;

        if (moveDirection != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, -1.1f);
        }

        if (IsGrounded() && jumpBufferCounter > 0f)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpBufferCounter = 0f;
        }
    }

    public void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBuffer;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundRadius, groundLayer);
    }
}

public interface IMovementController
{
    void Move(Vector3 playerInput);
    void Jump();
}

[System.Serializable]
public struct MovementControllerConfig
{
    public float movementSpeed;
}

public class Character : MonoBehaviour, IDamageable, IDeathLogic
{
    public float movementSpeed;
    private bool canMove = true;
    
    [SerializeField] private Transform interactionPoint;
    public float interactionRadius;
    [SerializeField] private LayerMask interactionLayer;
    
    public bool godMode = false;
    public MovementControllerConfig walkConfig;
    
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float jumpBuffer;
    [SerializeField] private float shield = 10f;
    public HealthComponent healthComponent;
    public IDamageable decoratedCharacter;
    
    private IMovementController currentController;
    public WalkController walkController;
    private Rigidbody rb;

    private Collider[] interactables = new Collider[5];
    
    [SerializeField] private int dmgSound;
    private AudioManager audioManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        walkController = new WalkController(walkConfig.movementSpeed, transform, rb, jumpForce, groundCheck, groundLayer, groundRadius, jumpBuffer);
        healthComponent = GetComponent<HealthComponent>();
        audioManager = GameManager.Instance.audioManager;
        
        currentController = walkController;

        decoratedCharacter = new ShieldDecorator(healthComponent, shield);
    }

    public void Update()
    {
        if (canMove)
        {
            Move();
            currentController.Jump();
            if (Input.GetKeyDown(KeyCode.E))
            {
                TryInteract();
            }
        }
    }

    private void Move()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");

        var movement = new Vector3(horizontal, 0, vertical).normalized;

        currentController.Move(movement);

        if (movement != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(movement);
            transform.rotation = newRotation;

            interactionPoint.rotation = newRotation;
        }
    }

    public void EnableMovement()
    {
        canMove = true;
    }

    public void DisableMovement()
    {
        canMove = false;
    }
    
    private void PlayDMGSound()
    {
        if (dmgSound >= 0 && dmgSound < audioManager.soundEffects.Count)
        {
            audioManager.PlaySFX(dmgSound);
        }
    }
    
    public void TakeDamage(float damage)
    {
        if (!godMode)
        {
            decoratedCharacter.TakeDamage(damage);
            PlayDMGSound();
        }
        else
        {
            Debug.Log("Gay Mode activated, cannot take damage");
        }
    }

    public void Die()
    {
        GameManager.Instance.ChangeGameStatus(new DefeatState());
    }

    private void TryInteract()
    {
        Debug.Log("Tried interacting");

        int elements = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionRadius, interactables, interactionLayer);

        if (elements == 0)
        {
            Debug.Log("No interactables found");
            return;
        }

        for (int i = 0; i < interactables.Length; i++)
        {
            var interactable = interactables[i];
            var interactableComponent = interactable.GetComponent<IInteractable>();

            if (interactableComponent != null)
            {
                interactableComponent.Interact();
                return;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}
