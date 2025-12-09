using System;
using System.Collections.Generic;
using System.Diagnostics;

Main();

void Main()
{
    Console.WriteLine("Start Dijkstra Test");

    TestLineGraph_0_to_4();
    TestLineGraph_2_to_3();
    TestLineGraph_2_to_2();
    TestWeightedGraph_0_to_3();
    TestWeightedGraph_1_to_3();

    Console.WriteLine("End Dijkstra Test");
}

void TestLineGraph_0_to_4()
{
    var g = new LineGraph(5); // nodes 0..4, cost 1 per step
    var path = Dijkstra.GetPath(g, 0, 4);
    var s = string.Join(",", path);
    Console.WriteLine("LineGraph 0->4: " + s);
    Debug.Assert(s == "0,1,2,3,4");
}

void TestLineGraph_2_to_3()
{
    var g = new LineGraph(5); // nodes 0..4
    var path = Dijkstra.GetPath(g, 2, 3);
    var s = string.Join(",", path);
    Console.WriteLine("LineGraph 2->3: " + s);
    Debug.Assert(s == "2,3");
}

void TestLineGraph_2_to_2()
{
    var g = new LineGraph(5); // nodes 0..4
    var path = Dijkstra.GetPath(g, 2, 2);
    var s = string.Join(",", path);
    Console.WriteLine("LineGraph 2->2: " + s);
    Debug.Assert(s == "2");   // start == goal
}

void TestWeightedGraph_0_to_3()
{
    var g = new WeightedExampleGraph();
    var path = Dijkstra.GetPath(g, 0, 3);
    var s = string.Join(",", path);
    Console.WriteLine("WeightedGraph 0->3: " + s);
    Debug.Assert(s == "0,2,3");   // cheaper route via node 2
}

void TestWeightedGraph_1_to_3()
{
    var g = new WeightedExampleGraph();
    var path = Dijkstra.GetPath(g, 1, 3);
    var s = string.Join(",", path);
    Console.WriteLine("WeightedGraph 1->3: " + s);
    Debug.Assert(s == "1,3");     // only one outgoing edge from 1
}


// Simple line: 0-1-2-...-(n-1), cost 1 per edge.
class LineGraph : IWeightedGraph<int>
{
    private readonly int n;
    public LineGraph(int n) { this.n = n; }

    public IEnumerable<WeightedEdge<int>> Neighbors(int node)
    {
        if (node - 1 >= 0)
            yield return new WeightedEdge<int>(node - 1, 1f);
        if (node + 1 < n)
            yield return new WeightedEdge<int>(node + 1, 1f);
    }
}

// 0 --(5)--> 1 --(1)--> 3
// 0 --(2)--> 2 --(2)--> 3  ==> shortest 0-2-3 (cost 4)
class WeightedExampleGraph : IWeightedGraph<int>
{
    public IEnumerable<WeightedEdge<int>> Neighbors(int node)
    {
        switch (node)
        {
            case 0:
                yield return new WeightedEdge<int>(1, 5f);
                yield return new WeightedEdge<int>(2, 2f);
                break;
            case 1:
                yield return new WeightedEdge<int>(3, 1f);
                break;
            case 2:
                yield return new WeightedEdge<int>(3, 2f);
                break;
        }
    }
}
