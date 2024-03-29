using Puhinsky.ScriptMeshTools.Editor.Editor;
using Puhinsky.ScriptMeshTools.Editor.Extensions;
using UnityEditor;
using UnityEngine;

namespace Puhinsky.ScriptMeshTools.Editor
{
    public class SplitMeshTool
    {
        [MenuItem("CONTEXT/MeshFilter/Split")]
        public static void Split(MenuCommand menuCommand)
        {
            var meshFilter = menuCommand.context as MeshFilter;
            meshFilter.SplitMesh();
        }

        [MenuItem("CONTEXT/MeshFilter/Merge")]
        public static void Merge(MenuCommand menuCommand)
        {
            var meshFilter = menuCommand.context as MeshFilter;

            var weldedMesh = Object.Instantiate(meshFilter.sharedMesh);
            weldedMesh.Weld();
            meshFilter.sharedMesh = weldedMesh;
        }
    }
}