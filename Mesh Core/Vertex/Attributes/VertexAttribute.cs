namespace Puhinsky.ScriptMeshTools.Core
{
    public abstract class VertexAttribute
    {
        public VertexAttributes Attribute;

        public VertexAttribute(VertexAttributes attribute)
        {
            Attribute = attribute;
        }

        public abstract bool Equals(VertexAttribute vertexAttribute, VertexCompareSettings settings);
        public abstract override int GetHashCode();
    }
}