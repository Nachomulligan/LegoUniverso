using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[DisallowMultipleComponent]
public class PlayerAction : MonoBehaviour
{
    [SerializeField] private PlayerWeaponSelector weaponSelector;
    [SerializeField] private KeyCode shootKey = KeyCode.P;
    private void Update()
    {
        if (weaponSelector.activeGun != null)
        {
            var fireMode = weaponSelector.activeGun.weaponConfig.fireMode;
            
            if (Input.GetKeyDown(shootKey) || (Input.GetKey(shootKey) && fireMode == WeaponConfiguration.FireMode.Auto))
            {
                HandleShooting();
            }
            
            //Dejar dentro de null para que no tire error
            if (!weaponSelector.activeGun.HasAmmo() && weaponSelector.activeGun.type != GunType.Crowbar)
            {
                weaponSelector.UnequipWeapon();
            }
        }
    }
    
    private void HandleShooting()
    {
        var fireMode = weaponSelector.activeGun.weaponConfig.fireMode;
        
        if (fireMode == WeaponConfiguration.FireMode.SingleShot ||
            fireMode == WeaponConfiguration.FireMode.Auto ||
            fireMode == WeaponConfiguration.FireMode.Burst ||
            fireMode == WeaponConfiguration.FireMode.Shotgun)
        {
            weaponSelector.activeGun.Shoot();
        }
    }
}
