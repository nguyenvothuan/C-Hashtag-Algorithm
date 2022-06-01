using System;
using System.Collections.Generic;
class NetworkFlow
{
    static readonly int V = 6; //number of vertices in this graph
    bool BFS(int[,] resGraph, int s, int t, int[] parent)
    {
        bool[] visited = new bool[V];
        Queue<int> q = new Queue<int>();
        q.Enqueue(s);
        visited[s] = true;
        parent[s] = 1;
        while (q.Count != 0)
        {
            int u = q.Dequeue();
            for (int v = 0; v < V; v++)
            {
                if (visited[v] == false && resGraph[u, v] > 0)
                {
                    if (v == t)
                    {
                        parent[v] = u;
                        return true;
                    }
                    q.Enqueue(v);
                    parent[v] = u;
                    visited[v] = true;
                }
            }
        }
        return false;
    }
    int FordFulkerson(int[,] graph, int s, int t)
    {
        int u, v;
        int[,] resGraph = new int[V, V];
        for (u = 0; u < V; u++)
        {
            for (v = 0; v < V; v++)
            {
                resGraph[u, v] = graph[u, v];
            }
        }
        int[] parent = new int[V];//to be filled by bfs
        int maxflow = 0;
        while (BFS(resGraph, s, t, parent))
        {
            // bfs from source to sink
            int pathflow = int.MaxValue;
            for (v = t; v != s; v = parent[v])
            {
                u = parent[v];
                //find the min by looking that the min forward edge of the path.
                pathflow = Math.Min(pathflow, resGraph[u, v]);
            }
            for (v = t; v != s; v = parent[v])
            {
                u = parent[v];
                resGraph[u, v] -= pathflow;
                resGraph[v, u] += pathflow;
            }
        }

        return maxflow;
    }
}