using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    
    public enum GameStatus
    {
        Menu,
        Gameplay,
        Pause,
        Puzzle,
        Victory,
        Defeat
    }

    private static GameManager instance;
    private string currentScene;

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
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            currentScene = SceneManager.GetActiveScene().name;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public GameStatus CurrentGameStatus { get; private set; } = GameStatus.Menu;
    
    private void Update()
    {
        switch (CurrentGameStatus)
        {
            case GameStatus.Menu:
                if (Input.GetKeyDown(KeyCode.Space)) { ChangeGameStatus(GameStatus.Gameplay, true); }
                break;
            case GameStatus.Gameplay:
                if (Input.GetKeyDown(KeyCode.Escape)) { ChangeGameStatus(GameStatus.Pause, false); }
                break;
            case GameStatus.Pause:
                if (Input.GetKeyDown(KeyCode.Escape)) { ChangeGameStatus(GameStatus.Gameplay, false); }
                break;
            case GameStatus.Puzzle:
                if (Input.GetKeyDown(KeyCode.E)) { ChangeGameStatus(GameStatus.Gameplay, false); }
                break;
            case GameStatus.Victory:
                if (Input.GetKeyDown(KeyCode.R)) { ChangeGameStatus(GameStatus.Menu, true); }
                break;
            case GameStatus.Defeat:
                if (Input.GetKeyDown(KeyCode.R)) { ChangeGameStatus(GameStatus.Menu, true); }
                break;
        }
    }
    
    public void ChangeGameStatus(GameStatus newStatus, bool loadScene)
    {
        CurrentGameStatus = newStatus;

        switch (newStatus)
        {
            case GameStatus.Menu:
                SceneManager.LoadScene("Menu");
                break;
            case GameStatus.Gameplay:
                if (loadScene) { SceneManager.LoadScene("Level 1"); }
                ResumeGame();
                break;
            case GameStatus.Pause:
                PauseGame();
                break;
            case GameStatus.Puzzle:
                Time.timeScale = 0f;
                break;
            case GameStatus.Victory:
                SceneManager.LoadScene("VictoryScene");
                break;
            case GameStatus.Defeat:
                SceneManager.LoadScene("DefeatScene");
                break;
        }
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }
    
    public void CompletePuzzle()
    {
        ChangeGameStatus(GameStatus.Gameplay, false);
    }
}
