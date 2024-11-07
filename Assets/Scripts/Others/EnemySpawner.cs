using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private BoxCollider spawnArea;
    private bool isSpawning = false;

    private BossEnemy bossEnemy;
    private EnemyFactory enemyFactory;
    
    private void Start()
    {
        bossEnemy = FindObjectOfType<BossEnemy>();
        if (bossEnemy != null)
        {
            bossEnemy.OnBossKilled += StopSpawning;
        }

        enemyFactory = FindObjectOfType<EnemyFactory>();
    }

    public void StartSpawning()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            StartCoroutine(SpawnEnemies());
        }
    }

    private IEnumerator SpawnEnemies()
    {
        while (isSpawning)
        {
            Vector3 randomPosition = GetRandomPositionInArea();
            enemyFactory.CreateEnemy(randomPosition, Quaternion.identity);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private Vector3 GetRandomPositionInArea()
    {
        Vector3 center = spawnArea.bounds.center;
        Vector3 size = spawnArea.bounds.size;

        float randomX = Random.Range(center.x - size.x / 2, center.x + size.x / 2);
        float randomZ = Random.Range(center.z - size.z / 2, center.z + size.z / 2);

        return new Vector3(randomX, center.y, randomZ);
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(spawnArea.transform.position, spawnArea.size);
    }
}

