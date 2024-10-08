using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent((typeof(Rigidbody)))]
public class Bullet : MonoBehaviour
{
    public BulletConfiguration bulletConfig;
    
    private Vector3 direction;

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection;
    }

    private void Start()
    {
        Destroy(gameObject, bulletConfig.maxLifetime);
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
            Destroy(gameObject);
        }
    }
}
