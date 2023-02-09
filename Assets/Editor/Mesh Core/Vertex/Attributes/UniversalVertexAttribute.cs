using System.Collections.Generic;
using System.Linq;

namespace ScriptMeshTools.Editor.MeshCore
{
    public abstract class UniversalVertexAttribute<T> : VertexAttribute
    {
        public T Value;

        public UniversalVertexAttribute(VertexAttributes attribute, T value) : base(attribute)
        {
            Value = value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
