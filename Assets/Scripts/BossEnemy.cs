using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth;

    private float currentHealth;
    public event Action OnBossKilled;
    private void Awake()
    {
        currentHealth = maxHealth;
    }


    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        OnBossKilled?.Invoke();
        GameManager.Instance.ChangeGameStatus(GameManager.GameStatus.Victory, true);
    }

}
