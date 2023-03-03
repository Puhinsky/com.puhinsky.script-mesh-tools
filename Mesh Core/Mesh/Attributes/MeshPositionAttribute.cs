using System.Collections.Generic;
using UnityEngine;

namespace Puhinsky.ScriptMeshTools.Core
{
    public class MeshPositionAttribute : MeshAttribute
    {
        public MeshPositionAttribute(VertexAttributes attribute) : base(attribute) { }

        public override VertexAttribute CreateVertexAttribute(Mesh mesh, int indexInMesh)
        {
            return new VertexPositionAttribute(Attribute, mesh.vertices[indexInMesh]);
        }

        public override void SetDataToMesh(Mesh mesh, List<Vertex> vertices)
        {
            mesh.vertices = GetData<Vector3>(vertices);
        }

        public override bool MeshDataIncluded(Mesh mesh) => MeshDataEnabled(mesh.vertices);
    }
}
