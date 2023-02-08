using UnityEngine;

namespace ScriptMeshTools.Editor.VertexCore
{
    public class VertexNormalAttribute : UniversalVertexAttribute<Vector3>
    {
        public VertexNormalAttribute(IncludedAttributes attribute, Vector3 value) : base(attribute, value) { }

        public override bool Equals(VertexAttribute vertexAttribute, VertexCompareSettings settings)
        {
            var other = vertexAttribute as VertexNormalAttribute;

            return Vector3.Angle(Value, other.Value) < settings.NormalThreshold;
        }
    }
}
