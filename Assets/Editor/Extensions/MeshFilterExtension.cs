using UnityEngine;

namespace MeshSplitTool.Editor.Extensions
{
    public static class MeshFilterExtension
    {
        public static void SplitMesh(this MeshFilter meshFilter)
        {
            var meshRenderer = meshFilter.GetComponent<MeshRenderer>();
            var meshParts = meshFilter.sharedMesh.Split();

            foreach (var meshPart in meshParts)
            {
                CreatePartObject(meshPart, meshFilter.transform, meshRenderer.sharedMaterials);
            }

            Object.DestroyImmediate(meshFilter, true);
            Object.DestroyImmediate(meshRenderer, true);
        }

        private static void CreatePartObject(Mesh mesh, Transform parent, Material[] materials)
        {
            var gameObject = new GameObject(string.Format($"{parent.name} Part"));

            var meshFilter = gameObject.AddComponent<MeshFilter>();
            var meshRenderer = gameObject.AddComponent<MeshRenderer>();

            gameObject.transform.SetParent(parent);
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localRotation = Quaternion.identity;

            meshFilter.sharedMesh = mesh;
            meshRenderer.sharedMaterials = materials;
        }
    }
}
