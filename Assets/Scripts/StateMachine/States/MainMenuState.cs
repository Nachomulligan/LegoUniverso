using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuState : GameState
{
    public override void Enter(GameManager gameManager)
    {
        Debug.Log("Entering Menu");
    }

    public override void Exit(GameManager gameManager)
    {
        Debug.Log("Leaving Menu");
    }

    public override void Update(GameManager gameManager)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameManager.ChangeGameStatus(new GameplayState(), true);
        }
    }
}
