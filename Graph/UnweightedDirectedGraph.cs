using System;
using System.Collections.Generic;
namespace Graph
{
    class UnweightedDirectedGraph
    {
        List<int>[] adj;
        int V;
        int E;
        int[] DSparent;
        public UnweightedDirectedGraph(int V, List<int>[] adj) 
        {
            this.V = V;
            this.adj = adj;
            for (int i = 0; i < V; i++)
                E += adj[i].Count;
        }
        public UnweightedDirectedGraph(int V)
        {
            this.V = V;
            adj = new List<int>[V];
            for (int i = 0; i < V; i++)
            {
                adj[i] = new List<int>();
            }
            E = 0;
        }
        public bool AddEdge(int src, int dst) 
        {//true if there is no edge between src and dst, false if there is already one
            if (!adj[src].Contains(dst))
            {
                adj[src].Add(dst);
                ++E;
                return true;
            }
            return false;

        }
        public bool RemoveEdge(int src, int dst)   
        { //true if there is one edge src to dst to remove, false if none.
            if (adj[src].Contains(dst))
            {
                adj[src].Remove(dst);
                E--;
                return true;
            }
            return false;
        }
        public int GetEdge()
        { return E; }
        public int GetVertices()
        { return V; }
        public bool IsCyclic()
        {
            bool[] visited = new bool[V];
            Array.Fill(visited, false);
            for (int count = 0; count < V; count++) //in case there
            {
                if (visited[count] == false)
                {
                    int start = count;
                    Stack<int> path = new Stack<int>();
                    path.Push(start);

                    foreach (int neighbor in adj[start])
                    {
                        if (IsCyclicUtil(path, neighbor, visited))
                            return true;
                    }
                    return false;
                }
            } return false;
        }
        private bool IsCyclicUtil(Stack<int>path, int cur, bool[] visited)
        {   //check if, with the presence of cur, there is any cycle formed
            int parent = path.Peek();
            path.Push(cur);
            visited[cur] = true;
            foreach (int neighbor in adj[cur])  //check all its neighbor to see if there is any cycle
            {
                
                    if (path.Contains(neighbor)) //if there is a back edge
                        return true;
                    // if there is no back edge
                    if (IsCyclicUtil(path, neighbor,visited))
                        return true;
                
            }
            //now that there is no cycle checking the cur's neighbor, backtracking
            path.Pop();
            return false;
        }       
        //public bool IsConnected()
        public void DepthFirstSearch(int v)
        {
            Stack<int> path = new Stack<int>();
            path.Push(v);
            while (path.Count != 0)
            {
                int cur = path.Peek();
                foreach (int neighbor in adj[cur])
                {
                    if (!path.Contains(neighbor))
                        path.Push(neighbor);
                }
                if (path.Peek() == cur) //if all descendants of cur is already visited
                    path.Pop();
            }
        }
        public int[] BFSLevelFromZero()
        {   //return an array level[], where level[i] is the level of i from 0
            Queue<int> q = new Queue<int>();
            q.Enqueue(0);
            int[] level = new int[V];
            bool[] visited = new bool[V];
            visited[0] = true;
            while (q.Count!=0)
            {
                int cur = q.Dequeue();
                foreach (int neighbor in adj[cur])
                {
                    if (!visited[neighbor])
                    {
                        visited[neighbor] = true;
                        level[neighbor] = level[cur] + 1;
                        q.Enqueue(neighbor);
                    }    
                }
            }
            return level;
        }
        private void DFSUtil(int v, bool[] visited)
        {
            visited[v] = true;
            foreach (int neighbor in adj[v])
            {
                if (!visited[neighbor])
                    DFSUtil(neighbor, visited);
            }
        }
        public int MotherVertex()
        {
            bool[] visited = new bool[V];
            int v = -1;
            for (int i = 0; i < V; i++)
            {
                if (visited[i] == false)
                {
                    DFSUtil(i, visited);
                    v = i;
                }
            }
            if (v == -1) { return -1; }
            bool[] check = new bool[V];
            DFSUtil(v, check);
            foreach (bool vertex in check)
                if (!vertex)
                    return -1;
            return v;
        }
        private Stack<int> DFSUtilSCC(Stack<int> finishTime, int v, bool[] visited, List<int>[] adj)
        {   //modify visited for each vertex traversed in this DFS, modify the Stack to store vertex traversed in this DFS, v is the source, adj is the graph to traverse
            visited[v] = true;
            foreach (int neighbor in adj[v])
            {
                if (!finishTime.Contains(neighbor) && !visited[neighbor])
                    DFSUtilSCC(finishTime, neighbor, visited,adj);
            }
            finishTime.Push(v);
            return finishTime;
        }
        private int SearchForFalse(bool[] arr)
        {
            for (int i = 0; i < V; i++)
            {
                if (!arr[i])
                    return i;
            }
            return -1;
        }
        public void StronglyConnectedComponents()
        {
            Stack<int> finishTime = new Stack<int>();
            bool[] visited = new bool[V];
            for (int i = 0; i < V; i++)
                visited[i] = false;
            DFSUtilSCC(finishTime, 0, visited, adj);
            while (true)    //search for unvisited vertex
            {
                int cur = SearchForFalse(visited);
                if (cur == -1)
                    break;
                DFSUtilSCC(finishTime, cur, visited,adj);
            }
            List<int>[] Tgraph = new List<int>[V];
            for (int i = 0; i < V; i++)
                Tgraph[i] = new List<int>();    //transpose graph
            for (int i =0;i<V;i++)
            {
                foreach (int j in adj[i])
                {
                    Tgraph[j].Add(i);
                }
            }

            //run DFS for each vertex in the stack in Tgraph
            List<Stack<int>> SCCs = new List<Stack<int>>();     //SCCs store SCC found in the next traversings
            bool[] check = new bool[V]; //check is used to mark visited vertex in the second DFS
            for (int i = 0; i < V; i++)
                check[i] = false;
            while (finishTime.Count!=0)
            {
                int curVertex = finishTime.Pop();
                if (!check[curVertex])  //if cur is not visited yet
                {
                    Stack<int> curSCC = DFSUtilSCC2(curVertex,check,Tgraph);           //do DFS for cur        
                    SCCs.Add(curSCC);
                }               
            }
            foreach (Stack<int> SCC in SCCs)
            {
                foreach (int i in SCC)
                    Console.Write(i + " ");
                Console.WriteLine();
            }
        }
        private Stack<int> DFSUtilSCC2(int v, bool[] check, List<int>[] graph)  
        {//start from cur, perform DFS in graph, and modify check
            Stack<int> path = new Stack<int>();
            Stack<int> output = new Stack<int>();
            path.Push(v);
            check[v] = true;
            while(path.Count!=0)
            {
                int cur = path.Peek();
                foreach (int neighbor in graph[cur])
                {
                    if (check[neighbor] == false)
                    {
                        path.Push(neighbor);
                        check[neighbor] = true;
                    }
                }
                if (path.Peek() == cur)
                    output.Push(path.Pop());
            }
            return output;
        }
        public int[,] TransitiveClosure()
        {
            int[,] tc = new int[V, V];
            for (int i =0;i<V;i++)
                DFSUtilTranClosure(i, i, tc);
            return tc;

        }
        private void DFSUtilTranClosure(int src, int v, int[,] tc)  
        { //perform a recursive DFS for source src and its descendant v
            tc[src, v] = 1;
            foreach (int i in adj[v])
            {
                if (tc[src, i] == 0)
                    DFSUtilTranClosure(src, i, tc);
            }
        }
        public int AllPossiblePaths(int src, int dst)
        {
            if (IsCyclic())
                return -1;
            Stack<int> path = new Stack<int>();
            bool[] visited = new bool[V];                       
            int[,] DPForAllPath = new int[V, V];
            for (int i = 0; i < V; i++)
                for (int j = 0; j < V; j++)
                    DPForAllPath[i, j] = -1;
            int count = AllPathUtil(src, dst, path, visited,DPForAllPath); 
            return count;
        }
        private int AllPathUtil(int src, int dst, Stack<int> path, bool[] visited, int[,] dpForPastResult)
        { //number of way to reach dst from src
            if (dpForPastResult[src, dst] != -1)
                return dpForPastResult[src, dst]; //if such paths are computed, simply return 
            int count = 0;
            path.Push(src);
            visited[src] = true;
            foreach (int neighbor in adj[src])
            {
                if (!visited[neighbor]) //make sure there is no duplicate visit
                {
                    if (neighbor == dst)
                        count++;
                    else
                    {
                        int temp = AllPathUtil(neighbor, dst, path, visited, dpForPastResult);
                        dpForPastResult[neighbor, dst] = temp;
                        count += temp;
                    }
                }
            }
            path.Pop(); //backtracking;

            return count;
        }
        public int[] TopoligicalSort()
        {
            //int startVertex = 0;
            Stack<int> topsort = new Stack<int>();
            bool[] visited = new bool[V];
            Array.Fill(visited, false);
            for (int i = 0; i < V; i++)
            {
                if (!visited[i])
                    TopSortUtil(i, topsort, visited);
            }
            int[] output = new int[topsort.Count];
            for (int i =0;i<V;i++)
                output[i] = topsort.Pop();
            return output;
        }
        private void TopSortUtil(int v, Stack<int> topsort, bool[] visited)
        {
            visited[v] = true;
            foreach (int neighbor in adj[v])
            {
                if (visited[neighbor] == false)
                    TopSortUtil(neighbor, topsort, visited);
            }
            topsort.Push(v);
        }
        public int[][] AllTopologicalSort()
        {
            int[] indegree = new int[V];
            foreach (List<int> u in adj)
            {
                foreach (int v in u)
                    indegree[v]++;
            }            
            bool[] visited = new bool[V];
            Stack<int> path = new Stack<int>();
            List<int[]> AllSorts = new List<int[]>();
            AllTopSortUtil(visited, path, indegree, AllSorts);
            int[][] output = AllSorts.ToArray();
            return output;
        }
        private void AllTopSortUtil(bool[] visited, Stack<int> path, int[] indegree, List<int[]> AllSorts)
        {
            bool NotAllVisitedFlag = false;
            for (int i=0;i<V;i++)
            {
                if (!visited[i]&&indegree[i]==0)
                {
                    path.Push(i);
                    visited[i] = true;
                    foreach (int neighbor in adj[i])
                        indegree[neighbor]--;
                    AllTopSortUtil(visited, path, indegree,AllSorts);
                    path.Pop();
                    visited[i] = false;
                    foreach (int neighbor in adj[i])
                        indegree[neighbor]++;
                    NotAllVisitedFlag = true;
                }                
            }
            if (!NotAllVisitedFlag) //all visited
            {
                int[] pathrecord = path.ToArray();
                int[] reverse = new int[pathrecord.Length];
                for (int i = 0; i < V; i++)
                    reverse[i] = pathrecord[V - i-1];
                AllSorts.Add(reverse);
            }
        }
        public int[] KahnTopSort()
        {
            int[] indegree = new int[V];
            foreach (List<int> u in adj)
            {
                foreach (int v in u)
                    indegree[v]++;
            }
            List<int> path = new List<int>();
            List<int> nextZeroIndegreeIndex = new List<int>();
            for (int i = 0; i < V; i++)
                if (indegree[i]==0)
                 nextZeroIndegreeIndex.Add(i);
            while (path.Count<V)
            {
                if (nextZeroIndegreeIndex.Count == 0)
                    break;
                List<int> temp = new List<int>();
                foreach (int vertex in nextZeroIndegreeIndex)
                {
                    path.Add(vertex);
                    foreach (int neighbor in adj[vertex])
                    { //update indegree
                        indegree[neighbor]--;
                        if (indegree[neighbor] == 0)
                            temp.Add(neighbor);
                    }
                }
                nextZeroIndegreeIndex = temp;
            }

            return path.ToArray();
        }
    }
}
