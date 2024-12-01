using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponAmmo", menuName = "Weapons/WeaponAmmo", order = 3)]
public class WeaponAmmo : ScriptableObject
{
    public int maxAmmo = 0;
    public int ammoSpentPerShot = 0;
    public BulletConfiguration bulletConfig;
}