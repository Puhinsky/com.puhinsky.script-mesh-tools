using System.Collections.Generic;
using UnityEngine;

namespace Puhinsky.ScriptMeshTools.Core
{
    public class MeshUv3Attribute : MeshAttribute
    {
        public MeshUv3Attribute(VertexAttributes attribute) : base(attribute) { }

        public override VertexAttribute CreateVertexAttribute(Mesh mesh, int indexInMesh)
        {
            return new VertexUvAttribute(Attribute, mesh.uv3[indexInMesh]);
        }

        public override bool MeshDataIncluded(Mesh mesh) => MeshDataEnabled(mesh.uv3);

        public override void SetDataToMesh(Mesh mesh, List<Vertex> vertices)
        {
            mesh.uv3 = GetData<Vector2>(vertices);
        }
    }
}
