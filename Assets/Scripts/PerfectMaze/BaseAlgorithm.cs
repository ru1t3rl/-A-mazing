using UnityEngine;
using System.Collections.Generic;

public abstract class BaseAlgorithm : MonoBehaviour
{
   public abstract void Execute(List<INode> nodes, INode startNode = null);
}
