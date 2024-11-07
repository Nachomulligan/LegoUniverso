using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private TMP_Text healthText;

    private void Update()
    {
        HealthDisplay();
    }

    private void HealthDisplay()
    {
        healthText.text = character.healthComponent.CurrentHealth.ToString();
    }
}
