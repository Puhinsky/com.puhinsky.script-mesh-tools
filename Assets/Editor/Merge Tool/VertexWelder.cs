using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MeshSplitTool.Editor
{
    public struct WeldSettings
    {
        public float PositionThreshold;
        public float NormalThreshold;
        public float UvThreshold;
    }

    public class VertexWelder
    {
        private Mesh _mesh;
        private WeldSettings _settings;
        private int[] _map;
        private List<Vertex> _newVertices;

        public VertexWelder(Mesh mesh, WeldSettings settings)
        {
            _mesh = mesh;
            _settings = settings;
        }

        private class Vertex
        {
            public Vector3 Position;
            public Vector3 Normal;
            public Vector2 Uv;

            public Vertex(Vector3 position)
            {
                Position = position;
            }
        }

        private class VertexEqualityComparer : IEqualityComparer<Vertex>
        {
            private WeldSettings _settings;

            public VertexEqualityComparer(WeldSettings settings)
            {
                _settings = settings;
            }

            public bool Equals(Vertex a, Vertex b)
            {
                /*return
                    Vector3.Distance(a.Position, b.Position) <= _settings.PositionThreshold &&
                    Vector3.Angle(a.Normal, b.Normal) <= _settings.NormalThreshold &&
                    Vector2.Distance(a.Uv, b.Uv) <= _settings.UvThreshold;*/
                return
                    Vector3.Distance(a.Position, b.Position) <= _settings.PositionThreshold;
            }

            public int GetHashCode(Vertex vertex)
            {
                //int hCode = vertex.Position.GetHashCode() ^ vertex.Normal.GetHashCode() ^ vertex.Uv.GetHashCode();
                int hCode = vertex.Position.GetHashCode();
                return hCode.GetHashCode();
            }
        }

        public void Weld()
        {
            CreateVertices();
            RemapTriangles();
            AssignVertex();
        }

        private void CreateVertices()
        {
            var positions = _mesh.vertices;
            var normals = _mesh.normals;
            var uv = _mesh.uv;

            var dublicates = new Dictionary<Vertex, int>(new VertexEqualityComparer(_settings));
            _newVertices = new List<Vertex>();
            _map = new int[positions.Length];

            for (int i = 0; i < positions.Length; i++)
            {
                var vertex = new Vertex(positions[i]) { Normal = normals[i], Uv = uv[i] };

                if (!dublicates.ContainsKey(vertex))
                {
                    dublicates.Add(vertex, _newVertices.Count);
                    _newVertices.Add(vertex);
                }

                _map[i] = dublicates[vertex];
            }
        }

        private void RemapTriangles()
        {
            for (int i = 0; i < _mesh.subMeshCount; i++)
            {
                var triangles = _mesh.GetTriangles(i);

                for (int j = 0; j < triangles.Length; j++)
                {
                    triangles[j] = _map[triangles[j]];
                }

                _mesh.SetTriangles(triangles, i);
            }
        }

        private void AssignVertex()
        {
            _mesh.vertices = _newVertices.Select(x => x.Position).ToArray();
            _mesh.normals = _newVertices.Select(x => x.Normal).ToArray();
            _mesh.uv = _newVertices.Select(x => x.Uv).ToArray();
        }
    }
}
