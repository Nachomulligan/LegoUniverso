using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSpawner : MonoBehaviour
{
    public ABB arbol;  
    public GameObject[] gameObjects;  
    public Transform[] spawnPoints; 
    public float delayBetweenSpawns = 1f; 
    [SerializeField] private int spawnSound;
    private AudioManager audioManager;
    
    void Awake()
    {
        arbol.InicializarArbol();
        
        audioManager = GameManager.Instance.audioManager;

        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (gameObjects[i] == null)
            {
                Debug.LogError($"El GameObject en el índice {i} no está asignado.");
                continue;
            }
            arbol.AgregarElem(ref arbol.raiz, gameObjects[i]);
        }
    }

    public void ActivateSpawn()
    {
        int randomTraversal = Random.Range(0, 4); 
        switch (randomTraversal)
        {
            case 0:
                Debug.Log("Spawn usando PreOrder");
                arbol.InstanciarPreOrder(arbol.raiz, spawnPoints, delayBetweenSpawns);
                break;
            case 1:
                Debug.Log("Spawn usando InOrder");
                arbol.InstanciarInOrder(arbol.raiz, spawnPoints, delayBetweenSpawns);
                break;
            case 2:
                Debug.Log("Spawn usando PostOrder");
                arbol.InstanciarPostOrder(arbol.raiz, spawnPoints, delayBetweenSpawns);
                break;
            case 3:
                Debug.Log("Spawn usando LevelOrder");
                arbol.InstanciarLevelOrder(arbol.raiz, spawnPoints, delayBetweenSpawns);
                break;
        }
    }
    
    public void PlaySpawnSound()
    {
        if (spawnSound >= 0 && audioManager != null)
        {
            audioManager.PlaySFX(spawnSound);
        }
    }
}