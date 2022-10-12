using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private Grid grid;
    [SerializeField] private BaseAlgorithm algorithm;

    private void Start()
    {
        grid.Generate(gridSize);
        StartCoroutine(algorithm.Execute(grid.Nodes, grid.Nodes[0]));
    }

    public void Generate()
    {
        grid.Reset();
        StartCoroutine(algorithm.Execute(grid.Nodes, grid.Nodes[0]));
    }

    public void Generate(Vector2Int gridSize)
    {
        grid.Generate(gridSize);
        StartCoroutine(algorithm.Execute(grid.Nodes, grid.Nodes[0]));
    }
}
