using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Object entered: {other.gameObject.name}, Layer: {LayerMask.LayerToName(other.gameObject.layer)}");

        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            EnemySpawner spawner = FindObjectOfType<EnemySpawner>();
            if (spawner != null)
            {
                spawner.StartSpawning();
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green; // Color del Gizmo
        Gizmos.DrawWireCube(transform.position, GetComponent<BoxCollider>().size);
    }
}