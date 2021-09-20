using System;
using System.Collections.Generic;
using System.Collections.Specialized;
class Solution
{


    //1
    public int updateTimes(List<int> signalOne, List<int> signalTwo)
    {
        int len1 = signalOne.Count;
        int len2 = signalTwo.Count;
        int len = len1 > len2 ? len2 : len1;
        int maxequal = int.MinValue;
        int count = 0;
        for (int i = 0; i < len; i++)
        {
            if (signalOne[i] == signalTwo[i] && signalOne[i] > maxequal)
            {
                count++;
                maxequal = signalOne[i];
            }
        }
        return count;
    }

    //3
    public static int countHighlyProfitableMonths(List<int> stockPrices, int k)
    {
        int len = stockPrices.Count;
        int[] greaterThanPrev = new int[len];
        int count = 0;
        greaterThanPrev[0] = 1;
        for (int i = 1; i < len; i++)
        {
            if (stockPrices[i] > stockPrices[i - 1])
            {
                greaterThanPrev[i] = greaterThanPrev[i - 1] + 1;
                if (i == len - 1 && greaterThanPrev[i] >= k)
                {
                    count += greaterThanPrev[i] - k + 1;
                }
            }
            else
            {//end of the wave. if i==len - 1 here, arr[i] is 
                greaterThanPrev[i] = 1;
                if (greaterThanPrev[i - 1] >= k)
                {
                    count += stockPrices[i - 1] - k + 1;//calculate in the previous wave
                }
            }
        }
        return count;
    }


    //4
    public static List<int> getUnallottedUsers(List<List<int>> bids, int totalShares)
    {
        bids.Sort((x, y) => x[2].CompareTo(y[2]));
        int noBidder = bids.Count;
        for (int i = noBidder - 1; i >= 0;)
        {//total share always greater than 0
            int lastOne = findAllSamePrice(bids, i);
            if (lastOne == i) //no same price bidder
            {
                totalShares -= bids[i][1];
                if (totalShares <= 0)
                {
                    return calculateLeft(bids, i);
                }
                i--;
            }
            else
            {
                List<int> concludedInLoops = new List<int>();
                int used = handleEqualPrice(bids, i, lastOne, totalShares, concludedInLoops);
                if (used == 0)
                {//all is used and everyone there get shared
                    return calculateLeft(bids, lastOne);
                }
                else if (used == -1)
                {//some in loops doesn't get share
                    List<int> left = calculateLeft(bids, lastOne);
                    //join left with concludedinloops and return here!!!!!!!!!!
                    foreach (int j in concludedInLoops)
                    {
                        left.Add(j);
                        return left;
                    }

                }
                else
                {
                    //update total share
                    totalShares = used;
                    i = lastOne - 1;
                }
            }
        }
        return new List<int>();
    }

    private static List<int> calculateLeft(List<List<int>> bids, int index)
    {//will not include index
        if (index == 0) return new List<int>();//no shit, return empty
        List<int> newList = new List<int>();
        for (int i = 0; i < index; i++)
        {
            newList.Add(bids[i][0]);
        }
        newList.Sort();
        return newList;
        ;
    }
    private static int handleEqualPrice(List<List<int>> samePriceBidder, int start, int end, int curShare, List<int> emptylist)
    {//return 0 if all share is used and everyone in the loops get their share, -1 if some is left and modify the emptylist, return left other wise
        if (start - end + 1 > curShare)
        {
            int leftStart = start - curShare;
            for (int i = end; i <= leftStart; i++)
            {
                emptylist.Add(samePriceBidder[i][0]);

            }
            return -1;
        }

        for (int i = end; i < start; i++)
        {
            curShare -= samePriceBidder[i][1];
        }
        return curShare > 0 ? curShare : 0;

    }
    private static int findAllSamePrice(List<List<int>> bids, int index)
    {//return index of the last same price bidder. return it self if no such

        int cur = index;
        // int count =1;
        while (bids[index][2] == bids[cur][2])
        {
            if (cur == 0)
            {
                //can't go any further
                return 0;
            }
            cur--;
        }
        return cur + 1;
    }

    public List<int> DigitList(int n)
    {
        List<int> digits = new List<int>();
        int cur = n;
        while (cur > 9)
        {
            int newCur = (int)(cur / 10);
            int last = cur - newCur * 10;
            digits.Add(last);
            cur = newCur;
        }
        digits.Add(cur);

        return digits;
    }
    private int Sum(List<int> li)
    {
        int sum = 0;
        foreach (int i in li)
        {
            sum += i;
        }
        return sum;
    }
    private int Multiply(List<int> li)
    {
        int times = 1;
        foreach (int i in li)
        {
            if (i == 0)
                return 0;
            times *= i;
        }
        return times;
    }

    public int digitsManipulations(int n)
    {
        List<int> list = DigitList(n);
        int sum = Sum(list);
        int times = Multiply(list);
        return times - sum;
    }


    ///////////////////////////


    public int[][] sortByBeauty(int[][] numbers, int size)
    {
        List<List<int>> sortedSquare = Divide(numbers, size);
        int count = numbers.Length / size;
        return Conquer(sortedSquare, size, count);
    }
    public List<List<int>> Divide(int[][] numbers, int size)
    {
        List<List<int>> listOfSquare = new List<List<int>>();
        int count = numbers.Length / size;
        for (int i = 0; i < count; i++)
        {
            for (int j = 0; j < count; j++)
            {
                List<int> square = new List<int>();
                for (int y = 0; y < size; y++)
                {
                    for (int x = 0; x < size; x++)
                    {
                        square.Add(numbers[size * i + y][size * j + x]);
                    }
                }
                listOfSquare.Add(square);
            }
        }
        List<int> beauty = new List<int>();
        Dictionary<int, List<List<int>>> beautyToList = new Dictionary<int, List<List<int>>>();
        for (int i = 0; i < listOfSquare.Count; i++)
        {
            beauty.Add(FindBeauty(listOfSquare[i]));
            if (!beautyToList.ContainsKey(beauty[i]))
            {
                List<List<int>> sameOrder = new List<List<int>>();
                sameOrder.Add(listOfSquare[i]);
                beautyToList.Add(beauty[i], sameOrder);
            }
            else
            {
                beautyToList[beauty[i]].Add(listOfSquare[i]);
            }
        }
        var noDuplicate = replaceDuplicate(beauty);
        noDuplicate.Sort();
        List<List<int>> sortedSquares = new List<List<int>>();
        foreach (int i in noDuplicate)
        {
            foreach (List<int> list in beautyToList[i])
                sortedSquares.Add(list);
        }
        return sortedSquares;
    }

    public List<int> replaceDuplicate(List<int> beauty)
    {
        List<int> replaced = new List<int>();
        foreach (int i in beauty)
        {
            if (!replaced.Contains(i))
                replaced.Add(i);
        }
        return replaced;
    }
    public int FindBeauty(List<int> list)
    {
        list.Sort();
        if (list[0] != 1) return 1;
        for (int i = 1; i < list.Count; i++)
        {
            if (list[i] - list[i - 1] > 1)
                return list[i - 1] + 1;
        }
        return list[list.Count - 1] + 1;
    }

    public int[][] Conquer(List<List<int>> listOfSquare, int size, int count)
    {//count: number of square of size in each row or columns
        int[][] arr = new int[count * size][];
        for (int i = 0; i < count * size; i++)
        {
            arr[i] = new int[count * size];
        }
        for (int i = 0; i < size; i++)
        {//populate each row fully
            // List<int> curSquare = listOfSquare.Count/si
        }
        return new int[6][];
    }

    //////////////////Google kickstart F
    public int TrashBin(int[] arr)
    {
        int[] dist = new int[arr.Length];
        Array.Fill(dist, 99999999);
        for (int i = 0; i < arr.Length; i++)
        {
            List<int> calculating = new List<int>();
            TrashBinUtil(arr, ref dist, i, calculating);
        }
        int sum = 0;
        foreach (int i in dist)
        {
            sum += i;
        }
        return sum;
    }
    private int TrashBinUtil(int[] arr, ref int[] dp, int cur, List<int> calculating)
    {
        if (calculating.Contains(cur))
            return 99999999;
        calculating.Add(cur);
        if (dp[cur] != 99999999)
        {
            return dp[cur];
        }
        if (arr[cur] == 1)
        {
            dp[cur] = 0;
        }
        else if (cur == 0)
        {
            dp[cur] = TrashBinUtil(arr, ref dp, cur + 1, calculating) + 1;
            if (dp[cur] == 100000000)
                dp[cur] = 99999999;
        }
        else if (cur == arr.Length - 1)
        {
            dp[cur] = TrashBinUtil(arr, ref dp, cur - 1, calculating) + 1;
            if (dp[cur] == 100000000)
                dp[cur] = 99999999;
        }
        else
        {
            List<int> calculatingList = new List<int>();
            int left, right;
            left = TrashBinUtil(arr, ref dp, cur - 1, calculating);
            right = TrashBinUtil(arr, ref dp, cur + 1, calculating);
            if (left == right && left == 99999999)
                dp[cur] = 99999999;
            else
                dp[cur] = Math.Min(left, right) + 1;
        }
        return dp[cur];
    }
    // private int TrashBinHelper(int[] arr, int[] dp, int cur){

    // }


    public void Happiness(int day, int attraction, int k, int[] start, int[] end, int[] happiness)
    {
        List<List<int>> sum = new List<List<int>>(day);
        for (int i = 0; i < sum.Count; i++)
        {
            sum[i] = new List<int>();
        }
        for (int i = 0; i < attraction; i++)
        {
            int s = start[i]; int e = end[i];
            for (int j = s; j < e; i++)
            {
                sum[i].Add(happiness[j]);
            }
        }

    }

    public int Fib(int n)
    {
        int[] fib = new int[n + 1];
        FibUtil(n, fib);
        return fib[n];
    }
    public int FibUtil(int n, int[] fib)
    {
        if (n == 0) { fib[n] = 0; }
        if (n == 1) { fib[n] = 1; }
        if (n == 2) { fib[n] = 1; }
        else if (fib[n] != 0) { return fib[n]; }
        else { fib[n] = FibUtil(n - 1, fib) + FibUtil(n - 2, fib); }
        return fib[n];
    }

    public int DistributeCandies(int[] candyType)
    {
        int n = candyType.Length / 2;
        List<int> types = new List<int>();
        foreach (int i in candyType)
        {
            if (Contains(types, i))
            {
                BubbleSort(types, i);
            }
        }
        return n > types.Count ? types.Count : n;
    }

    public void BubbleSort(List<int> arr, int n)
    {
        int min = 0;
        int max = arr.Count - 1;
        while (min < max)
        {
            int mid = (min + max) / 2;
            if (arr[mid] == n) return;
            if (arr[mid] > n)
            {
                max = mid - 1;
            }
            else
            {
                min = mid + 1;
            }
        }
        if (arr[min] == n) return;
        if (min >= arr.Count - 1)
            arr.Add(n);
        else
            arr.Insert(min, n);
    }
    public bool Contains(List<int> inputArray, int key)
    {
        int min = 0;
        int max = inputArray.Count - 1;
        while (min <= max)
        {
            int mid = (min + max) / 2;
            if (key == inputArray[mid])
            {
                return true;
            }
            else if (key < inputArray[mid])
            {
                max = mid - 1;
            }
            else
            {
                min = mid + 1;
            }
        }
        return false;
    }

    public bool CanWinNim(int n)
    {
        int[] dp = new int[n + 1];
        Array.Fill(dp, 0);//0 not decided yet, -1 100% lose, 1 winnable
        dp[1] = 1;
        dp[2] = 1;
        dp[3] = 1;
        CanWinNimUtil(n,dp);
        return dp[n] == 1;

    }
    private int CanWinNimUtil(int n, int[] dp) {
        if (dp[n]!=0) return dp[n];
        dp[n-1] = CanWinNimUtil(n-1, dp);
        
        dp[n] = dp[n-1]==-1||dp[n-2]==-1||dp[n-3]==-1? 1: -1;
        return dp[n];
    }

    public int HammingDistance(int x, int y) {
        
        string n1 = Convert.ToString(Math.Min(x,y),2);
        string n2 = Convert.ToString(Math.Max(x,y),2);
        int l1 = n1.Length;
        int l2 = n2.Length;
        int count = 0;
        for (int i =0;i<l2;i++) {
            
            if (i<l2-l1) {
                if (n2[i]=='1') 
                    count++;
            }
            else {
                count += (n1[i-l2+l1]!=n2[i]?1:0);
            }
        }
        return count;
    }

}