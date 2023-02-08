using ScriptMeshTool.Editor.MeshCore;
using ScriptMeshTools.Editor.VertexCore;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptMeshTool.Editor
{
    public class MeshUv1Attribute : MeshAttribute
    {
        public MeshUv1Attribute(IncludedAttributes attribute) : base(attribute) { }

        public override VertexAttribute CreateVertexAttribute(Mesh mesh, int indexInMesh)
        {
            return new VertexUvAttribute(Attribute, mesh.uv[indexInMesh]);
        }

        public override bool MeshDataIncluded(Mesh mesh) => MeshDataEnabled(mesh.uv);

        public override void SetDataToMesh(Mesh mesh, List<Vertex> vertices)
        {
            if (!MeshDataEnabled(mesh.uv))
                return;

            mesh.uv = GetData<Vector2>(vertices);
        }
    }
}
