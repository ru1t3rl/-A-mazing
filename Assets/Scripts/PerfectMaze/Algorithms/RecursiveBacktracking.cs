using System.Collections.Generic;
using Ru1t3rl.PerfectMaze.Nodes;

namespace Ru1t3rl.PerfectMaze.Algorithms
{
    public class RecursiveBacktracking : BaseAlgorithm
    {
        private Stack<BorderedNode> stack;

        public override void Execute(List<INode> nodes, INode startNode)
        {
            stack = new Stack<BorderedNode>();

            if (startNode is null)
            {
                startNode = nodes[0];  //nodes[Random.Range(0, nodes.Count)];
            }

            stack.Push(startNode as BorderedNode);
            startNode.Visit();

            while (stack.Count > 0)
            {
                BorderedNode currentNode = stack.Peek();
                BorderedNode nextNode = currentNode?.GetRandomUnvisitedNeighbor() as BorderedNode;

                if (nextNode != null)
                {
                    stack.Push(nextNode);
                    nextNode.Visit();
                    currentNode.RemoveWall(nextNode);
                    //nextNode.RemoveWall(currentNode);
                }
                else
                {
                    stack.Pop();
                }
            }

        }
    }
}