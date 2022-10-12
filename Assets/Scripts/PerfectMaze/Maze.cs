using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private Grid grid;
    [SerializeField] private BaseAlgorithm algorithm;

    private void Start() {
        grid.Generate(gridSize);
        algorithm.Execute(grid.Nodes, grid.Nodes[0]);
    }
}
