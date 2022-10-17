using System.Collections.Generic;

namespace Ru1t3rl.PerfectMaze
{
    public interface INode
    {
        long ID { get; }

        Dictionary<Position, INode> neighbors { get; }

        bool visited { get; }
        bool HasUnvisitedNeighbors { get; }

        void Visit();
        void Reset();
    }
}