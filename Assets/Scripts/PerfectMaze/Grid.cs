using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public const int MAX_WIDTH = 250;
    public const int MAX_HEIGHT = 250;
    public const int MIN_WIDTH = 10;
    public const int MIN_HEIGHT = 10;

    protected List<INode> nodes;
    public List<INode> Nodes => nodes;

    [SerializeField] protected float distanceBetweenNodes = 1;

    /// <summary>Generate the grid</summary>
    /// <param name="size">The size of the grid</param>
    public virtual void Generate(Vector2Int size)
    {
        if (nodes.Count > 0)
        {
            for (int i = nodes.Count; i-- > 0;)
            {
                Destroy((nodes[i] as Node).gameObject);
            }
            nodes.Clear();
        }

        size.x = Mathf.Max(size.x, MIN_WIDTH);
        size.x = Mathf.Min(size.x, MAX_WIDTH);

        size.y = Mathf.Max(size.y, MIN_HEIGHT);
        size.y = Mathf.Min(size.y, MAX_HEIGHT);

        nodes = new List<INode>();
        for (int iPosition = 0; iPosition < size.x * size.y; iPosition++)
        {
            GameObject node = new GameObject($"Node_{iPosition}");
            node.transform.parent = transform;

            nodes.Add(node.AddComponent<Node>());

            (nodes[iPosition] as Node).SetID(iPosition);

            // The x position x is the remainder of x times the distance modulo the width
            // The y position is distnace * (x divider width) floored
            node.transform.position = new Vector3(
                (iPosition * distanceBetweenNodes) % size.x,
                0,
                distanceBetweenNodes * (Mathf.Floor(iPosition / size.x))
            );

            #region LinkNeighbours
            if (iPosition - size.x >= 0)
            {
                (nodes[iPosition] as Node).AddNeighbor(Position.North, nodes[iPosition - size.x]);
                (nodes[iPosition - size.x] as Node).AddNeighbor(Position.South, nodes[iPosition]);
            }

            if ((iPosition - 1) >= 0 &&
                Mathf.Floor((iPosition - 1) / size.x) == Mathf.Floor((iPosition) / size.x))
            {
                (nodes[iPosition] as Node).AddNeighbor(Position.West, nodes[iPosition - 1]);
                (nodes[iPosition - 1] as Node).AddNeighbor(Position.East, nodes[iPosition]);
            }
            #endregion
        }
    }

    public virtual void Reset()
    {
        if (nodes != null)
        {
            foreach (INode node in nodes)
            {
                node.Reset();
            }
        }
    }

    /// <summary>Set the neigbors of a node</summary>
    /// <param name="node">The node to set the neighbors of</param>
    /// <param name="index">The index of the node</param>
    /// <param name="size">The size of the grid</param>
    /// <returns>The node with the neighbors set</returns>
    protected Node SetNeighbors(Node node, int index, Vector2Int size)
    {
        // North = index - width
        // East = index + 1
        // South = index + width
        // West = index - 1

        if (index - size.x >= 0)
        {
            node.AddNeighbor(Position.North, nodes[index - size.x]);
        }

        if (Mathf.Floor((index + 1) / size.x) == Mathf.Floor((index) / size.x))
        {
            node.AddNeighbor(Position.East, nodes[index + 1]);
        }

        if (index + size.x < size.x * size.y)
        {
            node.AddNeighbor(Position.South, nodes[index + size.x]);
        }

        if (Mathf.Floor((index - 1) / size.x) == Mathf.Floor((index) / size.x))
        {
            node.AddNeighbor(Position.West, nodes[index - 1]);
        }

        return node;
    }
}
