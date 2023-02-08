using System;

namespace ScriptMeshTools.Editor.VertexCore
{
    [Flags]
    public enum IncludedAttributes
    {
        Positions = 1,
        Normals = 2,
        Uv1 = 4,
        Uv2 = 8,
        Uv3 = 16,
        Uv4 = 32
    }
}
