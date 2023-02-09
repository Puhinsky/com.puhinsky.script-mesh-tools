using ScriptMeshTool.Editor.MeshCore;
using ScriptMeshTools.Editor.MeshCore;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptMeshTools.Editor.WeldTool
{
    public class VertexWelder
    {
        private Mesh _mesh;
        private VertexCompareSettings _settings;
        private VertexAttributes _attributes;
        private VertexAttributes _excludedAttributes;
        private int[] _map;
        private List<Vertex> _newVertices;

        public VertexWelder(Mesh mesh, VertexCompareSettings settings)
        {
            _mesh = mesh;
            _settings = settings;
        }

        public VertexWelder(Mesh mesh, VertexCompareSettings settings, VertexAttributes excludedAttributes)
        {
            _mesh = mesh;
            _settings = settings;
            _excludedAttributes = excludedAttributes;
        }

        public void Weld()
        {
            CheckAttributes();
            ExcludeAttributes();
            CreateVertices();
            RemapTriangles();
            AssignVertices();
            Optimize();
        }

        private void CreateVertices()
        {
            var vertexCount = _mesh.vertexCount;
            var dublicates = new Dictionary<Vertex, int>(new VertexEqualityComparer(_settings));
            _newVertices = new List<Vertex>();
            _map = new int[vertexCount];

            for (int i = 0; i < vertexCount; i++)
            {
                var vertex = new Vertex(_mesh, _attributes, i);

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
            foreach (VertexAttributes attribute in Enum.GetValues(typeof(VertexAttributes)))
            {
                if(_attributes.HasFlag(attribute))
                {
                    if (MeshAttributeDefenition.Attributes.TryGetValue(attribute, out MeshAttribute meshAttribute))
                    {
                        meshAttribute.SetDataToMesh(_mesh, _newVertices);
                    }
                }
            }
        }

        private void CheckAttributes()
        {
            foreach (VertexAttributes attribute in Enum.GetValues(typeof(VertexAttributes)))
            {
                if (MeshAttributeDefenition.Attributes.TryGetValue(attribute, out MeshAttribute meshAttribute))
                {
                    _attributes = meshAttribute.CheckAttributes(_mesh, _attributes);
                }
            }
        }

        private void ExcludeAttributes()
        {
            _attributes &= ~_excludedAttributes; 
        }

        private void Optimize()
        {
            _mesh.Optimize();
        }
    }
}