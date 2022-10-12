using System.Collections.Generic;

public interface INode
{
    long ID { get; }

    Dictionary<Position, INode> neighbors { get; }

    bool visited { get; }
    bool HasUnvisitedNeighbors { get; }

    void Visit();
    void Reset();
}
