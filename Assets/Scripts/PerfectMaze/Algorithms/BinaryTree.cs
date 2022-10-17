using System.Collections.Generic;
using UnityEngine;
using Ru1t3rl.PerfectMaze.Nodes;
using BaseGrid = Ru1t3rl.PerfectMaze.Grids.Grid;

namespace Ru1t3rl.PerfectMaze.Algorithms
{
    public class BinaryTree : BaseAlgorithm
    {
        public override void Execute(List<INode> nodes, INode startNode = null)
        {
            List<INode> availableNeigbors = new List<INode>();

            for (int i = 0; i < nodes.Count; i++)
            {
                if (!(nodes[i] as Node).gameObject.activeSelf)
                {
                    i = i + (250 - (i % 250)) - 1;
                }
                else
                {
                    availableNeigbors.Clear();

                    // Check for the north neighbor
                    if (i - BaseGrid.MAX_WIDTH >= 0)
                    {
                        availableNeigbors.Add(nodes[i - BaseGrid.MAX_WIDTH]);
                    }

                    // Check for the west neighbor
                    if (i + 1 < nodes.Count &&
                        Mathf.Floor((i + 1) / BaseGrid.MAX_WIDTH) == Mathf.Floor(i / BaseGrid.MAX_WIDTH) &&
                        (nodes[i + 1] as Node).gameObject.activeSelf)
                    {
                        availableNeigbors.Add(nodes[i + 1]);
                    }

                    if (availableNeigbors.Count > 0)
                    {
                        nodes[i].Visit();
                        (nodes[i] as BorderedNode).RemoveWall(availableNeigbors[Random.Range(0, availableNeigbors.Count)]);
                    }
                }
            }
        }
    }
}