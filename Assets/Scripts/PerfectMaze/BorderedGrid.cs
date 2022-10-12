using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderedGrid : Grid
{
    [SerializeField] private GameObject wallPrefab;

    /// <summary>Generate the grid</summary>
    /// <param name="size">The size of the grid</param>
    public override void Generate(Vector2Int size)
    {
        if (nodes.Count > 0)
        {
            for (int i = nodes.Count; i-- > 0;)
            {
                Destroy((nodes[i] as BorderedNode).gameObject);
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
            GameObject node = new GameObject($"BorderedNode_{iPosition}");
            node.transform.parent = transform;

            nodes.Add(node.AddComponent<BorderedNode>());
            (nodes[iPosition] as BorderedNode).Initialize(iPosition, wallPrefab);

            // The x position x is the remainder of x times the distance modulo the width
            // The y position is distnace * (x divider width) floored
            node.transform.position = new Vector3(
                ((iPosition * distanceBetweenNodes) % size.x),
                0,
                -(distanceBetweenNodes * (Mathf.Floor(iPosition / size.x)))
            );

            #region LinkNeighbours
            if (iPosition - size.x >= 0)
            {
                (nodes[iPosition] as BorderedNode).AddNeighbor(Position.North, nodes[iPosition - size.x]);
                (nodes[iPosition - size.x] as BorderedNode).AddNeighbor(Position.South, nodes[iPosition]);
            }

            if ((iPosition - 1) >= 0 &&
                Mathf.Floor((iPosition - 1) / size.x) == Mathf.Floor((iPosition) / size.x))
            {
                (nodes[iPosition] as BorderedNode).AddNeighbor(Position.West, nodes[iPosition - 1]);
                (nodes[iPosition - 1] as BorderedNode).AddNeighbor(Position.East, nodes[iPosition]);
            }
            #endregion
        }
    }
}
