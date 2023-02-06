using UnityEngine;

namespace ScriptMeshTools.Editor.VertexCore
{
    public class PositionAttribute : UniversalVertexAttribute<Vector3>
    {
        public PositionAttribute(IncludedAttributes attribute, Vector3 value) : base(attribute, value) { }

        public override bool Equals(object obj, VertexCompareSettings settings)
        {
            var other = obj as PositionAttribute;

            return Vector3.Distance(Value, other.Value) < settings.PositionThreshold;
        }
    }
}
