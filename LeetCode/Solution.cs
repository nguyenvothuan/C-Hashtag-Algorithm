using System;
using System.Collections.Generic;
class Solution
{
    public int ArrayGCD(int[] arr)
    {
        int min = arr[0];
        int max = arr[arr.Length];
        foreach (int i in arr)
        {
            if (i > max) max = i;
            if (i < min) min = i;
        }
        return GCD(min, max);

    }
    private int GCD(int n1, int n2)
    {
        if (n2 == 0)
        {
            return n1;
        }
        else
        {
            return GCD(n2, n1 % n2);
        }
    }

    public string LeftOverBinary(string[] nums)
    {//array of length n, element of length n
        int[] arr = new int[nums.Length];
        for (int i = 0; i < nums.Length; i++)
        {
            arr[i] = BinaryToDec(nums[i]);
        }
        char[] onemax = new char[nums.Length];
        for (int i = 0; i < onemax.Length; i++) onemax[i] = '1';


        int lim = Convert.ToInt32("11");
        Random rand = new Random();
        List<int> visited = new List<int>();
        while (true)
        {
            int cur = rand.Next(0, lim);
            if (!visited.Contains(cur))
            {
                if (!Contains(arr, cur))
                    return Convert.ToString(cur, 2);
            }
        }
    }
    private bool Contains(int[] arr, int k)
    {
        foreach (int i in arr)
            if (i == k)
                return true;
        return false;
    }
    private int BinaryToDec(string input)
    {
        char[] array = input.ToCharArray();
        // Reverse since 16-8-4-2-1 not 1-2-4-8-16. 
        Array.Reverse(array);
        /*
         * [0] = 1
         * [1] = 2
         * [2] = 4
         * etc
         */
        int sum = 0;

        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == '1')
            {
                // Method uses raising 2 to the power of the index. 
                if (i == 0)
                {
                    sum += 1;
                }
                else
                {
                    sum += (int)Math.Pow(2, i);
                }
            }

        }

        return sum;
    }

    private bool Contains(string[] arr, string key)
    {
        foreach (string str in arr)
        {
            if (str.Equals(key))
                return true;
        }
        return false;
    }

    public string FindDifferentBinaryString(string[] nums)
    {
        int n = nums.Length;
        List<string> visited = new List<string>();
        Random rand  = new Random();
        while (true) 
        {
            char[] temp = new char[n];
            for (int i =0;i<n;i++) {
                temp[i] = Convert.ToChar(rand.Next(2));
            }
            string cur = new string(temp);
            if (!visited.Contains(cur)){
                if (!Contains(nums, cur))
                    return cur;
            }
        }
        
    }



    public int MinimumDifference(int[] nums, int k) {
        if (nums.Length==1) return 0;
        
        List<int> list = new List<int>(nums);
        list.Sort();
        int minDist = 999999;
        for(int i=0;i<nums.Length-k+1;i++)
        {
            int start = list[i];
            int end = list[i+k-1];
            if (end-start<minDist)
                minDist=end-start;
        }
        return minDist; 
    }
    public string KthLargestNum(string[] nums, int k) {
        long[] arr = new long[nums.Length];
        for (int i =0;i<arr.Length;i++){
            arr[i] = Int64.Parse(nums[i]);
        }
        List<long> newarr = new List<long>(arr);
        newarr.Sort();
        return Convert.ToString(newarr[newarr.Count-k]);
    }
    public string KLargestNum(string[] nums, int k ){
        Dictionary<int, List<string>> dict = new Dictionary<int, List<string>>();
        foreach(string str in nums ){
            int len = str.Length;
            if (!dict.ContainsKey(len))
            {
                List<string> list = new List<string>();
                list.Add(str);
                dict.Add(len, list);
            }
            else {
                dict[len].Add(str);
            }
        }
        int curSum =0;
        int match = -1;
        int leftOver = -1;
        foreach (int key in dict.Keys)
        {
            int cur = dict[key].Count;
            curSum += cur;
            if (curSum>=k)
            {
                match = key;
                leftOver = match-key;
                break;
            }
        }
        //dict[match] now contains the k
        //find the dict[match][leftOver] after sort


    }
    private string KthLargestSameLength(List<string> nums, int leftOver ){
        if (nums[0].Length==1)
        {
            return nums[leftOver];
        }
        Dictionary<int, List<string>> dict  = new Dictionary<int, List<string>>();
        foreach (string num in nums)
        {
            int len = num.Length;
            if (!dict.ContainsKey(len))
            {
                List<string> newl = new List<string>();
                newl.Add(num);
                dict.Add(len, newl);
            }
            else {
                dict[len].Add(num);
            }

        }
        



    }
}