using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBoss : MonoBehaviour
{
    public ABB arbol;  
    public GameObject[] gameObjects;  
    public Transform[] spawnPoints; 
    public float delayBetweenSpawns = 1f; 

    void Start()
    {
        arbol.InicializarArbol();

        for (int i = 0; i < gameObjects.Length; i++)
        {
            arbol.AgregarElem(ref arbol.raiz, gameObjects[i]);
        }

      // arbol.InstanciarPreOrder(arbol.raiz, spawnPoints, delayBetweenSpawns);
        //arbol.InstanciarInOrder(arbol.raiz, spawnPoints, delayBetweenSpawns);
        arbol.InstanciarPostOrder(arbol.raiz, spawnPoints, delayBetweenSpawns);
      // arbol.InstanciarLevelOrder(arbol.raiz, spawnPoints, delayBetweenSpawns);
   
    }
}
