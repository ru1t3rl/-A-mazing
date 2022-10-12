using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public abstract class BaseAlgorithm : MonoBehaviour
{
    public abstract IEnumerator Execute(List<INode> nodes, INode startNode = null);
}
