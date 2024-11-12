using UnityEngine;

public class MazeTrigger : MonoBehaviour
{
    [SerializeField] private MazeEnemy enemy;

    private void OnTriggerEnter(Collider other)
    {
        enemy.SetPlayerInMaze(true);
    }
}