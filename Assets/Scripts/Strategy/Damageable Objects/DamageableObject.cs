using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableObject : MonoBehaviour, IDamageable, IDeathLogic
{
    public HealthComponent healthComponent;
    
    [SerializeField] private int dmgSound;
    private AudioManager audioManager;

    private void Awake()
    {
        healthComponent = GetComponent<HealthComponent>();
        audioManager = GameManager.Instance.audioManager;
    }

    public void TakeDamage(float damage)
    {
        healthComponent.TakeDamage(damage);
        
        if (dmgSound >= 0 && dmgSound < audioManager.soundEffects.Count)
        {
            audioManager.PlaySFX(dmgSound);
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
