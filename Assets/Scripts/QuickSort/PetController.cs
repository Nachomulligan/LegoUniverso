using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour, IInteractable
{
    public InteractPriority InteractPriority => InteractPriority.High;

    private List<Enemy> detectedEnemies = new List<Enemy>();
    private Transform character;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] ShootingBehavior shootingBehavior;
    private bool canShoot = false;

    [SerializeField] private int interactionSound;
    private AudioManager audioManager;
    
    private void Awake()
    {
        Character playerComponent = FindObjectOfType<Character>();
        audioManager = GameManager.Instance.audioManager;
        if (playerComponent != null)
        {
            character = playerComponent.transform;
        }
    }

    private void PlayInteractionSound()
    {
        if (interactionSound >= 0 && interactionSound < audioManager.soundEffects.Count)
        {
            audioManager.PlaySFX(interactionSound);
        }
    }

    public void Interact()
    {
        transform.SetParent(character.transform);
        transform.localPosition = new Vector3(0, 2f, 0);
        
        Collider petCollider = GetComponent<Collider>();
        Collider characterCollider = character.GetComponent<Collider>();
        if (petCollider != null && characterCollider != null)
        {
            Physics.IgnoreCollision(petCollider, characterCollider, true);
        }

        canShoot = true;
    }
    
    private IEnumerator<WaitForSeconds> EnableShootingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canShoot = true;
    }
    
    private void Update()
    {
        if (canShoot)
        {
            if (detectedEnemies.Count > 0)
            {
                Enemy closestEnemy = FindClosestEnemy();
                if (closestEnemy != null)
                {
                    LookAtEnemy(closestEnemy);
                    Vector3 shootDirection = (closestEnemy.transform.position - transform.position).normalized;
                    shootingBehavior.Shoot(shootDirection);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & enemyLayer) != 0)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null && !detectedEnemies.Contains(enemy))
            {
                detectedEnemies.Add(enemy);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & enemyLayer) != 0)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                detectedEnemies.Remove(enemy);
            }
        }
    }

    private Enemy FindClosestEnemy()
    {
        detectedEnemies.RemoveAll(enemy => enemy == null);
        
        if (detectedEnemies.Count == 0) return null;
        
        Enemy[] enemiesArray = detectedEnemies.ToArray();
        QuickSort(enemiesArray, 0, enemiesArray.Length - 1);
    
        return enemiesArray[0];
    }
    
    private void LookAtEnemy(Enemy enemy)
    {
        Vector3 direction = (enemy.transform.position - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f);
    }

    public void QuickSort(Enemy[] arr, int left, int right)
    {
        if (left < right)
        {
            int pivot = Partition(arr, left, right);
            QuickSort(arr, left, pivot);
            QuickSort(arr, pivot + 1, right);
        }
    }

    public int Partition(Enemy[] arr, int left, int right)
    {
        float pivotDistance = Vector3.Distance(transform.position, arr[(left + right) / 2].transform.position);
        while (true)
        {
            while (Vector3.Distance(transform.position, arr[left].transform.position) < pivotDistance && left < right)
            {
                left++;
            }
            while (Vector3.Distance(transform.position, arr[right].transform.position) > pivotDistance && left < right)
            {
                right--;
            }
            if (left < right)
            {
                Enemy temp = arr[right];
                arr[right] = arr[left];
                arr[left] = temp;
            }
            else
            {
                return right;
            }
        }
    }
}