using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "LoadLevel", menuName = "Commands/LoadLevel", order = 1)]
public class LoadLevelCommand : CommandSO
{
    [SerializeField] private string levelName;
    [SerializeField] private string newLevel = "Level 2";
    
    public override void Execute()
    {
        Scene currentScene = SceneManager.GetSceneByName(levelName);
        
        if (currentScene.isLoaded)
        {
            SceneManager.UnloadSceneAsync(currentScene);
        }
        
        SceneManager.LoadScene(newLevel, LoadSceneMode.Additive);
        GameManager.Instance.SetCurrentLevel(newLevel);
    }
}
