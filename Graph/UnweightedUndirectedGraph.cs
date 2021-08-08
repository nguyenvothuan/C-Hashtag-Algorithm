using System;
using System.Collections.Generic;
using System.Text;

namespace Graph
{
    public class UnweightedUndirectedGraph
    {
        List<int>[] adj;
        int V;
        int E;
        int[] DSparent;
        int[] DSsize;
       
        public UnweightedUndirectedGraph(int V)
        {
            this.V = V;
            adj = new List<int>[V];
            for (int i = 0; i < V; i++)
                adj[i] = new List<int>();
            E = 0;
            DSparent = new int[V];
            DSsize = new int[V];
            for (int i = 0; i < V; i++)
                MakeSet(i);
        }
        public UnweightedUndirectedGraph(int V, List<int>[] adj)
        {
            this.adj = adj;
            this.V = V;
            foreach (List<int> i in adj)
            {
                E += i.Count;
            }
            E = E / 2;  //undirected graph be like
            for (int i = 0; i < V; i++)
                MakeSet(i);
            for (int i =0;i<V;i++)
            {
                foreach (int j in adj[i])
                    UnionSets(i, j);  //merge sets if there is an edge ij
            }
        }
        void MakeSet(int v)
        {
            DSparent[v] = v;
            DSsize[v] = 1;            
        }
        void UnionSets(int a, int b)
        {
            int ra = FindSet(a); // ra is the representative vertex of the set that contains a
            int rb = FindSet(b);
            if (ra!=rb)
            {
                if (DSsize[ra]<DSsize[rb])
                {
                    DSparent[ra] = rb;
                    DSsize[rb] += DSsize[ra];
                } else
                {
                    DSparent[rb] = ra;
                    DSsize[ra] += DSsize[rb];
                }
            }
        }
        int FindSet(int v)
        {
            if (v == DSparent[v])
                return v;
            DSparent[v] = FindSet(DSparent[v]);
            return DSparent[v];

        }
        public int CountComponents()
        {
            int count = 0;
            for (int i = 0; i < V; i++)
                if (DSparent[i] == i)
                    count++;
            return count;
        }
        public int GetEdge() { return E; }
        public bool AddEdge(int src, int dst)
        {
            if (adj[src].Contains(dst) || adj[dst].Contains(src)) return false;
            adj[src].Add(dst);
            adj[dst].Add(src);
            E++;
            UnionSets(src, dst); //merge two disjoint sets of src and dst
            return true;

        }
        public bool RemoveEdge(int src, int dst)
        {
            if (!adj[src].Contains(dst) || !adj[dst].Contains(src))
                return false;
            adj[src].Remove(dst);
            adj[dst].Remove(src);
            return true;
        }
        public void KCore(int k)
        {   //a connected components left after deleting all vertices with degree smaller than k
            bool[] visited = new bool[V];
            Array.Fill(visited, false);
            int[] Degree = new int[V];
            int count = 0;
            foreach (List<int> i in adj)
            {
                Degree[count] = i.Count;
                count++;
            }
            for (int i = 0; i < V; i++)
            {
                if (!visited[i])
                    DFSUtilKCore(i, visited, Degree, k);    //in case the graph is unconnected, do the for loop
            }
            List<List<int>> kcore = new List<List<int>>();
            for (int i = 0; i < V; i++)
            {
                if (Degree[i] >= k)
                {
                    Console.Write("[{0}]: ", i);
                    foreach (int neighbor in adj[i])
                    {
                        if (Degree[neighbor] >= k)
                            Console.Write(neighbor + ", ");
                    }
                    Console.WriteLine();
                }
            }

        }
        private bool DFSUtilKCore(int v, bool[] visited, int[] degree, int k)
        {   //update degree of v, after processing all of its descendants
            visited[v] = true;
            foreach (int neighbor in adj[v])
            {
                if (degree[v] < k)  //if degree of v is smaller than k, then one neighbor of neighbor is not in the kcore
                    degree[neighbor]--;
                if (!visited[neighbor])
                {
                    if (!DFSUtilKCore(neighbor, visited, degree, k)) //if degree of neighbor, after processing, is smaller than k
                        degree[v]--;
                }
            }
            return (degree[v] > k);
        }
        public int ShortestPathFromTwoPrimes(int src, int dst)
        {   //let's use bfs
            Queue<int> que = new Queue<int>();
            bool[] visited = new bool[9999];    //there are 1061 4-digit prime numbers
            if (src == dst)
                return 0;
            if (!CheckPrime(src) || !CheckPrime(dst))
                return -1;
            que.Enqueue(src);
            visited[src] = true;
            Array.Fill(visited, true);
            for (int i = 1000; i < 9999; i++)
            {
                if (CheckPrime(i))
                    visited[i] = false;
            }
            return DFSPrimeUtil(que, visited, dst);
        }
        private int DFSPrimeUtil(Queue<int> que, bool[] visited, int dst)
        {
            int cur = que.Dequeue();
            List<int> neighbor = FindNeighborPrime(cur);
            foreach (int prime in neighbor)
            {
                if (visited[prime] == false)
                {
                    if (prime == dst)
                        return 1;   //just take one more step to reach dst from cur
                    que.Enqueue(prime);
                    visited[prime] = true;
                }
            }
            //if none of the neighbor matches dst
            if (CountElementArray(visited, false) == 1) //all other prime numbers except dst are used without reaching dst
                return -1;
            return 1 + DFSPrimeUtil(que, visited, dst);

        }
        private List<int> FindNeighborPrime(int p)
        {
            int a = p % 1000;
            p = p - a * 1000;
            int b = p % 100;
            p = p - b * 100;
            int c = p % 10;
            int d = p - c * 10;
            List<int> neighbor = new List<int>();
            for (int i = 1; i <= 9; i++)
            {
                if (i != a)
                {
                    int abcd = FormNumber(i, b, c, d);
                    if (CheckPrime(abcd))
                        neighbor.Add(abcd);
                }
            }
            for (int i = 1; i <= 9; i++)
            {
                if (i != b)
                {
                    int abcd = FormNumber(a, i, c, d);
                    if (CheckPrime(abcd))
                        neighbor.Add(abcd);
                }
            }
            for (int i = 1; i <= 9; i++)
            {
                if (i != c)
                {
                    int abcd = FormNumber(a, b, i, d);
                    if (CheckPrime(abcd))
                        neighbor.Add(abcd);
                }
            }
            for (int i = 1; i <= 9; i++)
            {
                if (i != d)
                {
                    int abcd = FormNumber(a, b, c, i);
                    if (CheckPrime(abcd))
                        neighbor.Add(abcd);
                }
            }
            return neighbor;
        }
        private int CountElementArray(bool[] arr, bool element)
        {
            int count = 0;
            foreach (bool ob in arr)
            {
                if (ob == element)
                    count++;
            }
            return count;
        }
        private bool CheckPrime(int num)
        {
            int breakpoint = (int)Math.Sqrt(num);
            for (int i = 2; i <= breakpoint; i++)
            {
                if (num % i == 0)
                    return false;
            }
            return true;
        }
        private int FormNumber(int a, int b, int c, int d)
        { return a * 1000 + b * 100 + c * 10 + d; }
        public void WaterJug(int m, int n, int k)
        {
            Stack<Pair> path = new Stack<Pair>();
            Pair start = new Pair(0, 0);
            List<Pair> visited = new List<Pair>();
            DFSUtilWaterJug(path, start, visited, m, n, k);

        }
        private bool DFSUtilWaterJug(Stack<Pair> path, Pair cur, List<Pair> visited, int m, int n, int k)
        { //check if adding pair cur will eventually lead to the solution, known that pair is not the final stage
            if (visited.Count > 1000)
                return false;
            visited.Add(cur);
            path.Push(cur);
            int x = cur.x; int y = cur.y;
            Pair[] allWays = new Pair[6];
            allWays[0] = new Pair(0, y); //empty x
            allWays[1] = new Pair(x, 0); //empty y
            allWays[2] = new Pair(m, y); //fill x
            allWays[3] = new Pair(x, n); //fill y
            allWays[4] = new Pair(x + y - n, n); //pour x to y
            allWays[5] = new Pair(m, x + y - m); //pour y to x
            foreach (Pair move in allWays) //traverse all reachable moves from x and y
            {
                if (IsSafeWaterJug(move, visited)) //if move is valid
                {
                    if (move.x == k || move.y == k)
                    {
                        foreach (Pair step in path)
                        {
                            step.DisplayPair();
                            Console.Write("; ");
                        }
                        return true;
                    }
                    //now that move is not the desired stage
                    if (DFSUtilWaterJug(path, move, visited, m, n, k))
                        return true;
                }
            }
            path.Pop(); //backtracking
            return false;
        }
        private bool IsSafeWaterJug(Pair cur, List<Pair> visited)
        {//check if the current move is valid
            int x = cur.x;
            int y = cur.y;
            if (x < 0 || y < 0 || visited.Contains(cur))
                return false;
            return true;
        }
        public int CountTreeInForest()
        {
            bool[] visited = new bool[V];
            int count = 0;
            for (int i = 0; i < V; i++)
            {
                if (!visited[i])
                {
                    DFSUtil(i, visited);
                    count++;
                }
            }
            return count;
        }
        public void DFSUtil(int cur, bool[] visited)
        {
            visited[cur] = true;
            foreach (int neighbor in adj[cur])
                if (!visited[neighbor])
                    DFSUtil(neighbor, visited);
        }
        public class Pair
        {
            public int x;
            public int y;
            public Pair(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
            public void DisplayPair()
            {
                Console.Write("({0}, {1})", x, y); //(x, y)
            }
        }
        public int[] ArticulationPoints()
        {
            bool[] visited = new bool[V];
            int[] depth = new int[V];
            int[] low = new int[V];
            bool[] ap = new bool[V];
            int[] parent = new int[V];
            Array.Fill(visited, false);
            Array.Fill(parent, -1);
            int time = 0;
            for (int i = 0; i < V && !visited[i]; i++)  //in case the graph is disconnected
                APUtil(i, time, visited, depth, low, parent, ap);
            List<int> aplist = new List<int>();
            for (int i = 0; i < V; i++)
                if (ap[i])
                    aplist.Add(i);
            return aplist.ToArray();

        }
        private void APUtil(int u, int time, bool[] visited, int[] depth, int[] low, int[] parent, bool[] ap)
        {
            visited[u] = true;
            depth[u] = low[u] = ++time;
            int childCount = 0;
            foreach (int v in adj[u])
            {
                if (visited[v] == false)
                {
                    childCount++;
                    parent[v] = u;
                    APUtil(v, time, visited, depth, low, parent, ap);
                    low[u] = Math.Min(low[v], low[u]); //that said, there is a back
                    if (parent[u] == -1 && adj[u].Count >= 2) //u is the root
                        ap[u] = true;
                    if (parent[u] != -1 && low[v] >= depth[u])
                        ap[u] = true;
                }
                else if (v != parent[u])
                    low[u] = Math.Min(low[u], depth[v]); //that said, there is a backedge from u to v, and v has a lower point than u
            }
        }
        public Pair[] Bridges()
        {
            bool[] visited = new bool[V];
            int[] parent = new int[V];
            int[] low = new int[V];
            int[] depth = new int[V];
            List<Pair> bridges = new List<Pair>();
            int time = 0;
            Array.Fill(parent, -1); //DFS from 0
            for (int i = 0; i < V; i++)
                if (!visited[i])
                    BridgeUtil(i, visited, depth, low, parent, bridges, time);
            Pair[] output = bridges.ToArray();
            return output;
        }
        private void BridgeUtil(int u, bool[] visited, int[] depth, int[] low, int[] parent, List<Pair> bridge, int time)
        {
            visited[u] = true;
            low[u] = depth[u] = ++time;
            foreach (int v in adj[u])
            {
                if (!visited[v])
                {
                    parent[v] = u;
                    BridgeUtil(v, visited, depth, low, parent, bridge, time);
                    low[u] = Math.Min(low[u], low[v]);
                    if (low[v] > depth[u])
                        bridge.Add(new Pair(u, v));
                }
                else if (v != parent[u])
                    low[u] = Math.Min(low[u], depth[v]);
            }
        }
        public int CountCyclesOfLength(int n)
        {
            bool[] visited = new bool[V];
            for (int i=0;i<V-(n-1);i++) //it only takes V-(n-1) vertices to start finding all cycles
            {
                DFSUtilCountCycle(visited, n, i, i);
                visited[i] = true;
            }
            return count/2;
        }
        private int count = 0;
        private void DFSUtilCountCycle(bool[] visited,int length, int cur, int end)
        {   //find a cycle of length length, start at start, end at cur
            visited[cur] = true;
            if (length==0)
            {
                if (adj[cur].Contains(end))
                    ++count; 
                visited[cur] = false;   //backtracking
                return;                
            }
            foreach (int neighborOfCur in adj[cur])
                if (!visited[neighborOfCur])
                    DFSUtilCountCycle(visited, length - 1, neighborOfCur, end);
            visited[cur] = false;   //backtracking
        }
        public bool PathMoreThanK(int k, int src)
        {   //src!=0
            int inf = int.MinValue;
            int[] farthest = new int[V]; //farthest[i] = 0 if i is in the tree, else it is the farthest vertex to i from the tree
            Array.Fill(farthest, src);
            List<int> tree = new List<int>();
            farthest[src] = 0;
            foreach (int neighborOfsrc in adj[src])
                farthest[neighborOfsrc] = src;
            int weight = 0;
            for (int j=0;j<V;j++)
            {
                int u = -1;int v = -1;
                int max = int.MinValue;
                for (int i=0;i<V;i++ )
                {
                    if (farthest[i]!=0 && adj[i][farthest[i]]>max)
                    {
                        max = adj[i][farthest[i]];
                        u = farthest[i];
                        v = i;
                    }
                }
                farthest[v] = 0;
                for (int i = 0; i < V; i++)
                    if (farthest[i] != 0 && adj[i][farthest[i]] < adj[i][v])
                        farthest[i] = v;
                weight += max;
                if (weight >= k)
                    return true;
            }
            return false;
        }
        public bool CheckPath(int src, int dst)
        {
            Queue<int> que = new Queue<int>();
            que.Enqueue(src);
            bool[] visited = new bool[V];
            while (que.Count!=0)
            {
                int cur = que.Dequeue();
                visited[cur] = true;
                foreach (int neighbor in adj[cur])
                    if (!visited[neighbor])
                    {
                        if (neighbor == dst)
                            return true;
                        que.Enqueue(neighbor);
                    }
            }
            return false;
        }
        public bool IncrementalConnectivity(int u, int v)
        {//check if adding uv decrease the number of components 
            int count1 = CountComponents();
            AddEdge(u, v);
            int count2 = CountComponents();
            RemoveEdge(u, v);
            if (count1 != count2)
                return true;
            return false;

            
        }
    }
}
