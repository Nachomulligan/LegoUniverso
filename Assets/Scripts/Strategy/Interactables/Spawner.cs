using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Spawner : MonoBehaviour, IInteractable
{
    public InteractPriority InteractPriority => InteractPriority.Low;

    [SerializeField] private BulletConfiguration bulletConfig;
    [SerializeField] private Transform spawnPoint;
    
    private BulletFactory bulletFactory;
    
    [SerializeField] private int spawnSound;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameManager.Instance.audioManager;
        
        if (bulletConfig != null && bulletConfig.bulletPrefab != null)
        {
            bulletFactory = new BulletFactory();
            bulletFactory.Initialize(bulletConfig.bulletPrefab.GetComponent<Bullet>(), bulletConfig);
        }
    }
    
    private void PlaySpawnSound()
    {
        if (spawnSound >= 0 && spawnSound < audioManager.soundEffects.Count)
        {
            audioManager.PlaySFX(spawnSound);
        }
    }
    
    public void Interact()
    {
        Vector3 direction = spawnPoint.forward;
        GameObject bullet = bulletFactory.Create(spawnPoint.position, Quaternion.LookRotation(direction));
        PlaySpawnSound();
        bullet.transform.SetParent(null);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.velocity = Vector3.zero;
        }
    }
}
