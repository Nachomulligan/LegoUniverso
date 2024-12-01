using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatState : GameState
{
    public override void Enter(GameManager gameManager)
    {
        AsyncScenesManager asyncScenesManager = ServiceLocator.Instance.GetService<AsyncScenesManager>();
        
        asyncScenesManager.UnloadSceneAsync(gameManager.currentLevel);
        
        asyncScenesManager.LoadNewLevel("DefeatScene");
        
        gameManager.audioManager.PlayBGM(3);
        
        Time.timeScale = 0f;

        Debug.Log("Entering Defeat");
    }

    public override void Exit(GameManager gameManager)
    {
        SceneManager.UnloadSceneAsync("DefeatScene");
        Time.timeScale = 1f;
        Debug.Log("Leaving Defeat");
    }

    public override void Update(GameManager gameManager)
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            gameManager.ChangeGameStatus(new MainMenuState());
        }
    }
}
