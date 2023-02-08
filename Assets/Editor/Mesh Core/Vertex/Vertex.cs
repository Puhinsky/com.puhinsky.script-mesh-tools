using ScriptMeshTool.Editor.MeshCore;
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
                if (includedAttributes.HasFlag(attribute))
                {
                    Attributes.Add(MeshAttributeDefenition.Attributes[attribute].CreateVertexAttribute(mesh, indexInMesh));
                }
            }
        }
    }
}
