using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyFactory
{
    GameObject CreateEnemy(Vector3 position, Quaternion rotation);
}
