using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public static class CollectionsExtensions
{
    public static void Remove(this List<Wall> nodes, Position position)
    {
        nodes.Remove(nodes.Find(node => node.position == position));
    }

    public static INode? GetRandomUnvisited(this Dictionary<Position, INode> nodes)
    {
        return nodes
            .Select(node => node.Value)
            .Where(node => !node.visited)
            .ToList()
            .GetRandom();
    }

    public static T? GetRandom<T>(this List<T> list)
    {
        return list.Count == 0 ? default(T) : list[Random.Range(0, list.Count)];
    }
}