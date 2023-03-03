using System.Collections.Generic;

namespace Puhinsky.ScriptMeshTools.Core
{
    public class VertexEqualityComparer : IEqualityComparer<Vertex>
    {
        private VertexCompareSettings _settings;

        public VertexEqualityComparer(VertexCompareSettings settings)
        {
            _settings = settings;
        }

        public bool Equals(Vertex a, Vertex b)
        {
            if (a.Attributes.Count != b.Attributes.Count)
                return false;

            for (int i = 0; i < a.Attributes.Count; i++)
            {
                if (!a.Attributes[i].Equals(b.Attributes[i], _settings))
                    return false;
            }

            return true;
        }

        public int GetHashCode(Vertex vertex)
        {
            int hCode = 0;

            if (vertex.Attributes.Count > 0)
            {
                hCode = vertex.Attributes[0].GetHashCode();

                for (int i = 1; i < vertex.Attributes.Count; i++)
                {
                    hCode ^= vertex.Attributes[i].GetHashCode();
                }
            }

            return hCode.GetHashCode();
        }
    }
}
