using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Graph : MonoBehaviour, IGraph<Node>
{
    [SerializeField] private List<Node> nodes = new List<Node>();

    public List<Node> Nodes => nodes;

    public void AddVertex(Node vertex)
    {
        if (!nodes.Contains(vertex))
            nodes.Add(vertex);
    }

    public void RemoveVertex(Node vertex)
    {
        if (nodes.Contains(vertex))
        {
            nodes.Remove(vertex);

            foreach (var node in nodes)
                node.Connections.Remove(vertex);
        }
    }

    public void AddEdge(Node from, Node to, int weight = 1)
    {
        if (nodes.Contains(from) && nodes.Contains(to) && !from.Connections.Contains(to))
        {
            from.Connections.Add(to);
        }
    }

    public void RemoveEdge(Node from, Node to)
    {
        if (nodes.Contains(from) && from.Connections.Contains(to))
        {
            from.Connections.Remove(to);
        }
    }

    public void InitializeGraph()
    {
        nodes.Clear();
        Node[] sceneNodes = FindObjectsOfType<Node>();

        foreach (Node node in sceneNodes)
        {
            AddVertex(node);
        }

        foreach (Node node in nodes)
        {
            foreach (Node connection in node.Connections)
            {
                AddEdge(node, connection);
            }
        }
    }

    public IEnumerable<Node> GetNeighbors(Node vertex)
    {
        if (nodes.Contains(vertex))
            return vertex.Connections;
        return Enumerable.Empty<Node>();
    }
    public PathFinding GetShortestPath(Node start, Node end)
    {
        if (start == null || end == null) throw new System.ArgumentNullException();

        PathFinding path = new PathFinding();
        if (start == end)
        {
            path.Nodes.Add(start);
            return path;
        }

        var unvisited = new HashSet<Node>(nodes);
        var previous = new Dictionary<Node, Node>();
        var distances = nodes.ToDictionary(node => node, node => int.MaxValue);

        distances[start] = 0;

        while (unvisited.Count > 0)
        {
            Node current = unvisited.OrderBy(node => distances[node]).First();
            unvisited.Remove(current);

            if (current == end)
            {
                while (previous.ContainsKey(current))
                {
                    path.Nodes.Insert(0, current);
                    current = previous[current];
                }
                path.Nodes.Insert(0, start);
                return path;
            }

            foreach (var neighbor in current.Connections)
            {
                if (!unvisited.Contains(neighbor)) continue;

                int tentativeDistance = distances[current] + 1;
                if (tentativeDistance < distances[neighbor])
                {
                    distances[neighbor] = tentativeDistance;
                    previous[neighbor] = current;
                }
            }
        }

        return path;
    }
}
