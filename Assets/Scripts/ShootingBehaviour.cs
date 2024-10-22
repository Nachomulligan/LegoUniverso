using UnityEngine;
using UnityEngine.Serialization;

public class ShootingBehavior : MonoBehaviour
{
    [SerializeField] private BulletConfiguration bulletConfig;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float fireRate = 1f;
    private float lastShootTime;
    
    public void Shoot(Vector3 direction)
    {
        if (Time.time >= fireRate + lastShootTime)
        {
            lastShootTime = Time.time;
            GameObject bullet = Instantiate(bulletConfig.bulletPrefab, shootPoint.position, Quaternion.LookRotation(direction));
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.SetDirection(direction);
            }
        }
    }
}