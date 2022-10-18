using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ru1t3rl.PerfectMaze;
using TMPro;

namespace Ru1t3rl.Utilities
{
    public class UpdateGridSize : MonoBehaviour
    {
        [SerializeField] private TMP_InputField width, height;
        [SerializeField] private Maze maze;

        public void SetWidth()
        {
            maze.SetWidth(int.TryParse(width.text, out int w) ? w : 10);
        }

        public void SetHeight()
        {
            maze.SetHeight(int.TryParse(height.text, out int h) ? h : 10);
        }
    }
}