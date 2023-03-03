using System.Collections.Generic;

namespace Puhinsky.ScriptMeshTools.Editor.UnionFind
{
    public class QuickFind
    {
        protected int[] Nodes;

        public QuickFind(int n)
        {
            Nodes = new int[n];

            for (int i = 0; i < n; i++)
            {
                Nodes[i] = i;
            }
        }

        public virtual void Union(int p, int q)
        {
            int pRoot = Find(p);
            int qRoot = Find(q);
            Nodes[qRoot] = pRoot;
        }

        public virtual bool Connected(int p, int q)
        {
            return Nodes[p] == Nodes[q];
        }

        public virtual int Find(int p)
        {
            while (p != Nodes[p])
            {
                p = Nodes[p];
            }

            return p;
        }

        public int[][] GetAllocatedUnions()
        {
            var nodesMap = new Dictionary<int, List<int>>();

            for (int i = 0; i < Nodes.Length; i++)
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