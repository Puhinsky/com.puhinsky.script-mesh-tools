using ScriptMeshTools.Editor.VertexCore;
using System.Collections.Generic;

namespace ScriptMeshTool.Editor.MeshCore
{
    public class MeshAttributeDefenition
    {
        public static Dictionary<IncludedAttributes, MeshAttribute> Attributes = new Dictionary<IncludedAttributes, MeshAttribute>()
        {
            { IncludedAttributes.Positions, new MeshPositionAttribute(IncludedAttributes.Positions) },
            { IncludedAttributes.Normals, new MeshNormalAttribute(IncludedAttributes.Normals)},
            { IncludedAttributes.Uv1, new MeshUv1Attribute(IncludedAttributes.Uv1)},
            { IncludedAttributes.Uv2, new MeshUv2Attribute(IncludedAttributes.Uv2)},
            { IncludedAttributes.Uv3, new MeshUv3Attribute(IncludedAttributes.Uv3)},
            { IncludedAttributes.Uv4, new MeshUv4Attribute(IncludedAttributes.Uv4)},
        };
    }
}
