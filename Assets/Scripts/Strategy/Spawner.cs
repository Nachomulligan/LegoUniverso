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
        bulletFactory = new BulletFactory();
        bulletFactory.Initialize(bulletConfig.bulletPrefab.GetComponent<Bullet>());
        audioManager = GameManager.Instance.audioManager; 
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
        GameObject bullet = bulletFactory.CreateBullet(bulletConfig, spawnPoint.position, direction);
        PlaySpawnSound();
        bullet.transform.SetParent(null);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.velocity = Vector3.zero;
        }
    }
}
