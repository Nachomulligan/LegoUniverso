using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseState : GameState
{
    public override void Enter(GameManager gameManager)
    {
        gameManager.PauseGame();
        Debug.Log("Pausing game");
    }

    public override void Exit(GameManager gameManager)
    {
        gameManager.ResumeGame();
        Debug.Log("Leaving pause");
    }

    public override void Update(GameManager gameManager)
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameManager.ChangeGameStatus(new GameplayState(), false);
        }
    }
}
