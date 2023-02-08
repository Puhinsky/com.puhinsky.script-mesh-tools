using ScriptMeshTools.Editor.VertexCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptMeshTool.Editor
{
    public class VertexUvAttribute : UniversalVertexAttribute<Vector2>
    {
        public VertexUvAttribute(IncludedAttributes attribute, Vector2 value) : base(attribute, value) { }

        public override bool Equals(VertexAttribute vertexAttribute, VertexCompareSettings settings)
        {
            var other = vertexAttribute as VertexUvAttribute;

            return Vector2.Distance(Value, other.Value) < settings.UvThreshold;
        }
    }
}
