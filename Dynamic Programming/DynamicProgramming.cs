using System;
using System.Collections.Generic;
using System.Numerics;
class DynamicProgramming
{

    public int CandyEqual(int[] arr)
    {
        int lv = 0;
        Queue<int[]> que = new Queue<int[]>();
        que.Enqueue(arr);
        que.Enqueue(null);
        while (true)
        {

            int[] cur = que.Dequeue();
            if (cur == null)
            {
                lv++;
                que.Enqueue(null);
            }
            else
            {
                List<int[]> possible = Possible(cur);
                foreach (int[] child in possible)
                {
                    if (CheckAllEqual(child))
                        return lv + 1;
                    que.Enqueue(child);
                }

            }
        }

    }

    private List<int[]> Possible(int[] arr)
    {//find all possible distribution
        List<int[]> all = new List<int[]>();
        int n = arr.Length;
        for (int i = 0; i < n; i++)
        {//arr[i] not distributed to

            int[] arr1 = (int[])arr.Clone();
            int[] arr2 = (int[])arr.Clone();
            int[] arr5 = (int[])arr.Clone();
            for (int j = 0; j < n; j++)
            {
                if (j != i)
                {
                    arr1[j] = arr[j] + 1;
                    arr2[j] = arr[j] + 2;
                    arr5[j] = arr[j] + 5;
                }
            }
            all.Add(arr1); all.Add(arr2); all.Add(arr5);
        }
        return all;
    }
    private bool CheckAllEqual(int[] arr)
    {
        int i = arr[0];
        foreach (int j in arr)
            if (j != i)
                return false;
        return true;
    }

    public int ParisLongestChain(List<int[]> arr)
    {
        //pass in an array of pair, sorted by the second element in each pair
        int[] dp = new int[arr.Count];//length of the longest chain that ends exactly at ith pair
        int[] max = new int[arr.Count];//lenght of the longest chain that starts and ends bw 0 and ith pair
        dp[0] = 1;
        for (int i = 1; i < arr.Count; i++)
        {
            max[i - 1] = dp[IndexOfMax(dp, 0, i - 1)];
            int pre = FindPredecessor(arr, i - 1, arr[i][0]);//start searching from i-1 to zero to find the first pair that has a second element smaller than this first element, the first one will be the most badass one
            dp[i] = 1 + max[pre];
        }
        return dp[IndexOfMax(dp, 0, dp.Length)];

    }
    private int FindPredecessor(List<int[]> arr, int endInd, int thisStart)
    {//so go from i-1 to zero and find the most badass pair
        for (int i = endInd; i >= 0; i--)
        {
            if (arr[i][1] < thisStart)
                return i;
        }
        return -1;

    }
    private int IndexOfMax(int[] arr, int start, int end)
    {
        int ind = start;
        for (int i = start + 1; i <= end; i++)
        {
            if (arr[i] > arr[ind])
                ind = i;
        }
        return ind;
    }
    // public int BalanceParenthese (string str){
    //     int sum =0;
    //     int curStart = 0;
    //     while (curStart<str.Length) {
    //         int[] breakAndEnd = FindBreakAndEnd(str, curStart);
    //         int open = 
    //     }

    // }
    // private int[] FindBreakAndEnd (string str, int start) {
    //     int cur = start;
    //     int[] arr = new int[2];
    //     while (true) {

    //         if (str[cur].Equals(")")||cur==str.Length-1)
    //         {
    //             arr[0] = cur;//first )
    //             break;
    //         }
    //         cur++;
    //     }
    //     while (true)
    //     {
    //         if (str.Length<=cur) {
    //             arr[1] = cur-1;
    //             return arr;
    //         }
    //         if (str[cur].Equals("("))//next session
    //         {
    //             arr[1] = cur;
    //             break;
    //         }
    //         cur++;
    //     }
    //     return arr;
    // }


    public int ShortestTime(List<int> processor, List<int> task)
    {
        task.Sort();
        Dictionary<int, List<int>> dict = new Dictionary<int, List<int>>();
        Dictionary<int, int> duplicate = new Dictionary<int, int>(); //key and duplicate
        foreach (int i in processor)
        {
            List<int> empty = new List<int>();
            if (!dict.ContainsKey(i))
                dict.Add(i, empty);
            else
            {
                if (!duplicate.ContainsKey(i))//first time dup
                    duplicate.Add(i, 2);
                else
                    duplicate[i] += 1;
            }

        }
        processor.Sort();
        int n = 4;
        int cur = task.Count - 1;
        foreach (int key in dict.Keys)
        {
            if (duplicate.ContainsKey(key))
                for (int i = 0; i < n * duplicate[key]; i++)
                    dict[key].Add(task[cur--]);
            else
            {//duplicate is not, first time
                for (int i = 0; i < n; i++)
                    dict[key].Add(task[cur--]);
            }
        }
        return CalculateMaxTime(dict, processor);


    }
    private int CalculateMaxTime(Dictionary<int, List<int>> dict, List<int> proc)
    {
        int maxSum = 0;
        foreach (int i in proc)
        {
            foreach (int j in dict[i])
            {
                int curSum = i + j;
                if (curSum > maxSum)
                {
                    maxSum = curSum;
                }
            }
        }
        return maxSum;
    }

    public int[] SubArrayWithMaxSum(int[] arr)
    {
        if (arr.Length==1){
            
        }

        int maxSeq = 0;
        bool AllNegative = true;
        int largestNegative = int.MinValue;
        foreach (int i in arr)
        {
            if (i >= 0)
            {
                AllNegative = false;
                maxSeq += i;
            }
            else
            {
                largestNegative = AllNegative && (i > largestNegative) ? i : largestNegative;
            }
        }
        
        if (!AllNegative)
        {       
            int n = arr.Length;
            int[] dp = new int[n];
            dp[0] = arr[0];
            int maxSub = int.MinValue;
            SubArrayUtil(arr, dp, n - 1, ref maxSub);
            return new int[2] {maxSub, maxSeq};
        }
        return new int[2]{largestNegative, largestNegative};
    }
    private int SubArrayUtil(int[] arr, int[] dp, int cur, ref int max)
    {//calculate and memorize dp[i]
        if (cur == 0) return arr[0];
        int prev = SubArrayUtil(arr, dp, cur - 1,ref max);
        int output = prev > 0 ? prev + arr[cur] : arr[cur];
        if (max < output) max = output;
        return output;
    }


    public long FibonacciModified(int n1, int n2, int term) {
        long[] dp = new long[term+1];
        Array.Fill(dp, int.MaxValue);
        dp[1]=n1;dp[2]=n2;
       
        return FibonacciModifiedUtil(dp, term);
    }
    private long FibonacciModifiedUtil(long[] dp, int cur){
        if (dp[cur]!=int.MaxValue) return dp[cur];
        dp[cur] = FibonacciModifiedUtil(dp, cur-2)+FibonacciModifiedUtil(dp, cur-1)*FibonacciModifiedUtil(dp, cur-1);
        return dp[cur];
    }


}