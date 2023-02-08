using ScriptMeshTool.Editor.MeshCore;
using ScriptMeshTools.Editor.VertexCore;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptMeshTool.Editor
{
    public class MeshUv2Attribute : MeshAttribute
    {
        public MeshUv2Attribute(IncludedAttributes attribute) : base(attribute) { }

        public override VertexAttribute CreateVertexAttribute(Mesh mesh, int indexInMesh)
        {
            return new VertexUvAttribute(Attribute, mesh.uv2[indexInMesh]);
        }

        public override bool MeshDataIncluded(Mesh mesh) => MeshDataEnabled(mesh.uv2);

        public override void SetDataToMesh(Mesh mesh, List<Vertex> vertices)
        {
            if (!MeshDataEnabled(mesh.uv2))
                return;

            mesh.uv2 = GetData<Vector2>(vertices);
        }
    }
}
