using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour, IEnemyFactory
{
    private GameObject enemyPrefab;

    public EnemyFactory(GameObject prefab)
    {
        enemyPrefab = prefab;
    }
    
    public void SetPrefab(GameObject prefab)
    {
        enemyPrefab = prefab;
    }

    public GameObject CreateEnemy(Vector3 position, Quaternion rotation)
    {
        return Instantiate(enemyPrefab, position, rotation);
    }
}
