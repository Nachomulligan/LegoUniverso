using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGraph<T>
{
    void AddVertex(T vertex);
    void RemoveVertex(T vertex);
    void AddEdge(T from, T to, int weight);
    void RemoveEdge(T from, T to);
    IEnumerable<T> GetNeighbors(T vertex);
    void InitializeGraph();
}