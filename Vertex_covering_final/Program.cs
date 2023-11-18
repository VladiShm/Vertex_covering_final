using Vertex_covering_final;
class Program
{
    static void Main(string[] args)
    {

        var graph = AutoTest.CreateTest(4);
        //Data.PrintGraph(graph);
        //var ex = new Explicit(graph);
        //var s = ex.Solve();
        //Data.PrintVector(s, "точный");

        //var gr = new Greedyy(graph);
        //var s2 = gr.Solve();
        //Data.PrintVector(s2, "Жадный");
        //AutoTest.GreedyTest();

        //AutoTest.GreedyTest();
        AutoTest.Test();
        //AutoTest.BestTest();
        ////var ap = new ApproximateAlg(graph);
        ////var solve = ap.Solve();
        ////Data.PrintVector(solve, "Жадный");

    }
}