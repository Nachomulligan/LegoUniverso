using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ShieldPickUp : MonoBehaviour, IInteractable
{
    public InteractPriority InteractPriority => InteractPriority.High;
    
    [SerializeField] private float shieldAmount = 10f;
    [SerializeField] private Character character;

    public void Interact()
    {
        if (character.decoratedCharacter is ShieldDecorator shieldDecorator)
        {
            shieldDecorator.IncreaseShield(shieldAmount);
        }
        else
        {
            character.decoratedCharacter = new ShieldDecorator(character.healthComponent, shieldAmount);
        }
        
        Destroy(gameObject);
    }
}
