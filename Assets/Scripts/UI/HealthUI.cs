using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text shieldText;
    
    private ShieldDecorator shield;
    
    private void Update()
    {
        HealthDisplay();
        ShieldDisplay();
    }

    private void HealthDisplay()
    {
        healthText.text = character.healthComponent.CurrentHealth.ToString();
    }
    
    private void ShieldDisplay()
    {
        if (character != null && character.decoratedCharacter != null)
        {
            if (character.decoratedCharacter is ShieldDecorator shieldDecorator && shieldDecorator.GetShieldAmount() > 0)
            {
                shieldText.text = shieldDecorator.GetShieldAmount().ToString();
            }
            else
            {
                shieldText.text = " ";
            }
        }
        
    }
}
