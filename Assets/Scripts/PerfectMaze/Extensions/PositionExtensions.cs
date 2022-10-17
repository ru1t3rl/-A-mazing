namespace Ru1t3rl.PerfectMaze.Extensions
{
    public static class PositionExtensions
    {
        public static Position Opposite(this Position position)
        {
            return position switch
            {
                Position.North => Position.South,
                Position.East => Position.West,
                Position.South => Position.North,
                Position.West => Position.East,
                _ => throw new System.Exception("Invalid position")
            };
        }
    }
}