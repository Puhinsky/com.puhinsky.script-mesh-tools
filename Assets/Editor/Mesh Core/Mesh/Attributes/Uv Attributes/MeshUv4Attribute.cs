using ScriptMeshTool.Editor.MeshCore;
using ScriptMeshTools.Editor.VertexCore;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptMeshTool.Editor
{
    public class MeshUv4Attribute : MeshAttribute
    {
        public MeshUv4Attribute(IncludedAttributes attribute) : base(attribute) { }

        public override VertexAttribute CreateVertexAttribute(Mesh mesh, int indexInMesh)
        {
            return new VertexUvAttribute(Attribute, mesh.uv4[indexInMesh]);
        }

        public override bool MeshDataIncluded(Mesh mesh) => MeshDataEnabled(mesh.uv4);

        public override void SetDataToMesh(Mesh mesh, List<Vertex> vertices)
        {
            if (!MeshDataEnabled(mesh.uv4))
                return;

            mesh.uv4 = GetData<Vector2>(vertices);
        }
    }
}
