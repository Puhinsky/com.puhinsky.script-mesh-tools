using Puhinsky.ScriptMeshTools.Core;
using System.Collections.Generic;
using UnityEngine;

namespace Puhinsky.ScriptMeshTools.Editor.UnionFind
{
    public class CombinerByPosition : IUnionCombiner
    {
        private Mesh _mesh;


        public CombinerByPosition(Mesh mesh)
        {
            _mesh = mesh;
        }

        public void Union(QuickFind quickFind)
        {
            var includedAttributes = VertexAttributes.Positions;
            var vertexComparer = new VertexEqualityComparer(new VertexCompareSettings() { PositionThreshold = 0.01f });
            var dublicates = new Dictionary<Vertex, int>(vertexComparer);
            var map = new int[_mesh.vertexCount];

            for (int i = 0; i < _mesh.vertexCount; i++)
            {
                var vertex = new Vertex(_mesh, includedAttributes, i);

                if (!dublicates.ContainsKey(vertex))
                {
                    dublicates.Add(vertex, i);
                }

                map[i] = dublicates[vertex];
            }

            for (int i = 0; i < map.Length; i++)
            {
                quickFind.Union(i, map[i]);
            }
        }
    }
}
