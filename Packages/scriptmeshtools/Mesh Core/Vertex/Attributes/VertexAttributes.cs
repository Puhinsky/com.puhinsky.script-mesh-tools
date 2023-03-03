using System;

namespace Puhinsky.ScriptMeshTools.Core
{
    [Flags]
    public enum VertexAttributes
    {
        Positions = 1,
        Normals = 2,
        Uv1 = 4,
        Uv2 = 8,
        Uv3 = 16,
        Uv4 = 32,
        Colors = 64
    }
}
