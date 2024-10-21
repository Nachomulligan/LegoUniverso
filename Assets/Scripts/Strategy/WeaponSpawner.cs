using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour, IInteractable
{
    public InteractPriority InteractPriority => InteractPriority.Low;

    [SerializeField] private GameObject[] weapons;
    [SerializeField] private Transform spawnPoint;

    public void Interact()
    {
        if (weapons != null && weapons.Length > 0)
        {
            int randomIndex = Random.Range(0, weapons.Length);
            GameObject randomWeapon = weapons[randomIndex];
            
            Instantiate(randomWeapon, spawnPoint.position, spawnPoint.rotation);
            
            Destroy(gameObject);
        }
    }
}
