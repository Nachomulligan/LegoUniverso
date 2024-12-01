using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleManager : MonoBehaviour
{
    [SerializeField] private GameObject consoleUI;
    [SerializeField] private Character character;
    [SerializeField] private PlayerAction playerAction;
    [SerializeField] private KeyCode toggleKey = KeyCode.Backslash;
    
    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleConsole();
        }
    }

    private void ToggleConsole()
    {
        bool isActive = consoleUI.activeSelf;
        consoleUI.SetActive(!isActive);

        if (!isActive)
        {
            character.DisableMovement();
            playerAction.DisableShooting();
        }
        else
        {
            character.EnableMovement();
            playerAction.EnbaleShooting();
        }
    }
}
