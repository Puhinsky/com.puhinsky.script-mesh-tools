using ScriptMeshTool.Editor.MeshCore;
using ScriptMeshTools.Editor.MeshCore;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptMeshTool.Editor
{
    public class MeshUv1Attribute : MeshAttribute
    {
        public MeshUv1Attribute(VertexAttributes attribute) : base(attribute) { }

        public override VertexAttribute CreateVertexAttribute(Mesh mesh, int indexInMesh)
        {
            return new VertexUvAttribute(Attribute, mesh.uv[indexInMesh]);
        }

        public override bool MeshDataIncluded(Mesh mesh) => MeshDataEnabled(mesh.uv);

        public override void SetDataToMesh(Mesh mesh, List<Vertex> vertices)
        {
            mesh.uv = GetData<Vector2>(vertices);
        }
    }
}
