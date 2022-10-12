using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour, INode
{
    public Dictionary<Position, INode> neighbors { get; private set; }

    public bool visited { get; private set; }

    public virtual void Visit()
    {
        visited = true;
    }

    /// <summary>Add a neighbor to the node</summary>
    /// <param name="position">The position of the neighbor</param>
    /// <param name="node">The node to add as a neighbor</param>
    public void AddNeighbor(Position position, INode node)
    {
        if (neighbors is null)
            neighbors = new Dictionary<Position, INode>();

        neighbors.Add(position, node);
    }

    /// <summary>Get a random unvisited neighbor</summary>
    public Node? GetRandomUnvisitedNeighbor() => neighbors.GetRandomUnvisited() as Node;

    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

#if UNITY_EDITOR
        if (UnityEditor.Selection.activeObject == gameObject)
        {
            Gizmos.color = Color.red;
        }
#endif

        Gizmos.DrawSphere(transform.position, 0.1f);
    }
}