using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MazeEnemy : MonoBehaviour
{
    [SerializeField]
    private Graph graph;
    [SerializeField]
    private Node start;
    [SerializeField]
    private Node end;
    [SerializeField]
    private float speed = 2f;

    private PathFinding path;
    private int currentNodeIndex;

    private void Start()
    {
        path = graph.GetShortestPath(start, end);
        if (path.Nodes.Count > 0)
        {
            StartCoroutine(FollowPath());
        }
    }

    private IEnumerator FollowPath()
    {
        currentNodeIndex = 0;

        while (currentNodeIndex < path.Nodes.Count)
        {
            Node currentNode = path.Nodes[currentNodeIndex];
            while (transform.position != currentNode.transform.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, currentNode.transform.position, speed * Time.deltaTime);
                yield return null;
            }
            currentNodeIndex++;
        }
    }
}
