using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour, IInteractable
{
    public InteractPriority InteractPriority => InteractPriority.Low;

    [SerializeField] private GameObject[] weapons; // Array de armas
    [SerializeField] private Transform spawnPoint;

    public void Interact()
    {
        // Verificamos que haya al menos un arma en la lista
        if (weapons != null && weapons.Length > 0)
        {
            // Elegir un arma aleatoria
            int randomIndex = Random.Range(0, weapons.Length);
            GameObject randomWeapon = weapons[randomIndex];

            // Instanciar el arma en el spawnPoint
            Instantiate(randomWeapon, spawnPoint.position, spawnPoint.rotation);

            // Destruir el spawner después de instanciar el arma
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("No weapons available to spawn!");
        }
    }
}
