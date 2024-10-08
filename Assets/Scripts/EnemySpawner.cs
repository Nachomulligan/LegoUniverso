using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab; // Prefab del enemigo a spawnear
    [SerializeField] private float spawnInterval = 2f; // Intervalo entre spawns
    [SerializeField] private BoxCollider spawnArea; // Área de spawn
    private bool isSpawning = false;

    private BossEnemy bossEnemy; // Referencia al jefe

    private void Start()
    {
        bossEnemy = FindObjectOfType<BossEnemy>();
        if (bossEnemy != null)
        {
            bossEnemy.OnBossKilled += StopSpawning; // Suscribirse al evento
        }
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
            // Genera una posición aleatoria dentro del área de spawn
            Vector3 randomPosition = GetRandomPositionInArea();
            Instantiate(enemyPrefab, randomPosition, Quaternion.identity);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private Vector3 GetRandomPositionInArea()
    {
        Vector3 center = spawnArea.bounds.center;
        Vector3 size = spawnArea.bounds.size;

        float randomX = Random.Range(center.x - size.x / 2, center.x + size.x / 2);
        float randomZ = Random.Range(center.z - size.z / 2, center.z + size.z / 2);

        return new Vector3(randomX, center.y, randomZ); // Mantener la misma altura
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Color del Gizmo
        Gizmos.DrawWireCube(spawnArea.transform.position, spawnArea.size);
    }
}

