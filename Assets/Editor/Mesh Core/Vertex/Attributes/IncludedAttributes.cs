using System;

namespace ScriptMeshTools.Editor.VertexCore
{
    [Flags]
    public enum IncludedAttributes
    {
        Positions = 1,
        Normals = 2,
        Uv = 4
    }
}
