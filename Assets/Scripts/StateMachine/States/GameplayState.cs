using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayState : GameState
{
    public override void Enter(GameManager gameManager)
    {
        Debug.Log("Joining game");
    }

    public override void Exit(GameManager gameManager)
    {
        Debug.Log("Leaving game");
    }

    public override void Update(GameManager gameManager)
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameManager.ChangeGameStatus(new PauseState(), false);
        }
    }
}
