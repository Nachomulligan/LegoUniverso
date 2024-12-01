using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteractable : MonoBehaviour, IInteractable
{
    public InteractPriority InteractPriority => InteractPriority.Medium;

    [SerializeField]
    private int buttonValue; 
    [SerializeField]
    private PuzzleManager puzzleManager;

    public void Interact()
    {
        if (puzzleManager != null)
        {
            puzzleManager.AddToSequence(buttonValue);
            Debug.Log($"Button {buttonValue} pressed.");
        }
        else
        {
            Debug.LogError("PuzzleManager is not assigned.");
        }
    }
}
