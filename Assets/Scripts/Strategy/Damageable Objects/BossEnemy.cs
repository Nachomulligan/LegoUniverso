using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour, IDamageable, IDeathLogic
{
    public HealthComponent healthComponent;

    private float currentHealth;
    public event Action OnBossKilled;
    
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
        OnBossKilled?.Invoke();
        GameManager.Instance.ChangeGameStatus(new VictoryState(), true);
    }

}
