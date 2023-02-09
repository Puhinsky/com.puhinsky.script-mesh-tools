using ScriptMeshTools.Editor.MeshCore;
using System.Collections.Generic;

namespace ScriptMeshTool.Editor.MeshCore
{
    public class MeshAttributeDefenition
    {
        public static Dictionary<VertexAttributes, MeshAttribute> Attributes = new Dictionary<VertexAttributes, MeshAttribute>()
        {
            { VertexAttributes.Positions, new MeshPositionAttribute(VertexAttributes.Positions) },
            { VertexAttributes.Normals, new MeshNormalAttribute(VertexAttributes.Normals)},
            { VertexAttributes.Uv1, new MeshUv1Attribute(VertexAttributes.Uv1)},
            { VertexAttributes.Uv2, new MeshUv2Attribute(VertexAttributes.Uv2)},
            { VertexAttributes.Uv3, new MeshUv3Attribute(VertexAttributes.Uv3)},
            { VertexAttributes.Uv4, new MeshUv4Attribute(VertexAttributes.Uv4)},
        };
    }
}
