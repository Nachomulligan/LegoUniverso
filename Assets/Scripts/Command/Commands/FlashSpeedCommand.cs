using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.TextCore.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "GodModeCommand", menuName = "Commands/Flash", order = 1)]
public class FlashSpeedCommand : CommandSO
{
    public float multiplier = 5f;
    private float originalSpeed;
    private bool isFlashActive = false;

    public override void Execute()
    {
        Character character = FindObjectOfType<Character>();

        if (character != null)
        {
            if (isFlashActive == false)
            {
                originalSpeed = character.movementSpeed;
                character.movementSpeed *= multiplier;
                character.walkController.SetMovementSpeed(character.movementSpeed);
                isFlashActive = true;
                Debug.Log("FlashSpeed activated! Current speed:  " + character.movementSpeed);
            }
            else
            {
                character.movementSpeed = originalSpeed;
                character.walkController.SetMovementSpeed(character.movementSpeed);
                isFlashActive = false;
                Debug.Log("FlashSpeed deactivated! Current speed: " + character.movementSpeed);
            }
        }
        else
        {
            Debug.Log("Cannot find Character");
        }
    }
    
    public void ResetState()
    {
        isFlashActive = false;
    }
}
