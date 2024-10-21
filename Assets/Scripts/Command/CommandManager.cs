using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    public List<CommandSO> commands;
    private Dictionary<string, ICommand> commandDictionary;

    private void Awake()
    {
        commandDictionary = new Dictionary<string, ICommand>();
        ResetFlashCommand();
        
        foreach (var command in commands)
        {
            commandDictionary[command.commandName.ToLower()] = command;
        }
    }
    
    public void ResetFlashCommand()
    {
        foreach (var command in commands)
        {
            if (command is FlashSpeedCommand flashCommand)
            {
                flashCommand.ResetState();
            }
        }
    }
    
    public void ExecuteCommand(string input)
    {
        string commandInput = input.ToLower();

        if (commandDictionary.TryGetValue(commandInput, out ICommand command))
        {
            command.Execute();
        }
        else
        {
            Debug.Log("Command does not exist");
        }
    }
}
