using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class BorderedNode : Node
{
    private new Dictionary<Position, BorderedNode> neighbors = new Dictionary<Position, BorderedNode>();
    private List<Wall> borders = new List<Wall>();

    public void Initialize(GameObject wall = null)
    {
        if (borders.Count > 0)
        {
            for (int i = 0; i < borders.Count; i++)
            {
                Destroy(borders[i].gameObject);
            }
            borders.Clear();
        }

        foreach (Position position in System.Enum.GetValues(typeof(Position)))
        {
            borders.Add(CreateBorder(position, wall));
        }
    }

    /// <summary>Remove the border in the given position.</summary>
    /// <param name="position">The position of the border to remove.</param>
    public void RemoveWall(Position position)
    {
        neighbors[position].RemoveWall(position.Opposite());

        borders.Find(border => border.position == position).Hide();
        borders.Remove(position);
    }

    public void RemoveWall(INode neighbor)
    {
        Position position = neighbors.First(node => node.Value == (Object)neighbor).Key;
        RemoveWall(position);
    }

    private Wall CreateBorder(Position position, GameObject wallObject = null)
    {
        wallObject = Instantiate(wallObject, transform);
        Wall wall = wallObject.GetComponent<Wall>() ?? wallObject.AddComponent<Wall>();        
        wall.Initialize(position, transform.localScale);
        wall.transform.SetParent(transform);
        return wall;
    }
}