using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GodModeCommand", menuName = "Commands/GodMode", order = 1)]
public class GodModeCommand : CommandSO
{
    public override void Execute()
    {
        Character character = FindObjectOfType<Character>();
        
        if (character != null)
        {
            character.godMode = !character.godMode;
            string status = character.godMode ? "activated!" : "deactivated!";
            Debug.Log("Gay Mode " + status);
        }
        else
        {
            Debug.Log("Cannot found Character");
        }
    }
}
