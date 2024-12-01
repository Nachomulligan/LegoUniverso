using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBoss : MonoBehaviour, IDamageable, IDeathLogic
{
    public ABB arbol;  
    public GameObject[] gameObjects;  
    public Transform[] spawnPoints; 
    public float delayBetweenSpawns = 1f; 
    public HealthComponent healthComponent;
    
    [SerializeField] private int dmgSound;
    [SerializeField] private int spawnSound;
    private AudioManager audioManager;
    
    void Awake()
    {
        healthComponent = GetComponent<HealthComponent>();
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
        arbol.InstanciarLevelOrder(arbol.raiz, spawnPoints, delayBetweenSpawns);
    }
    
    public void TakeDamage(float damage)
    {
        healthComponent.TakeDamage(damage);
        PlayDMGSound();
    }
    
    public void Die()
    {
        Destroy(gameObject);
    }
    
    public void PlayDMGSound()
    {
        if (spawnSound >= 0 && audioManager != null)
        {
            audioManager.PlaySFX(dmgSound);
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