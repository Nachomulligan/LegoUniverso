using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleState : GameState
{
    public override void Enter(GameManager gameManager)
    {
        Time.timeScale = 0f;
        Debug.Log("Pausing game");
    }

    public override void Exit(GameManager gameManager)
    {
        Time.timeScale = 1f;
        Debug.Log("Leaving pause");
    }

    public override void Update(GameManager gameManager)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            gameManager.ChangeGameStatus(new GameplayState(), false);
        }
    }
}
