using ScriptMeshTools.Editor.VertexCore;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptMeshTool.Editor.MeshCore
{
    public class MeshPositionAttribute : MeshAttribute
    {
        public MeshPositionAttribute(IncludedAttributes attribute) : base(attribute) { }

        public override VertexAttribute CreateVertexAttribute(Mesh mesh, int indexInMesh)
        {
            return new VertexPositionAttribute(Attribute, mesh.vertices[indexInMesh]);
        }

        public override void SetDataToMesh(Mesh mesh, List<Vertex> vertices)
        {
            if (!MeshDataEnabled(mesh.vertices))
                return;

            mesh.vertices = GetData<Vector3>(vertices);
        }

        public override bool MeshDataIncluded(Mesh mesh) => MeshDataEnabled(mesh.vertices);
    }
}
