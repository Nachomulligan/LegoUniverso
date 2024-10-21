using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KillAllEnemies", menuName = "Commands/KillAllEnemies", order = 1)]
public class KillAllEnemiesCommand : CommandSO
{
    public override void Execute()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        if (enemies.Length > 0)
        {
            foreach (Enemy enemy in enemies)
            {
                enemy.TakeDamage(999f);
            }
            
            Debug.Log("All enemies have been erased");
        }
        else
        {
            Debug.Log("Cannot find enemies in the level");
        }
    }
}
