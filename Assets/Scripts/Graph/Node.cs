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
        // Establece el color del gizmo a rojo
        Gizmos.color = Color.red;

        // Dibuja un gizmo esférico en la posición del nodo
        Gizmos.DrawSphere(transform.position, 1f); // El tamaño del gizmo es 0.3f, puedes ajustarlo
    }
}