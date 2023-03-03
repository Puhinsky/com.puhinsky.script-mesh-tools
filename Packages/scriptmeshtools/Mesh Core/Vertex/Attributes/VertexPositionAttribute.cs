using UnityEngine;

namespace Puhinsky.ScriptMeshTools.Core
{
    public class VertexPositionAttribute : UniversalVertexAttribute<Vector3>
    {
        public VertexPositionAttribute(VertexAttributes attribute, Vector3 value) : base(attribute, value) { }

        public override bool Equals(VertexAttribute vertexAttribute, VertexCompareSettings settings)
        {
            var other = vertexAttribute as VertexPositionAttribute;

            return Vector3.Distance(Value, other.Value) < settings.PositionThreshold;
        }
    }
}
