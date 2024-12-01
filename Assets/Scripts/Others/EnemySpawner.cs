using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private BoxCollider spawnArea;
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private Vector3 triggerBoxSize = new Vector3(10f, 10f, 10f);
    private EnemyFactory enemyFactory;
    private bool isSpawning = false;

    private void Start()
    {
        enemyFactory = ServiceLocator.Instance.GetService<EnemyFactory>();
        enemyFactory.Initialize(enemyPrefab, triggerBoxSize);
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
            enemyFactory.Create(randomPosition, Quaternion.identity);

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
}

