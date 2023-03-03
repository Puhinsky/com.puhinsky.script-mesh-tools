using System.Collections.Generic;
using UnityEngine;

namespace Puhinsky.ScriptMeshTools.Core
{
    public class MeshUv2Attribute : MeshAttribute
    {
        public MeshUv2Attribute(VertexAttributes attribute) : base(attribute) { }

        public override VertexAttribute CreateVertexAttribute(Mesh mesh, int indexInMesh)
        {
            return new VertexUvAttribute(Attribute, mesh.uv2[indexInMesh]);
        }

        public override bool MeshDataIncluded(Mesh mesh) => MeshDataEnabled(mesh.uv2);

        public override void SetDataToMesh(Mesh mesh, List<Vertex> vertices)
        {
            mesh.uv2 = GetData<Vector2>(vertices);
        }
    }
}
