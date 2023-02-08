using ScriptMeshTool.Editor.MeshCore;
using ScriptMeshTools.Editor.VertexCore;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptMeshTools.Editor.WeldTool
{
    public class VertexWelder
    {
        private Mesh _mesh;
        private VertexCompareSettings _settings;
        private IncludedAttributes _includedAttributes;
        private int[] _map;
        private List<Vertex> _newVertices;

        public VertexWelder(Mesh mesh, VertexCompareSettings settings)
        {
            _mesh = mesh;
            _settings = settings;
        }

        public void Weld()
        {
            CheckAttributes();
            CreateVertices();
            RemapTriangles();
            AssignVertices();
        }

        private void CreateVertices()
        {
            var vertexCount = _mesh.vertexCount;
            var dublicates = new Dictionary<Vertex, int>(new VertexEqualityComparer(_settings));
            _newVertices = new List<Vertex>();
            _map = new int[vertexCount];

            for (int i = 0; i < vertexCount; i++)
            {
                var vertex = new Vertex(_mesh, _includedAttributes, i);

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

        private void AssignVertices()
        {
            foreach (IncludedAttributes attribute in Enum.GetValues(typeof(IncludedAttributes)))
            {
                if (MeshAttributeDefenition.Attributes.TryGetValue(attribute, out MeshAttribute meshAttribute))
                {
                    meshAttribute.SetDataToMesh(_mesh, _newVertices);
                }
            }
        }

        private void CheckAttributes()
        {
            foreach (IncludedAttributes attribute in Enum.GetValues(typeof(IncludedAttributes)))
            {
                if (MeshAttributeDefenition.Attributes.TryGetValue(attribute, out MeshAttribute meshAttribute))
                {
                    _includedAttributes = meshAttribute.CheckAttributes(_mesh, _includedAttributes);
                }
            }
        }
    }
}