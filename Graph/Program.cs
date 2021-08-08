using System;
using System.Collections.Generic;
using System.Text;

namespace Graph
{
    class Test
    {
        static void Main()
        {
            {
                //UnweightedDirectedGraph udgraph = new UnweightedDirectedGraph(6);
                //udgraph.AddEdge(0, 1);
                //udgraph.AddEdge(0, 2);
                //udgraph.AddEdge(0, 3);
                //udgraph.AddEdge(1, 3);
                //udgraph.AddEdge(2, 3);
                //udgraph.AddEdge(1, 4);
                //udgraph.AddEdge(2, 4);
                //udgraph.AddEdge(5, 2);
                //udgraph.AddEdge(5, 0);
                //udgraph.AddEdge(4, 0);
                //udgraph.AddEdge(4, 1);
                //udgraph.AddEdge(2, 3);
                //udgraph.AddEdge(3, 1);
            }// test for unweighted directed graph

            {
                //UnweightedUndirectedGraph uugraph = new UnweightedUndirectedGraph(9);
                //uugraph.AddEdge(0, 1);
                //uugraph.AddEdge(0, 2);
                //uugraph.AddEdge(1, 2);
                //uugraph.AddEdge(1, 5);
                //uugraph.AddEdge(2, 3);
                //uugraph.AddEdge(2, 4);
                //uugraph.AddEdge(2, 5);
                //uugraph.AddEdge(2, 6);
                //uugraph.AddEdge(3, 4);
                //uugraph.AddEdge(3, 6);
                //uugraph.AddEdge(3, 7);
                //uugraph.AddEdge(4, 6);
                //uugraph.AddEdge(4, 7);
                //uugraph.AddEdge(5, 6);
                //uugraph.AddEdge(5, 8);
                //uugraph.AddEdge(6, 7);
                //uugraph.AddEdge(6, 8);
            }// test for unweighted undirected graph


            int INF = 9999;
            int[,] graph =
           { {INF, INF, INF, INF, INF, INF, INF,INF},
               {INF, 1, 2, 5, INF, INF, INF, INF},
               {INF, INF, INF, INF, 4, 11, INF, INF},
               {INF, INF, INF, INF, 9, 5, 16, INF},
               {INF, INF, INF, INF, INF, INF, 2, INF},
               {INF, INF, INF, INF, INF, INF, INF, 18},
               {INF, INF, INF, INF, INF, INF, INF, 13},
               {INF, INF, INF, INF, INF, INF, INF, 2}};
            WeightedDirectedGraph wdgraph = new WeightedDirectedGraph(7);
            int src = 0; int dst = 6;
            wdgraph.AddEdge(0, 1, 1);
            wdgraph.AddEdge(0, 2, 1);
            wdgraph.AddEdge(0, 3, 2);
            wdgraph.AddEdge(0, 4, 3);
            wdgraph.AddEdge(1,5 ,2);
            wdgraph.AddEdge(2, 6, 4);
            wdgraph.AddEdge(3, 6, 2);
            wdgraph.AddEdge(4, 6, 4);
            wdgraph.AddEdge(5, 6, 1);
            int test = wdgraph.AlmostShortestPath(src, dst);
            







            Console.WriteLine(test);
            //test for weighted directed graph

        }
    }
}

