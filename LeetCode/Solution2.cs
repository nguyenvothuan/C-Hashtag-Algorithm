using System;
using System.Collections.Generic;
using System.Text;
class Solution2
{
    public int MinDistance(string w1, string w2)
    {
        int n = w1.Length, m = w2.Length;
        int[,] dp = new int[n, m]; // dp[i,j] lcs that ends at i of w1 and j of w2
        for (int i = 0; i < n; i++)
            for (int j = 0; j < m; j++)
                dp[i, j] = -1;
        int LCS(int i, int j)
        {
            if (i < 0 || j < 0) return 0;
            if (dp[i, j] == -1)
            {
                if (w1[i] == w2[j]) dp[i, j] = 1 + LCS(i - 1, j - 1);
                else dp[i, j] = Math.Max(LCS(i - 1, j), LCS(i, j - 1));
            }
            return dp[i, j];
        }
        int lcs = LCS(n - 1, m - 1);
        return m + n - 2 * lcs;
    }
    public int OddEvenJumps(int[] arr)
    {
        int n = arr.Length;
        int[] odd = new int[n], even = new int[n];
        int[] oddNext = new int[n], evenNext = new int[n]; //next move if we are at i and next move is either odd or even numbered.
        Array.Fill(evenNext, -1); Array.Fill(oddNext, -1);
        //so let's fill oddNext first: the next closest larger number's index
        Stack<int> stack = new Stack<int>();
        for (int i = 0; i < n; i++)
        {
            if (stack.Count == 0) stack.Push(i);
            else
            {
                while (stack.Count != 0 && arr[stack.Peek()] <= arr[i]) stack.Pop();
                stack.Push(i);
            }
        }
        while (stack.Count != 0)
        {
            int last = stack.Pop();
            if (stack.Count == 0)
            {
                for (int i = 0; i < last; i++) oddNext[i] = last;
                oddNext[last] = -1;
            }
            else
            {
                for (int i = stack.Peek() + 1; i < last; i++) oddNext[i] = last;
                oddNext[last] = -1;
            }
        }
        stack.Clear();
        for (int i = 0; i < n; i++)
        {
            if (stack.Count == 0) stack.Push(i);
            else
            {
                while (stack.Count != 0 && arr[stack.Peek()] >= arr[i]) stack.Pop();
                stack.Push(i);
            }
        }
        while (stack.Count != 0)
        {
            int last = stack.Pop();
            if (stack.Count == 0)
            {
                for (int i = 0; i < last; i++) evenNext[i] = last;
                evenNext[last] = -1;
            }
            else
            {
                for (int i = stack.Peek() + 1; i < last; i++) evenNext[i] = last;
                evenNext[last] = -1;
            }
        }

        bool Odd(int i)
        {
            if (i == n - 1) return true;
            if (i < 0 || i >= n) return false;
            if (odd[i] == 0)
            {
                if (oddNext[i] == i || oddNext[i] == -1) odd[i] = -1; //can't move
                else odd[i] = Even(oddNext[i]) ? 1 : -1;
            }
            return odd[i] == 1;
        }
        bool Even(int i)
        {
            if (i == n - 1) return true;
            if (i < 0 || i >= n) return false;
            if (even[i] == 0)
            {
                if (evenNext[i] == i || evenNext[i] == -1) even[i] = -1;//can't move
                else even[i] = Odd(evenNext[i]) ? 1 : -1;
            }
            return even[i] == 1;
        }
        int count = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            if (Odd(i)) count++;
        }
        return count;
    }

    public int SubArrayRanges(int[] nums)
    {
        //TODO: https://leetcode.com/problems/sum-of-subarray-ranges/discuss/1624222/JavaC%2B%2BPython-O(n)-solution-detailed-explanation
        return 1;
    }
    public int[] CanSeePersonsCount(int[] heights) {
        //TODO: https://leetcode.com/problems/number-of-visible-people-in-a-queue/
    }
    public int[] ExclusiveTime(int n, IList<string> logs)
    {
        int used = 0;
        Dictionary<int, int> res = new Dictionary<int, int>();
        int[] split(string s)
        {
            var res = s.Split(':');
            return new int[]{int.Parse(res[0]), res[1] == "start" ? 0 : 1, int.Parse(res[2])};
        }
        Stack<int[]> stack = new Stack<int[]>();
        foreach (string log in logs)
        {
            var cur = split(log);
            if (cur[1] == 0) stack.Push(cur);
            else
            {
                int id = cur[0];
                int dis = cur[2] - stack.Peek()[2] - used+1;
                if (!res.TryAdd(id, dis)) res[id]+=dis;
                used += cur[2]-stack.Pop()[2]+1;
            }
        }
        int[] ans = new int[res.Count];
        foreach(var key in res.Keys){
            ans[key] = res[key];
        }
        return ans;
    }

    public string DecodeString(string s) {
        Stack<string> stack = new Stack<string>();
        int curNum = 0;
        StringBuilder buffer = new StringBuilder();
        foreach(char chr in s) {
            if (chr == '[') 
            {
                stack.Push()
            }
        }
    }
}