using System.Collections.Generic;
using UnityEngine;
using Ru1t3rl.PerfectMaze.Extensions;

namespace Ru1t3rl.PerfectMaze.Nodes
{
    public class BorderedNode : Node
    {
        private List<Wall> borders = new List<Wall>();

        public void Initialize(long ID, GameObject wall = null)
        {
            this.ID = ID;

            if (borders.Count > 0)
            {
                for (int i = 0; i < borders.Count; i++)
                {
                    Destroy(borders[i].gameObject);
                }
                borders.Clear();
            }

            foreach (Position position in System.Enum.GetValues(typeof(Position)))
            {
                borders.Add(CreateBorder(position, wall));
            }
        }

        /// <summary>Remove the border in the given position.</summary>
        /// <param name="position">The position of the border to remove.</param>
        public void RemoveWall(Position position, bool fromNeighbor = false)
        {
            if (!fromNeighbor)
            {
                // Request the neigbor the also remove the wall
                (neighbors[position] as BorderedNode).RemoveWall(
                    position.Opposite(),
                    fromNeighbor: true
                );
            }

            borders.Find(border => border.position == position).Hide();
            //borders.Remove(position);
        }

        /// <summary>Remove the border between this node and the given node.</summary>
        /// <param name="node">The node to remove the border with.</param>
        public void RemoveWall(INode neighbor)
        {
            foreach (Position key in neighbors.Keys)
            {
                if (neighbors[key].ID == neighbor.ID)
                {
                    RemoveWall(key);
                    return;
                }
            }

            throw new System.Exception($"[Node-{ID}] Failed to find matching neighbor (ID: {neighbor.ID})");
        }

        public override void Reset()
        {
            base.Reset();
            borders.ForEach(border => border.Show());
        }

        private Wall CreateBorder(Position position, GameObject wallObject = null)
        {
            wallObject = wallObject == null ? new GameObject("Wall") : Instantiate(wallObject);
            wallObject.transform.SetParent(transform);
            wallObject.transform.localPosition = Vector3.zero;

            Wall wall = wallObject.GetComponent<Wall>() ?? wallObject.AddComponent<Wall>();
            wall.Initialize(position, transform.localScale);
            wall.transform.SetParent(transform);
            return wall;
        }
    }
}