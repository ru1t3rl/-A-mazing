using UnityEngine;

namespace Ru1t3rl.PerfectMaze.Nodes
{
    public class Wall : MonoBehaviour
    {
        public Position position { get; protected set; }
        protected Vector3 size;

        public virtual void Initialize(Position position, Vector3 size)
        {
            this.position = position;
            this.size = size;

            transform.position = position switch
            {
                Position.North => new Vector3(0, 0, size.z / 2),
                Position.South => new Vector3(0, 0, -size.z / 2),
                Position.East => new Vector3(size.x / 2, 0, 0),
                Position.West => new Vector3(-size.x / 2, 0, 0),
                _ => Vector3.zero
            };

            transform.localScale = position switch
            {
                Position.North => new Vector3(size.x, transform.localScale.y, transform.localScale.z),
                Position.South => new Vector3(size.x, transform.localScale.y, 0.1f),
                Position.East => new Vector3(transform.localScale.z, transform.localScale.y, size.z),
                Position.West => new Vector3(transform.localScale.z, transform.localScale.y, size.z),
                _ => Vector3.zero
            };
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(transform.position, position switch
            {
                Position.North => new Vector3(size.x, 0.1f, 0.1f),
                Position.South => new Vector3(size.x, 0.1f, 0.1f),
                Position.East => new Vector3(0.1f, 0.1f, size.z),
                Position.West => new Vector3(0.1f, 0.1f, size.z),
                _ => Vector3.zero
            });
        }
    }
}