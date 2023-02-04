using MeshSplitTool.Editor.UnionFind;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MeshSplitTool.Editor.Extensions
{
    public static class MeshSplitExtension
    {
        public static Mesh[] Split(this Mesh mesh)
        {
            var meshParts = new List<Mesh>();

            foreach (var nodes in CreateUnionsByMesh(mesh))
            {
                var meshPart = new Mesh();

                var vertices = new List<Vector3>();
                var uv0 = new List<Vector2>();
                var uv1 = new List<Vector2>();
                var normals = new List<Vector3>();
                var tangents = new List<Vector4>();

                for (int i = 0; i < nodes.Length; i++)
                {
                    vertices.Add(mesh.vertices[nodes[i]]);
                    uv0.Add(mesh.uv[nodes[i]]);
                    uv1.Add(mesh.uv[nodes[i]]);
                    normals.Add(mesh.normals[nodes[i]]);
                    tangents.Add(mesh.tangents[nodes[i]]);
                }

                meshPart.vertices = vertices.ToArray();
                meshPart.triangles = GetMeshPartTriangles(mesh, nodes);
                meshPart.uv = uv0.ToArray();
                meshPart.uv2 = uv1.ToArray();
                meshPart.normals = normals.ToArray();
                meshPart.tangents = tangents.ToArray();

                meshPart.RecalculateBounds();
                meshPart.OptimizeReorderVertexBuffer();

                meshParts.Add(meshPart);
            }

            return meshParts.ToArray();
        }

        public static int[][] CreateUnionsByMesh(Mesh mesh)
        {
            var unionFind = new QuickFind(mesh.vertexCount);

            foreach (var indices in mesh.GroupIndicesByTriangles())
            {
                unionFind.Union(indices[0], indices[1]);
                unionFind.Union(indices[0], indices[2]);
            }

            return unionFind.GetAllocatedUnions();
        }

        private static int[] GetMeshPartTriangles(Mesh originMesh, int[] verticesInMeshPart)
        {
            var groupedTriangles = new List<int[]>();
            var triangles = new List<int>();
            var nodesList = verticesInMeshPart.ToList();

            foreach (var triangle in originMesh.GroupIndicesByTriangles())
            {
                if (triangle.All(x => verticesInMeshPart.Contains(x)))
                {
                    groupedTriangles.Add(triangle);
                }
            }

            foreach (var face in groupedTriangles)
            {
                foreach (var index in face)
                {
                    triangles.Add(nodesList.IndexOf(index));
                }
            }

            return triangles.ToArray();
        }
    }
}