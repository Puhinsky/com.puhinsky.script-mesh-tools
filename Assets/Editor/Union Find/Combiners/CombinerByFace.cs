using ScriptMeshTools.Editor.Extensions;
using ScriptMeshTools.Editor.UnionFind;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptMeshTool.Editor
{
    public class CombinerByFace : IUnionCombiner
    {
        private Mesh _mesh;

        public CombinerByFace(Mesh mesh)
        {
            _mesh = mesh;
        }

        public void Union(QuickFind unionFind)
        {
            foreach (var indices in _mesh.GroupIndicesByTriangles())
            {
                unionFind.Union(indices[0], indices[1]);
                unionFind.Union(indices[0], indices[2]);
            }
        }
    }
}
