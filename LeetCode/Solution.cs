using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Text;
class Solution
{
    Tool<int> tool = new Tool<int>();

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
        CanWinNimUtil(n, dp);
        return dp[n] == 1;

    }
    private int CanWinNimUtil(int n, int[] dp)
    {
        if (dp[n] != 0) return dp[n];
        dp[n - 1] = CanWinNimUtil(n - 1, dp);

        dp[n] = dp[n - 1] == -1 || dp[n - 2] == -1 || dp[n - 3] == -1 ? 1 : -1;
        return dp[n];
    }

    public int HammingDistance(int x, int y)
    {

        string n1 = Convert.ToString(Math.Min(x, y), 2);
        string n2 = Convert.ToString(Math.Max(x, y), 2);
        int l1 = n1.Length;
        int l2 = n2.Length;
        int count = 0;
        for (int i = 0; i < l2; i++)
        {

            if (i < l2 - l1)
            {
                if (n2[i] == '1')
                    count++;
            }
            else
            {
                count += (n1[i - l2 + l1] != n2[i] ? 1 : 0);
            }
        }
        return count;
    }
    public bool IsRectangleOverlap(int[] rec1, int[] rec2)
    {
        int x1 = rec1[0]; int y1 = rec1[1]; int x2 = rec1[2]; int y2 = rec1[3];
        int a1 = rec2[0]; int b1 = rec2[1]; int a2 = rec2[2]; int b2 = rec2[3];
        if (x2 <= a1 || a2 <= x1) return false;
        if (y2 <= b1 || b2 <= y1) return false;
        return true;
    }

    public IList<int> PartitionLabels(string s)
    {

        Dictionary<char, int[]> dict = new Dictionary<char, int[]>();//key: [start, end]
        for (int i = 0; i < s.Length; i++)
        {
            if (dict.ContainsKey(s[i]))
            {
                dict[s[i]][1] = i;
            }
            else
            {
                int[] startend = { i, i };
                dict.Add(s[i], startend);
            }
        }
        int cur = 0;
        int start = dict[s[cur]][0];
        int end = dict[s[cur]][1];
        // int count=0;
        List<int> record = new List<int>();
        while (true)
        {
            if (dict[s[cur]][1] > end)
            {
                end = dict[s[cur]][1];
                cur++;
                if (end == s.Length - 1)
                {
                    record.Add(end - start + 1);
                    break;
                }
            }
            else if (cur == end)
            {
                record.Add(end - start + 1);
                cur = cur + 1;
                start = dict[s[cur]][0];
                end = dict[s[cur]][1];
                if (end == s.Length - 1)
                {
                    record.Add(end - start);
                    break;
                }
            }
            else
            {
                cur++;
            }
        }
        return record;
    }

    public static int balancedSum(List<int> sales)
    {
        int sum = 0;
        foreach (int i in sales) sum += i;
        int curSum = 0;
        for (int i = 0; i < sales.Count; i++)
        {
            if (sum - curSum - sales[i] == curSum)
            {
                return i;
            }
            curSum += sales[i];
        }
        return -1;
    }

    public int fountainActivation(List<int> a)
    {
        List<int[]> coverage = new List<int[]>(a.Count);
        Dictionary<int, int[]> startIend = new Dictionary<int, int[]>();
        List<int> startTime = new List<int>();
        for (int i = 0; i < coverage.Count; i++)
        {
            coverage[i] = calculateCoverage(i, a[i], a.Count);
            int start = coverage[i][0];
            startTime.Add(start);
            if (startIend.ContainsKey(start))
            {
                int lastEnd = startIend[start][1];
                if (coverage[i][1] > lastEnd)
                {
                    int[] Iend = { i, coverage[i][1] };
                    startIend[i] = Iend;
                }
            }
            else
            {
                int[] Iend = { i, coverage[i][1] };
                startIend.Add(start, Iend);
            }
        }
        startTime = replaceDuplicate(startTime);
        startTime.Sort();
        int cur = 0;
        int count = 0;
        int next = -1;
        while (true)
        {
            if (startIend.ContainsKey(cur))
                next = startIend[cur][1];
            else
            {
                for (int i = cur - 1; i > 0; i--)
                {
                    if (startIend.ContainsKey(i))
                    {
                        next = startIend[i][1];
                    }
                }
            }
            ++count;
            if (next == a.Count - 1)
                break;
            cur = next + 1;
        }



        return count;
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

    public int[] calculateCoverage(int i, int ai, int len)
    {
        int[] leftright = new int[2];
        leftright[0] = Math.Max(i - ai, 0);
        leftright[1] = Math.Min(i + ai, len - 1);
        return leftright;
    }

    public int Reverse(int x)
    {
        if (x > Math.Pow(2, 31) - 1 || x < -Math.Pow(2, 31))
            return 0;
        if (x > 0)
        {
            long cur = x;
            long newX = 0;
            while (cur > 9)
            {
                long rightOfCur = cur % 10;
                newX = 10 * newX + rightOfCur;
                if (newX > Math.Pow(2, 29) - 1 || newX < -Math.Pow(2, 29))
                    return 0;
                cur = (cur - rightOfCur) / 10;
            }
            newX = newX * 10 + cur;
            return (int)newX;
        }
        else
        {
            x = -x;
            long cur = x;
            long newX = 0;
            while (cur > 9)
            {
                long rightOfCur = cur % 10;
                newX = 10 * newX + rightOfCur;
                if (newX > Math.Pow(2, 31) - 1 || newX < -Math.Pow(2, 31))
                    return 0;
                cur = (cur - rightOfCur) / 10;
            }
            newX = newX * 10 + cur;
            return -(int)newX;
        }
    }

    public int ArrayPairSum(int[] nums)
    {
        List<int> list = new List<int>(nums);
        list.Sort();
        int sum = 0;
        for (int i = 0; i < nums.Length - 1; i += 2)
        {
            sum += list[i];
        }
        return sum;
    }

    public string ReverseStr(string s, int k)
    {
        int left = s.Length;
        int cur = 0;
        char[] newString = new char[s.Length];
        while (left > 2 * k)
        {
            //reverse from cur to cur+k-1
            for (int i = 0; i < k; i++)
            {
                newString[cur + i] = s[cur + k - 1 - i];
            }
            left = left - 2 * k;
            cur = cur + 2 * k;
        }
        //now that left is less than 2k
        if (left >= k)
        {
            for (int i = 0; i < s.Length - cur; i++)
            {
                newString[cur + i] = s[s.Length - i - 1];
            }
        }
        return new string(newString);
    }
    //K-Difference
    public int kDifference(List<int> a, int k)
    {
        if (a.Count == 2) return Math.Abs(a[0] - a[1]) == k ? 1 : 0;
        a.Sort();
        int count = 0;
        for (int i = 0; i < a.Count - 1; i++)
        {
            for (int j = i + 1; j < a.Count; j++)
            {
                if (Math.Abs(a[i] - a[j]) == k)
                {
                    count++;
                    if (j == a.Count - 1 || a[j] != a[j + 1])
                        break;
                }
            }
        }
        return count;
    }




    public static string canReach(int x1, int y1, int x2, int y2)
    {
        return canReachUtil(x1, y1, x2, y2) ? "Yes" : "No";
    }
    private static bool canReachUtil(int x1, int y1, int x2, int y2)
    {
        if (x1 > x2 || y1 > y2) return false;
        if (x1 == x2 && y1 == y2) return true;
        return canReachUtil(x1 + y1, y1, x2, y2) || canReachUtil(x1, x1 + y1, x2, y2);
    }

    public string FractionToDecimal(int numerator, int denominator)
    {
        double n = (double)numerator / denominator;
        string str = n.ToString();
        if (str.Length < 10) return str;
        for (int i = 3; i < Math.Sqrt(str.Length); i++)
        {
            if (IsRepeat(str, 2, i, 0))
            {
                char[] rep = new char[i + 2];
                rep[0] = str[0];
                rep[1] = '.';
                rep[2] = '(';
                for (int j = 3; j <= i; j++)
                {
                    rep[j] = str[j - 1];
                }
                rep[i + 1] = ')';
                return new string(rep);
            }
        }
        return str;
    }
    private bool IsRepeat(string str, int s, int e, int count)
    {//check if the sequence from s to e (0 based) is repeated
        //s start at 2, srting at least 10 chars long
        if (count > 10) return true;
        int length = e - s + 1;
        for (int i = 0; i < length; i++)
        {
            if (str[s] != str[s + length])
            {
                return false;
            }
        }
        if (e + length > str.Length) return true;
        return IsRepeat(str, e + 1, e + length, count + 1);

    }

    public IList<int> FindDuplicates(int[] nums)
    {//I found this solution on leetcode, absolutely genius to use the index as an indicator for duplicate
     //so the idea is that when A[i] = k is seen or the first time, go to A[k] and set it to negative to mark that k is seen once. 
     //next time we see A[j] = k, now we know that the idicator for k is negative it has been seen, => add
        List<int> duplicates = new List<int>();
        for (int i = 0; i < nums.Length; i++)
        {
            int cur = Math.Abs(nums[i]);
            if (nums[cur - 1] < 0)
            {
                duplicates.Add(cur);
            }
            else
            {
                nums[cur - 1] = -nums[cur - 1];
            }
        }
        return duplicates;

    }

    public int MaxSubArray(int[] nums)
    {
        int[] max = (int[])nums.Clone();
        int soFar = max[0];

        for (int i = 1; i < nums.Length; i++)
        {
            max[i] = max[i - 1] > 0 ? max[i - 1] + max[i] : max[i];
            if (max[i] > soFar) soFar = max[i];
        }
        return soFar;
    }

    public int Jump(int[] num)
    {
        //https://leetcode.com/problems/jump-game-ii/
        int[] dp = new int[num.Length];//dp[i] be the number of minimum jumps possible at index i from 0 ->n-1
        Array.Fill(dp, 999999);
        JumpUtil(dp, num, 0);
        return dp[0];
    }
    private int JumpUtil(int[] dp, int[] nums, int cur)
    {//calculate the minimum jump needed at index cur
        if (dp[cur] != 999999) return dp[cur];//calculated
        if (cur >= dp.Length - 1)
        {
            dp[cur] = 0;
            return 0;
        }
        if (nums[cur] == 0)
        {
            dp[cur] = int.MaxValue;
            return int.MaxValue;
        }
        int min = int.MaxValue;
        for (int i = 1; i <= nums[cur]; i++)
        {
            int curMin = 1 + JumpUtil(dp, nums, cur + i);
            if (min > curMin)
            {
                min = curMin;
            }
        }
        dp[cur] = min;
        return dp[cur];
    }

    int guess(int num)
    {
        int a = 9;
        if (num == a) return 0;
        if (num < a) return -1;
        return 1;
    }

    public int GuessNumber(int n)
    {
        int left = 1; int right = n;
        while (left <= right)
        {
            int mid = (left + right) / 2;
            if (guess(mid) == 0) return mid;
            else if (guess(mid) == -1)
            {
                right = mid - 1;
            }
            else
            {
                left = mid + 1;
            }
        }
        return left;
    }

    public string Reverse(string str)
    {
        Stack<char> chars = new Stack<char>();
        foreach (char i in str)
        {
            chars.Push(i);
        }
        return new string(chars.ToArray());
    }

    public int MaxArea(int[] height)
    {
        int i = 0, j = height.Length - 1;
        int water = 0;
        while (i < j)
        {
            water = Math.Max(water, Area(i, j, height));
            if (height[i] < height[j])
            {
                i++;
            }
            else
            {
                j--;
            }

        }
        return water;
    }
    private int Area(int a, int b, int[] height)
    {
        return Math.Abs(a - b) * Math.Min(height[a], height[b]);
    }

    public List<int> getMinimumDifference(List<string> a, List<string> b)
    {
        List<int> res = new List<int>();
        for (int i = 0; i < a.Count; i++)
        {
            res.Add(getDifference(a[i], b[i]));
        }
        return res;
    }
    public int getDifference(string a, string b)
    {
        if (a.Length != b.Length) return -1;
        Dictionary<char, int> dict1 = new Dictionary<char, int>();
        Dictionary<char, int> dict2 = new Dictionary<char, int>();
        for (int i = 0; i < a.Length; i++)
        {
            if (dict1.ContainsKey(a[i]))
            {
                dict1[a[i]]++;
            }
            else
            {
                dict1.Add(a[i], 1);
            }
            if (dict2.ContainsKey(b[i]))
            {
                dict2[b[i]]++;
            }
            else
            {
                dict2.Add(b[i], 1);
            }
        }
        int count = 0;
        foreach (char chr in dict1.Keys)
        {
            if (dict2.ContainsKey(chr))
                count += Math.Abs(dict1[chr] - dict2[chr]);
            else
                count += dict1[chr];
        }
        foreach (char chr in dict2.Keys)
        {
            if (!dict1.ContainsKey(chr))
                count += dict2[chr];
        }
        return count / 2;
    }

    public bool isPossible(List<int> calCounts, int requiredCals)
    {
        return isPossibleUtil(calCounts, requiredCals, 0);
    }
    private bool isPossibleUtil(List<int> calCounts, int sum, int curInd)
    {
        // if it is possible to finish at day curIndex, whehter to eat or not
        if (sum == 0) return true;
        if (curInd >= calCounts.Count || sum < 0) return false;
        return isPossibleUtil(calCounts, sum - calCounts[curInd], curInd + 1) || isPossibleUtil(calCounts, sum, curInd + 1);
    }

    public int CanCompleteCircuit(int[] gas, int[] cost)
    {
        //https://leetcode.com/problems/gas-station/
        int count = 0;
        for (int i = 0; i < gas.Length; i++)
        {
            count += gas[i] - cost[i];
        }
        if (count < 0) return -1;
        int acc = 0;
        int cur = 0;
        for (int i = 0; i < gas.Length; i++)
        {
            int gain = gas[i] - cost[i];
            if (acc + gain < 0)
            {
                cur = i + 1;
                acc = 0;
            }
            else
            {
                acc += gain;
            }
        }
        return cur;
    }

    public bool labyrinthEscape(int n, int m, int[][] obstables, int[][] teleports)
    {//obs[i]: obs at (obs[i][0],obs[i][1]).
     //tel[i]: from tel[i][0], tel[i][1] to tel[i][2], tel[i][3]
        int[,][] board = new int[n, m][];

        foreach (var cor in obstables)
        {
            if (cor[0] < n && cor[1] < m)
            { board[cor[0], cor[1]] = new int[2] { -1, -1 }; }
        }
        foreach (var cor in teleports)
        {
            if (cor[0] < n && cor[1] < m)
            { board[cor[0], cor[1]] = new int[2] { cor[2], cor[3] }; }
        }
        int i = 0; int j = 0; //to n-1 and m-1
        while (true)
        {
            if (i == n - 1 && j == m - 1) return true;
            if (j >= m)
            {
                j = 0;
                i++;
            }
            if (board[i, j] == null) //free 
                j++;
            else if (board[i, j][0] == -1)
                return false;
            else
            {
                int a = board[i, j][0]; int b = board[i, j][1];
                i = a;
                j = b;
                if (j >= m || i >= n) return false;
            }
        }
    }
    public int solution(int[] arr)
    {
        int[] digitSum = new int[arr.Length];
        for (int i = 0; i < arr.Length; i++)
        {
            digitSum[i] = DigitSum(arr[i]);
        }
        Dictionary<int, List<int>> dict = new Dictionary<int, List<int>>();
        for (int i = 0; i < arr.Length; i++)
        {
            if (dict.ContainsKey(digitSum[i]))
            {
                dict[digitSum[i]].Add(arr[i]);
                dict[digitSum[i]].Sort();
            }
            else
            {
                var newList = new List<int>();
                newList.Add(arr[i]);
                dict.Add(digitSum[i], newList);
            }
        }
        int max = -1;
        foreach (int key in dict.Keys)
        {
            if (dict[key].Count > 1)
            {
                int len = dict[key].Count;
                int temp = dict[key][len - 1] + dict[key][len - 2];
                if (max < temp)
                {
                    max = temp;
                }
            }
        }
        return max;
    }
    public int DigitSum(int n)
    {
        int sum = 0;
        while (n != 0)
        {
            sum += n % 10;
            n /= 10;
        }
        return sum;
    }

    public int CountDuplicateForX(int[] arr)
    {
        Dictionary<int, int> dict = new Dictionary<int, int>();
        foreach (int i in arr)
        {
            if (dict.ContainsKey(i))
            {
                dict[i]++;
            }
            else
            {
                dict.Add(i, 1);
            }
        }
        int max = 0;
        foreach (var pair in dict)
        {
            if (pair.Key == pair.Value)
            {
                if (pair.Key > max)
                    max = pair.Key;
            }
        }

        return max;

    }

    public int evenSubarray(List<int> numbers, int k)
    {
        if (k == 0) return 0;
        bool[] IsOdd = new bool[numbers.Count];
        foreach (int i in numbers)
        {
            IsOdd[i] = numbers[i] % 2 != 0;
        }
        int count = 0;
        for (int i = 0; i < numbers.Count; i++)
        {
            int odd = 0;
            for (int j = i; j < numbers.Count; j++)
            {
                if (IsOdd[j])
                {
                    odd++;
                    if (odd > k)
                    {
                        break;
                    }
                }
                count++;
            }
        }
        return count;
    }
    public int ClimbStairs(int n)
    {
        if (n <= 3) return n;
        int[] db = new int[n + 1];
        db[n - 1] = 1;
        db[n - 2] = 2;
        return ClimbUtil(0, db);
    }
    int ClimbUtil(int i, int[] db)
    {
        if (db[i] == 0)
            db[i] = ClimbUtil(i + 1, db) + ClimbUtil(i + 2, db);
        return db[i];
    }
    public int NumSquares(int n)
    {

        List<int> square = PerfectSquareToN(n);
        int[] db = new int[n + 1];
        Array.Fill(db, -1); //-1 is not found,0 is non-composable, >0 means yes
        foreach (int i in square)
        {
            db[i] = 1;
        }
        //TODO: fix this shit
        return 1;
    }
    int NumSquaresUtil(int n, List<int> squares, int[] db)
    {
        if (n < 0) return -9999;
        if (IsSquare(n)) return 1;
        if (db[n] == -1)
        {
            int cur = 1;
            int min = int.MaxValue;
            while (cur * cur < n / 2)
            {
                int square = cur * cur;
                int temp = NumSquaresUtil(n - square, squares, db);
                if (temp != -9999)
                {
                    min = Math.Min(temp + 1, min);
                }
                cur++;
            }
        }
        return db[n];
    }
    bool IsSquare(int n)
    {
        int temp = (int)Math.Sqrt(n);
        return temp * temp == n;
    }
    List<int> PerfectSquareToN(int n)
    {
        List<int> list = new List<int>();
        int cur = 1;
        while (cur * cur <= n)
        {
            list.Add(cur * cur);
            cur++;
        }
        return list;
    }
    public string ReverseWords(string s)
    {
        int cur = 0;

        Stack<string> str = new Stack<string>();
        List<char> chr = new List<char>();
        while (cur < s.Length)
        {
            if (s[cur] != ' ')
            {
                chr.Add(s[cur]);
                if (cur == s.Length - 1)
                    str.Push(new string(chr.ToArray()));
            }
            else
            {
                if (chr.Count > 0)
                {
                    string word = new string(chr.ToArray());
                    str.Push(word);
                    chr.Clear();
                }
            }
            cur++;
        }
        StringBuilder buffer = new StringBuilder();
        while (str.Count != 0)
        {
            buffer.Append(str.Pop());
            if (str.Count != 0)
            {
                buffer.Append(" ");
            }
        }
        return buffer.ToString();
    }

    public double FindMedianSortedArrays(int[] nums1, int[] nums2)
    {
        if (nums1 == null || nums2 == null || nums1.Length == 0 || nums2.Length == 0) return 0;
        int m = nums1.Length; int n = nums2.Length;
        int l = (m + n + 1) / 2;
        int r = (m + n + 2) / 2;
        //if m+n is odd, then l and r are the same, if m+n is even, r - l=1;
        return (getKth(nums1, 0, nums2, 0, l) + getKth(nums1, 0, nums2, 0, r)) / 2;
    }

    double getKth(int[] nums1, int start1, int[] nums2, int start2, int k)
    {
        //return the kth elements in nums1 + nums2
        if (start1 > nums1.Length - 1) return nums2[start2 + k - 1];
        if (start2 > nums2.Length - 1) return nums1[start1 + k - 1];
        if (k == 1) return Math.Min(nums1[start1], nums2[start2]);
        int mid1 = int.MaxValue;
        int mid2 = int.MaxValue;
        if (start1 + k / 2 - 1 < nums1.Length) mid1 = nums1[start1 + k / 2 - 1];
        if (start2 + k / 2 - 1 < nums2.Length) mid2 = nums2[start2 + k / 2 - 1];
        if (mid1 < mid2)
        {
            //take half right of nums1 and half left of nums2
            return getKth(nums1, start1 + k / 2, nums2, start2, k - k / 2);
        }
        else
        {
            return getKth(nums1, start1, nums2, start2 + k / 2, k - k / 2);
        }

    }
    public int MissingNumber(int[] nums)
    {
        int xor = 0;
        int i = 0;
        for (i = 0; i < nums.Length; i++)
        {
            xor = xor ^ i ^ nums[i];
        }
        return xor ^ i;
    }

    public int RomanToInt(string s)
    {//in range 3999
        Dictionary<char, int> dict = new Dictionary<char, int>();
        dict.Add('I', 1);
        dict.Add('V', 5);
        dict.Add('X', 10);
        dict.Add('L', 50);
        dict.Add('C', 100);
        dict.Add('D', 500);
        dict.Add('M', 1000);
        int cur = 0;
        int cum = 0;
        while (cur < s.Length)
        {
            //handle substraction
            if (HandleSubstraction(s, cur, ref cum, dict))
                cur += 2;
            else
            {
                cum += dict[s[cur]]; cur++;
            }
        }
        return cum;
    }
    bool HandleSubstraction(string s, int cur, ref int cum, Dictionary<char, int> dict)
    {
        //check cur and cur + 1 is substraction. if yes then substract 
        int before = cum;
        if (cur == s.Length - 1 || s[cur] == 'M') return false;
        if (s[cur] == 'C' && s[cur + 1] == 'D') cum += 400;
        if (s[cur] == 'C' && s[cur + 1] == 'M') cum += 900;
        if (s[cur] == 'X' && s[cur + 1] == 'L') cum += 40;
        if (s[cur] == 'X' && s[cur + 1] == 'C') cum += 90;
        if (s[cur] == 'I' && s[cur + 1] == 'V') cum += 4;
        if (s[cur] == 'I' && s[cur + 1] == 'X') cum += 9;
        return before != cum;
    }

    public int[] TwoSum(int[] nums, int target)
    {
        int[] order = new int[nums.Length];
        for (int i = 0; i < order.Length; i++) order[i] = i;
        tool.QuickSort(nums, order, 0, order.Length - 1);
        List<int> sorted = new List<int>(nums);
        CompareInt c = new CompareInt();
        for (int i = 0; i < nums.Length - 1; i++)
        {
            int cur = target - nums[i];
            int indexOfSearch = sorted.BinarySearch(cur);
            if (indexOfSearch > 0)
            {
                int[] result = new int[2];
                result[0] = order[indexOfSearch];
                result[1] = order[i];
                return result;
            }
        }
        return new int[0];
    }

    public int RemoveOneDigit(string s, string t)
    {
        //now that t!= s and both of them not zero
        StringBuilder S = new StringBuilder(s);
        StringBuilder T = new StringBuilder(t);
        int count = 0;
        for (int i = 0; i < s.Length; i++)
        {
            S.Remove(i, 1);
            string removed = S.ToString();
            if (String.Compare(removed, t) < 0)
            {//a to b. -1 if b<a, 1 if a>b
                count++;
            };
            S = new StringBuilder(s);
        }
        for (int i = 0; i < t.Length; i++)
        {
            T.Remove(i, 1);
            if (String.Compare(T.ToString(), s) > 0)
            {
                count++;
            };
            T = new StringBuilder(t);
        }
        return count;
    }

    int findFullLine(int[][] field, int[][] figure)
    {
        int colCount = field[0].Length;
        int rowCount = field.Length;
        //so it be from 0 to colCount -3;
        for (int i = 0; i < colCount - 2; i++)
        {
            if (IsPossible(field, figure, i))
                return i;
        }
        return -1;
    }

    bool IsPossible(int[][] field, int[][] figure, int cur)
    {
        int height = field.Length;
        int curRow = 0;
        for (int i = 0; i < height - 3; i++)
        {
            for (int a = 0; a < 3; a++)
            {
                for (int b = 0; b < 3; b++)
                {
                    if (field[curRow + a][cur + b] == figure[a][b] && figure[a][b] == 1)
                        return false;

                }
            }
        }
        return true;
    }

    public int WiggleMaxLength(int[] nums)
    {
        int n = nums.Length;
        if (n == 1) return n;
        if (n == 2) return nums[0] == nums[1] ? 1 : 2;
        int[] up = new int[n];
        int[] down = new int[n];
        up[n - 1] = down[n - 1] = 1;
        up[n - 2] = nums[n - 2] < nums[n - 1] ? 2 : 1;
        down[n - 2] = 3 - up[n - 2];
        CalUp(nums, up, down, 0);
        CalDown(nums, up, down, 0);
        int max = int.MinValue;
        foreach (int i in up)
        {
            if (i > max) max = i;
        }
        foreach (int i in down)
        {
            if (i > max) max = i;
        }
        return max;
    }
    int CalUp(int[] nums, int[] up, int[] down, int cur)
    {
        if (up[cur] == 0)
        {
            int max = int.MinValue;
            for (int i = cur + 1; i < down.Length; i++)
            {
                if (nums[i] > nums[cur])
                {
                    int temp = CalDown(nums, up, down, i);
                    max = Math.Max(max, temp);
                }
            }
            up[cur] = max + 1;
        }
        return up[cur];
    }
    int CalDown(int[] nums, int[] up, int[] down, int cur)
    {
        if (down[cur] == 0)
        {
            int max = int.MinValue;
            for (int i = cur + 1; i < down.Length; i++)
            {
                if (nums[i] < nums[cur])
                {
                    int temp = CalUp(nums, up, down, i);
                    max = Math.Max(max, temp);
                }
            }
            down[cur] = max + 1;
        }
        return down[cur];
    }

    public bool IsSubsequence(string s, string t)
    {//check if s is a subseq of t
        if (s.Length == 0) return true;
        if (t.Length == 0) return false;
        int[] dp = new int[t.Length];
        //dp recieve 0 - s.length, indicates the current number it contains
        //dp[i] be the largest subseq length end at ith, to return dp[t.length-1];
        dp[0] = t[0] == s[0] ? 1 : 0;
        for (int i = 1; i < dp.Length; i++)
        {
            int cur = dp[i - 1];
            if (t[i] == s[dp[i - 1]])
                dp[i] = dp[i - 1] + 1;
            else
                dp[i] = dp[i - 1];
            if (dp[i] == s.Length)
                return true;
        }
        return false;
    }

    public static int getMostVisited(int n, List<int> sprints)
    {
        //sprints 2n from
        int[] arr = new int[n];
        for (int i = 0; i < sprints.Count - 1; i++)
        {
            int low = sprints[i] - 1;
            int high = sprints[i + 1] - 1;
            bool forward = high > low;
            if (forward)
            {
                while (high >= low)
                {
                    arr[low]++;
                    low++;
                }
            }
            else
            {
                while (high <= low)
                {
                    arr[low]++;
                    low--;
                }
            }
        }
        int ind = 0;
        int max = arr[0];
        for (int i = 1; i < arr.Length; i++)
        {
            if (arr[i] > max)
            {
                ind = i;
                max = arr[i];
            }
        }
        return ind + 1;
    }

    public long maxValue(int n, List<List<int>> rounds)
    {
        long max = 0;
        long[] overtime = new long[n];
        foreach (List<int> day in rounds)
        {
            int left = day[0] - 1; int right = day[1] - 1; int contribution = day[2];
            for (int i = left; i < right + 1; i++)
            {
                overtime[i] += contribution;
                max = Math.Max(max, overtime[i]);
            }
        }
        return max;
    }

    void Swap(ref int a, ref int b)
    {
        int temp = a;
        a = b; b = a;
    }

    public int RemoveDuplicates(int[] nums)
    {
        if (nums.Length < 2) return nums.Length;
        int curIndex = 1;
        int curNum = nums[0];
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] > curNum)
            {
                curNum = nums[i];
                nums[curIndex] = nums[i];
                curIndex++;
            }
        }
        return curIndex;
    }

    public int RemoveElement(int[] nums, int val)
    {

        int moveLeft = 0;
        int count = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] == val)
            {
                moveLeft++;
            }
            else
            {
                nums[i - moveLeft] = nums[i];
                count++;
            }
        }
        return count;

    }
    public int[][] meanGroups(int[][] a)
    {
        Dictionary<double, List<int>> dict = new Dictionary<double, List<int>>();
        for (int i = 0; i < a.Length; i++)
        {
            double avg = 0;
            foreach (int j in a[i])
            {
                avg += j;
            }
            avg /= (double)a[i].Length;
            if (dict.ContainsKey(avg))
            {
                AddLastSort(dict[avg], i);
            }
            else
            {
                List<int> newl = new List<int>(); newl.Add(i);
                dict.Add(avg, newl);
            }
        }
        List<int[]> res = new List<int[]>();
        foreach (var pair in dict)
        {
            res.Add(pair.Value.ToArray());
        }
        return res.ToArray();


    }
    public void AddLastSort(List<int> list, int key)
    {
        //append key then sort
        int index = list.BinarySearch(0, list.Count, key, new CompareInt());
        if (index >= 0)
        {
            list.Insert(index, key);
        }
        else if (index == -list.Count)
        {
            list.Add(key);
        }
        else
        {
            index = 0 - index;
            index -= 1;
            list.Insert(index, key);
        }
    }

    public int[] isZigzag(int[] numbers)
    {
        int[] zz = new int[numbers.Length - 2];
        for (int i = 0; i < numbers.Length - 2; i++)
        {
            int a = numbers[i];
            int b = numbers[i + 1];
            int c = numbers[i + 2];
            if ((a < b && b > c) || (a > b && b < c))
                zz[i] = 1;
        }
        return zz;
    }

    public bool makeIncreasing(int[] numbers)
    {
        bool swapped = false;
        for (int i = 1; i < numbers.Length; i++)
        {
            if (numbers[i] <= numbers[i - 1])
            {
                if (!swapped)
                {
                    if (Swappable(numbers[i - 2], numbers[i], numbers[i - 1]))
                    {
                        swapped = true;
                    }
                    else return false;
                }
                else return false;
            }
        }
        return true;
    }

    bool Swappable(int a, int b, int c)
    {//right now b>c or a>b but a<c
        //true if b can be swapped to be strictly greater than a and strictly smaller than c
        char[] A = new char[FindNumberOfDigit(a)];
        char[] B = new char[FindNumberOfDigit(b)];
        //TODO this shit
        return true;
    }

    private int FindNumberOfDigit(int number)
    {
        int count = 0;
        while (number > 0)
        {
            number /= 10;
            count++;
        }
        return count;
    }

    public long subarraysCountBySum(int[] arr, int k, long s)
    {//return number of contiguous array of length at most k and sum equals s
        int count = 0;
        for (int i = 0; i < arr.Length; i++)
        {//must have i
            int cur = 0;
            for (int j = i; j < Math.Min(i + k, arr.Length); j++)
            {
                cur += arr[i];
                if (cur == s)
                    count++;
            }
        }
        return count;
    }

    public static int StockPairs(List<int> stocksProfit, long target)
    {
        int count = 0;
        stocksProfit.Sort();
        int curDup = -1;

        for (int i = 0; i < stocksProfit.Count - 1; i++)
        {
            if (stocksProfit[i] != curDup && stocksProfit[i] <= target / 2)
            {
                if (stocksProfit[i] == stocksProfit[i + 1])
                {//new dup
                    curDup = stocksProfit[i];
                }
                for (int j = i + 1; j < stocksProfit.Count; j++)
                {
                    if (stocksProfit[i] + stocksProfit[j] == target)
                    {
                        count++;
                    }
                }

            }
        }

        return count;
    }

    public bool camelCaseSeparation(string[] words, string variableName)
    {
        Dictionary<char, List<string>> startWith = new Dictionary<char, List<string>>();
        foreach (string word in words)
        {
            char first = word[0];
            if (startWith.ContainsKey(first))
            {
                startWith[first].Add(word);
            }
            else
            {
                List<string> newl = new List<string>();
                newl.Add(word);
                startWith.Add(first, newl);
            }

        }

        List<string> split = getWord(variableName);
        foreach (string word in split)
        {
            char first = word[0];
            if (!startWith.ContainsKey(first)) return false;
            if (!startWith[first].Contains(word)) return false;
        }
        return true;
    }

    public List<string> getWord(string str)
    {
        //split by the capital one
        StringBuilder buffer = new StringBuilder("");
        int cur = 0;
        List<string> final = new List<string>();
        while (cur < str.Length)
        {
            if (Char.IsUpper(str[cur]) && cur != 0)
            {
                final.Add(buffer.ToString());
                buffer.Clear();
            }
            buffer.Append(Char.ToLower(str[cur]));
            cur++;
        }
        final.Add(buffer.ToString());
        return final;
    }

    public int Trap(int[] height)
    {//super two pointers
        if (height.Length < 3) return 0;
        int count = 0;
        int l = 0;
        int r = height.Length - 1;
        while (l < r && height[l] <= height[l + 1]) l++;
        while (r > l && height[r] <= height[r - 1]) r--;
        while (l < r)
        {
            int left = height[l];
            int right = height[r];
            if (left <= right)
            {
                while (l < r && left >= height[++l])
                {
                    count += left - height[l];
                }
            }
            else
            {
                while (l < r && right >= height[--r])
                {
                    count += right - height[r];
                }
            }
        }
        return count;
    }

    public bool EnoughParentheses(string s)
    {
        Stack<char> stack = new Stack<char>();
        foreach (char chr in s)
        {
            if (chr == '(' || chr == '[' || chr == '{')
                stack.Push(chr);
            else
            {
                if (stack.Count == 0) return false;
                char cur = stack.Peek();

                if (cur == '(')
                {
                    if (chr == ')') stack.Pop();
                    else return false;
                }
                else if (cur == '[')
                {
                    if (chr == ']') stack.Pop();
                    else return false;
                }
                else
                {
                    if (chr == '}') stack.Pop();
                    else return false;
                }
            }
        }
        return stack.Count == 0;
    }
    public string LongestPalindrome(string s)
    {
        //dp[i] for the last index of the longest palindrome that starts at i
        if (s.Length <= 1) return s;
        if (s.Length == 2) return s[0] == s[1] ? s : s[0].ToString();
        if (s.Length == 3) return s[0] == s[2] ? s : s[0] == s[1] || s[1] == s[2] ? new string(new char[2] { s[0], s[1] }) : new string(new char[2] { s[1], s[2] });
        int n = s.Length;
        int[] dp = new int[n];
        dp[n - 2] = dp[n - 1] == dp[n - 2] ? 2 : 1;
        dp[n - 3] = dp[n - 3] == dp[n - 1] ? 3 : dp[n - 3] == dp[n - 2] ? 2 : 1;
        int max = 0;//longest length encountered
        int maxIndex = -1;
        for (int i = n - 4; i >= 0; i--)
        {
            int next = dp[i + 1];
            if (dp[i + 1] == n - 1)
                dp[i] = i;
            else
            {
                dp[i] = s[dp[i + 1] + 1] == s[i] ? dp[i + 1] + 1 : i;
                if (max < dp[i] - i + 1)
                {
                    max = dp[i] - i + 1;
                    maxIndex = i;
                }
            }
        }
        StringBuilder final = new StringBuilder(s, maxIndex, max, maxIndex + max + 1);
        return final.ToString();
        //this is returning shit but you get it so yh, fuk it anyhow.
    }

    public IList<IList<int>> ThreeSum(int[] nums)
    {
        Array.Sort(nums);
        int cur = 0;
        int n = nums.Length;
        var final = new List<IList<int>>();
        while (cur < n - 2)
        {
            if (cur != 0 && nums[cur] == nums[cur - 1])
                cur++;//duplicate detected
            else
            {
                var supasum = ThreeSome(nums, cur);
                if (supasum.Count != 0)
                    foreach (var list in supasum)
                        final.Add(list);
                cur++;
            }
        }
        return final;
    }
    IList<IList<int>> ThreeSome(int[] nums, int cur)
    {//cur is sure <=n-3
        //find from cur + 1 to n-1 for -nums[cur]
        int l = cur + 1;
        int r = nums.Length - 1;
        int target = -nums[cur];
        int lastLeft = -99;
        IList<IList<int>> final = new List<IList<int>>();
        while (l < r)
        {
            while (nums[l] + nums[r] < target && l < r)
            {
                ++l;
            }
            while (nums[l] + nums[r] > target && l < r)
            {
                --r;
            }

            if (nums[l] + nums[r] == target && l != r && nums[l] != lastLeft)
            {
                lastLeft = nums[l];
                IList<int> list = new List<int>(new int[3] { nums[cur], nums[l], nums[r] });
                final.Add(list);
                ++l;
            }
            else
            {
                ++l;
            }
        }
        return final;
    }

    public void SortColors(int[] nums)
    {
        int second = nums.Length - 1; int zero = 0;
        for (int i = 0; i <= second; i++)
        {
            while (nums[i] == 2 && i < second) Swap(ref nums[i], ref nums[second--]);
            while (nums[i] == 0 && i > zero) Swap(ref nums[i], ref nums[zero++]);
        }
    }

    public int ThreeSumClosest(int[] nums, int target)
    {

        Array.Sort(nums);
        int closest = nums[0] + nums[1] + nums[2];
        for (int i = 0; i < nums.Length - 3; i++)
        {
            if ((i != 0 && nums[i] != nums[i - 1]) || i == 0)
            {
                int curClosest = ThreeSomeClosestUtil(nums, target, i);
                if (Dist(curClosest, target) < Dist(closest, target))
                    closest = curClosest;
            }
        }
        return closest;
    }

    int ThreeSomeClosestUtil(int[] nums, int target, int cur)
    {
        //given the chain start with cur, find the closest one to it
        int l = cur + 1;
        int r = nums.Length - 1;
        int final = nums[l] + nums[r];
        target -= nums[cur];
        int closest = Dist(final, target);//distance from cur to target
        while (l < r)
        {
            int curSum = nums[l] + nums[r];
            if (curSum < target)
                ++l;
            else
                --r;
            if (Dist(curSum, target) < closest)
            {
                final = curSum; closest = Dist(curSum, target);
            }
        }
        return final;
    }

    int Dist(int a, int b) { return Math.Abs(a - b); }

    public int StrStr(string haystack, string needle)
    {
        if (needle.Length == 0 || needle == null) return 0;
        if (haystack.Length < needle.Length) return -1;

        for (int i = 0; i < haystack.Length; i++)
        {
            if (StrStrUtil(haystack, needle, i))
                return i;
        }
        return -1;
    }

    bool StrStrUtil(string a, string b, int cur)
    {
        if (cur + b.Length - 1 >= a.Length) return false;
        for (int i = cur; i < cur + b.Length; i++)
            if (a[i] != b[i - cur])
                return false;
        return true;
    }

    public void NextPermutation(int[] nums)
    {
        //search for k that k is the largest int such that arr[k]<arr[k+1]
        int k;
        for (k = nums.Length - 2; k >= 0; k--)
        {//since we are interested in the largest k, saerch from the end of the array saves more time
            if (nums[k + 1] > nums[k])
            {
                break;
            }
        }
        //then search from k+1 to n-1 for the largest l such that arr[l] > arr[k]
        if (k < 0)
            Array.Reverse(nums);
        else
        {
            int l;
            for (l = nums.Length - 1; l > k; l--)
            {
                if (nums[l] > nums[k])
                    break;
            }
            Swap(ref nums[l], ref nums[k]);
            Array.Reverse(nums, k + 1, nums.Length - k - 1);
        }

    }

    public int UniquePaths(int m, int n)
    {
        //from 0,0 to m-1, n-1
        if (m == 1) return 1;
        if (n == 1) return 1;
        int[,] dp = new int[m + 1, n + 1];//number of way to move if there is only i x j cells
        for (int i = 0; i <= n; i++)
            dp[1, i] = 1;
        for (int i = 0; i <= m; i++)
            dp[i, 1] = 1;
        UniquePathsUtil(m, n, dp);
        return dp[m, n];
    }
    int UniquePathsUtil(int m, int n, int[,] dp)
    {
        if (dp[m, n] != 0) return dp[m, n];
        dp[m, n] = UniquePathsUtil(m - 1, n, dp) + UniquePathsUtil(m, n - 1, dp);
        return dp[m, n];
    }

    public IList<string> GenerateParenthesisFuck(int n)
    {
        //base case: n=1 n=2

        List<string>[] dp = new List<string>[n + 1];
        Array.Fill(dp, new List<string>());
        //dp[i] be the list of well formed parentheses with i pairs. 1-based indexing
        dp[0] = new List<string>(new string[1] { "" });
        dp[1] = new List<string>(new string[1] { "()" });
        dp[2] = new List<string>(new string[2] { "()()", "(())" });
        if (n <= 2) return dp[n];
        GenerateParenthesisUtil(n, dp);
        return dp[n];

    }

    List<string> GenerateParenthesisUtil(int n, List<string>[] dp)
    {
        if (dp[n].Count != 0) return dp[n];
        List<string> final = new List<string>();
        StringBuilder coverCase = new StringBuilder();
        var inside = GenerateParenthesisUtil(n - 1, dp);
        foreach (string str in inside)
        {
            coverCase.Append("(");
            coverCase.Append(str);
            coverCase.Append(")");
            final.Add(coverCase.ToString());
            coverCase.Clear();
        }
        for (int i = 1; i < n; i++)
        {
            var left = GenerateParenthesisUtil(i, dp);
            var right = GenerateParenthesisUtil(n - i, dp);
            foreach (string leftPart in left)
            {
                foreach (string rightPart in right)
                {
                    coverCase.Append(leftPart);
                    coverCase.Append(rightPart);
                    final.Add(coverCase.ToString());
                    coverCase.Clear();
                }
            }
        }
        dp[n] = final;
        return final;
    }//this shit got overlapping solution. GOD DAMN IT FUCCKKKKK

    public IList<string> GenerateParenthesis(int n)
    {
        List<string> final = new List<string>();
        GenerateParenthesisBacktrack(final, new StringBuilder(""), 0, 0, n);
        return final;
    }

    void GenerateParenthesisBacktrack(List<string> final, StringBuilder str, int open, int close, int max)
    {
        if (str.Length == max * 2)
        {
            final.Add(str.ToString());
            return;
        }
        if (open < max)
        {
            str.Append("(");
            GenerateParenthesisBacktrack(final, str, open + 1, close, max);
            str.Remove(str.Length - 1, 1);
        }
        if (close < open)
        {
            str.Append(")");
            GenerateParenthesisBacktrack(final, str, open, close + 1, max);
            str.Remove(str.Length - 1, 1);
        }
    }
    int start = 0;
    int maxLength = 0;
    public string NewLongestPalindrome(string s)
    {
        int n = s.Length;
        if (n < 2) return s;

        for (int i = 0; i < n - 1; i++)
        {//repeatedly find the palindrome with center be i
            ExtendPalindrome(s, i, i);
            ExtendPalindrome(s, i, i + 1);
        }
        return s.Substring(start, maxLength);
    }

    public void ExtendPalindrome(string s, int j, int k)
    {
        while (j >= 0 && k < s.Length && s[j] == s[k])
        {
            j--;
            k++;
        }
        if (maxLength < k - j - 1)
        {
            start = j + 1;
            maxLength = k - j - 1;
        }
    }
    public int MinPathSum(int[][] grid)
    {
        int m = grid.Length;
        int n = grid[0].Length;
        if (m == 1)
        {
            int cum = 0;
            foreach (int i in grid[0])
                cum += i;
            return cum;
        }
        int[,] dp = new int[m, n];
        for (int i = 0; i < m; i++)
            for (int j = 0; j < n; j++)
                dp[i, j] = -1;
        dp[m - 1, n - 1] = grid[m - 1][n - 1];
        MinPathSumUtil(dp, grid, 0, 0);
        return dp[0, 0];
    }
    int MinPathSumUtil(int[,] dp, int[][] grid, int i, int j)
    {
        if (i >= grid.Length || j >= grid[0].Length) return int.MaxValue;
        if (dp[i, j] != -1) return dp[i, j];
        dp[i, j] = grid[i][j] + Math.Min(MinPathSumUtil(dp, grid, i + 1, j), MinPathSumUtil(dp, grid, i, j + 1));
        return dp[i, j];
    }

    public int LengthOfLongestSubstring(string s)
    {
        int n = s.Length;
        if (n <= 1) return n;
        if (n == 2) return s[0] != s[1] ? 2 : 1;
        int[] dp = new int[n];
        dp[n - 1] = n - 1;
        int max = 1;
        for (int i = n - 2; i >= 0; i--)
        {
            char cur = s[i];
            for (int j = i + 1; j <= dp[i + 1]; j++)
            {
                if (j == dp[i + 1] && cur != s[j])
                    dp[i] = j;
                if (cur == s[j])
                { dp[i] = j - 1; break; }

            }
            int curLength = dp[i] - i + 1;
            max = Math.Max(curLength, max);
        }
        return max;
    }
    public int FirstMissingPositive(int[] nums)
    {
        for (int i = 0; i < nums.Length; i++)
        {
            while (nums[i] > 0 && nums[i] <= nums.Length && nums[nums[i] - 1] != nums[i])
                Swap(ref nums[i], ref nums[nums[i] - 1]);
        }
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] != i + 1)
                return i + 1;
        }
        return nums.Length + 1;
    }

    public int NumDecodings(string s)
    {
        int n = s.Length;
        if (n == 1) return s[0] != 0 ? 1 : 0;
        if (n == 2)
        {
            if (CharToInt(s[0]) == 0) return 0;
            return StringToInt(s) <= 27 ? 2 : 1;
        }
        int[] dp = new int[n];
        if (CharToInt(s[n - 1]) == 0) return 0;
        dp[n - 1] = 1;
        dp[n - 2] = CharToInt(s[n - 2]) == 0 ? 0 : StringToInt(s, n - 2, 2) <= 26 ? 2 : 1;
        for (int i = n - 3; i >= 0; i--)
        {
            if (CharToInt(s[i]) == 0) dp[i] = 0;
            else
            {
                if (dp[i + 1] == 0) return 0;//impossible to handle
                if (StringToInt(s, i, 2) <= 26)
                {
                    dp[i] = dp[i + 1] + dp[i + 2];
                }
                else
                {
                    dp[i] = dp[i + 1];
                }
            }
        }
        return dp[0];
    }

    int CharToInt(char chr) { return (int)char.GetNumericValue(chr); }
    int StringToInt(string str) { return Int32.Parse(str); }
    int StringToInt(string str, int start, int length) { return Int32.Parse(str.Substring(start, length)); }

    public IList<IList<int>> CombinationSum(int[] candidates, int target)
    {
        Array.Sort(candidates);
        //TODO: Finish this one
        return null;
    }

    public string RemoveDuplicateLetters(string s)
    {
        int[] hash = new int[26];
        int pos = 0;//position of the smallest s[i]
        for (int i = 0; i < s.Length; i++) hash[s[i] - 'a']++;
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] < s[pos]) pos = i;
            if (--hash[s[i] - 'a'] == 0) break;
        }
        return s.Length == 0 ? "" : s[pos] + RemoveDuplicateLetters(s.Substring(pos + 1).Replace("" + s[pos], ""));
    }

    public string LargestNumber(int[] nums)
    {
        int n = nums.Length;
        if (n == 0) return "";
        if (n == 1) return nums[0].ToString();
        CompareLexiString comparer = new CompareLexiString();
        string[] arr = new string[n];
        for (int i = 0; i < n; i++) arr[i] = nums[i].ToString();
        Array.Sort(arr, comparer);
        StringBuilder buffer = new StringBuilder();
        for (int i = n - 1; i >= 0; i--)
            buffer.Append(arr[i]);
        return buffer.ToString();
    }

    public string LongestCommonPrefix(string[] strs)
    {
        char[] cur = strs[0].ToCharArray();
        if (cur.Length == 0) return "";
        int curIndex = cur.Length;
        foreach (string str in strs)
        {
            curIndex = Math.Min(curIndex, str.Length);
            for (int i = 0; i < curIndex; i++)
            {
                if (cur[i] != str[i])
                {
                    curIndex = i;
                    if (curIndex == 0) return "";
                }
            }
        }
        return new string(cur, 0, curIndex);
    }

    int treeSum = 0;
    public int SumNumbers(TreeNode root)
    {
        if (root == null) return 0;
        SumNumbersUtil(root, 0);
        return treeSum;
    }

    void SumNumbersUtil(TreeNode root, int sofar)
    {
        //with the presence of root, factorize it into sofar and continues and add to treeSum
        if (root == null) return;
        if (root.left == null && root.right == null) treeSum += sofar * 10 + root.val;
        SumNumbersUtil(root.left, sofar * 10 + root.val);
        SumNumbersUtil(root.right, sofar * 10 + root.val);

    }

    public int SumOfLeftLeaves(TreeNode root)
    {
        return LeftSum(root, false);
    }

    public int LeftSum(TreeNode cur, bool isLeft)
    {
        if (cur.right == null && cur.left == null)
        {
            if (isLeft) return cur.val;
            else return 0;
        }
        if (cur.left == null) return LeftSum(cur.right, false);
        if (cur.right == null) return LeftSum(cur.left, true);
        return LeftSum(cur.right, false) + LeftSum(cur.left, true);
    }

    public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
    {
        int curLeft = 0;
        ListNode final = new ListNode();
        var next1 = l1; var next2 = l2;
        var curNode = final;
        while (next1 != null && next2 != null)
        {
            int sum = curLeft + next1.val + next2.val;
            if (sum <= 9)
            {
                curLeft = 0;
                curNode.next = new ListNode(sum);
            }
            else
            {
                curNode.next = new ListNode(sum - 10);
                curLeft = 1;
            }
            next1 = next1.next; next2 = next2.next; curNode = curNode.next;
        }
        if (next1 == null && next2 == null)
        {
            if (curLeft != 0)
                curNode.next = new ListNode(curLeft);
        }
        else if (next1 == null)
        {//next1 exhausted
            while (next2 != null)
            {
                int sum = next2.val + curLeft;
                if (sum <= 9)
                {
                    curNode.next = new ListNode(sum);
                    curLeft = 0;
                }
                else
                {
                    curNode.next = new ListNode(sum - 10);
                    curLeft = 1;
                }
                next2 = next2.next;
            }
            if (curLeft != 0)
            {
                curNode.next = new ListNode(curLeft);
            }
        }
        else
        {//next2 exhausted
            while (next1 != null)
            {
                int sum = next1.val + curLeft;
                if (sum <= 9)
                {
                    curNode.next = new ListNode(sum);
                    curLeft = 0;
                }
                else
                {
                    curNode.next = new ListNode(sum - 10);
                    curLeft = 1;
                }
                next1 = next1.next;
            }
            if (curLeft != 0)
            {
                curNode.next = new ListNode(curLeft);
            }
        }
        return final.next;
    }

    public bool IsPalindrome(int x)
    {
        if (x < 0) return false;
        if (x < 10) return true;
        return IsPalindrome(x.ToString(), 0, x.ToString().Length - 1);
    }
    bool IsPalindrome(string str, int l, int r)
    {
        if (l == r || r - l + 1 == 2) return str[l] == str[r];
        return str[l] == str[r] && IsPalindrome(str, l + 1, r - 1);
    }

    public ListNode SwapPairs(ListNode head)
    {
        if (head == null) return null;
        if (head.next == null) return head;
        var n = head.next;
        head.next = SwapPairs(head.next.next);
        n.next = head;
        return n;
    }

    public int ArrangeCoins(long n)
    {
        if (n <= 2) return 1;
        int k = ((int)Math.Sqrt(8 * n) - 1) / 2;
        return Math.Floor((double)((k + 1) * (k + 2) / 2)) > n ? k : k + 1;
    }

    public int[] SingleNumber(int[] nums)
    {
        int xor = 0;
        foreach (int i in nums)
            xor ^= i;
        int rightSetBit = xor & -xor;
        int a = 0;
        foreach (int i in nums)
            if ((i & rightSetBit) != 0)
                a ^= i;
        return new int[2] { a, xor ^ a };
    }

    public bool SolveSudoku(char[][] board)
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (board[i][j] == '.')
                {
                    for (char k = '1'; k < '9'; k++)
                    {
                        board[i][j] = k;
                        if (IsValidSudoku(board))
                        {
                            if (SolveSudoku(board))
                                return true;
                            board[i][j] = '.';
                        }
                    }
                    return false;//after trying all 9 numbers
                }

            }
        }
        return false;
    }

    public bool IsValidSudoku(char[][] board)
    {
        for (int i = 0; i < 9; i++)
        {
            //row first
            for (int j = 0; j < 8; j++)
            {
                for (int t = j + 1; t < 9; t++)
                {
                    if (board[i][j] == board[i][t] && board[i][t] != '.') return false;
                }
            }
        }
        for (var i = 0; i < 9; i++)
        {
            //col 
            for (int j = 0; j < 8; j++)
            {
                for (int t = j + 1; t < 9; t++)
                {
                    if (board[j][i] == board[t][i] && board[t][i] != '.') return false;
                }
            }
        }
        List<int> list = new List<int>();
        for (int i = 0; i <= 6; i += 3)
        {
            for (int j = 0; j <= 6; j += 3)
            {
                //start at (i,j) => (i+2, j+2);
                for (int x = 0; x < 3; x++)
                {
                    for (int y = 0; y < 3; y++)
                    {
                        if (board[i + x][j + y] != '.')
                        {
                            if (list.Contains(board[i + x][j + y]))
                                return false;
                            list.Add(board[i + x][j + y]);
                        }
                    }
                }
                list.Clear();

            }
        }
        return true;
    }



    public string Multiply(string num1, string num2)
    {
        if (num1 == "0" || num2 == "0") return "0";
        //set num1 to be longer than num2. num.length>1
        int[] final = new int[num1.Length + num2.Length];
        for (int i = num1.Length - 1; i >= 0; i--)
        {
            for (int j = num2.Length - 1; j >= 0; j--)
            {
                final[i + j + 1] += (num1[i] - '0') * (num2[j] - '0');
                final[i + j] += final[i + j + 1] / 10;
                final[i + j + 1] %= 10;
            }
        }
        StringBuilder buffer = new StringBuilder();
        int start = 0;
        while (final[start] == 0) start++;
        for (int i = start; i < final.Length; i++)
            buffer.Append(final[i]);
        return buffer.ToString();
    }

    Random randomSeed = new Random();
    public T[][] MatrixGenerator<T>(int width, int length, int range, Type type)
    {
        //TODO: Add more functionality and generic to this shit
        if (type.Equals(typeof(System.Int32)))
        {
            int[][] matrix = new int[width][];
            for (int i = 0; i < width; i++)
            {
                matrix[i] = new int[length];
                for (int j = 0; j < length; j++)
                    matrix[i][j] = randomSeed.Next(range + 1);
            }
        }
        return null;
    }

    public bool SearchMatrix(int[][] matrix, int target)
    {
        int numRow = matrix.Length;
        int numCol = matrix[0].Length;
        if (numRow == 1 && numCol == 1) return matrix[0][0] == target;
        if (numRow == 1) return Array.BinarySearch(matrix[0], target) > 0;
        //binary search row
        int l = 0; int r = numRow - 1;
        while (l < r)
        {
            int m = (l + r) / 2;
            if (matrix[m][0] == target) return true;
            if (matrix[m][0] > target)
            {
                r = m - 1;
            }
            else
            {
                l = m + 1;
            }
        }

        int row;
        if (matrix[l][0] == target) return true;
        if (numCol == 1) return false;
        if (matrix[l][0] > target) row = l - 1;
        else row = l;
        if (row < 0) return false;
        //search in row from 0 -> numCol-1
        return Array.BinarySearch(matrix[row], target) > 0;
    }

    public ListNode ReverseKGroup(ListNode head, int k)
    {
        //TODO: Solve this shit
        if (k == 1) return head;
        //when there are less than k nodes, return fully

        //else 
        return null;
    }
    public int NumTrees(int n)
    {
        if (n <= 1) return n;
        int[] dp = new int[n + 1];//dp[n] no way to have n
        dp[0] = dp[1] = 1;
        dp[2] = 2;
        NumTreesUtil(n, dp);
        return dp[n];
    }
    int NumTreesUtil(int n, int[] dp)
    {
        if (dp[n] != 0) return dp[n];
        for (int k = 0; k <= n - 1; k++)
        {
            dp[n] += NumTreesUtil(k, dp) * NumTreesUtil(n - 1 - k, dp);
        }
        return dp[n];
    }

    public ListNode RemoveNthFromEnd(ListNode head, int n)
    {
        if (head == null || head.next == null) return null;
        if (head.next.next == null)
        {
            if (n == 1)
            {
                head.next = null; return head;
            }
            if (n == 2) return head.next;
        }
        ListNode maybeLast = head;
        ListNode maybeN = head;
        for (int i = 0; i < n; i++)
        {
            maybeLast = maybeLast.next;
        }
        if (maybeLast == null) //point to head 
        {
            return head.next;
        }
        while (maybeLast.next != null)
        {
            maybeLast = maybeLast.next;
            maybeN = maybeN.next;
        }
        maybeN.next = maybeN.next.next;
        return head;
    }

    public List<List<int>> match_records(List<string> input_data)
    {
        int n = input_data.Count;
        int[][] adj = new int[n][];
        for (int i = 0; i < n; i++)
        { adj[i] = new int[n]; adj[i][i] = 1; }
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                if (CompareTokens(input_data[i], input_data[j]))
                {
                    adj[i][j] = 1;
                    adj[j][i] = 1;
                }
            }
        }
        int[][] closure = new int[n][];

        for (int i = 0; i < n; i++)
        {
            closure[i] = new int[n];
            for (int j = 0; j < n; j++)
            {
                closure[i][j] = adj[i][j];
            }
        }
        for (int k = 0; k < n; k++)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    closure[i][j] = (closure[i][j] != 0 || (closure[i][k] != 0 && closure[k][j] != 0)) ? 1 : 0;
                }
            }
        }
        List<List<int>> final = new List<List<int>>();
        for (int i = 0; i < n; i++)
            final.Add(new List<int>(closure[i]));
        return final;
    }
    public bool CompareTokens(string str1, string str2)
    {
        var t1 = CommaSeperator(str1);
        var t2 = CommaSeperator(str2);
        for (int i = 0; i <= 2; i++)
            for (int j = 0; j <= 2; j++)
                if (t1[i] == t2[j] && t1[i] != "")
                    return true;
        return false;
    }
    public string[] CommaSeperator(string str)
    {
        List<string> final = new List<string>();
        StringBuilder buffer = new StringBuilder();
        foreach (char chr in str)
        {
            if (chr == ',')
            {
                final.Add(buffer.ToString()); buffer.Clear();
            }
            else
            {
                buffer.Append(chr);
            }
        }
        if (buffer.Length > 0 || (str[str.Length - 1] == ',')) final.Add(buffer.ToString());
        return final.ToArray();
    }

    public IList<int> FindNumOfValidWords(string[] words, string[] puzzles)
    {
        //TODO: Solve after studying Trie
        return null;
    }

    Dictionary<int, Node> mapForClone = new Dictionary<int, Node>();
    public Node CloneGraph(Node node)
    {
        // var cur = node;
        if (node == null) return null;
        if (mapForClone.ContainsKey(node.val)) return mapForClone[node.val];

        var clone = new Node(node.val);
        mapForClone.Add(clone.val, clone);
        foreach (var neighbor in node.neighbors)
        {
            clone.neighbors.Add(CloneGraph(neighbor));
        }
        return clone;
    }

    public int Rob(int[] nums)
    {
        int n = nums.Length;
        int[] dp = new int[n];
        dp[n - 1] = nums[n - 1];
        if (n == 1) return dp[n - 1];
        dp[n - 2] = nums[n - 2];
        if (n == 2) return Math.Max(dp[n - 2], dp[n - 1]);
        dp[n - 3] = nums[n - 3] + nums[n - 1];
        if (n == 3) return Math.Max(dp[n - 3], dp[n - 2]);

        for (int i = n - 4; i >= 0; i--)
        {
            dp[i] = nums[i] + Math.Max(dp[i + 2], dp[i + 3]);
        }
        return Math.Max(dp[0], dp[1]);
    }

    public IList<string> FindRepeatedDnaSequences(string s)
    {
        int n = s.Length;
        if (n <= 10) return new List<string>();
        List<string> final = new List<string>();
        Dictionary<string, int> dict = new Dictionary<string, int>();
        for (int i = 0; i <= n - 10; i++)
        {
            var cur = s.Substring(i, 10);
            if (!dict.ContainsKey(cur))
            {
                dict.Add(cur, 1);
            }
            else
            {
                if (dict[cur] == 1)
                { final.Add(cur); dict[cur]++; }
            }
        }
        return final;
    }

    public int MaxProfit(int[] prices)
    {
        int n = prices.Length;
        if (n == 2) return prices[1] > prices[0] ? prices[1] - prices[0] : 0;
        int l = 0;
        while (l + 1 < n && prices[l] >= prices[l + 1])
        {
            l++;
        }
        int r = n - 1;
        while (r - 1 >= 0 && prices[r - 1] >= prices[r])
        {
            r--;
        }
        if (l >= r) return 0;
        int cur = 0;
        int sum = 0;
        for (int i = l; i <= r;)
        {
            cur = prices[i];
            while (i + 1 <= r && prices[i] < prices[i + 1])
            {
                i++;
            }
            sum += prices[i] - cur;
            i++;
        }
        return sum;
    }

    public IList<int> FindMinHeightTrees(int n, int[][] edges)
    {
        if (n == 0) return new List<int>();
        HashSet<int>[] adj = new HashSet<int>[n];
        for (int i = 0; i < n; i++) adj[i] = new HashSet<int>();
        foreach (var edge in edges)
        {
            adj[edge[1]].Add(edge[0]);
            adj[edge[0]].Add(edge[1]);
        }
        List<int> leaves = new List<int>();
        // add leaves
        for (int i = 0; i < n; i++)
            if (adj[i].Count == 1)
                leaves.Add(i);
        while (n > 2)
        {
            n -= leaves.Count; List<int> newLeaves = new List<int>();
            foreach (int leaf in leaves)
            {
                // remove current leaves and add next leaves

                var neighborOfLeaf = adj[leaf];
                foreach (int i in neighborOfLeaf)
                {
                    adj[i].Remove(leaf);
                    if (adj[i].Count == 1)
                    {
                        newLeaves.Add(i);
                    }
                }

            }
            leaves = newLeaves;
        }
        return leaves;
    }

    public int MinPatches(int[] nums, int n)
    {
        long miss = 1; int added = 0, i = 0;
        while (miss <= n)
        {
            if (i < nums.Length && nums[i] <= miss)
            {// miss>=num[i] means nums[i] is just too big to cover miss
                miss += nums[i++];
            }
            else
            {
                miss += miss;
                added++;
            }
        }
        return added;
    }

    public int Search(int[] nums, int target)
    {//todo later
        return 0;
    }

    public int MinStartValue(int[] nums)
    {
        //find the smallest number 
        int min = 1;
        int sofar = 0;
        foreach (int i in nums)
        {
            sofar += i;
            if (sofar < 1)
            {//if we spot a place that needed to be added to be greaeter than or equals 1
                min = Math.Min(sofar, min);
            }
        }
        if (min >= 1) return 1;
        return 1 - min;
    }

    public int BalancedStringSplit(string s)
    {
        if (s.Length <= 1) return 0;
        int L = 0, R = 0, count = 0;
        foreach (char chr in s)
        {
            if (chr == 'L') L++;
            if (chr == 'R') R++;
            if (L == R)
            {
                count++; L = 0; R = 0;
            }
        }
        return count;

    }

    public int Maximum69Number(int num)
    {
        char[] arr = num.ToString().ToCharArray();
        for (int i = 0; i < arr.Length; i++)
            if (arr[i] == '6')
            {
                arr[i] = '9';
                break;
            }

        return Int32.Parse(new string(arr));
    }

    public bool LemonadeChange(int[] bills)
    {
        int five = 0, ten = 0;
        foreach (int i in bills)
        {
            if (i == 5) five++;
            if (i == 10) { five--; ten++; }
            if (i == 20)
            {
                five--;
                if (ten == 0) five -= 2;
                else ten--;
            }
            if (ten < 0 || five < 0) return false;
        }
        return true;
    }

    public ListNode RemoveElements(ListNode head, int val)
    {
        if (head == null) return null;
        head.next = RemoveElements(head.next, val);
        if (head.val == val) return head.next;
        return head;
    }

    public bool CanPlaceFlowers(int[] flowerbed, int n)
    {
        int count = 0;
        if (n == 0) return true;
        if (flowerbed.Length == 0) return true;
        for (int i = 0; i < flowerbed.Length;)
        {
            if (flowerbed[i] == 0)
            {
                if ((i == 0 || flowerbed[i - 1] == 0) && (i == flowerbed.Length - 1 || flowerbed[i + 1] == 0))
                {
                    count++; flowerbed[i] = 1;
                    if (count >= n) return true;
                    i += 2;
                }
                else i++;
            }
            else i++;
        }
        return count >= n;
    }

    public int FindContentChildren(int[] g, int[] s)
    {
        if (s.Length == 0) return 0;
        Array.Sort(g); Array.Sort(s);
        int gPointer = 0;
        int sPointer = 0;
        while (true)
        {
            while (g[gPointer] > s[sPointer])
            {
                sPointer++;
                if (sPointer >= s.Length) return gPointer;
            }
            //s[sPointer] now is greater or equal to g[gPointer]
            gPointer++; sPointer++;
            if (gPointer >= g.Length || sPointer >= s.Length) return gPointer;
        }
    }

    public int LongestPalindromeCaseSensitive(string s)
    {
        Dictionary<char, int> dict = new Dictionary<char, int>();
        foreach (char chr in s)
        {
            if (!dict.ContainsKey(chr))
            {
                dict.Add(chr, 1);
            }
            else
            {
                dict[chr]++;
            }
        }
        int sum = 0;
        foreach (var pair in dict)
        {
            if (pair.Value % 2 == 0) sum += pair.Value;
            else
            {
                if (sum % 2 != 0) sum += pair.Value - 1;
                else sum += pair.Value;
            }
        }
        return sum;
    }

    public int[] DailyTemperatures(int[] temperatures)
    {
        int[] ans = new int[temperatures.Length];
        Stack<int> stack = new Stack<int>();
        for (int i = temperatures.Length - 1; i >= 0; i--)
        {
            while (stack.Count != 0 && temperatures[stack.Peek()] <= temperatures[i])
                stack.Pop();
            ans[i] = stack.Count != 0 ? stack.Peek() : 0;
            stack.Push(i);
        }
        for (int i = 0; i < temperatures.Length; i++)
            ans[i] = ans[i] != 0 ? ans[i] - i : 0;
        return ans;
    }

    public int[] NextGreaterElement(int[] nums)
    {
        int[] ans = new int[nums.Length];
        Stack<int> stack = new Stack<int>();
        for (int i = ans.Length - 1; i >= 0; i--)
        {
            while (stack.Count != 0 && stack.Peek() <= nums[i])
                stack.Pop();
            ans[i] = stack.Count != 0 ? stack.Peek() : -1;
            stack.Push(nums[i]);
        }
        return ans;
    }

    public int[] NextGreaterElement(int[] nums1, int[] nums2)
    {
        int[] ans = new int[nums2.Length];
        Stack<int> stack = new Stack<int>();
        for (int i = nums2.Length - 1; i >= 0; i--)
        {
            while (stack.Count != 0 && stack.Peek() <= nums2[i])
            {
                stack.Pop();
            }
            ans[i] = stack.Count != 0 ? stack.Peek() : -1;
            stack.Push(nums2[i]);
        }
        for (int i = 0; i < nums1.Length; i++)
        {
            int index = Array.FindIndex(nums2, x => x == nums1[i]);
            nums1[i] = ans[index];
        }
        return nums1;

    }
    public int[] FinalPrices(int[] prices)
    {
        int[] ans = new int[prices.Length];
        Stack<int> stack = new Stack<int>();
        for (int i = prices.Length - 1; i >= 0; i--)
        {
            while (stack.Count != 0 && stack.Peek() >= prices[i])
            {
                stack.Pop();
            }
            ans[i] = prices[i] - (stack.Count != 0 ? stack.Peek() : 0);
            stack.Push(prices[i]);
        }
        return ans;
    }

    public int NumberOfWays2Sum(int[] arr, int target)
    {
        Array.Sort(arr);
        int n = arr.Length;
        int l = 0; int r = n - 1;
        int count = 0;
        while (r > l)
        {
            while (l > 0 && l < r && arr[l] == arr[l - 1])
                l++;//remove duplicate
            while (r < n - 1 && r > l && arr[r] == arr[r + 1])
                r--;//remove duplicate in the right part
            if (r > l)
            {
                if (arr[r] + arr[l] == target)
                {
                    count++;
                    r--;
                }
                else if (arr[r] + arr[l] > target)
                    r--;
                else
                    l++;
            }
        }
        return count;
    }

    public int NumberOfWaysHash(int[] arr, int target)
    {
        Dictionary<int, int> dict = new Dictionary<int, int>();
        foreach (int i in arr)
        {
            if (!dict.ContainsKey(i))
                dict.Add(i, 1);
            else
                dict[i]++;
        }
        int sum = 0;
        foreach (var pair in dict)
        {
            int key = pair.Key;
            if (dict.ContainsKey(target - key))
            {
                if (key == target - key)
                { sum += pair.Value * (pair.Value - 1) / 2; }
                else
                { sum += pair.Value * dict[target - key]; }
            }
        }
        return sum;
    }

    public string EncryptFacebook(string str)
    {
        int length = str.Length;
        if (length <= 2) return str;
        int mid = (length - 1) / 2; // 3-> 1, 4->1; 5->2, 6->2
        StringBuilder buffer = new StringBuilder();
        buffer.Append(str[mid].ToString());
        buffer.Append(EncryptFacebook(str.Substring(0, mid)));
        buffer.Append(EncryptFacebook(str.Substring(mid + 1, length - 1 - mid - 1 + 1)));
        return buffer.ToString();
    }

    public IList<int> InorderTraversal(TreeNode root)
    {
        //TODO: Solve this shit with Stack
        return null;
    }

    public bool CheckAlmostEquivalent(string word1, string word2)
    {
        if (word1.Length != word2.Length) return false;
        if (word1.Length == 0) return true;
        Dictionary<char, int> dict1 = new Dictionary<char, int>();
        Dictionary<char, int> dict2 = new Dictionary<char, int>();
        foreach (char chr in word1)
        {
            if (dict1.ContainsKey(chr))
                dict1[chr]++;
            else
                dict1.Add(chr, 1);
        }
        foreach (char chr in word2)
        {
            if (dict2.ContainsKey(chr))
                dict2[chr]++;
            else
                dict2.Add(chr, 1);
        }
        for (char chr = 'a'; chr <= 'z'; chr++)
        {
            if (dict1.ContainsKey(chr) && dict2.ContainsKey(chr))
            {
                if (Math.Abs(dict1[chr] - dict2[chr]) > 3) return false;
            }
            if (!dict1.ContainsKey(chr) && dict2.ContainsKey(chr))
                if (dict2[chr] > 3) return false;
            if (!dict2.ContainsKey(chr) && dict1.ContainsKey(chr))
                if (dict1[chr] > 3) return false;
        }
        return true;
    }

    public void TestRobot()
    {
        Robot robot = new Robot(6, 3);
        robot.Move(7);
        var pos = robot.GetPos();
        Console.WriteLine("(x,y): ", pos[0], pos[1]);
        Console.WriteLine("Direction: ", robot.GetDir());
    }

    public bool BalancedSplitExists(int[] arr)
    {
        int sum = 0;
        foreach (int i in arr) sum += i;
        if (sum % 2 == 1) return false;
        Array.Sort(arr);
        int sofar = 0;
        foreach (int i in arr)
        {
            sofar += i;
            if (sofar == sum / 2) return true;
            if (sofar > sum / 2) return false;
        }
        return true;
    }

    public int CountDistinctTriangles(int[][] arr)
    {
        Dictionary<int, int> dict = new Dictionary<int, int>();
        CompareThreeArray comparer = new CompareThreeArray();
        //Array.Sort(dict, comparer);
        return 1; //TODO: solve this shit
    }

    public IList<int> LargestDivisibleSubset(int[] nums)
    {
        int n = nums.Length;
        int[] pre = new int[n];//keep track of the previous divisible element index
        int[] count = new int[n];//count[i] = max length that the top one is nums[i]
        Array.Sort(nums);
        int max = 0, maxIndex = -1;
        for (int i = 0; i < n; i++)
        {
            pre[i] = -1; count[i] = 1;
            for (int j = i - 1; j >= 0; j--)
            {
                if (nums[i] % nums[j] == 0)
                {
                    if (count[i] < count[j] + 1)
                    {
                        count[i] = count[j] + 1;
                        pre[i] = j;
                    }
                }
            }
            if (count[i] > max) { max = count[i]; maxIndex = i; }
        }
        List<int> list = new List<int>();
        while (maxIndex != -1)
        {
            list.Add(nums[maxIndex]);
            maxIndex = pre[maxIndex];
        }
        return list;
    }

    public bool Enough(int x, int m, int n, int k)
    {
        int count = 0;
        for (int i = 0; i <= m; i++)
        {
            count += Math.Min(x / i, n);
        }
        return count >= k;
    }

    public int FindKthNumber(int m, int n, int k)
    {
        int l = 1; int r = m * n;
        while (l < r)
        {
            int mid = l + (r - l) / 2;
            if (!Enough(mid, m, n, k)) l = mid + 1;
            else r = mid;

        }
        return l;
    }

    public IList<IList<int>> CombinationSumBT(int[] candidates, int target)
    {
        List<IList<int>> final = new List<IList<int>>();
        Array.Sort(candidates);
        CombinationSumUtil(final, new List<int>(), candidates, target, 0);
        return final;
    }
    void CombinationSumUtil(List<IList<int>> final, List<int> sofar, int[] candidates, int target, int start)
    {//only hceck from start
        if (target < 0) return;
        if (target == 0) { final.Add(sofar); return; }
        else
        {
            for (int i = start; i < candidates.Length; i++)
            {
                sofar.Add(candidates[i]);
                CombinationSumUtil(final, sofar, candidates, target - candidates[i], start);
                sofar.RemoveAt(sofar.Count - 1);
            }
        }
    }

    public int MinCostClimbingStairs(int[] cost)
    {
        int n = cost.Length;
        if (n == 1) return cost[0];
        int[] dp = new int[n];
        dp[n - 1] = cost[n - 1];
        dp[n - 2] = cost[n - 2];
        for (int i = n - 3; i >= 0; i--)
        {
            dp[i] = cost[i] + Math.Min(dp[i + 1], dp[i + 2]);
        }
        return Math.Min(dp[0], dp[1]);
    }

    public void MoveZeroes(int[] nums)
    {
        if (nums.Length <= 1) return;
        int insertPos = 0;
        foreach (int i in nums)
            if (i != 0)
                nums[insertPos++] = i;
        while (insertPos < nums.Length)
            nums[insertPos++] = 0;
    }

    public bool HasPathSum(TreeNode root, int targetSum)
    {
        if (root == null)
        {
            if (targetSum == 0) return true;
            else return false;
        }
        return HasPathSum(root.left, targetSum - root.val) || HasPathSum(root.right, targetSum - root.val);
    }

    public IList<IList<int>> Permute(int[] nums)
    {

        List<IList<int>> final = new List<IList<int>>();
        if (nums.Length == 1)
        {
            final.Add(new List<int>(nums));
        }
        else
            PermuteUtil(final, new List<int>(), new List<int>(nums));
        return final;
    }
    void PermuteUtil(List<IList<int>> final, List<int> sofar, List<int> notUsedYet)
    {
        //start choosing number 
        if (notUsedYet.Count == 0)
        {
            final.Add(new List<int>(sofar.ToArray()));
            return;
        }
        for (int i = 0; i < notUsedYet.Count; i++)
        {
            int temp = notUsedYet[i];
            sofar.Add(notUsedYet[i]);
            notUsedYet.RemoveAt(i);
            PermuteUtil(final, sofar, notUsedYet);
            sofar.RemoveAt(sofar.Count - 1);
            notUsedYet.Insert(i, temp);
        }
    }

    public int[] FindSignatureCounts(int[] arr)
    {
        int[] count = new int[arr.Length];
        while (true)
        {
            foreach (int i in arr)
            {
                if (i != -1)
                {
                    //TODO: solve this shit
                }
            }
        }
    }

    public bool CanJump(int[] nums)
    {
        int n = nums.Length;
        if (n <= 1)
        {
            return true;
        }
        bool[] dp = new bool[n];//dp[i] = true if can jump to n-1 if start at i;
        Array.Fill(dp, false);
        dp[n - 1] = true;
        for (int i = n - 2; i >= 0; i--)
        {
            for (int j = 1; j <= nums[i]; j++)
            {//if dp[i+j] -> dp[i+nums[j]] is tru
                if (dp[i + j]) { dp[i] = true; break; }
            }
        }
        return dp[0];
    }

    int CanJump(int[] nums, int start, int[] dp)
    {
        if (start >= nums.Length - 1) return 1;// base case
        if (dp[start] != 0) return dp[start];// computed, just return
        for (int i = 1; i <= nums[start]; i++)
        {
            if (CanJump(nums, start + i, dp) == 1)
            {
                dp[start] = 1; break;
            }
        }
        return -1;
    }

    public bool CanJumpRecursion(int[] nums)
    {
        int[] dp = new int[nums.Length];
        //dp[i]=1 true, dp[i] =-1 flase, 0 
        CanJump(nums, 0, dp);
        return dp[0] == 1;
    }

    public IList<IList<int>> Combine(int n, int k)
    {
        List<IList<int>> final = new List<IList<int>>();
        List<int> notUseYet = new List<int>();
        for (int i = 1; i <= n; i++) notUseYet.Add(i);
        CombineUtil(final, 0, k, notUseYet, new List<int>());//so start from index 0, end at index n-1
        return final;
    }
    void CombineUtil(List<IList<int>> final, int start, int k, List<int> notUsedYet, List<int> sofar)
    {
        if (k == 0)
        {
            final.Add(new List<int>(sofar.ToArray()));
        }
        for (int i = start; i < notUsedYet.Count; i++)
        {
            int temp = notUsedYet[i];
            sofar.Add(temp);
            notUsedYet.RemoveAt(i);
            CombineUtil(final, i, k - 1, notUsedYet, sofar);
            sofar.RemoveAt(sofar.Count - 1);
            notUsedYet.Insert(i, temp);
        }
    }
    public bool WordExist(char[][] board, string word)
    {
        //say board only contains uppercae
        int m = board.Length; int n = board[0].Length;
        for (int i = 0; i < m; i++)
            for (int j = 0; j < n; j++)
                if (WordExistUtil(board, word, i, j, m, n))
                    return true;
        return false;
    }
    bool WordExistUtil(char[][] boardSofar, string remain, int row, int col, int m, int n)
    {
        if (remain.Length == 0) return true;//done
        if (row >= m || col >= n || row < 0 || col < 0) return false;//out of bound
        if (boardSofar[row][col] < 0) return false; //moved
        if (boardSofar[row][col] != remain[0]) return false; //if this move can't match the first character of remain
        boardSofar[row][col] = (char)-boardSofar[row][col]; //now it does, mark it as visited
        if (WordExistUtil(boardSofar, remain.Substring(1), row + 1, col, m, n) ||
            WordExistUtil(boardSofar, remain.Substring(1), row - 1, col, m, n) ||
            WordExistUtil(boardSofar, remain.Substring(1), row, col + 1, m, n) ||
            WordExistUtil(boardSofar, remain.Substring(1), row, col - 1, m, n)) return true;
        //now this move is not true, backtracking
        boardSofar[row][col] = (char)-boardSofar[row][col];
        return false;
    }

    public int[] KSwapSmallestArray(int[] arr, int k)
    {
        //TODO: solve this shit
        return new int[0];
    }

    public ListNode Partition(ListNode head, int x)
    {
        //howy, think simple
        ListNode smallerHead = new ListNode(), biggerHead = new ListNode();
        ListNode smaller = smallerHead, bigger = biggerHead;
        while (head != null)
        {
            if (head.val < x)
            {
                smaller = smaller.next = head;
            }
            else
            {
                bigger = bigger.next = head;
            }
            head = head.next;
        }
        smaller.next = biggerHead.next;
        bigger.next = null;
        return smallerHead.next;
    }

    public IList<int> FindDisappearedNumbers(int[] nums)
    {
        int[] appear = new int[nums.Length + 1];
        foreach (int i in nums)
        {
            appear[i]++;
        }
        List<int> final = new List<int>();
        for (int i = 1; i <= nums.Length; i++)
            if (appear[i] == 0)
                final.Add(i);
        return final;
    }

    public IList<int> ConstantSpaceMissingNumber(int[] nums)
    {
        //to mark i has appeared, goes to num[i-1] and negate the number
        for (int i = 0; i < nums.Length; i++)
        {
            int cur = Math.Abs(nums[i]);
            if (nums[cur - 1] > 0) nums[cur - 1] = -nums[cur - 1];
        }
        List<int> final = new List<int>();
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] > 0)
                final.Add(i + 1);//index i set to negative means i+1 is positive
        }
        return final;
    }

    public IList<string> LetterCombinations(string digits)
    {
        if (String.Compare(digits, "")==0) return new List<string>(new string[0]);
        Dictionary<int, List<char>> dict = new Dictionary<int, List<char>>();
        dict.Add(2, new List<char>(new char[3]{'a','b','c'}));
        dict.Add(3, new List<char>(new char[3]{'d','e','f'}));
        dict.Add(4, new List<char>(new char[3]{'g','h','i'}));
        dict.Add(5, new List<char>(new char[3]{'j','k','l'}));
        dict.Add(6, new List<char>(new char[3]{'m','n','o'}));
        dict.Add(7, new List<char>(new char[4]{'p','q','r','s'}));
        dict.Add(8, new List<char>(new char[3]{'t','u','v'}));
        dict.Add(9, new List<char>(new char[4]{'w','x','y','z'}));
        List<string> final = new List<string>();
        LetterCombinationsUtil(digits, new List<char>(), final, dict);
        return final;
    }

    void LetterCombinationsUtil(string remain, List<char> sofar, List<string> final, Dictionary<int, List<char>> dict) {
        if (remain==""){
            final.Add(new string(sofar.ToArray()));
            return;
        }
        int cur = (int)Char.GetNumericValue(remain[0]);
        foreach (char chr in dict[cur]) {
            sofar.Add(chr);
            LetterCombinationsUtil(remain.Substring(1), sofar, final, dict);
            sofar.RemoveAt(sofar.Count-1);
        }
    }   












    public class Robot
    {

        int width; int height;
        int x = 0; int y = 0;
        string Dir = "East";
        void ChangeDirection()
        {
            if (Dir == "East") Dir = "North";
            else if (Dir == "North") Dir = "West";
            else if (Dir == "West") Dir = "South";
            else Dir = "East";
        }
        void NStepForward(int num)
        {
            for (int i = 0; i < num; i++)
            {
                if (Dir == "East")
                {
                    if (x == width - 1) { ChangeDirection(); y++; }
                    else x++;
                }
                else if (Dir == "North")
                {
                    if (y == height - 1) { ChangeDirection(); x--; }
                    else y++;
                }
                else if (Dir == "West")
                {
                    if (x == 0) { ChangeDirection(); y--; }
                    else x--;
                }
                else if (Dir == "South")
                {
                    if (y == 0) { ChangeDirection(); x++; }
                    else y--;
                }
            }
        }
        public Robot(int width, int height)
        {// (0,0)=> (width-1, height-1)
            this.width = width; this.height = height;
        }

        public void Move(int num)
        {
            num %= 2 * (width + height) - 4;
            NStepForward(num);
        }

        public int[] GetPos()
        {
            return new int[2] { x, y };
        }

        public string GetDir()
        {
            return Dir;
        }
    }
}
// public class CombinationIterator {
//TODO1: Finish this shit
//     List<char> list = 
//     public CombinationIterator(string characters, int combinationLength) {

//     }

//     public string Next() {

//     }

//     public bool HasNext() {

//     }
// }





