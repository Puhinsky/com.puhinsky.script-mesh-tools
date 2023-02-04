using MeshSplitTool.Editor.Extensions;
using UnityEditor;
using UnityEngine;

namespace MeshSplitTool.Editor
{
    public class SplitMeshTool
    {
        [MenuItem("CONTEXT/MeshFilter/Split")]
        public static void Split(MenuCommand menuCommand)
        {
            var mesh = (menuCommand.context as MeshFilter).sharedMesh;

            var meshParts = mesh.Split();

            foreach (var meshPart in meshParts)
            {
                var gameObject = new GameObject();
                var meshFilter = gameObject.AddComponent<MeshFilter>();
                var meshRenderer = gameObject.AddComponent<MeshRenderer>();
                meshFilter.sharedMesh = meshPart;
                meshRenderer.sharedMaterial = (menuCommand.context as MeshFilter).GetComponent<MeshRenderer>().sharedMaterial;
            }
        }
    }
}