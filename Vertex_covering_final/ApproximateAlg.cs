
namespace Vertex_covering_final
{
    class ApproximateAlg
    {
        List<List<int>> graph;
        int len;

        public ApproximateAlg(List<List<int>> graph)
        {
            this.graph = graph;
            len = graph.Count;
        }

        private int ChoseFirstEdge(int k)
        {
            for (int i = 1; i < len; i++)
            {
                if (graph[i][k] == 1)
                    return i;
            }

            return -1;
        }

        //Удаление инциндентных ребер
        private void DeleteIncident(int i, int j)
        {
            for (int k = 0; k < len; k++)
            {
                graph[i][k] = 0;
                graph[k][i] = 0;
                graph[j][k] = 0;
                graph[k][j] = 0;
            }
        }

        public List<int> Solve()
        {
            List<int> VertexCover = new List<int>();
            for (int i = 0; i < len; i++)
            {
                int k = ChoseFirstEdge(i);

                if (k == -1)
                    continue;

                VertexCover.Add(i);
                VertexCover.Add(k);

                DeleteIncident(i, k);
            }

            return VertexCover;
        }

    }
}
