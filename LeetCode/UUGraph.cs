using System;
using System.Collections.Generic;

public class UUGraph
{
    int V;
    int E = 0;
    List<int>[] adj;
    public UUGraph(int V, int[,] adj)
    {
        this.V = V;
        this.adj = new List<int>[V];
        for (int i = 0; i < V; i++) this.adj[V] = new List<int>();
        for (int i = 0; i < V; i++)
        {
            for (int j = 0; j < i; j++)
            {
                if (adj[i, j] != 0)
                {
                    this.adj[i].Add(j);
                    this.adj[j].Add(i);
                    E++;
                }
            }
        }
    }
    public UUGraph(int V, List<int>[] adj)
    {
        this.V = V;
        this.adj = adj;
    }
    public UUGraph(int V, int[][] adj)
    {
        //TODO: If needed, finish this shit
    }
    public UUGraph(int V)
    {
        this.V = V;
        adj = new List<int>[V];
        for(int i =0;i<V;i++) adj[i] =new List<int>();
        //TODO: Do this later
    }

    public void AddEdge(int u, int v)
    {
        //Caution: only add edge when you know for sure there isn't such an edge before
        adj[u].Add(v);
        adj[v].Add(u);
    }
    public bool IsNeighbor(int u, int v)
    {
        return adj[u].Contains(v);
    }

    public IList<int> BFS(int s, bool log = false)
    {
        Queue<int> queue = new Queue<int>();
        bool[] visited = new bool[V];
        queue.Enqueue(s);
        visited[s] = true;
        List<int> final = new List<int>();
        while (queue.Count != 0)
        {
            int cur = queue.Dequeue();
            final.Add(cur);
            if (log) Console.Write(cur + " ");
            foreach (int i in adj[cur])
                if (!visited[i]) {queue.Enqueue(i);visited[i] = true;}
        }
        return final;
    }

    public IList<int> DFS(int s, bool log = false) {
        Stack<int> stack = new Stack<int>();
        bool[] visited= new bool[V];
        stack.Push(s);
        visited[s] = true;
        List<int> final = new List<int>();
        while (stack.Count!=0){
            int cur = stack.Pop();
            if (log) Console.Write(cur+" ");
            final.Add(cur);
            foreach (int i in adj[cur])
                if (!visited[i]) {
                    stack.Push(i);
                    visited[i] = true;
                }
        }
        return final;
    }

    

}