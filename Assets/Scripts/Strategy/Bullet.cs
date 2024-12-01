using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent((typeof(Rigidbody)))]
public class Bullet : MonoBehaviour
{
    public BulletConfiguration bulletConfig;
    
    private Vector3 direction;
    private BulletFactory bulletFactory;
    
    public void Initialize(BulletConfiguration config, BulletFactory factory, Vector3 direction)
    {
        bulletConfig = config;
        bulletFactory = factory;
        SetDirection(direction);
        StartCoroutine(DestroyBulletAfterTime(bulletConfig.maxLifetime));
    }
    
    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection;
    }

    private void Update()
    {
        if (bulletConfig != null)
        {
            transform.position += direction * bulletConfig.speed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(bulletConfig.dmg);
        }
        
        if (bulletConfig.destroyOnContact)
        {
            bulletFactory.ReturnToPool(this.gameObject);
        }
    }
    
    private IEnumerator DestroyBulletAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        bulletFactory.ReturnToPool(this.gameObject);
    }
}
