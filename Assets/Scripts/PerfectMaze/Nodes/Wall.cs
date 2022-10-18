using UnityEngine;

namespace Ru1t3rl.PerfectMaze.Nodes
{
    public class Wall : MonoBehaviour
    {
        public Position position { get; protected set; }
        protected Vector3 size;

        public bool visible { get; protected set; }

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

            transform.localScale = new Vector3(size.x, size.y, size.z);

            transform.Rotate(position switch
            {
                Position.North or Position.South => Vector3.zero,
                Position.East or Position.West => new Vector3(0, -90, 0),
                _ => Vector3.zero
            });

            visible = true;
        }

        public virtual void Hide()
        {
            visible = false;
            gameObject.SetActive(false);
        }

        public virtual void Show()
        {
            visible = true;
            gameObject.SetActive(true);
        }

        private void OnDrawGizmos()
        {
            if (!visible)
                return;

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