using Puhinsky.ScriptMeshTools.Core;
using Puhinsky.ScriptMeshTools.Editor.WeldTool;
using UnityEngine;

namespace Puhinsky.ScriptMeshTools.Editor.Editor
{
    public static class MeshVerticesWeldExtension
    {
        public static void Weld(this Mesh mesh)
        {
            var welder = new VertexWelder(mesh, new VertexCompareSettings() 
            {
                PositionThreshold = 0.01f, 
                NormalThreshold = 1f, 
                UvThreshold = 0.001f 
            }, VertexAttributes.Uv2);
            welder.Weld();
        }
    }
}
