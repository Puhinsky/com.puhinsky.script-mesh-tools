namespace Puhinsky.ScriptMeshTools.Editor.UnionFind
{
    public class WeightedQuickFind : QuickFind
    {
        private int[] _sz;

        public WeightedQuickFind(int n) : base(n)
        {
            _sz = new int[n];
        }

        public override int Find(int p)
        {
            while (p != Nodes[p])
            {
                Nodes[p] = Nodes[Nodes[p]];
                p = Nodes[p];
            }

            return p;
        }

        public override void Union(int p, int q)
        {
            int pRoot = Find(p);
            int qRoot = Find(q);

            if (pRoot == qRoot)
                return;

            if (_sz[pRoot] < _sz[qRoot])
            {
                Nodes[pRoot] = qRoot;
                _sz[qRoot] += _sz[pRoot];
            }
            else
            {
                Nodes[qRoot] = pRoot;
                _sz[pRoot] += _sz[qRoot];
            }
        }
    }
}
