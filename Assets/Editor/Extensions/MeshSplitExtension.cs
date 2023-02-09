using ScriptMeshTools.Editor.UnionFind;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace ScriptMeshTools.Editor.Extensions
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
                //meshPart.triangles = GetMeshPartTriangles(mesh, nodes);
                SetMeshPartTriangles(meshPart, mesh, nodes);
                meshPart.uv = uv0.ToArray();
                meshPart.uv2 = uv1.ToArray();
                meshPart.normals = normals.ToArray();
                meshPart.tangents = tangents.ToArray();

                meshPart.RecalculateBounds();
                meshPart.Optimize();

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

            foreach (var triangle in groupedTriangles)
            {
                foreach (var index in triangle)
                {
                    triangles.Add(nodesList.IndexOf(index));
                }
            }

            return triangles.ToArray();
        }

        private static void SetMeshPartTriangles(Mesh meshPart, Mesh originMesh, int[] verticesInMeshPart)
        {
            var originSubMeshes = GetSubMeshesInMeshPart(originMesh, verticesInMeshPart);
            meshPart.subMeshCount = originSubMeshes.Item1.Length;
            var nodesList = verticesInMeshPart.ToList();

            for (int i = 0; i < meshPart.subMeshCount; i++)
            {
                var groupedTriangles = new List<int[]>();
                var triangles = new List<int>();

                foreach (var triangle in originMesh.GroupIndicesByTriangles(originSubMeshes.Item1[i]))
                {
                    if (triangle.All(x => verticesInMeshPart.Contains(x)))
                    {
                        groupedTriangles.Add(triangle);
                    }
                }

                foreach (var triangle in groupedTriangles)
                {
                    foreach (var index in triangle)
                    {
                        triangles.Add(nodesList.IndexOf(index));
                    }
                }

                meshPart.SetTriangles(triangles.ToArray(), i);
            }
        }

        private static (int[], SubMeshDescriptor[]) GetSubMeshesInMeshPart(Mesh originMesh, int[] verticesInMeshPart)
        {
            var subMeshes = new List<SubMeshDescriptor>();
            var subMeshesIndices = new List<int>();

            for (int i = 0; i < originMesh.subMeshCount; i++)
            {
                var subMesh = originMesh.GetSubMesh(i);

                if (verticesInMeshPart.Any(x => x >= subMesh.firstVertex && x <= subMesh.vertexCount + subMesh.indexCount))
                {
                    subMeshes.Add(subMesh);
                    subMeshesIndices.Add(i);
                }
            }

            return (subMeshesIndices.ToArray(), subMeshes.ToArray());
        }
    }
}