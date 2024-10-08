using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[DisallowMultipleComponent]
public class PlayerWeaponSelector : MonoBehaviour
{
    [SerializeField] private GunType currentWeapon = GunType.Crowbar;
    [SerializeField] private Transform gunParent;
    [SerializeField] private List<WeaponScriptableObject> guns;

    [Space]
    [Header("Runtime Filled")] 
    public WeaponScriptableObject activeGun;

    private WeaponScriptableObject crowbarWeapon;

    private void Start()
    {
        EquipInitialWeapon();
    }

    private void EquipInitialWeapon()
    {
        crowbarWeapon = guns.Find(gun => gun.type == GunType.Crowbar);
        
        EquipCrowbar();
    }

    private void EquipCrowbar()
    {
        activeGun = crowbarWeapon;
        crowbarWeapon.Spawn(gunParent,this);
        crowbarWeapon.ReloadWeapon();
    }
    
    public void UnequipWeapon()
    {
        if (activeGun != null)
        {
            activeGun.RemoveWeapon();
            EquipCrowbar();
        }
    }

    public void EquipWeapon(WeaponScriptableObject newWeapon)
    {
        if (activeGun != null)
        {
            activeGun.RemoveWeapon();
        }

        activeGun = newWeapon;
        activeGun.Spawn(gunParent, this);
        activeGun.ReloadWeapon();
    }
}
