using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[DisallowMultipleComponent]
public class PlayerAction : MonoBehaviour
{
    [SerializeField] private PlayerWeaponSelector weaponSelector;
    [SerializeField] private KeyCode shootKey = KeyCode.P;
    public bool canShoot = true;
    
    private void Update()
    {
        if (weaponSelector.activeGun != null)
        {
            var fireMode = weaponSelector.activeGun.weaponConfig.fireMode;
            
            if (Input.GetKeyDown(shootKey) && canShoot || (Input.GetKey(shootKey) && fireMode == WeaponConfiguration.FireMode.Auto) && canShoot)
            {
                HandleShooting();
            }
            
            //Keep It inside null to avoid errors
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

    public void EnbaleShooting()
    {
        canShoot = true;
    }

    public void DisableShooting()
    {
        canShoot = false;
    }
}
