using System.Collections;
using System.Diagnostics;
using UnityEngine;
using Ru1t3rl.Events;
using Ru1t3rl.PerfectMaze.Nodes;

namespace Ru1t3rl.PerfectMaze.Grids
{
    public class PreBuildBorderedGrid : PreBuildGrid
    {
        [SerializeField] private GameObject wall;

        protected override IEnumerator PreBuild()
        {
            Stopwatch performanceControl = new Stopwatch(),
                      totalDuration = new Stopwatch();
            nodes = new System.Collections.Generic.List<INode>();

            performanceControl.Start();
            totalDuration.Start();
            for (int iPosition = 0; iPosition < MAX_HEIGHT * MAX_WIDTH; iPosition++)
            {
                GameObject node = new GameObject($"Node_{iPosition}");
                node.SetActive(false);
                node.transform.parent = transform;

                nodes.Add(node.AddComponent<BorderedNode>());

                (nodes[iPosition] as BorderedNode).Initialize(iPosition, wall);

                // The x position x is the remainder of x times the distance modulo the width
                // The y position is distnace * (x divider width) floored
                node.transform.position = new Vector3(
                    (iPosition * distanceBetweenNodes) % MAX_WIDTH,
                    0,
                    distanceBetweenNodes * (Mathf.Floor(iPosition / MAX_WIDTH))
                );

                // 60 frames per second | 1000/60 == 1 frame
                if (performanceControl.ElapsedMilliseconds > (1000f / GeneralSettings.TARGET_FRAME_RATE))
                {
                    yield return null;
                    performanceControl.Reset();
                    performanceControl.Start();
                }
            }

            totalDuration.Stop();
            UnityEngine.Debug.Log($"Grid build took {totalDuration.ElapsedMilliseconds}ms");

            EventManager.Instance.Invoke(eventName);
        }
    }
}