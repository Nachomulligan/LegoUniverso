using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float damage);
}

public interface IHealth
{
    float CurrentHealth { get; set; }
    float MaxHealth { get; }
}
