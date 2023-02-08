namespace ScriptMeshTools.Editor.VertexCore
{
    public abstract class VertexAttribute
    {
        public IncludedAttributes Attribute;

        public VertexAttribute(IncludedAttributes attribute)
        {
            Attribute = attribute;
        }

        public abstract bool Equals(VertexAttribute vertexAttribute, VertexCompareSettings settings);
        public abstract override int GetHashCode();
    }
}