using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeTrigger : MonoBehaviour
{
    [SerializeField] private MazeEnemy enemy;

    private void OnTriggerEnter(Collider other)
    {
        enemy.ActivatePlayerInMaze(true);
    }
}
