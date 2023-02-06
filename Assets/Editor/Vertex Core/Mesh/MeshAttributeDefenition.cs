using ScriptMeshTools.Editor.VertexCore;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ScriptMeshTool.Editor.VertexCore
{
    public class MeshAttributeDefenition
    {
        public static Dictionary<IncludedAttributes, MeshAttribute> AttributeDefenition = new Dictionary<IncludedAttributes, MeshAttribute>()
        {
            { IncludedAttributes.Position, new PositionsAttribute(IncludedAttributes.Position) }
        };
    }

    public abstract class MeshAttribute
    {
        protected IncludedAttributes Attribute;

        public MeshAttribute(IncludedAttributes attribute)
        {
            Attribute = attribute;
        }

        public abstract VertexAttribute CreateVertexAttribute(Mesh mesh, int indexInMesh);
        public abstract void SetDataToMesh(Mesh mesh, List<Vertex> vertices);

        public abstract IncludedAttributes CheckAttributes(Mesh mesh, IncludedAttributes attributes);

        protected T[] GetData<T>(List<Vertex> vertices)
        {
            return vertices.Select(x => x.Attributes.Single(y => y.Attribute == Attribute)).Cast<UniversalVertexAttribute<T>>().Select(z => z.Value).ToArray();
        }
    }

    public class PositionsAttribute : MeshAttribute
    {
        public PositionsAttribute(IncludedAttributes attribute) : base(attribute) { }

        public override IncludedAttributes CheckAttributes(Mesh mesh, IncludedAttributes attributes)
        {
            if (mesh.vertices == null || mesh.vertices.Count() == 0)
                attributes &= ~IncludedAttributes.Position;

            return attributes;
        }

        public override VertexAttribute CreateVertexAttribute(Mesh mesh, int indexInMesh)
        {
            return new PositionAttribute(Attribute, mesh.vertices[indexInMesh]);
        }

        public override void SetDataToMesh(Mesh mesh, List<Vertex> vertices)
        {
            mesh.vertices = GetData<Vector3>(vertices);
        }
    }
}
