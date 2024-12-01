using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Shoot Config", menuName = "Weapons/Shoot Config", order = 2)]
public class WeaponConfiguration : ScriptableObject
{
    public enum FireMode
    {
        SingleShot,
        Auto,
        Burst,
        Shotgun
    }
    
    public Vector3 spread = new Vector3(0.1f, 0.1f, 0.1f);
    public float fireRate = 0.25f;
    public FireMode fireMode = FireMode.SingleShot;
    
    [Header("Burst Settings")]
    public int burstBulletCount = 0;
    public float burstInterval = 0f;
    
    [Header("Shotgun Settings")]
    public int shotgunBulletCount = 0;
}
