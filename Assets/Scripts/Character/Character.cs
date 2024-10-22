using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

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

public class ClimbController : IMovementController
{
    private float movementSpeed;
    private Transform transform;

    public ClimbController(float movementSpeed, Transform transform)
    {
        this.movementSpeed = movementSpeed;
        this.transform = transform;
    }

    public void Move(Vector3 playerInput)
    {
        var vertical = playerInput.y;
        var movement = new Vector3(0, vertical, 0).normalized * movementSpeed * Time.deltaTime;
        transform.position += movement;
    }

    public void Jump()
    {

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

public interface IWalk
{
    void SetWalkState();
}

public interface IClimb
{
    void SetClimbState();
}

public class Character : MonoBehaviour, IDamageable, IWalk, IClimb, IDeathLogic
{
    public float movementSpeed;
    private bool canMove = true;
    [SerializeField] private Transform interactionPoint;
    public float interactionRadius;
    [SerializeField] private LayerMask interactionLayer;
    public bool godMode = false;
    public MovementControllerConfig walkConfig;
    [SerializeField] private MovementControllerConfig climbConfig;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float jumpBuffer;
    public HealthComponent healthComponent;
    
    private IMovementController currentController;
    public WalkController walkController;
    private ClimbController climbController;
    private Rigidbody rb;

    private Collider[] interactables = new Collider[5];

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        walkController = new WalkController(walkConfig.movementSpeed, transform, rb, jumpForce, groundCheck, groundLayer, groundRadius, jumpBuffer);
        climbController = new ClimbController(climbConfig.movementSpeed, transform);
        healthComponent = GetComponent<HealthComponent>();

        currentController = walkController;
    }

    public void Update()
    {
        if (canMove)
        {
            var horizontal = Input.GetAxisRaw("Horizontal");
            var vertical = Input.GetAxisRaw("Vertical");

            var movement = new Vector3(horizontal, 0, vertical).normalized;

            if (currentController is ClimbController)
            {
                movement = new Vector3(0, vertical, 0).normalized;
            }

            currentController.Move(movement);

            if (movement != Vector3.zero)
            {
                Quaternion newRotation = Quaternion.LookRotation(movement);
                transform.rotation = newRotation;

                interactionPoint.rotation = newRotation;
            }

            currentController.Jump();

            if (Input.GetKeyDown(KeyCode.E))
            {
                TryInteract();
            }
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
    
    public void SetWalkState()
    {
        currentController = walkController;
    }

    public void SetClimbState()
    {
        currentController = climbController;
    }

    public void TakeDamage(float damage)
    {
        if (!godMode)
        {
            healthComponent.TakeDamage(damage);
        }
        else
        {
            Debug.Log("Gay Mode activated, cannot take damage");
        }
    }

    public void Die()
    {
        GameManager.Instance.ChangeGameStatus(GameManager.GameStatus.Defeat, true);
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
