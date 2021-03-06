using System;
using System.Collections.Generic;
using System.Collections;


class TrivialHasing
{
    static int Max = 1000;
    static bool[,] contained = new bool[Max, 2];

    static bool Search(int i)
    {
        if (i >= 0)
        {
            return (contained[i, 1]);
        }
        return contained[Math.Abs(i), 0];
    }
    static bool Remove(int i)
    {
        if (i >= 0)
        {
            if (contained[i, 1]) { contained[i, 1] = false; return true; }
            return false;
        }

        if (contained[Math.Abs(i), 0]) { contained[Math.Abs(i), 0] = false; return !false; }
        return false;
    }
}

class GeneralHashing
{
    ///<summary>Check if the first arr is a subset of the later</summary>
    public bool IsSubSet(int[] M, int[] N)
    {
        HashSet<int> hashSet = new HashSet<int>(M);
        foreach (int i in N)
            if (!hashSet.Contains(i))
                return false;
        return true;
    }
    public void TestDictionary()
    {
        Dictionary<int, string> dict = new Dictionary<int, string>() {
            {1,"one"},
            {2,"two"},
            {3,"three"},

        };
        dict.Add(12, "twelve");
        dict.Add(11, "eleven");
        // foreach(var stuff in dict) {//var here is KeyValuePair<TKey, TValue>
        //     Console.WriteLine(stuff);
        // }
        for (int i = 0; i < 20; i++)
        {
            if (dict.ContainsKey(i))
                Console.WriteLine(dict[i]);
        }

    }
    public void TestHashTable()
    {
        Hashtable hashtable = new Hashtable(100);
        hashtable.Add(1, "one");
        hashtable.Add("one", 1);
        hashtable.Add(1, "second one");
        foreach (var shit in hashtable)
        {
            Console.WriteLine(shit);

        }
    }

    public void TestHashSet() {
        HashSet<int> hash= new HashSet<int>();
        hash.Add(1);
        hash.Add(2);hash.Add(3);

    }



    public int MinimumNumberToDeleteIdArray(int[] arr)
    {
        Dictionary<int, int> hash = new Dictionary<int, int>();
        foreach (int i in arr)
        {
            if (hash.ContainsKey(i))
                hash[i]++;
            else
                hash.Add(i, 1);
        }
        int max = 0;
        foreach (KeyValuePair<int, int> pair in hash)
        {
            if (pair.Value > max)
            {
                max = pair.Value;
            }
        }
        return arr.Length - max;
    }
    public int MaximumDistanceOccurence(int[] arr)
    {
        Dictionary<int, int[]> dict = new Dictionary<int, int[]>();
        for (int i = 0; i < arr.Length; i++)
        {
            if (!dict.ContainsKey(arr[i]))
            {
                int[] firstAndLast = { i, i };
                dict.Add(arr[i], firstAndLast);
            }
            else
            {
                dict[arr[i]][1] = i; //set index of new occurence
            }
        }
        int maxDist = 0;
        foreach (var pair in dict)
        {
            int cur = pair.Value[1] - pair.Value[0];
            if (cur > maxDist)
                maxDist = cur;
        }
        return maxDist;
    }

    public List<List<int>> PrintBinaryTreeInVerticalOrder(BinaryTree bst)
    {
        Dictionary<int, List<int>> hash = new Dictionary<int, List<int>>();
        Node root = bst.Root;
        BSTVerticalOrderHelper(root, 0, hash);
        List<List<int>> list = new List<List<int>>();
        foreach (var pair in hash)
        {
            list.Add(pair.Value);

        }
        return list;
    }
    private void BSTVerticalOrderHelper(Node curNode, int curLv, Dictionary<int, List<int>> hash)
    {
        if (curNode == null) return;
        if (!hash.ContainsKey(curLv))
        {
            List<int> thisLvNodes = new List<int>();
            thisLvNodes.Add(curNode.value);
            hash.Add(curLv, thisLvNodes);
            BSTVerticalOrderHelper(curNode.Left, curLv - 1, hash);
            BSTVerticalOrderHelper(curNode.Right, curLv + 1, hash);
        }
        else
        {
            hash[curLv].Add(curNode.value);
            BSTVerticalOrderHelper(curNode.Left, curLv - 1, hash);
            BSTVerticalOrderHelper(curNode.Right, curLv + 1, hash);
        }


    }

    private float[] GetLineAB(int x1, int y1, int x2, int y2)
    {
        if (x1 == x2 && y1 == y2)
        {
            return new float[0];
        }
        if (x1 == x2 || y1 == y2)
        {
            return new float[0];
        }
        float[] ab = new float[2];

        ab[0] = (y1 - y2) / (x1 - x2);
        ab[1] = y1 - ab[0] * x1;
        return ab;
    }

    public int MaxPointsContainedOnALine(int[][] points)
    {//TODO 4: The problem with this one is that it will check if the hash table contains EXACTLY the array represents 
    //the line or not. So when another line is identified to represent the same line, it returns false because their hashkeys
    //are different
        Dictionary<float[], List<int[]>> lineAndPoints = new Dictionary<float[], List<int[]>>();
        int numPoint = points.Length;
        for (int i = 0; i < numPoint - 1; i++)
        {
            for (int j = i + 1; j < numPoint; j++)
            {
                int[] p1 = points[i];
                int[] p2 = points[j];

                float[] line = GetLineAB(p1[0], p1[1], p2[0], p2[1]);
                
                

                if (line.Length > 0)
                {
                    if (!lineAndPoints.ContainsKey(line))
                    {
                        List<int[]> newlist = new List<int[]>();
                        newlist.Add(p1); newlist.Add(p2);
                        lineAndPoints.Add(line,newlist);
                       
                    }
                    else {
                        if (!lineAndPoints[line].Contains(p1))  lineAndPoints[line].Add(p1);
                        if (!lineAndPoints[line].Contains(p2))  lineAndPoints[line].Add(p2);
                    }
                }
            }

        }
        int max =0;
        foreach (var line in lineAndPoints) {
            if (line.Value.Count>max) 
            {
                max = line.Value.Count;
            }
        }
        return max;
    }

    public bool DuplicateInKDistance(int k, int[] arr) {
        Dictionary<int, int> dict = new Dictionary<int, int>();//key for cur, int been the last happened index
        for (int i = 0; i < arr.Length; i++)
        {
            if (!dict.ContainsKey(arr[i]))
                dict.Add(arr[i], i);
            else {
                if (i - dict[arr[i]]<=k) return true;
                dict[arr[i]]=i;
            }
        }
        return false;
    }

    public int[] ShortestSubSegmentWithAllHighestFreqElement(int[] arr) {
        List<int> valWithMaxFreq = new List<int>();//contains value as 
        int curMaxFreq = 0;
        Dictionary<int, int[]> valFreqFirstLast= new Dictionary<int, int[]>();//first is freq, then first occurence, then last occurence
        
        for (int i =0;i<arr.Length;i++) {
            if (!valFreqFirstLast.ContainsKey(arr[i])){
                if (curMaxFreq==0) curMaxFreq=1;
                int[] curFreqFirstLast = {1, i, i};
                valFreqFirstLast.Add(arr[i], curFreqFirstLast);
            }
            else {
                int curFreq = ++valFreqFirstLast[arr[i]][0];//increment by 1 freq
                valFreqFirstLast[arr[i]][2] = i;//last seen: 9 years ago
                if (curFreq==curMaxFreq) valWithMaxFreq.Add(arr[i]);
                if (curFreq>curMaxFreq) {
                    valWithMaxFreq.Clear();
                    valWithMaxFreq.Add(arr[i]);
                }
            }
        }
        int smallestSubSeg = 99999;
        int smallestSubSegIndex = -1;//first index of this seg
        
        foreach (int i in valWithMaxFreq) {
            int curSegLength = valFreqFirstLast[i][2] - valFreqFirstLast[i][1];
            if (curSegLength<smallestSubSeg) {
                smallestSubSeg =  curSegLength;
                smallestSubSegIndex = valFreqFirstLast[i][1];
            }
            
        }
        
        int curVal = arr[smallestSubSegIndex];
        int sample = curVal;
        List<int> seg = new List<int>();
        seg.Add(curVal);
        while (smallestSubSeg>0) {
            curVal = arr[++smallestSubSegIndex];
            if  (curVal==sample)
            {
                smallestSubSeg--;
            } 
        }
        return seg.ToArray();
    }


    public bool IsDisJoint(int[] arr1, int[] arr2) {
        HashSet<int> a1 = new HashSet<int>(arr1);
        HashSet<int> a2 = new HashSet<int>(arr2);
        a1.UnionWith(a2);
        return a1.Count == a1.Count+a2.Count;
    }

    // public string[] Itinerary(string[] src, string[] dst) {
    //     if (src.Length!=dst.Length) 
    //         throw new Exception("src must have the same length of dst");
    //     Dictionary<string, string> toFrom = new Dictionary<string, string>();//dst -> src
    //     Dictionary<string, string> fromTo = new Dictionary<string, string>(); 
    //     for (int i =0;i<src.Length;i++)
    //     {
    //         toFrom.Add(dst, src);
    //         fromTo.Add(src, dst);
    //     }
    //     string cur = dst[0];
    //     string start = cur;
    //     bool all = false;
    //     List<string> reverse = new List<string>();
    //     for (int i =0;i<src.Length;i++){
    //         if (!toFrom.ContainsKey(cur)){//then cur is start
    //             start = cur;
    //             break;
    //         }
    //         reverse.Add(cur);
    //         cur = toFrom[cur];
    //         all = i == src.Length-1;
    //     }
    //     if (all) {
    //         reverse.Reverse();
    //         return reverse;
    //     }
    //     List<string> nextSeg = new List<string>();
    //     cur = reverse[0];
        
    //     while (true) {
    //         if (!fromTo.ContainsKey(cur)) {
    //             nextSeg.Add(cur);
    //             int l = reverse.Count;
    //             List<string> fullRoute = new List<string>();
    //             for (int i=l-1;i>=0;i--){
    //                 fullRoute.Add(reverse[i]);
    //             }
    //             foreach(string str in nextSeg){
    //                 fullRoute.Add(str);

    //             }
    //             return fullRoute;
    //         }
    //         nextSeg.Add(cur);
    //         cur= fromTo[cur];
    //     }
    // }


    // public Dictionary<string, int> NumberOfEmployeePerBoss(Dictionary<string, string> empBoss) {
    //     Dictionary<string, int> result = new Dictionary<string, int>();
    //     Dictionary<string, List<string>> bossEmp = new Dictionary<string, List<string>>();
    //     foreach (var pair in empBoss) {
    //         if (!empBoss.ContainsKey(pair.ToString))
    //         {
    //             List<string> empOfThisBoss = new List<string>();
    //             empOfThisBoss.Add(pair.Key);
    //             bossEmp.Add(pair.Value);
    //         }
    //         else if (pair.Value!=pair.Key) {
    //             bossEmp[pair.Value].Add(pair.Key);
    //         }
    //     }
    //     foreach (var pair in bossEmp) {
    //         string boss = pair.Key;
    //         FindEmployeeForBoss(boss,bossEmp, result);
    //     }
    //     return result;

    // }

    // private int FindEmployeeForBoss(string boss, Dictionary<string,List<string>> bossEmp, Dictionary<string, int> result ){
    //     if (!bossEmp.ContainsKey(boss)){
    //         result.Add(boss, 0);
    //         return 0;
    //     }
    //     if (result.ContainsKey(boss))
    //     {
    //         return result[boss];
    //     }
    //     List<string> empList = new List<string>(bossEmp[boss]);
    //     int count = empList.Count;
    //     foreach (string emp in empList) {
    //         count+= FindEmployeeForBoss(emp, bossEmp, result);
    //     }
    //     result[boss]=count;
    //     return count;
    // }

    // public bool KDivisiblePair(int[] arr, int k){
    //     if (arr.Length%2==0) return false;
    //     //return true if arr can be divided into pairs such that sum of elements in each pair is divisible by k
    //     Dictionary<int, List<int>> possiblePair = new Dictionary<int, List<int>>();
    //     int countEdge = 0;
    //     for (int i =0;i<arr.Length-1;i++)
    //     {
    //         int key = arr[i];
    //         for (int j=i+1;j<arr.Length;j++)
    //         {
    //             int key = arr[i];
    //             int val = arr[j];
    //             CheckAndAdd<int, int>(key, val, possiblePair);
    //             CheckAndAdd<int, int>(val, key, possiblePair);
    //             countEdge++;//another edge
    //         }
    //         if (!possiblePair.ContainsKey(key))
    //             return false; //if no element can form a sum with one element:

    //     }
    //     //now we have an ajacency list. let's fuck off
    //     if (countEdge<arr.Length/2)//not enough pair
    //         return false;
    //     Dictionary<int, int> curMatching1 = new Dictionary<int, int>();
        



    // }
    // private bool FindIndependentPerfectMatching(Dictionary<int, int> curMatching, int node1, int node2, Dictionary<int, List<int>> possiblePair) {
    //     //return true if by adding an edge between node1 node2 to the matching will eventually lead to a solution
    //     curMatching.Add(node1, node2);
    //     curMatching.Add(node2, node1);
    //     if(curMatching.Count>= possiblePair.Count/2) return true;
    //     //TODO 5: Solve this shit (Hint: use perfect independent matching)
    // }




    // private void CheckAndAdd<T,K>(T key,K value, Dictionary<T, List<K>> dict) {//check if exist a key T. If yes
    //     if (dict.ContainsKey(key))
    //     {//if dict does contains key, means that someshit has been added to its list and no need to create a new shit
    //         dict[key].Add(value);
    //     }
    //     else {
    //         List<K> newList = new List<K>();
    //         newList.Add(value);
    //         dict.Add(key, newList);
    //     }
        
    // }


    public int LongestDivisibleSubarray(int[] arr, int k) {
        Dictionary<int, int> modi = new Dictionary<int, int>();
        int[] mod = new int [arr.Length];
        int cursum =0;
        for(int i=0;i<arr.Length;i++){
            cursum += arr[i];
            mod[i] = cursum%k;
        }
        int maxLength =0;
        for (int i=0;i<arr.Length;i++){
            if (mod[i]==0)
                maxLength=i;
            else if (!modi.ContainsKey(mod[i]))
            {
                modi.Add(mod[i], i);
            }
            else {
                if (i-modi[mod[i]]>maxLength)
                    maxLength = i-modi[mod[i]];
            }
        }
        return maxLength;
    }



}