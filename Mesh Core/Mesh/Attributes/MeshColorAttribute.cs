using System.Collections.Generic;
using UnityEngine;

namespace Puhinsky.ScriptMeshTools.Core
{
    public class MeshColorAttribute : MeshAttribute
    {
        public MeshColorAttribute(VertexAttributes attribute) : base(attribute)
        {
        }

        public override VertexAttribute CreateVertexAttribute(Mesh mesh, int indexInMesh)
        {
            return new VertexColorAttribute(Attribute, mesh.colors[indexInMesh]);
        }

        public override bool MeshDataIncluded(Mesh mesh) => MeshDataEnabled(mesh.colors);

        public override void SetDataToMesh(Mesh mesh, List<Vertex> vertices)
        {
            mesh.colors = GetData<Color>(vertices);
        }
    }
}
