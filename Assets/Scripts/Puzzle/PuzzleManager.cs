using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField]
    private List<int> correctSequence = new List<int> { 1, 2, 3 };
    private Queue<int> playerSequence = new Queue<int>();
    [SerializeField] private PuzzleSpawner puzzleSpawner;


    public void AddToSequence(int number)
    {
        if (playerSequence.Count >= correctSequence.Count)
            playerSequence.Dequeue(); 

        playerSequence.Enqueue(number);

        if (playerSequence.Count == correctSequence.Count)
            CheckSequence();
    }

    private void CheckSequence()
    {
        var playerArray = playerSequence.ToArray();

        for (int i = 0; i < correctSequence.Count; i++)
        {
            if (playerArray[i] != correctSequence[i])
            {
                Debug.Log("You lost! Resetting sequence.");
                TriggerPuzzleSpawner();
                ResetSequence();
                return;
            }
        }

        Debug.Log("You won!");
    }

    private void TriggerPuzzleSpawner()
    {
        if (puzzleSpawner != null)
        {
            puzzleSpawner.ActivateSpawn();
        }
        else
        {
            Debug.LogError("PuzzleSpawner no está asignado.");
        }
    }


    private void ResetSequence()
    {
        playerSequence.Clear();
    }

}