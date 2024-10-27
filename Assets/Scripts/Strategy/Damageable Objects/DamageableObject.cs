using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableObject : MonoBehaviour, IDamageable, IDeathLogic
{
    public HealthComponent healthComponent;

    private void Awake()
    {
        healthComponent = GetComponent<HealthComponent>();
    }

    public void TakeDamage(float damage)
    {
        healthComponent.TakeDamage(damage);
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
