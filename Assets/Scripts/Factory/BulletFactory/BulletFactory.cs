using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : AbstractFactory<Bullet>
{
    private BulletPool bulletPool;
    private BulletConfiguration bulletConfig;

    public void Initialize(Bullet bulletPrefab, BulletConfiguration config)
    {
        bulletPool = new BulletPool(bulletPrefab);
        bulletConfig = config;
    }

    public override GameObject Create(Vector3 position, Quaternion rotation)
    {
        Bullet bullet = bulletPool.GetFromPool(position, rotation);
        bullet.Initialize(bulletConfig, this, rotation * Vector3.forward);
        return bullet.gameObject;
    }

    public void ReturnToPool(GameObject obj)
    {
        Bullet bullet = obj.GetComponent<Bullet>();
        bulletPool.ReturnToPool(bullet);
    }
}

