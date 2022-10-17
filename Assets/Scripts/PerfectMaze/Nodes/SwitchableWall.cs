using UnityEngine;

namespace Ru1t3rl.PerfectMaze.Nodes
{
    [RequireComponent(typeof(MeshFilter))]
    public class SwitchableWall : Wall
    {
        private MeshFilter meshFilter;

        private Mesh closedMesh;
        [SerializeField] private Mesh openMesh;

        public override void Initialize(Position position, Vector3 size)
        {
            this.position = position;
            this.size = size;

            transform.position = position switch
            {
                Position.North => new Vector3(0, 0, size.z/2),
                Position.South => new Vector3(0, 0, -size.z/2),
                Position.East => new Vector3(size.x/2, 0, 0),
                Position.West => new Vector3(-size.x/2, 0, 0),
                _ => Vector3.zero
            };

            transform.localScale = new Vector3(size.x, size.y, size.z);

            transform.Rotate(position switch
            {
                Position.North or Position.South => Vector3.zero,
                Position.East or Position.West => new Vector3(0, -90, 0),
                _ => Vector3.zero
            });
        }

        private void Awake()
        {
            meshFilter ??= GetComponent<MeshFilter>();
            closedMesh = meshFilter.mesh;
        }

        public override void Hide()
        {
            meshFilter.mesh = openMesh;
        }

        public override void Show()
        {
            meshFilter.mesh = closedMesh;
        }
    }
}