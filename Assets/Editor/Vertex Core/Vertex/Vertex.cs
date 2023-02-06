using ScriptMeshTool.Editor.VertexCore;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptMeshTools.Editor.VertexCore
{
    public class Vertex
    {
        public List<VertexAttribute> Attributes = new List<VertexAttribute>();

        public Vertex(Mesh mesh, IncludedAttributes includedAttributes, int indexInMesh)
        {
            foreach (IncludedAttributes attribute in Enum.GetValues(typeof(IncludedAttributes)))
            {
                if (attribute.Equals(includedAttributes))
                {
                    Attributes.Add(MeshAttributeDefenition.AttributeDefenition[attribute].CreateVertexAttribute(mesh, indexInMesh));
                }
            }
        }
    }
}
