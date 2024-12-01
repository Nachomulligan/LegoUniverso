using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossEnemy : MonoBehaviour, IDamageable, IDeathLogic
{
    public HealthComponent healthComponent;
    [SerializeField] private string scene = "Level 1";

    private float currentHealth;
    
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
        SceneManager.UnloadSceneAsync("Level 1");
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        GameManager.Instance.SetCurrentLevel("Level 2");
    }

}
