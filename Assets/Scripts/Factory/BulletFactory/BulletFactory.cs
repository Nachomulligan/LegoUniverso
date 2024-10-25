using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : MonoBehaviour, IBulletFactory
{
    public GameObject CreateBullet(BulletConfiguration bulletConfig, Vector3 position, Vector3 direction)
    {
        GameObject bullet = Instantiate(bulletConfig.bulletPrefab, position, Quaternion.LookRotation(direction));

        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.bulletConfig = bulletConfig;
            bulletScript.SetDirection(direction);
        }

        return bullet;
    }
}
