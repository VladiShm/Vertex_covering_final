using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Vertex_covering_final
{
    static class AutoTest
    {
        //генерация рандомной матрицы смежности с учетом ограничений
        public static List<List<int>> CreateTest(int n)
        {
            var random = new Random();
            //размер графа
            var graph = CreateEmptyMatrix(n);
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    //graph[i][j] = 0;
                    //graph[i][j] = i == j ? 0 : 1;
                    graph[i][j] = i == j ? 0 : random.Next(0, 2);
                    graph[j][i] = graph[i][j];
                }
            }
            return graph;
        }

        //создание нулевой матрицы
        static private List<List<int>> CreateEmptyMatrix(int n)
        {
            var graph = new List<List<int>>();
            for (int i = 0; i < n; i++)
            {
                graph.Add(new List<int>());
                for (int j = 0; j < n; j++)
                {
                    graph[i].Add(0);
                }
            }
            return graph;
        }

        //тест для приближенного алгоритма
        public static void ApproximateTest()
        {
            int mxValue = 32800;
            var random = new Random();
            Stopwatch sw = new Stopwatch();
            for (int i = 5; i < mxValue; i += 3000)
            {
                var graph = CreateTest(i);
                var la = new ApproximateAlg(graph);
                sw.Start();
                var b = la.Solve();
                sw.Stop();
                //Data.PrintGraph(graph);
                Data.WriteData(sw, graph.Count, "Приближенный алгоритм");
                sw.Restart();

            }
        }

        //тест для жадного алгоритма
        public static void GreedyTest()
        {
            int mxValue = 32800;
            var random = new Random();
            Stopwatch sw = new Stopwatch();
            for (int i = 24005; i < mxValue; i += 3000)
            {
                var graph = CreateTest(i);
                var la = new Greedyy(graph);
                sw.Start();
                var b = la.Solve();
                sw.Stop();
                Data.WriteData(sw, graph.Count, "Жадный алгоритм");
                Console.WriteLine(graph.Count);
                sw.Restart();

            }
        }

        //тест для точного алгоритма
        public static void ExplicitTest()
        {
            int mxValue = 27;
            var random = new Random();
            Stopwatch sw = new Stopwatch();
            for (int i = 26; i <= mxValue; i++)
            {
                var graph = CreateTest(i);
                var la = new Explicit(graph);
                sw.Start();
                var b = la.Solve();
                sw.Stop();
                Data.WriteData(sw, graph.Count, "Точный алгоритм");

                Console.WriteLine(graph.Count);
                sw.Restart();

            }
        }

        public static void Test()
        {

            var v = 25;
            var count = 5;
            for (int i = v; i <= v; i += 3)
            {
                TimeSpan approx_time = new TimeSpan();
                TimeSpan ex_time = new TimeSpan();
                TimeSpan gr_time = new TimeSpan();
                double count_solve_ex_approx = 0;
                double count_solve_ex_greed = 0;
                double sum_otkl_ex_approx = 0;
                double sum_otkl_ex_greedy = 0;

                for (int j = 0; j < count; j++)
                {
                    Stopwatch sw1 = new Stopwatch();
                    Stopwatch sw2 = new Stopwatch();
                    Stopwatch sw3 = new Stopwatch();
                    int f, f_, f__;
                    var graph = CreateTest(i);
                    var ex = new Explicit(graph);
                    sw1.Start();
                    var solve_ex = ex.Solve();
                    sw1.Stop();
                    ex_time += sw1.Elapsed;
                    f_ = solve_ex.Count();

                    var graph2 = new List<List<int>>();
                    foreach (var innerList in graph)
                    {
                        var newList = new List<int>(innerList);
                        graph2.Add(newList);
                    }

                    var graph3 = new List<List<int>>();
                    foreach (var innerList in graph)
                    {
                        var newList = new List<int>(innerList);
                        graph3.Add(newList);
                    }

                    var approx = new ApproximateAlg(graph2);
                    sw2.Start();
                    var solve_approx = approx.Solve();
                    sw2.Stop();
                    f = solve_approx.Count();
                    approx_time += sw2.Elapsed;


                    var greedy = new Greedyy(graph3);
                    sw3.Start();
                    var solve_greedy = greedy.Solve();
                    sw3.Stop();
                    f__ = solve_greedy.Count();
                    gr_time += sw3.Elapsed;

                    if (RightAnswers(solve_ex, solve_greedy))
                    {
                        count_solve_ex_greed++;
                    }

                    if (RightAnswers(solve_ex, solve_approx))
                    {
                        count_solve_ex_approx++;
                    }
                    Data.PrintVector(solve_approx, $"\n{solve_approx.Count()}");
                    Data.PrintVector(solve_ex, $"\n{solve_ex.Count()}");
                    sum_otkl_ex_approx += (double)Math.Abs(f - f_) / (double)f_;
                    sum_otkl_ex_greedy += (double)Math.Abs(f__ - f_) / (double)f_;
                    ; Data.WriteData(sw1, graph.Count, $"Точный алгоритм. Тест №{j + 1}");
                    Data.WriteVector(solve_ex);
                    Data.WriteData(sw2, graph2.Count, $"Приближенный алгоритм.Тест №{j + 1}");
                    Data.WriteVector(solve_approx);
                    Data.WriteData(sw3, graph.Count, $"Жадный алгоритм. Тест №{j + 1}");
                    Data.WriteVector(solve_greedy);
                    sw1.Restart();
                    sw2.Restart();
                    sw3.Restart();
                }
                double o = (double)sum_otkl_ex_approx / count;
                string res = o.ToString("F8");
                double o2 = (double)sum_otkl_ex_greedy / count;
                string res2 = o2.ToString("F8");
                Data.WriteComments($"В {(double)count_solve_ex_approx / count * 100} процентах приближ совпадает с точным\n");
                Data.WriteComments($"В {(double)count_solve_ex_greed / count * 100} процентах жадный совпадает с точным\n");
                Data.WriteComments($"Суммарное среднее относит. отклонение приближенного алгоритма  = {res}");
                Data.WriteComments($"Суммарное среднее относит. отклонение жадного алгоритма  = {res2}");

            }
        }

        //лучший случай генерации графа
        public static void BestTest()
        {
            Stopwatch sw1 = new Stopwatch();
            int n = 32000;
            for (int i = 4; i <= n; i += 3000)
            {
                var graph = CreateTest(i);
                var ex = new ApproximateAlg(graph);
                sw1.Start();
                var solve_ex = ex.Solve();
                sw1.Stop();
                Console.WriteLine(graph.Count);
                Data.WriteData(sw1, graph.Count, $"жадный алгоритм. Тест №{i + 1}");
                sw1.Restart();
            }

        }
        //проверка на совпадение решений
        public static bool RightAnswers(List<int> expl, List<int> approx)
        {
            return expl.All(x => approx.Contains(x));
        }
    }
}
