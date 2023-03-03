using System.Collections.Generic;
using UnityEngine;

namespace Puhinsky.ScriptMeshTools.Core
{
    public class MeshUv4Attribute : MeshAttribute
    {
        public MeshUv4Attribute(VertexAttributes attribute) : base(attribute) { }

        public override VertexAttribute CreateVertexAttribute(Mesh mesh, int indexInMesh)
        {
            return new VertexUvAttribute(Attribute, mesh.uv4[indexInMesh]);
        }

        public override bool MeshDataIncluded(Mesh mesh) => MeshDataEnabled(mesh.uv4);

        public override void SetDataToMesh(Mesh mesh, List<Vertex> vertices)
        {
            mesh.uv4 = GetData<Vector2>(vertices);
        }
    }
}
