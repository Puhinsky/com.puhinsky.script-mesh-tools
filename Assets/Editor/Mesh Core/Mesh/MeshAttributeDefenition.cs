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

        };
    }
}
