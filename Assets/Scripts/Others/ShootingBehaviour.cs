using UnityEngine;
using UnityEngine.Serialization;

public class ShootingBehavior : MonoBehaviour
{
    [SerializeField] private BulletConfiguration bulletConfig;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float fireRate = 1f;
    private float lastShootTime;
    private BulletFactory bulletFactory;
    
    [SerializeField] private int shootSound;
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
    
    public void Shoot(Vector3 direction)
    {
        if (bulletConfig == null || bulletConfig.bulletPrefab == null)
        {
            return;
        }
        
        if (Time.time >= fireRate + lastShootTime)
        {
            lastShootTime = Time.time;
            GameObject bullet = bulletFactory.Create(shootPoint.position, Quaternion.LookRotation(direction));
            PlayShootSound();
        }
    }
    
    private void PlayShootSound()
    {
        if (shootSound >= 0 && shootSound < audioManager.soundEffects.Count)
        {
            audioManager.PlaySFX(shootSound);
        }
    }
}