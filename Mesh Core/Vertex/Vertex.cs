using System;
using System.Collections.Generic;
using UnityEngine;

namespace Puhinsky.ScriptMeshTools.Core
{
    public class Vertex
    {
        public List<VertexAttribute> Attributes = new List<VertexAttribute>();

        public Vertex(Mesh mesh, VertexAttributes includedAttributes, int indexInMesh)
        {
            foreach (VertexAttributes attribute in Enum.GetValues(typeof(VertexAttributes)))
            {
                if (includedAttributes.HasFlag(attribute))
                {
                    Attributes.Add(MeshAttributeDefenition.Attributes[attribute].CreateVertexAttribute(mesh, indexInMesh));
                }
            }
        }

        public static VertexAttributes GetAttributesForMesh(Mesh mesh)
        {
            var attributes = new VertexAttributes();

            foreach (VertexAttributes attribute in Enum.GetValues(typeof(VertexAttributes)))
            {
                if (MeshAttributeDefenition.Attributes.TryGetValue(attribute, out MeshAttribute meshAttribute))
                {
                    attributes = meshAttribute.CheckAttributes(mesh, attributes);
                }
            }

            return attributes;
        }

        public static void AssignVerticesToMesh(Mesh mesh, VertexAttributes includedAttributes, List<Vertex> vertices)
        {
            foreach (VertexAttributes attribute in Enum.GetValues(typeof(VertexAttributes)))
            {
                if (includedAttributes.HasFlag(attribute))
                {
                    if (MeshAttributeDefenition.Attributes.TryGetValue(attribute, out MeshAttribute meshAttribute))
                    {
                        meshAttribute.SetDataToMesh(mesh, vertices);
                    }
                }
            }
        }
    }
}
