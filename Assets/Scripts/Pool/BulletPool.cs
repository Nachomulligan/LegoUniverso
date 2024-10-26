using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : ObjectPool<Bullet>
{
    public BulletPool(Bullet bulletPrefab) : base(bulletPrefab) { }
}
