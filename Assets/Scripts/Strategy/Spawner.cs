using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Spawner : MonoBehaviour, IInteractable
{
    public InteractPriority InteractPriority => InteractPriority.Low;

    [SerializeField] private GameObject go;
    [SerializeField] private Transform spawnPoint;
    
    public void Interact()
    {
        Instantiate(go, spawnPoint.position, spawnPoint.rotation);
    }
}
