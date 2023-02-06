using System.Collections.Generic;

namespace ScriptMeshTools.Editor.UnionFind
{

    public class QuickFind
    {
        private int[] _nodes;

        public QuickFind(int n)
        {
            _nodes = new int[n];

            for (int i = 0; i < n; i++)
            {
                _nodes[i] = i;
            }
        }

        public void Union(int p, int q)
        {
            int pRoot = Find(p);
            int qRoot = Find(q);
            _nodes[qRoot] = pRoot;
        }

        public bool Connected(int p, int q)
        {
            return _nodes[p] == _nodes[q];
        }

        public int Find(int p)
        {
            while (p != _nodes[p])
            {
                p = _nodes[p];
            }

            return p;
        }

        public int[][] GetAllocatedUnions()
        {
            var nodesMap = new Dictionary<int, List<int>>();

            for (int i = 0; i < _nodes.Length; i++)
            {
                var currentUnionIndex = Find(i);

                if (!nodesMap.ContainsKey(currentUnionIndex))
                {
                    nodesMap.Add(currentUnionIndex, new List<int>());
                }

                nodesMap[currentUnionIndex].Add(i);
            }

            var unionMapArray = new List<int[]>();

            foreach (var union in nodesMap.Values)
            {
                unionMapArray.Add(union.ToArray());
            }

            return unionMapArray.ToArray();
        }
    }
}