using ScriptMeshTools.Editor.VertexCore;
using ScriptMeshTools.Editor.WeldTool;
using UnityEngine;

namespace ScriptMeshTools.Editor
{
    public static class MeshVerticesWeldExtension
    {
        public static void Weld(this Mesh mesh, float weldDelta)
        {
            var welder = new VertexWelder(mesh, new VertexCompareSettings() 
            {
                PositionThreshold = 0.01f, 
                NormalThreshold = 1f, 
                UvThreshold = 0.001f 
            });
            welder.Weld();
        }
    }
}
