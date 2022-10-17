using UnityEngine;
using System.Collections.Generic;

namespace Ru1t3rl.PerfectMaze.Algorithms
{
    public abstract class BaseAlgorithm : MonoBehaviour
    {
        public abstract void Execute(List<INode> nodes, INode startNode = null);
    }
}