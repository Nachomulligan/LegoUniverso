using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeEnemy : MonoBehaviour
{
    [SerializeField] private Graph graph;
    [SerializeField] private Node initialNode;
    [SerializeField] private Transform player; 
    [SerializeField] private float speed = 2f; 
    [SerializeField] private float nodeReachThreshold = 0.1f;
    [SerializeField] private float pathUpdateInterval = 0.5f;

    private PathFinding path;
    private int currentNodeIndex;
    private Node currentNode;
    private Coroutine followPathCoroutine;
    private bool isPlayerInMaze = false;

    private void Start()
    {
        currentNode = initialNode;
        followPathCoroutine = StartCoroutine(UpdatePath());
    }
    
    private void Update()
    {
        if (isPlayerInMaze && path != null && currentNodeIndex < path.Nodes.Count)
        {
            MoveTowardsCurrentNode();
        }
    }

    public void SetPlayerInMaze(bool value)
    {
        isPlayerInMaze = value;
    }

    private Node FindClosestNodeToPlayer()
    {
        Node closestNode = null;
        float closestDistance = Mathf.Infinity;

        foreach (Node node in graph.Nodes)
        {
            float distance = Vector3.Distance(player.position, node.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestNode = node;
            }
        }

        return closestNode;
    }

    private IEnumerator UpdatePath()
    {
        while (true)
        {
            if (isPlayerInMaze)
            {
                Node playerNode = FindClosestNodeToPlayer();
                if (path == null || playerNode != path.Nodes[path.Nodes.Count - 1])
                {
                    path = graph.GetShortestPath(currentNode, playerNode);
                    currentNodeIndex = FindClosestNodeInPath();
                }
            }

            yield return new WaitForSeconds(pathUpdateInterval);
        }
    }
    
    private int FindClosestNodeInPath()
    {
        int closestNodeIndex = 0;
        float closestDistance = Mathf.Infinity;
        
        for (int i = 0; i < path.Nodes.Count; i++)
        {
            float distance = Vector3.Distance(transform.position, path.Nodes[i].transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestNodeIndex = i;
            }
        }

        return closestNodeIndex;
    }

    private void MoveTowardsCurrentNode()
    {
        Node targetNode = path.Nodes[currentNodeIndex];
        float distanceToTarget = Vector3.Distance(transform.position, targetNode.transform.position);

        Vector3 direction = (targetNode.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, speed * Time.deltaTime);

        if (distanceToTarget > nodeReachThreshold)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetNode.transform.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = targetNode.transform.position;
            currentNode = targetNode;
            currentNodeIndex++;
        }
    }
}