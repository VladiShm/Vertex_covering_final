using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Vertex_covering_final
{
    internal class Greedyy
    {
        List<List<int>> graph;
        //массив степеней вершин
        int[] d;
        int m;
        List<int> V;
        public Greedyy(List<List<int>> graph)
        {
            this.graph = graph;

        }

        public List<int> Solve()
        {
            d = new int[graph.Count];
            V = new List<int>();
            for (int i = 0; i < graph.Count; i++)
            {
                d[i] = 0;
                for (int j = 0; j < graph.Count; j++)
                {
                    if (graph[i][j] == 1)
                        d[i]++;
                }
                m += d[i];
            }
            m = m / 2;
            V.Clear();
            while (m > 0)
            {
                int idx = Array.IndexOf(d, d.Max());
                m -= d[idx];
                d[idx] = 0;
                for (int j = 0; j < graph.Count; j++)
                {
                    if (graph[idx][j] == 1)
                    {
                        d[j]--;
                        graph[j][idx] = 0;
                    }

                }
                V.Add(idx);
            }
            return V;
        }
    }
}
