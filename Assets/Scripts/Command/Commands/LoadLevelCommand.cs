using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "LoadLevel", menuName = "Commands/LoadLevel", order = 1)]
public class LoadLevelCommand : CommandSO
{
    [SerializeField] private string levelName;
    
    public override void Execute()
    {
        SceneManager.LoadScene(levelName);
    }
}
