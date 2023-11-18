using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex_covering_final
{
    class Explicit
    {
        List<List<int>> graph;

        public Explicit(List<List<int>> graph)
        {
            this.graph = new List<List<int>>(graph);
        }

        private int Count_Edge(List<int> vertexs, bool[,] visited) // количество ребор у просматриваемых вершин без повторов
        {
            int count = 0;  // 1
            foreach (var vertex in vertexs) // n (т.к. макс длина vertex = n)
            {
                for (int j = 0; j < graph.Count; j++) // 2n + 2
                {
                    if (visited[vertex, j] == true) // 1
                    {
                        count++;    // 1
                        visited[vertex, j] = visited[j, vertex] = false; // 2
                    }
                }
            }
            return count; // 1
        } // O(n^2)

        private List<int> Get_Combinations(List<int> list, int count_edge, bool[,] visited) // создание всех 2^n вариантов
        {
            List<int> min_variant = new List<int>();
            int min_len = int.MaxValue;

            bool[,] visited2 = visited.Clone() as bool[,];
            List<List<int>> combos = new List<List<int>>();
            double count = Math.Pow(2, list.Count);
            for (int i = 1; i < count - 1; i++) // 2^n
            {
                List<int> combo = new List<int>(); // 1
                string str = Convert.ToString(i, 2).PadLeft(list.Count, '0'); // n + 2i + 6
                for (int j = 0; j < str.Length; j++) // 2n + 2
                    if (str[j] == '1') // 1
                        combo.Add(list[j]); // 1
                if (Check(combo, count_edge, visited)) // 0(n^2)
                {
                    combos.Add(combo); // 1
                    if (combo.Count < min_len) // 1
                    {
                        min_variant = (combo.ToArray().Clone() as int[]).ToList(); // 1
                        min_len = combo.Count;
                    }
                }
                visited = visited2.Clone() as bool[,]; // 1
            }
            return min_variant; // 1
        } // O(2^n)

        private bool Check(List<int> list, int count_edge, bool[,] visited) // необходимо, чтобы количество ребер было не меньше всех ребер в графе
        {
            return Count_Edge(list, visited) >= count_edge; // t(Count_Edge) + 1   ~ O(n^2)
        }


        private List<int> FindMin()
        {
            int n = graph.Count,
                count_edge = 0; // количество ребер

            List<int> for_combos = new List<int>(); // набор номеров всех вершин

            bool[,] visited = new bool[graph.Count, graph.Count]; // посещенные вершины
            bool[,] visited2 = new bool[graph.Count, graph.Count];

            // этот цикл не входит в сам алгоритм, а чисто заполняет необходимые штучки
            for (int i = 0; i < n; i++)
            {
                for_combos.Add(i);
                for (int j = 0; j < n; j++)
                {
                    if (graph[i][j] == 1)
                    {
                        visited[i, j] = visited[j, i] = true;
                        visited2[i, j] = visited2[j, i] = true;
                    }
                    if (graph[i][j] == 1 && j >= i + 1)
                        count_edge++;
                }
            }

            // алгоритм
            return Get_Combinations(for_combos, count_edge, visited); // O(2^n);
        }


        public List<int> Solve()
        {
            return FindMin();
        }
    }
}
