using System.Collections.Generic;
using UnityEngine;

namespace Puhinsky.ScriptMeshTools.Editor.Extensions
{
    public static class MeshExtensions
    {
        public static int[][] GroupIndicesByTriangles(this Mesh mesh)
        {
            var faces = new List<int[]>();

            for (int i = 0; i < mesh.triangles.Length; i += 3)
            {
                faces.Add(mesh.triangles[i..(i + 3)]);
            }

            return faces.ToArray();
        }

        public static int[][] GroupIndicesByTriangles(this Mesh mesh, int subMeshIndex)
        {
            var faces = new List<int[]>();

            for (int i = 0; i < mesh.GetTriangles(subMeshIndex).Length; i += 3)
            {
                faces.Add(mesh.GetTriangles(subMeshIndex)[i..(i + 3)]);
            }

            return faces.ToArray();
        }
    }
}