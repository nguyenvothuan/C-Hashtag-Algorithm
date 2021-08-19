using System;
using System.Collections.Generic;
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



}