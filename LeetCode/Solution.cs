using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
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
        int[] db = new int[n+1]; 
        Array.Fill(db, -1); //-1 is not found,0 is non-composable, >0 means yes
        foreach (int i in square) {
            db[i] = 1;
        }
        //TODO: fix this shit
        return 1;
    }
    int NumSquaresUtil(int n, List<int> squares, int[] db)
    {
        if (n<0) return -9999;
        if (IsSquare(n)) return 1;
        if (db[n] == -1)
        {
            int cur = 1;
            int min = int.MaxValue;
            while (cur*cur < n / 2)
            {
                int square = cur*cur;
                int temp = NumSquaresUtil(n - square, squares, db);
                if (temp!= -9999) {
                    min = Math.Min(temp+1, min);
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
    public string ReverseWords(string s) {
        int cur =0;
        
        Stack<string> str = new Stack<string>();
        List<char> chr = new List<char>();
        while (cur<s.Length) {
            if (s[cur]!=' ') {
                chr.Add(s[cur]);
                if (cur==s.Length-1) 
                    str.Push(new string(chr.ToArray()));
            }else {
                if (chr.Count>0){
                    string word = new string(chr.ToArray());
                    str.Push(word);
                    chr.Clear();
                }
            }
            cur++;
        }
        StringBuilder buffer = new StringBuilder();
        while (str.Count!=0) {
            buffer.Append(str.Pop());
            if (str.Count!=0) {
                buffer.Append(" ");
            }
        }
        return buffer.ToString();
    }
}

