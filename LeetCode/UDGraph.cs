using System;
using System.Collections.Generic;

public class UDGraph
{
    List<int>[] adj;
    int V, E=0;
    public UDGraph(int V)
    {
        this.V = V;
        this.adj = new List<int>[V];
        for (int i = 0; i < V; i++) adj[i] = new List<int>();
         E = 0;
    }
    public UDGraph(int V, List<int>[] adj) {
        this.adj = adj;
        this.V = V;
        foreach (var list in adj) 
            foreach (int i in list)
                E+= i;
    }
    public void AddEdge(int u, int v) {
        adj[u].Add(v);
    }
    public int MotherVertex() {
        bool[] visited = new bool[V];
        int v=0;
        for(int i=0;i<V;i++) {
            if (!visited[i])
            {   
                DFSUtil(i, visited);
                v =i;
            }   
        }
        /*so the idea is that we run DFS on a random node. If that node can cover all other nodes 
        in the graph, it or its' descendants are  mother vertex. Else, they are not (the whole of them)
        so we will go to the next unvisited node (as not all are visited) and run DFS again, if this time there
        still exist an unvisited node, continues as they are not. Do this until all are visited.
        The last one to finished is one of the mother vertex, if any.
        */
        visited = new bool[V];
        DFSUtil(v, visited);
        foreach (bool vs in visited)
            if (!vs) return -1;
        return v;
    }   
    void DFSUtil(int s, bool[] visited) {
        visited[s] = true;
        foreach (int i in adj[s])
            if (!visited[i])
                DFSUtil(i, visited);
    }

    public int[,] TransitiveClosure(bool log = false) {
        int[,] closure = new int[V,V];
        for(int i =0;i<V;i++)
            TransitiveClosureDFSUtil(i, i, closure);
        if (log) {
            for(int i =0;i<V;i++){
                for(int j=0;j<V;j++)
                    Console.Write(closure[i,j]+" ");
                Console.WriteLine();
            }
        }
        return closure;
    }
    void TransitiveClosureDFSUtil(int s, int v, int[,] closure) {
        //find all reachabilities from s to v
        closure[s,v] = 1;
        foreach (int neighborOfV in adj[v]) {
            if (closure[s,neighborOfV]!=1) 
                TransitiveClosureDFSUtil(s, neighborOfV, closure);
        }
    }
}