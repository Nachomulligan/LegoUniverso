using UnityEngine;
using UnityEngine.Serialization;

public class ShootingBehavior : MonoBehaviour
{
    [SerializeField] private BulletConfiguration bulletConfig;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float fireRate = 1f;
    private float lastShootTime;
    private IBulletFactory bulletFactory;

    private void Awake()
    {
        bulletFactory = new BulletFactory();
    }
    
    public void Shoot(Vector3 direction)
    {
        if (Time.time >= fireRate + lastShootTime)
        {
            lastShootTime = Time.time;
            GameObject bullet = bulletFactory.CreateBullet(bulletConfig, shootPoint.position, direction);
        }
    }
}