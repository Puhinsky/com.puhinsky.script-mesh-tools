using ScriptMeshTools.Editor.VertexCore;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ScriptMeshTool.Editor.MeshCore
{
    public abstract class MeshAttribute
    {
        protected IncludedAttributes Attribute;

        public MeshAttribute(IncludedAttributes attribute)
        {
            Attribute = attribute;
        }

        public abstract VertexAttribute CreateVertexAttribute(Mesh mesh, int indexInMesh);
        public abstract void SetDataToMesh(Mesh mesh, List<Vertex> vertices);
        public abstract bool MeshDataIncluded(Mesh mesh);

        public IncludedAttributes CheckAttributes(Mesh mesh, IncludedAttributes attributes)
        {
            if (MeshDataIncluded(mesh))
                attributes |= Attribute;
            else
                attributes &= ~Attribute;

            return attributes;
        }

        protected T[] GetData<T>(List<Vertex> vertices)
        {
            return vertices.Select(x => x.Attributes.Single(y => y.Attribute == Attribute)).Cast<UniversalVertexAttribute<T>>().Select(z => z.Value).ToArray();
        }

        protected bool MeshDataEnabled<T>(T[] data)
        {
            return data != null && data.Length != 0;
        }
    }
}
