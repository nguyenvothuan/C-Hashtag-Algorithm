using System;
using System.Collections.Generic;

//an undirected unweighted graph initialized on a collection of edge
public class EdgeBasedGraph { 
    class Edge {
        public int src, dst;
    }
    int V, E;
    Edge[] edges;
    int curEmptyEdge = 0;
    public EdgeBasedGraph (int v, int e) {
        V = v; E=e;
        edges = new Edge[E];
        for(int i =0;i<E;i++)
            edges[i] = new Edge();
    }
    public void AddEdge(int src, int dst) {
        edges[curEmptyEdge].src = src;
        edges[curEmptyEdge++].dst=dst;
    }

    //find the subset where i is the top representative
    int Find(int[] parent, int i ) 
    {
        if(parent[i]==-1) return i;
        return Find(parent, parent[i]);
    }
    //union x into y subset, thouhg parent of y may still be -1
    void Union(int[] parent, int x, int y) {
        parent[x] = y;
    }

    public bool IsCyclic() {
        int[] parent = new int[V];
        for(int i =0;i<V;i++) parent[i] = -1; //initially, every node is in only one subset.
        for (int i =0;i<E;i++) {
            //loop through each edge. As both vertices are at the end of an edge, these are connected
            //If they are somehow children of a parent, bump, a cycle
            //Naive implementation of union
            //else, union src into dst's subset
            int x = Find(parent, edges[i].src);
            int y = Find(parent, edges[i].dst);
            if (x==y) return true;
            Union(parent, x,y);
        }
        return false;
    }
}