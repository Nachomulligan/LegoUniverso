using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour, IInteractable
{
    public InteractPriority InteractPriority => InteractPriority.High;

    [SerializeField] private WeaponScriptableObject weaponToPickUp;
    private PlayerWeaponSelector playerWeaponSelector;

    private void Start()
    {
        playerWeaponSelector = FindObjectOfType<PlayerWeaponSelector>();
    }
    
    public void Interact()
    {
        if (playerWeaponSelector.activeGun != null && playerWeaponSelector.activeGun.type == GunType.Crowbar)
        {
            playerWeaponSelector.EquipWeapon(weaponToPickUp);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Player can't pick up a new weapon while holding another firearm.");
        }
    }
}
