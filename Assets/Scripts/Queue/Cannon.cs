using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Transform firePoint;
    public Transform targetPoint;

    private ColaEnlazada bulletQueue = new ColaEnlazada();
    [SerializeField] private float fireRate = 5f;
    private bool isFiring = false;
    [SerializeField] private float bulletSpeed = 40f;
    
    [SerializeField] private int shootSound;
    private AudioManager audioManager;
    
    private void Awake()
    {
        audioManager = GameManager.Instance.audioManager; 
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Item>(out Item item))
        {
            Debug.Log("Item detected: " + item.gameObject.name);

            item.transform.SetParent(null);
            item.transform.position = firePoint.position;
            bulletQueue.Acolar(item.gameObject);

            Debug.Log("Item added to bullet queue: " + item.gameObject.name);

            if (!isFiring)
            {
                StartCoroutine(FireRoutine());
            }
        }
    }

    private IEnumerator FireRoutine()
    {
        isFiring = true;

        while (!bulletQueue.ColaVacia())
        {
            Fire();
            yield return new WaitForSeconds(fireRate);
            Debug.Log("Waiting for: " + fireRate + " seconds before next fire");
        }

        isFiring = false;
    }
    
    private void PlayShootSound()
    {
        if (shootSound >= 0 && shootSound < audioManager.soundEffects.Count)
        {
            audioManager.PlaySFX(shootSound);
        }
    }
    
    private void Fire()
    {
        if (!bulletQueue.ColaVacia())
        {
            GameObject bullet = bulletQueue.Primero();

            if (bullet != null)
            {
                Debug.Log("Firing bullet: " + bullet.name);

                bulletQueue.Desacolar();
                Debug.Log("Bullet removed from queue: " + bullet.name);

                bullet.transform.SetParent(null);
                
                bullet.transform.position = firePoint.position;

                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                if (bulletRb != null && targetPoint != null)
                {
                    Vector3 direction = (targetPoint.position - bullet.transform.position).normalized;
                    bulletRb.velocity = direction * bulletSpeed;
                    PlayShootSound();
                }
            }
        }
    }
}
