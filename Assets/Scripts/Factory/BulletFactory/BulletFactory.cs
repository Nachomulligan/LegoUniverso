using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : MonoBehaviour, IBulletFactory
{
    private BulletPool bulletPool;
    
    public void Initialize(Bullet bulletPrefab)
    {
        bulletPool = new BulletPool(bulletPrefab);
    }
    
    public GameObject CreateBullet(BulletConfiguration bulletConfig, Vector3 position, Vector3 direction)
    {
        Bullet bullet = bulletPool.GetFromPool(position, Quaternion.LookRotation(direction));
        bullet.Initialize(bulletConfig, this, direction);
        return bullet.gameObject;
    }
    
    public void ReturnToPool(Bullet bullet)
    {
        bulletPool.ReturnToPool(bullet);
    }
}
