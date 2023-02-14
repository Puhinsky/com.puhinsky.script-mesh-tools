using ScriptMeshTools.Editor.MeshCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptMeshTool.Editor
{
    public class VertexColorAttribute : UniversalVertexAttribute<Color>
    {
        public VertexColorAttribute(VertexAttributes attribute, Color value) : base(attribute, value)
        {
        }

        public override bool Equals(VertexAttribute vertexAttribute, VertexCompareSettings settings)
        {
            var other = vertexAttribute as VertexColorAttribute;

            return CompareComponents(Value.r, other.Value.r, settings) &&
                CompareComponents(Value.g, other.Value.g, settings) &&
                CompareComponents(Value.b, other.Value.b, settings) &&
                CompareComponents(Value.a, other.Value.a, settings);
        }

        private bool CompareComponents(float a, float b, VertexCompareSettings settings)
        {
            return Mathf.Abs(a - b) < settings.ColorThreshold;
        }
    }
}
