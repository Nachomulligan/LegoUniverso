using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField]
    private List<Node> connections = new List<Node>();

    public List<Node> Connections => connections;

    public Node this[int index] => connections[index];
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(transform.position, 1f); 
    }
}