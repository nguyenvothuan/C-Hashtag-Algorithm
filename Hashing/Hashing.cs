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
            return new int[];
        }
        if (x1 == x2 || y1 == y2)
        {
            return new int[];
        }
        float[] ab = new int[];

        ab[0] = (y1 - y2) / (x1 - x2);
        ab[1] = y1 - a * x1;
        return ab;
    }

    public int MaxPointsContainedOnALine(int[][] points)
    {
        Dictionary<float[], List<int[]>> lineAndPoints = new Dictionary<int[], List<int[]>>();
        int numPoint = points.Length;
        for (int i = 0; i < numPoint - 1; i++)
        {
            for (int j = i + 1; j < numPoints; j++)
            {
                int[] p1 = points[i];
                int[] p2 = points[j];

                float[] line = GetLineAB(p1[0], p1[1], p2[0], p2[1]);
                if (line.Length > 0)
                {
                    if (!lineAndPoints.ContainsKey(line))
                    {

                        lineAndPoints.Add(line);
                        lineAndPoints[line].Add(p1); 
                        lineAndPoints[line].Add(p2);
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
}