using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDecorator : IDamageable
{
    private readonly IDamageable decorateOBJ;
    private float shield;

    public float Shield => shield;

    public ShieldDecorator(IDamageable decoratedObject, float shieldAmount)
    {
        decorateOBJ = decoratedObject;
        shield = shieldAmount;
    }

    public void TakeDamage(float damage)
    {
        if (shield > 0)
        {
            if (damage <= shield)
            {
                shield -= damage;
                return;
            }
            else
            {
                damage -= shield;
                shield = 0;
            }
        }
        
        decorateOBJ.TakeDamage(damage);
    }

    public void IncreaseShield(float amount)
    {
        shield += amount;
    }
    
    public float GetShieldAmount()
    {
        return shield;
    }
}


