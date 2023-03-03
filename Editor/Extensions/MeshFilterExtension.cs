using System.Collections.Generic;
using UnityEngine;

namespace Puhinsky.ScriptMeshTools.Editor.Extensions
{
    public static class MeshFilterExtension
    {
        public static void SplitMesh(this MeshFilter meshFilter)
        {
            var meshRenderer = meshFilter.GetComponent<MeshRenderer>();
            var meshSeparator = new MeshSeparator(meshFilter.sharedMesh);

            var meshParts = meshSeparator.GetSplittedMeshes();

            foreach (var meshPart in meshParts)
            {
                var materials = new List<Material>();

                foreach (var materialIndex in meshSeparator.OriginSubMeshesMap[meshPart])
                {
                    materials.Add(meshRenderer.sharedMaterials[materialIndex]);
                }

                CreatePartObject(meshPart, meshFilter.transform, materials.ToArray());
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
