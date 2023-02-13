using ScriptMeshTools.Editor.Extensions;
using ScriptMeshTools.Editor.MeshCore;
using ScriptMeshTools.Editor.UnionFind;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

namespace ScriptMeshTool.Editor
{
    public class MeshSeparator
    {
        public Dictionary<Mesh, int[]> OriginSubMeshesMap = new Dictionary<Mesh, int[]>();

        private Mesh _mesh;
        private VertexAttributes _attributes;

        public MeshSeparator(Mesh mesh)
        {
            _mesh = mesh;
            _attributes = Vertex.GetAttributesForMesh(_mesh);
        }

        public Mesh[] GetSplittedMeshes()
        {
            var meshParts = new List<Mesh>();

            foreach (var nodes in CreateUnionsByMesh(_mesh))
            {
                var meshPart = new Mesh();
                var vertices = new List<Vertex>();

                for (int i = 0; i < nodes.Length; i++)
                {
                    var vertex = new Vertex(_mesh, _attributes, nodes[i]);
                    vertices.Add(vertex);
                }

                Vertex.AssignVerticesToMesh(meshPart, _attributes, vertices);
                SetMeshPartTriangles(meshPart, _mesh, nodes);

                meshPart.RecalculateBounds();
                meshPart.Optimize();

                meshParts.Add(meshPart);
            }

            return meshParts.ToArray();
        }

        public int[][] CreateUnionsByMesh(Mesh mesh)
        {
            var unionFind = new QuickFind(mesh.vertexCount);
            var combiners = new List<IUnionCombiner>()
            {
                new CombinerByFace(mesh),
                new CombinerByPosition(mesh)
            };

            foreach (var combiner in combiners)
            {
                combiner.Union(unionFind);
            }

            return unionFind.GetAllocatedUnions();
        }

        private void SetMeshPartTriangles(Mesh meshPart, Mesh originMesh, int[] verticesInMeshPart)
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

            OriginSubMeshesMap.Add(meshPart, originSubMeshes.Item1);
        }

        private (int[], SubMeshDescriptor[]) GetSubMeshesInMeshPart(Mesh originMesh, int[] verticesInMeshPart)
        {
            var subMeshes = new List<SubMeshDescriptor>();
            var subMeshesIndices = new List<int>();

            for (int i = 0; i < originMesh.subMeshCount; i++)
            {
                var subMesh = originMesh.GetSubMesh(i);

                if (verticesInMeshPart.Any(x => x >= subMesh.firstVertex && x < subMesh.firstVertex + subMesh.vertexCount))
                {
                    subMeshes.Add(subMesh);
                    subMeshesIndices.Add(i);
                }
            }

            return (subMeshesIndices.ToArray(), subMeshes.ToArray());
        }
    }
}
