using System;
using System.Collections.Generic;
using System.Text;

namespace Graph
{
    class WeightedDirectedGraph
    {
        
        int V;
        int E;
        int[,] adj;
        public WeightedDirectedGraph(int V)
        {
            this.V = V;
            adj = new int[V, V];
            for (int i = 0; i < V; i++)
            {
                for (int j = 0; j < V; j++)
                    adj[i, j] = inf;
            }          
            E = 0;
        }
        public WeightedDirectedGraph(int V, int[,] adj)
        {
            this.adj = adj;
            this.V = V;
            E = 0;
            for (int i = 0; i < V; i++)
            {
                for (int j = 0; j < V; j++)
                {
                    if (adj[i, j] != 0)
                        E++;
                }
            }
        }
        public void AddEdge(int src, int dst, int weight)
        {
            adj[src, dst] = weight;
            
        }
        public void RemoveEdge(int src, int dst)
        {
            adj[src, dst] = 0;
        }
        private int[] Descendants(int v)
        {
            List<int> des = new List<int>(V / 2);
            for (int i = 0; i < V; i++)
            {
                if (adj[v, i] != 0)
                    des.Add(i);
            }
            int[] output = des.ToArray();
            return output;
        }
        private int[] Parent(int v)
        {
            List<int> par = new List<int>(V / 2);
            for (int i = 0; i < V; i++)
            {
                if (adj[i, v] != 0)
                    par.Add(i);
            }
            int[] output = par.ToArray();
            return output;
        }
        static protected int inf = 99999;
        public int[] BellmanFord(int src)
        {
            int[] distance = new int[V];
            int[] predecessor = new int[V];
            distance[src] = 0;
            Array.Fill(predecessor, inf);
            for (int i = 1; i < V; i++) //repeat V-1 times
            {
                for (int u = 0; u < V; u++)
                {
                    int[] son = Descendants(u);
                    foreach (int v in son)
                    {
                        if (distance[u] + adj[u, v] < distance[v])
                        {
                            distance[v] = distance[u] + adj[u, v];
                            predecessor[v] = u;
                        }
                    }
                }
            }
            for (int u = 0; u < V; u++) //check for negative cycle
            {
                int[] son = Descendants(u);
                foreach (int v in son)
                {
                    if (distance[u] + adj[u, v] < distance[v])
                        return null;
                }
            }
            return distance;
        }
        public int LongestPathBetweenAllPairs()
        {
            int[] dist = new int[V];    //an approximation of the longest path to a vertex i
            int[] topsort = TopSort();
            int inf = int.MinValue;
            Array.Fill(dist, inf);
            dist[topsort[0]] = 0;
            int max = 0;
            foreach (int u in topsort)
            {
                for (int v = 0; v < V; v++)
                {
                    if (adj[u, v] != 0)
                    {
                        if (dist[v] < dist[u] + adj[u, v])
                            dist[v] = dist[u] + adj[u, v];
                        if (dist[v] > max)
                            max = dist[v];
                    }
                }
            }
            return max;
        }
        public int[] TopSort()
        {
            bool[] visited = new bool[V];
            Stack<int> path = new Stack<int>();
            for (int i = 0; i < V; i++)
                if (visited[i] == false)
                    TopSortUtil(visited, path, i);
            int[] output = new int[V];
            for (int i = 0; i < V; i++)
                output[i] = path.Pop();
            return output;
        }
        public void TopSortUtil(bool[] visited, Stack<int> path, int v)
        {
            visited[v] = true;
            int[] children = Descendants(v);
            foreach (int child in children)
            {
                if (visited[child] == false)
                    TopSortUtil(visited, path, child);
            }
            path.Push(v);
        }
        int MinIndex(int[] dist, bool[] sptSet)
        {
            int min = int.MaxValue, min_index = -1;
            for (int v = 0; v < V; v++)
                if (sptSet[v] == false && dist[v] <= min)
                {
                    min = dist[v];
                    min_index = v;
                }
            return min_index;
        }
        public int[] Dijkstra(int src)
        {
            int[] dist = new int[V];
            Array.Fill(dist, inf);
            bool[] spt = new bool[V];
            Array.Fill(spt, false);
            dist[src] = 0;
            int[] prev = new int[V];
            prev[src] = -1;
            for (int count = 0; count < V - 1; count++)
            {
                int u = MinIndex(dist, spt);
                spt[u] = true;
                for (int v = 0; v < V; v++)
                {
                    if (adj[u, v] != 0 && !spt[v] && dist[u] != inf)
                    {
                        int temp = dist[u] + adj[u, v];
                        if (temp < dist[v])
                        {
                            dist[v] = temp;
                            prev[v] = u;
                        }
                    }
                }
            }
            return prev;

        }
        public int Dijkstra(int src, int dst)
        {
            int[] dist = new int[V];
            Array.Fill(dist, inf);
            bool[] spt = new bool[V];
            Array.Fill(spt, false);
            dist[src] = 0;
            int[] prev = new int[V];
            prev[src] = -1;
            for (int count = 0; count < V - 1; count++)
            {
                int u = MinIndex(dist, spt);
                spt[u] = true;
                for (int v = 0; v < V; v++)
                {
                    if (adj[u, v] != 0 && !spt[v] && dist[u] != inf)
                    {
                        int temp = dist[u] + adj[u, v];
                        if (temp < dist[v])
                        {
                            dist[v] = temp;
                            prev[v] = u;
                        }
                    }
                }
            }
            return dist[dst];
        }
        public int[][] DijkstraPrevDist(int src)
        {
            int[] dist = new int[V];
            Array.Fill(dist, inf);
            bool[] spt = new bool[V];
            Array.Fill(spt, false);
            dist[src] = 0;
            int[] prev = new int[V];
            prev[src] = -1;
            for (int count = 0; count < V - 1; count++)
            {
                int u = MinIndex(dist, spt);
                spt[u] = true;
                for (int v = 0; v < V; v++)
                {
                    if (adj[u, v] != 0 && !spt[v] && dist[u] != inf)
                    {
                        int temp = dist[u] + adj[u, v];
                        if (temp < dist[v])
                        {
                            dist[v] = temp;
                            prev[v] = u;
                        }
                    }
                }
            }
            int[][] result = new int[2][];
            result[0] = prev;
            result[1] = dist;
            return result;
        }
        public int[] ShortestPathFromSourceDAG(int src)
        {
            int[] top = TopSort();
            int[] dist = new int[V];
            Array.Fill(dist, inf);
            dist[src] = 0; 
            foreach (int i in top)
            {
                if (dist[i]!=inf)
                {
                    int[] children = Descendants(i);
                    foreach (int child in children)
                    {
                        if (dist[i] + adj[i, child] < dist[child])
                            dist[child] = dist[i] + adj[i, child];
                    }
                }
            }
            return dist;
        }
        public int ShortestPathMultiStageDAG(int src, int dst) 
        {
            int length = dst - src+1;
            int[] dist = new int[length];
            Array.Fill(dist, inf);
            dist[src] = 0;
            for (int i =src+1;i<dst;i++)
            {
                List<int> parentOfI = new List<int>();
                for (int par=src;par<i;par++)
                {
                    if (adj[par,i] != 0)
                        parentOfI.Add(par);
                }
                if (parentOfI.Count == 0)
                    dist[i] = inf;
                else
                {
                    int min = inf;
                    foreach (int par in parentOfI)
                    {
                        if (dist[par]!=inf)
                        {
                            int temp = dist[par] + adj[par, i];
                            if (min > temp)
                                min = temp;
                        }    
                    }
                    dist[i] = min;
                }
            }
            return dist[length - 1];
        }
        public int Min(int a, int b, int c)
        {
            int min = a;
            if (b < min) min = b;
            if (c < min) min = c;
            return min;
        }
        public int ShortestPathByAdding2WayEdge(int k, int[,] proposed2way, int src, int dst)
        {
            int[] shortesPathFromSrc = Dijkstra(src);
            int st = shortesPathFromSrc[dst];
            int shortest = st; 
            for (int i = 0; i < k; i++)
            {//consider each proposed path
                //first case from u to v: if shortest[src, dst] > shortes[src, u] + weight[u,v] + shortest[v,dst]
                int u = proposed2way[i, 0];
                int v = proposed2way[i, 1];
                int uv = proposed2way[i, 2];
                int uToDst = Dijkstra(u, dst); Console.WriteLine("From u to dst {0}: {1}", i, uToDst);
                int vToDst = Dijkstra(v, dst); Console.WriteLine("From v to dst {0}: {1}", i, vToDst);
                int srcToU = shortesPathFromSrc[u];
                int srcToV = shortesPathFromSrc[v];
                //Console.WriteLine("From s to u to v to d: "+srcToU + uv + vToDst);
                //Console.WriteLine("From s to v to u to d: " + srcToV + uv + uToDst);
                shortest = Min(shortest, srcToU + uv + vToDst, srcToV + uv + uToDst);
            }
            return shortest;
        }
        public int AlmostShortestPath(int src, int dst)
        {
            int[][] dGsrc = DijkstraPrevDist(src);
            int[] prev = dGsrc[0];
            int[] dist = dGsrc[1];
            int shortest = dist[dst];
            WeightedDirectedGraph newGraph = this;
            removeShortestPath(newGraph, prev, src, dst);
            return AlmostShortestPathUtil(src, dst, shortest, newGraph);

        }
        private int AlmostShortestPathUtil(int src, int dst, int shortest, WeightedDirectedGraph newGraph)
        {
            int[][] dGsrc = DijkstraPrevDist(src);
            int[] prev = dGsrc[0];
            int[] dist = dGsrc[1];
            if (dist[dst] >= inf) return -1; //no path to dst found
            if (dist[dst] != shortest) return dist[dst];//not the shortest path;
            //did find out shortest path,dst is stil reachable
            removeShortestPath(newGraph, prev, src, dst);
            return AlmostShortestPathUtil(src, dst, shortest, newGraph);
        }
        private void removeShortestPath(WeightedDirectedGraph newGraph,int[] prev,int src,int dst )//dont modify the original graph
        { 
            int cur = dst;
            
            while (cur!=src)
            {
                newGraph.RemoveEdge(prev[cur], cur);
                cur = prev[cur];
            }
        }
    }
}
