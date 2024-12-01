using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : AbstractFactory<Enemy>
{
    private Enemy enemyPrefab;
    private Vector3 triggerBoxSize = new Vector3(10f, 10f, 10f);
    
    private ObjectPool<Enemy> enemyPool;

    public void Initialize(Enemy enemy, Vector3 triggerSize)
    {
        enemyPrefab = enemy;
        triggerBoxSize = triggerSize;
        enemyPool = new ObjectPool<Enemy>(enemyPrefab);
    }

    public override GameObject Create(Vector3 position, Quaternion rotation)
    {
        Enemy enemy = enemyPool.GetFromPool(position, rotation);
        enemy.healthComponent.HealToMax();
        enemy.SetTriggerSize(triggerBoxSize);
        
        return enemy.gameObject;
    }

    public void ReturnToPool(GameObject obj)
    {
        Enemy enemy = obj.GetComponent<Enemy>();
        enemyPool.ReturnToPool(enemy);
    }

    public void ClearPool()
    {
        enemyPool.ClearPool();
    }
}