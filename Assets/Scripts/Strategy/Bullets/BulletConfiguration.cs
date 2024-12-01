using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletConfig", menuName = "Bullets/BulletConfig", order = 1)]
public class BulletConfiguration : ScriptableObject
{
    public float dmg = 0f;
    public float speed = 20f;
    public float maxLifetime = 1f;
    public GameObject bulletPrefab;
    public bool destroyOnContact = true;
}
