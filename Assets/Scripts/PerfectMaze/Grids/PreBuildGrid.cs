using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using Ru1t3rl.Events;
using Ru1t3rl.PerfectMaze.Nodes;

namespace Ru1t3rl.PerfectMaze.Grids
{
    public class PreBuildGrid : Grid
    {
        public UnityEvent onFinishBuild;
        public string eventName = "onFinishGrid";

        public void Awake()
        {
            EventManager.Instance.AddEvent(eventName, onFinishBuild);
            StartCoroutine(PreBuild());
        }

        protected virtual IEnumerator PreBuild()
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

                nodes.Add(node.AddComponent<Node>());

                (nodes[iPosition] as Node).SetID(iPosition);

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

        public override void Generate(Vector2Int size)
        {
            Reset();

            int index = 0;
            for (int i = 0; i < size.x * size.y; i++)
            {
                index = i % size.x + (Mathf.FloorToInt(i / size.x) * MAX_WIDTH);

                (nodes[index] as Node).gameObject.SetActive(true);

                #region LinkNeighbours
                if (index - MAX_WIDTH >= 0)
                {
                    (nodes[index] as Node).AddNeighbor(Position.South, nodes[index - MAX_WIDTH]);
                    (nodes[index - MAX_WIDTH] as Node).AddNeighbor(Position.North, nodes[index]);
                }

                if ((index - 1) >= 0 &&
                    Mathf.Floor((index - 1) / MAX_WIDTH) == Mathf.Floor((index) / MAX_WIDTH))
                {
                    (nodes[index] as Node).AddNeighbor(Position.West, nodes[index - 1]);
                    (nodes[index - 1] as Node).AddNeighbor(Position.East, nodes[index]);
                }

                #endregion
            }
        }

        public override void Reset()
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                if (!nodes[i].visited && (nodes[i] as Node).gameObject.activeSelf)
                {
                    i = i + (250 - (i % 250)) - 1;
                }
                else
                {
                    (nodes[i] as Node).Reset();
                    (nodes[i] as Node).gameObject.SetActive(false);
                }
            }
        }
    }
}