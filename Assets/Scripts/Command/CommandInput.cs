using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CommandInput : MonoBehaviour
{
    public InputField inputField;
    public CommandManager commandManager;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            OnSubmit();
        }
    }

    public void OnSubmit()
    {
        string input = inputField.text;
        Debug.Log("Input received: " + input);
        commandManager.ExecuteCommand(input);
        inputField.text = "";
        inputField.Select();
        inputField.ActivateInputField();
    }
}
