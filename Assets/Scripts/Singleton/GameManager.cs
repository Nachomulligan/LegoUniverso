using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string currentLevel { get; private set; } = "Level 1";
    public GameObject pauseMenu;
    private StateMachine stateMachine = new StateMachine();
    
    private static GameManager instance;

    public AudioManager audioManager;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject managerObject = new GameObject("GameManager");
                    instance = managerObject.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    private void Start()
    {
        ServiceLocator.Instance.SetService<AsyncScenesManager>(new AsyncScenesManager());
        ServiceLocator.Instance.SetService<EnemyFactory>(new EnemyFactory()); //The first time the project is initialized an error appears, the second time it disappears. It's because of how Unity initializes the project
        ServiceLocator.Instance.SetService<EnemySpawner>(new EnemySpawner());
        audioManager = ServiceLocator.Instance.GetService<AudioManager>();
        ChangeGameStatus(new MainMenuState());
    }

    private void Update()
    {
        stateMachine.Update(this);
    }

    public void ChangeGameStatus(GameState newStatus)
    {
        stateMachine.ChangeState(newStatus, this);
    }

    public void SetCurrentLevel(string levelName)
    {
        currentLevel = levelName;
    }

    public void DeactivatePauseOverlay()
    {
        pauseMenu.SetActive(false);
    }

    public void ActivePauseOverlay()
    {
        pauseMenu.SetActive(true);
    }
}