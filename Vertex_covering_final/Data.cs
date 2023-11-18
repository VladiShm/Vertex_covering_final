using System.Diagnostics;
using System.Numerics;
using static System.Net.Mime.MediaTypeNames;

namespace Vertex_covering_final
{
    static class Data
    {
        static string path = "C:\\Users\\vladi\\source\\repos\\Vertex _Covering\\Vertex _Covering\\bin\\Debug\\net7.0\\input.txt";
        static string outPath = "C:\\Users\\vladi\\source\\repos\\Vertex _Covering\\Vertex _Covering\\bin\\Debug\\net7.0\\output15.txt";

        //Ввод матрицы из текстового файла
        public static List<List<int>> ReadGraph()
        {
            var graph = new List<List<int>>();
            using (StreamReader sr = new StreamReader(path))
            {
                string str;
                while ((str = sr.ReadLine()) != null)
                {
                    var r = new List<int>();
                    int[] row = Array.ConvertAll(str.Split(' '), int.Parse);
                    foreach (int i in row)
                    {
                        r.Add(i);

                    }
                    graph.Add(r);
                }
            }
            return graph;

        }

        //Вывод графа на консоль
        public static void PrintGraph(List<List<int>> graph)
        {
            Console.WriteLine($"Размер графа = {graph.Count}");
            for (int i = 0; i < graph.Count; i++)
            {
                for (int j = 0; j < graph.Count; j++)
                {
                    Console.Write($"{graph[i][j]} ");
                }
                Console.WriteLine();
            }
        }

        //запись вектора в файл
        public static void PrintVector(List<int> vector, string message)
        {
            Console.WriteLine(message);
            foreach (int i in vector)
            {
                Console.Write($"{i} ");
            }

        }

        //запись нек-х данных в файл(для экспериментов)
        public static void WriteData(Stopwatch sw, int n, string text)
        {
            using (StreamWriter s = new StreamWriter(outPath, true))
            {
                s.WriteLine(text);
                s.WriteLine($"Кол-во вершин = {n}");
                s.WriteLine(sw.Elapsed);
                s.WriteLine();
            }

        }

        //запись вектора в текстовый файл
        public static void WriteVector(List<int> vector)
        {
            using (StreamWriter s = new StreamWriter(outPath, true))
            {
                foreach (var el in vector)
                    s.Write(el + " ");

            }
        }

        //запись текстовых комментариев в текстовый файл
        public static void WriteComments(string text)
        {
            using (StreamWriter s = new StreamWriter(outPath, true))
            {
                s.WriteLine(text);

            }
        }
    }
}
