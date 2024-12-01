using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryState : GameState
{
    public override void Enter(GameManager gameManager)
    {
        AsyncScenesManager asyncScenesManager = ServiceLocator.Instance.GetService<AsyncScenesManager>();
        
        asyncScenesManager.UnloadSceneAsync(gameManager.currentLevel);
        
        asyncScenesManager.LoadNewLevel("VictoryScene");
        
        gameManager.audioManager.PlayBGM(2);
        
        Time.timeScale = 0f;
        Debug.Log("Entering Victory");
    }

    public override void Exit(GameManager gameManager)
    {
        SceneManager.UnloadSceneAsync("VictoryScene");
        Time.timeScale = 1f;
        Debug.Log("Leaving Victory");
    }

    public override void Update(GameManager gameManager)
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            gameManager.ChangeGameStatus(new MainMenuState());
        }
    }
}
