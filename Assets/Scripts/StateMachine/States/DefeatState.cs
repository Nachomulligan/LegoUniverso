using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatState : GameState
{
    public override void Enter(GameManager gameManager)
    {
        SceneManager.LoadScene("DefeatScene");
        Time.timeScale = 0f;
    }

    public override void Exit(GameManager gameManager)
    {
        Time.timeScale = 1f;
        Debug.Log("Leaving Defeat");
    }

    public override void Update(GameManager gameManager)
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            gameManager.ChangeGameStatus(new MainMenuState(), true);
        }
    }
}
