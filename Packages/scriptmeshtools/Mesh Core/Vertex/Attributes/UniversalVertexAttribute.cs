namespace Puhinsky.ScriptMeshTools.Core
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
