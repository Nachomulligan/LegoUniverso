using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TMPro.EditorUtilities;
using Unity.VisualScripting;

public class PuzzleTrigger : MonoBehaviour, IInteractable
{
    public InteractPriority InteractPriority => InteractPriority.High;

    [SerializeField] private Canvas minijuegoCanvas;
    [SerializeField] private GameObject puzzle;

    private bool isPuzzleActive = false;

    private void Start()
    {
        HanoiManager hanoiManager = FindObjectOfType<HanoiManager>();
        if (hanoiManager != null)
        {
            hanoiManager.OnVictory += CloseMiniGame;
        }
    }
    public void Interact()
    {
        if (!isPuzzleActive)
        {
            OpenMiniGame();
        }
        else
        {
            CloseMiniGame();
        }
    }

    private void OpenMiniGame()
    {
        puzzle.SetActive(true);
        minijuegoCanvas.gameObject.SetActive(true);
        GameManager.Instance.ChangeGameStatus(GameManager.GameStatus.Puzzle, false);
        isPuzzleActive = true;
    }

    public void CloseMiniGame()
    {
        minijuegoCanvas.gameObject.SetActive(false);
        GameManager.Instance.CompletePuzzle();
        isPuzzleActive = false;
        puzzle.SetActive(false);
    }

    private void OnDestroy()
    {
        HanoiManager hanoiManager = FindObjectOfType<HanoiManager>();
        if (hanoiManager != null)
        {
            hanoiManager.OnVictory -= CloseMiniGame;
        }
    }
}