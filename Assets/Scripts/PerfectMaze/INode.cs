using System.Collections.Generic;

public interface INode
{
    Dictionary<Position, INode> neighbors { get; }

    bool visited { get; }

    void Visit();
}
