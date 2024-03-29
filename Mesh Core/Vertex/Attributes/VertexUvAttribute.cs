using UnityEngine;

namespace Puhinsky.ScriptMeshTools.Core
{
    public class VertexUvAttribute : UniversalVertexAttribute<Vector2>
    {
        public VertexUvAttribute(VertexAttributes attribute, Vector2 value) : base(attribute, value) { }

        public override bool Equals(VertexAttribute vertexAttribute, VertexCompareSettings settings)
        {
            var other = vertexAttribute as VertexUvAttribute;

            return Vector2.Distance(Value, other.Value) < settings.UvThreshold;
        }
    }
}
