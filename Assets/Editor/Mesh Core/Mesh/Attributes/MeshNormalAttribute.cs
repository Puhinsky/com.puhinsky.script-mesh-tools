using ScriptMeshTools.Editor.VertexCore;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptMeshTool.Editor.MeshCore
{
    public class MeshNormalAttribute : MeshAttribute
    {
        public MeshNormalAttribute(IncludedAttributes attribute) : base(attribute) { }

        public override VertexAttribute CreateVertexAttribute(Mesh mesh, int indexInMesh)
        {
            return new VertexNormalAttribute(Attribute, mesh.normals[indexInMesh]);
        }

        public override void SetDataToMesh(Mesh mesh, List<Vertex> vertices)
        {
            mesh.normals = GetData<Vector3>(vertices);
        }

        public override bool MeshDataIncluded(Mesh mesh) => !CheckMeshDataDisabled(mesh.normals);
    }
}
