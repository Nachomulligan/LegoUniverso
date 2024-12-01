using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuState : GameState
{
    public override void Enter(GameManager gameManager)
    {
        AsyncScenesManager asyncScenesManager = ServiceLocator.Instance.GetService<AsyncScenesManager>();
        
        if (!asyncScenesManager.IsPermanentSceneLoaded())
        {
            asyncScenesManager.LoadPermanentSceneAsync();
        }
        
        if (!SceneManager.GetSceneByName("Menu").isLoaded)
        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
        }

        gameManager.audioManager.PlayBGM(0);
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
            gameManager.SetCurrentLevel("Level 1");
            gameManager.ChangeGameStatus(new GameplayState());
        }
    }
}
