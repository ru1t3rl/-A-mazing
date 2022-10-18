using UnityEngine;
using Ru1t3rl.PerfectMaze.Algorithms;

namespace Ru1t3rl.PerfectMaze
{
    public class Maze : MonoBehaviour
    {
        [SerializeField] private Vector2Int gridSize;
        [SerializeField] private Ru1t3rl.PerfectMaze.Grids.Grid grid;
        [SerializeField] private BaseAlgorithm algorithm;


        public void Generate()
        {
            grid.Generate(gridSize);
            algorithm.Execute(grid.Nodes, grid.Nodes[0]);
        }

        public void Generate(Vector2Int gridSize)
        {
            //grid.Generate(gridSize);
            //algorithm.Execute(grid.Nodes, grid.Nodes[0]);
        }

        public void SetWidth(int width) {
            gridSize.x = width;
        }

        public void SetHeight(int height) {
            gridSize.y = height;
        }
    }
}