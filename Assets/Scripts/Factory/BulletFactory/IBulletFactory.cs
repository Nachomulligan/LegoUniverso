using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBulletFactory
{
    GameObject CreateBullet(BulletConfiguration bulletConfig, Vector3 position, Vector3 direction);
}
