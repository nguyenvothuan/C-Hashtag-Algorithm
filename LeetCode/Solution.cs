using System;
using System.Collections.Generic;
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
        if (String.Compare(digits, "") == 0) return new List<string>(new string[0]);
        Dictionary<int, List<char>> dict = new Dictionary<int, List<char>>();
        dict.Add(2, new List<char>(new char[3] { 'a', 'b', 'c' }));
        dict.Add(3, new List<char>(new char[3] { 'd', 'e', 'f' }));
        dict.Add(4, new List<char>(new char[3] { 'g', 'h', 'i' }));
        dict.Add(5, new List<char>(new char[3] { 'j', 'k', 'l' }));
        dict.Add(6, new List<char>(new char[3] { 'm', 'n', 'o' }));
        dict.Add(7, new List<char>(new char[4] { 'p', 'q', 'r', 's' }));
        dict.Add(8, new List<char>(new char[3] { 't', 'u', 'v' }));
        dict.Add(9, new List<char>(new char[4] { 'w', 'x', 'y', 'z' }));
        List<string> final = new List<string>();
        LetterCombinationsUtil(digits, new List<char>(), final, dict);
        return final;
    }

    void LetterCombinationsUtil(string remain, List<char> sofar, List<string> final, Dictionary<int, List<char>> dict)
    {
        if (remain == "")
        {
            final.Add(new string(sofar.ToArray()));
            return;
        }
        int cur = (int)Char.GetNumericValue(remain[0]);
        foreach (char chr in dict[cur])
        {
            sofar.Add(chr);
            LetterCombinationsUtil(remain.Substring(1), sofar, final, dict);
            sofar.RemoveAt(sofar.Count - 1);
        }
    }

    public IList<int> InorderTraversal(TreeNode root)
    {
        List<int> final = new List<int>();
        InorderTraversalUtil(root, final);
        return final;
    }
    void InorderTraversalUtil(TreeNode cur, List<int> final)
    {
        if (cur == null) return;
        InorderTraversalUtil(cur.left, final);
        final.Add(cur.val);
        InorderTraversalUtil(cur.right, final);
    }

    public bool IsSameTree(TreeNode p, TreeNode q)
    {
        if (p == null && q == null) return true;
        if (p == null || q == null) return false;
        if (p.val != q.val) return false;
        return IsSameTree(p.left, q.left) && IsSameTree(p.right, q.right);
    }

    public bool IsSymmetric(TreeNode root)
    {
        if (root == null) return true;
        Stack<TreeNode> stack = new Stack<TreeNode>();
        stack.Push(root.left);
        stack.Push(root.right);
        while (stack.Count > 0)
        {
            var n1 = stack.Pop(); var n2 = stack.Pop(); //we always push 2 nodes each repeat, so this will not RTE
            if (n1 == null && null == n2) continue;
            if (n1 == null || n2 == null || n1.val != n2.val) return false;
            stack.Push(n1.left); stack.Push(n2.right);
            stack.Push(n1.right); stack.Push(n2.left);
        }
        return true;
    }

    public string LargestNumber1(int[] num)
    {
        if (num == null || num.Length == 0) return "";
        var strArr = new string[num.Length];
        for (int i = 0; i < num.Length; i++)
            strArr[i] = num[i].ToString();
        var comparer = new CompareConcatString();
        Array.Sort(strArr, comparer);
        if (strArr[0] == "0") return "0";
        StringBuilder buffer = new StringBuilder();
        for (int i = num.Length - 1; i >= 0; i--)
            buffer.Append(strArr[i]);
        return buffer.ToString();
    }

    public bool CanThreePartsEqualSum(int[] arr)
    {
        int sum = 0;
        foreach (int i in arr) sum += i;
        if (sum % 3 != 0) return false;
        int count = 0; //if count == 2 return true since we have 2 parts that euqal sum/3 so the rest is also euqla sum/3
        int curSum = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            curSum += arr[i];
            if (curSum == sum / 3)
            {
                count++;
                if (count == 2 && i != arr.Length - 1) return true;
                curSum = 0;
            }
        }
        return false;
    }

    // public int SingleNonDuplicate(int[] nums)
    // {
    //     int n = nums.Length;
    //     if (n <= 2) return -1;
    //     int l = 0, r = n - 1;
    //     while (r > l)
    //     {//at least 3 numbers left
    //         int mid = (l + r) / 2;
    //         if (mid == 0 && nums[0] != nums[1]) return nums[0];
    //         if (mid == n - 1 && nums[n - 2] != nums[n - 1]) return nums[n - 1];
    //         if (mid!=0 && nums[mid] != nums[mid - 1] && mid!=n-1 && nums[mid] != nums[mid + 1])
    //             return nums[mid];
    //         if (mid!=0 && nums[mid] == nums[mid - 1])
    //         {
    //             if (mid - l + 1 % 2 == 0) l = mid + 1;
    //             else r = mid - 2;
    //         }
    //         else if (mid!=n-1 && nums[mid] == nums[mid + 1])
    //         {
    //             if (r - mid + 1 % 2 == 0) r = mid - 1; //from right to mid, there are an even number of elements
    //             else l = mid + 2;
    //         }
    //     }
    //     return nums[l];
    // }

    public int SingleNonDuplicate1(int[] nums)
    {
        int l = 0, r = nums.Length - 1;
        while (l < r)
        {
            int mid = (l + r) / 2;
            if (mid % 2 == 1) mid--;// odd number of element in the left
            if (nums[mid] != nums[mid + 1]) r = mid;
            else l = mid + 2;
        }
        return nums[l];
    }

    public int SearchInsert(int[] nums, int target)
    {
        if (nums.Length == 1)
        {
            if (nums[0] >= target) return 0;
            else return 1;
        }
        int l = 0, r = nums.Length - 1;
        int mid;
        while (l < r)
        {
            mid = (l + r) / 2;
            if (nums[mid] == target) return mid;
            if (nums[mid] > target) r = mid - 1;
            else l = mid + 1;
        }
        return target <= nums[l] ? l : l + 1;
    }

    public int SqrtWithBinarySearch(long x)
    {
        //search from 1 to x/2;
        long l = 1, r = x / 2;
        while (r > l)
        {
            long mid = (l + r) / 2;
            if (mid * mid == x) return (int)mid;
            if (mid * mid < x) l = mid + 1;
            else r = mid - 1;
        }
        return (int)(l * l > x ? l - 1 : l);
    }

    public int[] FairCandySwap(int[] aliceSizes, int[] bobSizes)
    {
        Array.Sort(aliceSizes); Array.Sort(bobSizes);
        int sum1 = 0, sum2 = 0;
        foreach (int i in aliceSizes) sum1 += i;
        foreach (int i in bobSizes) sum2 += i;
        int halfSum = sum1 - sum2 / 2;
        if (aliceSizes.Length > bobSizes.Length)
        {
            //search in the greater array
            foreach (int i in bobSizes)
            {
                // int complement = half
            }
        }
        return new int[0];
    }

    public void ReverseString(char[] s)
    {
        ReverseStringUtil(s, 0, s.Length - 1);
    }
    void ReverseStringUtil(char[] s, int l, int r)
    {
        if (l >= r) return;
        Swap(ref s[l], ref s[r]);
        ReverseStringUtil(s, l + 1, r - 1);
    }

    void Swap(ref char c1, ref char c2)
    {
        char temp = c1; c1 = c2; c2 = temp;
    }
    public bool HasPathSum1(TreeNode root, int targetSum)
    {
        if (targetSum == 0) return true;
        targetSum -= root.val;
        if (targetSum < 0) return false;
        return HasPathSum1(root.left, targetSum) || HasPathSum1(root.right, targetSum);
    }

    public TreeNode SortedArrayToBSTNonBalanced(int[] nums)
    {
        TreeNode root = new TreeNode(nums[0]);
        foreach (int i in nums)
        {
            var cur = root;
            var pre = root;
            while (cur != null)
            {
                if (cur.val > i) { pre = cur; cur = cur.left; }
                else { pre = cur; cur = cur.right; }
            }
            if (pre.val > i) pre.left = new TreeNode(i);
            else pre.right = new TreeNode(i);
        }
        return root;
    }

    TreeNode SortedArrayToBSTUtil(int[] nums, int start, int end)
    {
        if (end < start || end >= nums.Length || start < 0) return null;
        if (end == start) return new TreeNode(nums[start]);
        int mid = (end + start) / 2;
        TreeNode root = new TreeNode(nums[mid]);
        root.right = SortedArrayToBSTUtil(nums, mid + 1, end);
        root.left = SortedArrayToBSTUtil(nums, mid - 1, start);
        return root;
    }

    public TreeNode SortedArrayToBST(int[] nums)
    {
        return SortedArrayToBSTUtil(nums, 0, nums.Length - 1);
    }

    public bool IsPalindrome(string s)
    {
        if (s.Length <= 1) return true;
        int l = 0; int r = s.Length - 1;
        while (r >= l)
        {
            while (Char.ToLower(s[r]) > 'z' || Char.ToLower(s[r]) < 'a' && r >= l && r > 0)
                r--;
            while (Char.ToLower(s[l]) > 'z' || Char.ToLower(s[l]) < 'a' && l <= r && l < s.Length - 1)
                l++;
            if (Char.ToLower(s[l]) != Char.ToLower(s[r])) return false;
            l++; r--;
        }
        return true;
    }

    public int[][] IntervalIntersection(int[][] firstList, int[][] secondList)
    {
        int first = 0;
        int second = 0;
        List<int[]> final = new List<int[]>();
        while (second < secondList.Length && first < firstList.Length)
        {
            var union = IntervalUnion(firstList[first], secondList[second]);
            if (union.Length == 1)
            {
                if (union[0] == 0) first++;
                else second++;
            }
            else
            {
                final.Add(union);
                if (union[1] == secondList[second][1]) second++;
                else first++;
            }
        }
        return final.ToArray();
    }
    int[] IntervalUnion(int[] i1, int[] i2)
    {
        if (i2[0] > i1[1]) return new int[1] { 0 };// 1 is exhausted
        if (i1[0] > i2[1]) return new int[1] { 1 }; //2 is exhausted
        return new int[2] { Math.Max(i1[0], i2[0]), Math.Min(i1[1], i2[1]) };
    }

    public int CastleOnTheHill(int[] arr)
    {
        int count = 1;
        int trend = arr[1] > arr[0] ? 1 : -1; // 1 for up, -1 for down;
        for (int i = 1; i < arr.Length; i++)
        {
            if (arr[i] == arr[i - 1]) continue; //nothing happens
            else
            {
                if (arr[i] > arr[i - 1] && trend < 0)
                {
                    count++;
                    trend = 1; // up again
                }
                else if (arr[i] < arr[i - 1] && trend > 0)
                {
                    count++;
                    trend = -1;
                }
            }
        }
        return count;
    }

    public int ScoreFromTestCase(string[] T, string[] R)
    {
        Dictionary<string, int> dict = new Dictionary<string, int>();
        // dict[taskname] = score of the current task name, if set to negative, take 0
        for (int i = 0; i < T.Length; i++)
        {
            string taskname = RetrieveTaskName(T[i]);
            if (dict.ContainsKey(taskname))
            {
                if (dict[taskname] > 0)
                {
                    if (R[i] == "OK") dict[taskname]++;
                    else dict[T[i]] = -1;
                }
            }
            else
            {
                if (R[i] == "OK")
                {
                    dict.Add(taskname, 1);
                }
                else
                {
                    dict.Add(taskname, -1);
                }
            }
        }
        int count = 0;
        foreach (var pair in dict)
        {
            if (pair.Value > 0) count++;
        }
        return (int)count * 100 / dict.Count;
    }
    public string RetrieveTaskName(string str)
    {
        int i = 0;
        bool passed = false;
        for (i = 0; i < str.Length; i++)
        {
            if (Char.IsNumber(str[i]))
                passed = true;
            if (passed && Char.IsLetter(str[i])) break;
        }
        return str.Substring(0, i);
    }

    public int[] ForgottenShootingDice(int[] A, int F, int M)
    {
        int sum = 0;
        foreach (int i in A) sum += i;
        int remain = M * (F + A.Length) - sum;
        List<int> final = new List<int>();
        if (ForgottenShootingDiceUtil(remain, F, final))
            return final.ToArray();
        return new int[1] { 0 };
    }
    bool ForgottenShootingDiceUtil(int sofar, int stepRemain, List<int> final)
    {//sofar is the sum left to be accumulated, stepRemain is the number of roll left
        if (sofar < 0 || stepRemain < 0) return false;
        if (sofar == 0 && stepRemain == 0) return true; //path found
        if (sofar == 0 || stepRemain == 0) return false;
        for (int i = 1; i <= 6; i++)
        {
            sofar -= i;
            final.Add(i);
            if (ForgottenShootingDiceUtil(sofar, stepRemain - 1, final)) return true;
            //backtracking
            sofar += i;
            final.RemoveAt(final.Count - 1);
        }
        //try all path without success
        return false;
    }

    public int CastleInTheSky(int[] arr)
    {
        if (arr.Length < 2) return arr.Length;
        int count = 1;
        int trend = 0;
        //count plus at the end of trend.
        for (int i = 0; i < arr.Length; i++)
        {
            if (trend == 0)
            {
                if (i != 0 && arr[i] != arr[i - 1])
                {
                    count++;
                    trend = arr[i] > arr[i - 1] ? 1 : -1;
                }
            }
            else
            {
                if (trend > 0 && arr[i] < arr[i - 1])
                {
                    trend = -1;
                    count++;
                }
                else if (trend < 0 && arr[i] > arr[i - 1])
                {
                    trend = 1;
                    count++;
                }
            }
        }
        return count;
    }

    public bool CanBeIncreasing(int[] nums)
    {
        // nums is increasing, means 
        // 1. 
        if (nums.Length <= 2) return true;
        int count = 0;
        for (int i = 1; i < nums.Length; i++)
        {
            if (nums[i] < nums[i - 1]) count++;
            if (count > 1) return false;
        }
        return true;
    }

    public int MinPairSum(int[] nums)
    {
        Array.Sort(nums);
        int maxSum = 0;
        for (int i = 0; i < nums.Length / 2; i++)
        {
            maxSum = Math.Max(nums[i] + nums[nums.Length - i - 1], maxSum);
        }
        return maxSum;
    }

    public bool IsHappy(int n) { return IsHappy(new List<int>(), n); }
    public bool IsHappy(List<int> sofar, int n)
    {
        if (n == 1) return true;
        int index = sofar.BinarySearch(n);
        if (index > 0) return false;
        index = -index;
        if (index >= sofar.Count) sofar.Add(n);
        else sofar.Insert(index, n);
        return IsHappy(sofar, SquareDigitSum(n));
    }
    int SquareDigitSum(int n)
    {
        string str = n.ToString();
        int sum = 0;
        foreach (char chr in str)
        {
            sum += (int)Char.GetNumericValue(chr) * (int)Char.GetNumericValue(chr);
        }
        return sum;
    }

    public bool IsValidAbbreviation(string str1, string str2)
    {
        if (str1 == str2) return true;
        if (str1.Length == str2.Length) return false;
        if (str1.Length < str2.Length) return IsValidAbbreviation(str2, str2);
        int p1 = 0; int p2 = 0;
        while (p1 < str1.Length && p2 < str2.Length)
        {
            int jump = 0;
            while (p2 < str2.Length && Char.IsNumber(str2[p2])) { jump += 10 * jump + Int32.Parse(str2[p2].ToString()); p2++; }

            //now that p2 is pointing to the next character after the abbreviated string
            p1 += jump;
            if (p1 >= str1.Length) return false; //fake abbreviation
            if (p2 == str2.Length) return true; // 
            if (str1[p1] != str2[p2]) return false;
            jump = 0;
            while (p2 < str2.Length && Char.IsLetter(str2[p2]))
                if (str1[p1++] != str2[p2++]) return false;
            //now p2 is pointing the next character in p2, which is a number, the cycle continues or str2 is exhausted
        }
        return p1 == str1.Length;
    }

    public int[] ProductExceptSelf(int[] nums)
    {
        int countZero = 0;
        int zeroIndex = 0;
        int product = 1;
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] == 0) { zeroIndex = i; countZero++; }
            else product *= nums[i];
        }
        int[] final = new int[nums.Length];
        if (countZero > 1) return final;
        if (countZero == 1)
        {
            final[zeroIndex] = product;
            return final;
        }
        for (int i = 0; i < final.Length; i++)
        {
            final[i] = product / nums[i];
        }
        return final;
    }

    public int CountBinarySubstrings(string s)
    {
        List<int> list = new List<int>();
        int cur = 48;
        int countCur = 0;
        foreach (char i in s)
        {
            if (i == cur) countCur++;
            else
            {
                cur = i;
                list.Add(countCur);
                countCur = 1;
            }
        }
        list.Add(countCur);
        if (list.Count == 1) return 0;
        if (list.Count == 2) return Math.Min(list[0], list[1]);

        int final = 0;
        for (int i = 0; i < list.Count - 1; i++)
        {
            final += Math.Min(list[i], list[i + 1]);
        }
        return final;
    }

    public void DuplicateZeros(int[] arr)
    {
        //TODO: review this shit
    }

    char IntToChar(int a)
    {
        return (char)(a + 48);
    }

    public string AddStrings(string num1, string num2)
    {
        if (num2.Length > num1.Length) return AddStrings(num2, num1);
        //num1 > num2
        int left = 0;
        int p1 = num1.Length - 1, p2 = num2.Length - 1;
        List<char> list = new List<char>();
        while (p1 >= 0 && p2 >= 0)
        {
            int cur = CharToInt(num1[p1]) + CharToInt(num2[p2]) + left;
            if (cur >= 10) { cur -= 10; left = 1; }
            list.Add(IntToChar(cur));
            p1--; p2--;
        }
        if (p1 < 0 && p2 < 0)
        {
            if (left != 0) list.Add(IntToChar(1));
        }
        while (p1 >= 0)
        {
            int cur = IntToChar(CharToInt(num1[p1--]) + left);
            if (cur >= 10) cur -= 10; left = 1;
        }
        if (left != 0) list.Add(IntToChar(1));
        list.Reverse();
        return new string(list.ToArray());
    }

    public IList<IList<int>> AllPathsSourceTarget(int[][] graph)
    {
        int V = graph.Length;
        int[][] dp = new int[V][];
        for (int i = 0; i < V; i++) dp[i] = null;
        Reachable(0, dp, graph);
        Stack<int> stack = new Stack<int>();
        stack.Push(0);
        List<IList<int>> final = new List<IList<int>>();
        DFSReachableUtil(final, new List<int>(), 0, graph.Length, dp);
        return final;
    }
    void DFSReachableUtil(List<IList<int>> final, List<int> sofar, int cur, int n, int[][] dp)
    {// n is the size of graph, not n-1
        sofar.Add(cur);
        if (cur == n - 1)
        {
            final.Add(new List<int>(sofar));
            sofar.RemoveAt(sofar.Count - 1);
            return;
        }

        foreach (int i in dp[cur])
        {
            DFSReachableUtil(final, sofar, i, n, dp);
        }
        sofar.RemoveAt(sofar.Count - 1);
    }

    int[] Reachable(int u, int[][] dp, int[][] graph)
    {
        if (dp[u] == null)
        {//now it is null
            List<int> final = new List<int>();
            foreach (int neighborOfU in graph[u])
            {
                if (neighborOfU == graph.Length - 1 || Reachable(neighborOfU, dp, graph).Length > 0)
                {
                    final.Add(neighborOfU);
                }
            }
            dp[u] = final.ToArray();
        }
        return dp[u];
    }

    public int[][] SplittingArray(int[] arr)
    {
        Array.Sort(arr);
        int cur = 0;
        int[][] final = new int[2][];
        final[0] = new int[arr.Length / 2];
        final[1] = new int[arr.Length / 2];
        int p1 = 0; int p2 = 0;

        while (cur < arr.Length)
        {
            if (cur < arr.Length - 2 && arr[cur] == arr[cur + 1] && arr[cur + 1] == arr[cur + 2])// more than 2
                return new int[0][];
            if (cur < arr.Length - 1 && arr[cur] == arr[cur + 1])
            {
                final[0][p1++] = arr[cur++];
                final[1][p2++] = arr[cur++];
            }
            else
            {
                if (p1 > p2) final[1][p2++] = arr[cur++];
                else final[0][p1++] = arr[cur++];
            }

        }
        return final;
    }

    public string[] FullJustify(string[] words, int l)
    {
        List<string> final = new List<string>();
        if (words == null || words.Length == 0)
            return final.ToArray();

        int i = 0;
        int n = words.Length;

        while (i < n)
        {
            int j = i + 1;
            int lineLength = words[i].Length;
            int spaces = j - i - 1;

            while (j < n && (lineLength + words[j].Length + (j - i - 1)) < l)
            {

                lineLength += words[j].Length;
                ++j;
            }

            int diff = l - lineLength;
            int numberOfWords = j - i;

            if (numberOfWords == 1 || j >= n) final.Add(LeftJustify(words, diff, i, j));
            else
                final.Add(MidJustify(words, diff, i, j));

            i = j;
        }
        return final.ToArray();
    }

    public string LeftJustify(string[] words, int diff, int i, int j)
    {
        int spacesOnRight = diff - (j - i - 1);
        StringBuilder result = new StringBuilder(words[i]);

        for (int k = i + 1; k < j; k++)
        {
            result.Append(" " + words[k]);
        }
        for (int k = 1; k <= spacesOnRight; k++)
        {
            result.Append(" ");
        }

        return result.ToString();
    }

    public string MidJustify(string[] words, int diff, int i, int j)
    {
        int spaceNeeded = j - i - 1;
        int spaces = diff / spaceNeeded;
        int extraSpace = diff % spaceNeeded;

        StringBuilder result = new StringBuilder(words[i]);

        for (int k = i + 1; k < j; k++)
        {
            int spacesToApply = spaces + (extraSpace-- > 0 ? 1 : 0);
            for (int l = 1; l <= spacesToApply; l++)
            {
                result.Append(" ");
            }
            result.Append(words[k]);
        }

        return result.ToString();
    }

    public int WordTransformation(string beginWord, string endWord, string[] wordList)
    {
        Dictionary<string, List<string>> adj = new Dictionary<string, List<string>>();
        //adj list to save connectivity of each word
        for (int i = 0; i < wordList.Length; i++)
        {
            if (!adj.ContainsKey(wordList[i])) adj.Add(wordList[i], new List<string>());
            for (int j = i + 1; j < wordList.Length; j++)
            {
                if (IsConnectedWord(wordList[i], wordList[j]))
                {
                    adj[wordList[i]].Add(wordList[j]);
                    if (adj.ContainsKey(wordList[j])) adj[wordList[j]].Add(wordList[i]);
                    else adj.Add(wordList[j], new List<string>(new string[1] { wordList[i] }));
                }
            }
        }
        List<string> neighborOfBegin = new List<string>();
        foreach (string str in wordList)
        {
            if (IsConnectedWord(beginWord, str)) neighborOfBegin.Add(str);
        }
        List<string> neighborOfEnd = new List<string>();
        foreach (string str in wordList)
        {
            if (IsConnectedWord(endWord, str)) neighborOfEnd.Add(str);
        }
        adj.Add(beginWord, neighborOfBegin); adj.Add(endWord, neighborOfEnd);
        //BFS
        Queue<string> queue = new Queue<string>();
        queue.Enqueue(beginWord);
        queue.Enqueue(null);
        int count = 0;
        while (queue.Count != 0)
        {
            string cur = queue.Dequeue();
            if (cur == endWord) return count;
            if (cur == null)
            {
                queue.Enqueue(null);
                count++;
            }
            else
            {
                foreach (string neighbor in adj[cur])
                {
                    queue.Enqueue(neighbor);

                }
            }
        }
        return count;
    }

    bool IsConnectedWord(string str1, string str2)
    {
        int count = 0;
        for (int i = 0; i < str1.Length; i++)
        {
            if (str1[i] != str2[i]) count++;
            if (count > 1) return false;
        }
        return count != 0;
    }

    public IList<IList<string>> AccountsMerge(IList<IList<string>> accounts)
    {
        int n = accounts.Count;
        bool[,] adj = new bool[n, n];
        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                if (accounts[i][0] == accounts[j][0])
                {
                    if (CheckCommonEmail(accounts[i], accounts[j]))
                    {
                        adj[i, j] = adj[j, i] = true;
                    }
                }
            }
        }
        bool[] visited = new bool[n];
        IList<IList<string>> final = new List<IList<string>>();
        EmailComparer comp = new EmailComparer();
        for (int i = 0; i < n; i++)
        {
            if (!visited[i])
            {
                List<string> sofar = new List<string>();
                sofar.Add(accounts[i][0]);//name first
                DFSEmailUtil(i, visited, adj, accounts, sofar);
                final.Add(sofar);
                sofar.Sort(1, sofar.Count - 1, comp);
            }
        }
        return final;
    }
    void DFSEmailUtil(int cur, bool[] visited, bool[,] adj, IList<IList<string>> accounts, List<string> sofar)
    {
        //run dfs for the current accounts, each time encounter a new descendant, add all its email to sofar.
        foreach (string email in accounts[cur])
            if (!sofar.Contains(email)) sofar.Add(email);
        visited[cur] = true;
        for (int i = 0; i < accounts.Count; i++)
        {
            if (adj[cur, i] && !visited[i])
            {
                DFSEmailUtil(i, visited, adj, accounts, sofar);
            }
        }
    }
    bool CheckCommonEmail(IList<string> acc1, IList<string> acc2)
    {
        for (int i = 1; i < acc1.Count; i++)
        {
            for (int j = 1; j < acc2.Count; j++)
            {
                if (acc1[i] == acc2[j])
                    return true;
            }
        }
        return false;
    }
    public int LargestSumAfterKNegations(int[] nums, int k)
    {
        int[] toNegate = FindKSmallest(k, nums);
        int sum = 0; foreach (int i in toNegate) sum += i;
        int final = 0;
        foreach (int i in nums) final += i;
        final -= 2 * sum;
        return final;
    }

    public int[] FindKSmallest(int k, int[] arr)
    {
        if (arr.Length <= k) return new int[0];
        //repeat k time. Push smallest to the right furthest.
        int[] final = new int[k];
        for (int i = 0; i < k; i++)
        {
            //finding the ith smallest index
            for (int j = 0; j < arr.Length - 1 - i; j++)
            {
                if (arr[j] < arr[j + 1])
                {
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
            }
        }
        for (int i = 0; i < k; i++)
        {
            final[i] = arr[arr.Length - 1 - i];
        }
        return final;
    }



    public int MaximalRectangle(char[][] matrix)
    {
        return 1;
    }

    public IList<int> RightSideView(TreeNode root)
    {
        if (root == null) return new List<int>();
        Queue<TreeNode> queue = new Queue<TreeNode>();
        queue.Enqueue(root);
        queue.Enqueue(null);
        TreeNode prev = null;
        List<int> final = new List<int>();
        while (queue.Count != 0)
        {
            TreeNode cur = queue.Dequeue();
            if (cur == null)
            {
                final.Add(prev.val);
                if (queue.Count != 0)
                    queue.Enqueue(null);
            }
            else
            {
                if (cur.left != null) queue.Enqueue(cur.left);
                if (cur.right != null) queue.Enqueue(cur.right);
                prev = cur;
            }
        }
        return final;
    }

    public int FindTilt(TreeNode root)
    {
        if (root == null) return 0;
        return Math.Abs(SubtreeSum(root.left) - SubtreeSum(root.right)) + FindTilt(root.left) + FindTilt(root.right);
    }
    int SubtreeSum(TreeNode root)
    {
        if (root == null) return 0;
        return root.val + SubtreeSum(root.left) + SubtreeSum(root.right);
    }

    public bool IsCousins(TreeNode root, int x, int y)
    {
        //bfs for root
        Queue<TreeNode> queue = new Queue<TreeNode>();
        queue.Enqueue(root);
        queue.Enqueue(null);
        bool foundX = false, foundY = false;
        bool justAdded = false;
        while (queue.Count != 0)
        {
            TreeNode cur = queue.Dequeue();
            if (cur == null) //end of a lv 
            {
                if (foundX ^ foundY) return false;
                if (foundX && foundY) return true;
                if (queue.Count != 0) queue.Enqueue(null);
            }
            else
            {
                if (!justAdded)
                {
                    if (cur.val == x) { foundX = true; justAdded = true; }
                    else if (cur.val == y) { foundY = true; justAdded = true; }
                    else justAdded = false;
                }
                else
                {
                    justAdded = false;
                }

                if (foundX && foundY) return true;

                if (cur.left != null) queue.Enqueue(cur.left);
                if (cur.right != null) queue.Enqueue(cur.right);
            }
        }
        return foundX && foundY;
    }

    public bool LeafSimilar(TreeNode root1, TreeNode root2)
    {
        if (root1 == root2) return true;
        //https://leetcode.com/problems/leaf-similar-trees/
        List<int> l1 = LeafSequence(root1), l2 = LeafSequence(root2);
        if (l1.Count != l2.Count) return false;
        for (int i = 0; i < l1.Count; i++)
            if (l1[i] != l2[i])
                return false;
        return true;
    }
    List<int> LeafSequence(TreeNode root)
    {
        Stack<TreeNode> stack = new Stack<TreeNode>();
        stack.Push(root);
        List<int> final = new List<int>();
        while (stack.Count != 0)
        {
            var cur = stack.Pop();
            var children = Children(cur);
            if (children == null) final.Add(cur.val);
            else
            {
                foreach (var child in children) stack.Push(child);
            }
        }
        return final;
    }
    TreeNode[] Children(TreeNode root)
    {
        if (root == null || (root.left == null && root.right == null)) return null;
        if (root.left == null) return new TreeNode[1] { root.right };
        if (root.right == null) return new TreeNode[1] { root.left };
        return new TreeNode[2] { root.left, root.right };
    }

    public int MinimumTotal(IList<IList<int>> triangle)
    {
        if (triangle.Count == 1) return triangle[0][0];
        //https://leetcode.com/problems/triangle/
        int[][] dp = new int[triangle.Count][];
        for (int i = 0; i < triangle.Count; i++)
        {
            dp[i] = new int[i + 1];
            Array.Fill(dp[i], int.MaxValue);
        }
        MinimumTotalUtil(0, 0, dp, triangle);
        return dp[0][0];
    }
    int MinimumTotalUtil(int i, int j, int[][] dp, IList<IList<int>> triangle)
    {
        if (j > i || i >= dp.Length) return int.MaxValue;
        if (dp[i][j] != int.MaxValue) return dp[i][j];
        if (i == dp.Length - 1) return triangle[i][j];
        dp[i][j] = triangle[i][j] + Math.Min(MinimumTotalUtil(i + 1, j, dp, triangle), MinimumTotalUtil(i + 1, j + 1, dp, triangle));
        return dp[i][j];
    }

    public bool IsInterleave(string s1, string s2, string s3)
    {
        if (s1.Length + s2.Length != s3.Length) return false;
        //https://leetcode.com/problems/interleaving-string/
        int[,] dp = new int[s1.Length, s2.Length];//dp[i,j] is whether we can solve this problem if we choose to break at i and j.
        // that being said, given s1[i, n-1] and s2[j, m-1] if we can create a target from s3[i+j, m+n-1];
        //this util goes 1 character per recursion.
        return IsInterleavingUtil(s1, 0, s2, 0, s3, 0, dp);
    }
    bool IsInterleavingUtil(string s1, int i, string s2, int j, string s3, int k, int[,] dp)
    {
        //start at i for s1, j s2, and i+j for s3 
        if (i == s1.Length) return s2.Substring(j) == s3.Substring(k);
        if (j == s2.Length) return s1.Substring(i) == s3.Substring(k);
        if (dp[i, j] != 0) return dp[i, j] == 1;
        bool ans = (s1[i] == s3[k] && IsInterleavingUtil(s1, i + 1, s2, j, s3, k + 1, dp)) || (s2[j] == s3[k] && IsInterleavingUtil(s1, i, s2, j + 1, s3, k + 1, dp));
        dp[i, j] = ans ? 1 : -1;
        return ans;
    }


    public bool CheckSubarraySum(int[] nums, int k)
    {
        if (k == 0) return false;
        Dictionary<int, int> dict = new Dictionary<int, int>();
        //(running sum at index mod k, index)
        dict.Add(0, -1); //initially 0;
        int running = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            running += nums[i];
            running %= k;
            if (dict.ContainsKey(running))
            {
                if (i - dict[running] > 1) return true;
            }
            else dict.Add(running, i);
        }
        return false;
    }

    public ListNode OddEvenList(ListNode head)
    {
        if (head == null) return null;
        ListNode odd = head;
        ListNode evenHead = head.next;
        ListNode even = evenHead;
        while (even != null && even.next != null)
        {
            odd.next = odd.next.next;
            even.next = even.next.next;
            odd = odd.next;
            even = even.next;
        }
        odd.next = evenHead;
        return head;
    }

    public int MaxProduct(int[] nums)
    {
        if (nums.Length == 1) return nums[0];
        int[] dpPos = new int[nums.Length]; //dp[i] = largest product that must end at i
        int[] dpNeg = new int[nums.Length]; //in case nums[i] is negative, we will want dp[i-1] to be  negative too. SO for this one, we store the smallest product ever that end at i
        for (int i = 0; i < nums.Length; i++)
        {
            dpPos[i] = nums[i];
            dpNeg[i] = nums[i];
        }
        int max = dpPos[0];
        bool hasZero = nums[0] == 0;
        for (int i = 1; i < nums.Length; i++)
        {
            if (nums[i] == 0) hasZero = true;
            if (nums[i - 1] == 0 || nums[i] == 0) { dpNeg[i] = nums[i]; dpPos[i] = nums[i]; }
            else
            {//let's not take 0 into account
                dpPos[i] = Math.Max(nums[i], Math.Max(dpPos[i - 1] * nums[i], dpNeg[i - 1] * nums[i]));
                dpNeg[i] = Math.Min(nums[i], Math.Min(dpPos[i - 1] * nums[i], dpNeg[i - 1] * nums[i]));
            }
            max = Math.Max(max, dpPos[i]);
        }
        return hasZero ? Math.Max(0, max) : max;
    }
    public int CompareVersion(string version1, string version2)
    {
        int cur1 = 0;
        int cur2 = 0;
        int p1 = 0, p2 = 0;
        while (p1 < version1.Length && p2 < version2.Length)
        {
            int last1 = cur1; int last2 = cur2;
            while (p1 < version1.Length && version1[p1] != '.') p1++;//after this p1 is pointing to .
            while (p2 < version2.Length && version2[p2] != '.') p2++;//after this p1 is pointing to .
            int res = String.Compare(version1.Substring(last1, cur1 - last1), version2.Substring(last2, cur2 - last2));
            if (res != 0) return res;
            p1++; p2++;
        }
        if (p1 == version1.Length && p2 == version2.Length) return 0;
        return p1 == version1.Length ? -1 : 1;
    }

    public int FindDuplicate(int[] nums)
    {
        int XOR = 0;
        int XORshouldbe = 0;
        foreach (int i in nums) XOR ^= i;
        for (int i = 1; i <= nums.Length; i++) XORshouldbe ^= i;
        return XORshouldbe ^ XOR;
    }

    public int ShipWithinDays(int[] weights, int days)
    {
        //https://leetcode.com/problems/capacity-to-ship-packages-within-d-days/
        int left = 0, right = 0;
        for (int i = 0; i < weights.Length; i++)
        {
            right += weights[i];
            left = Math.Max(left, weights[i]);
        }
        while (left < right)
        {
            int mid = (left + right) / 2, need = 1, cur = 0;
            foreach (int weight in weights)
            {
                if (cur + weight > mid) { need += 1; cur = 0; }//once the cargo exceeds the ship's load
                cur += weight;
            }
            if (need > days) left = mid + 1;
            else right = mid;
        }
        return left;
    }

    bool IsVowel(char chr)
    {
        return chr == 'a' || chr == 'e' || chr == 'i' || chr == 'o' || chr == 'u' || chr == 'A' || chr == 'E' || chr == 'I' || chr == 'O' || chr == 'U';
    }

    public string ReverseVowels(string s)
    {
        if (s.Length == 1) return s;
        if (s.Length == 2)
        {
            if (IsVowel(s[0]) && IsVowel(s[1])) return new string(new char[2] { s[1], s[0] });
            return s;
        }

        StringBuilder buffer = new StringBuilder(s);
        int left = 0, right = s.Length - 1;
        while (right > left)
        {
            while (left < right && !IsVowel(s[left])) left++;
            while (right > left && !IsVowel(s[right])) right--;
            char temp = buffer[left]; buffer[left++] = buffer[right]; buffer[right--] = temp;
        }
        return buffer.ToString();
    }

    public string PushDominoes(string dominoes)
    {
        //TODO: https://leetcode.com/problems/push-dominoes/
        return "";

    }
    public int FindPeakElement(int[] nums)
    {
        //https://leetcode.com/problems/find-peak-element/
        int left = 0, right = nums.Length - 1;
        while (nums[left + 1] > nums[left]) { left++; if (left == nums.Length - 1) return nums.Length - 1; }
        while (nums[right - 1] > nums[right]) { right--; if (right == 0) return 0; }
        while (left < right)
        {
            int mid = (left + right) / 2;
            if (nums[mid] > nums[mid - 1])
            {
                if (nums[mid] > nums[mid + 1]) return mid;
                else left = mid;
            }
            else
            {
                right = mid;
            }
        }
        return left;
    }



    public bool IsNumber(string str)
    {
        bool flagDot = str[0] == '.';
        for (int i = 1; i < str.Length; i++)
        {
            if (str[i] == '.')
            {
                if (flagDot) return false;
                else flagDot = true;
            }
            else
            {
                if (str[i] > '9' || str[i] < '0') return false;
            }
        }
        return true;
    }

    public int CountNodes(TreeNode root)
    {
        //https://leetcode.com/problems/count-complete-tree-nodes/
        return 01;
    }

    public string Translate(string test)
    {
        StringBuilder buffer = new StringBuilder();
        for (int i = 0; i < test.Length; i++)
        {
            if (IsVowel(test[i]) && i > 0 && !IsVowel(test[i - 1]))
            {
                buffer.Append("av");

            }
            buffer.Append(test[i]);
        }
        return buffer.ToString();
    }

    public int FindNetworkEndpoint(int startNodeId, int[] fromIds, int[] toIds)
    {
        Dictionary<int, List<int>> adj = new Dictionary<int, List<int>>();
        for (int i = 0; i < fromIds.Length; i++)
        {
            int from = fromIds[i], to = toIds[i];
            if (adj.ContainsKey(from)) adj[i].Add(to);
            else adj.Add(from, new List<int>(new int[1] { to }));
        }
        return DFSUtil(adj, new List<int>(), startNodeId);
    }
    int DFSUtil(Dictionary<int, List<int>> adj, List<int> visited, int cur)
    {
        //from current, visit all of its neighbors, if all are visited, return itself. Else, repeat the dfs for each neighbor, if a neighor is spotted to be an endpoint, return that neighbor
        if (!adj.ContainsKey(cur)) return -cur;
        visited.Add(cur);
        bool flag = false;
        int waiting = -1;
        foreach (int i in adj[cur])
        {
            if (!visited.Contains(i))
            {
                flag = true;
                int ran = DFSUtil(adj, visited, i);
                if (ran < 0) return -ran;
                if (waiting < 0) waiting = ran;
            }
        }
        if (!flag) return cur;
        //here no node is spotted means this is a circle
        return waiting;


    }

    public int FindSmallestInterval(int[] numbers)
    {
        Array.Sort(numbers);
        int min = int.MaxValue;
        for (int i = 1; i < numbers.Length; i++)
        {
            if (numbers[i] - numbers[i - 1] < min)
                min = numbers[i] - numbers[i - 1];
        }
        return min;
    }

    public int ComputeJoinPoint(int s1, int s2)
    {
        if (s1 == s2) return s1;
        while (s1 != s2)
        {
            if (s1 == 0 || s2 == 0) return -1;// no way
            if (s1 > s2)
            {
                s2 = GetSum(s2);
            }
            else
            {
                s1 = GetSum(s1);
            }
        }
        return s1;
    }

    public int GetSum(int num)
    {
        int sum = num;
        while (num > 0)
        {
            sum += num % 10;
            num /= 10;
        }
        sum += num;
        return sum;
    }


    public int Rob(TreeNode root)
    {
        //https://leetcode.com/problems/house-robber-iii/
        Dictionary<TreeNode, int> withThisNode = new Dictionary<TreeNode, int>();
        Dictionary<TreeNode, int> withoutThisNode = new Dictionary<TreeNode, int>();
        return Math.Max(RobWithUtil(withThisNode, withoutThisNode, root), RobWithoutUtil(withThisNode, withoutThisNode, root));
    }
    int RobWithUtil(Dictionary<TreeNode, int> withThisNode, Dictionary<TreeNode, int> withoutThisNode, TreeNode cur)
    {
        if (cur == null) return 0;
        if (!withThisNode.ContainsKey(cur))
        {
            int withoutLeftWithoutRight = cur.val + RobWithoutUtil(withThisNode, withoutThisNode, cur.left) + RobWithoutUtil(withThisNode, withoutThisNode, cur.right);
            withThisNode.Add(cur, withoutLeftWithoutRight);
        }
        return withThisNode[cur];
    }
    int RobWithoutUtil(Dictionary<TreeNode, int> withThisNode, Dictionary<TreeNode, int> withoutThisNode, TreeNode cur)
    {
        if (cur == null) return 0;
        if (!withoutThisNode.ContainsKey(cur))
        {
            int withLeftWithRight = RobWithUtil(withThisNode, withoutThisNode, cur.left) + RobWithUtil(withThisNode, withoutThisNode, cur.right);
            int withoutLeftWithRight = RobWithoutUtil(withThisNode, withoutThisNode, cur.left) + RobWithUtil(withThisNode, withoutThisNode, cur.right);
            int withoutRightWithLeft = RobWithoutUtil(withThisNode, withoutThisNode, cur.right) + RobWithUtil(withThisNode, withoutThisNode, cur.left);
            withoutThisNode.Add(cur, Math.Max(withLeftWithRight, Math.Max(withoutLeftWithRight, withoutRightWithLeft)));
        }
        return withoutThisNode[cur];
    }

    public int MoreElegantRob(TreeNode root)
    {
        int[] num = DFSRobUtil(root);
        return Math.Max(num[0], num[1]);
    }
    int[] DFSRobUtil(TreeNode root)
    {
        if (root == null) return new int[2];
        int[] left = DFSRobUtil(root.left);
        int[] right = DFSRobUtil(root.right);
        int[] res = new int[2];
        res[0] = left[1] + right[1] + root.val;
        res[1] = Math.Max(left[0], left[1]) + Math.Max(right[0], right[1]);
        return res;
    }

    public int[] FindEvenNumbers(int[] digits)
    {
        Dictionary<int, int> dict = new Dictionary<int, int>();
        foreach (int i in digits)
        {
            if (dict.ContainsKey(i)) dict[i]++;
            else dict.Add(i, 1);
        }
        List<int> final = new List<int>();
        for (int i = 1; i < 10; i++)
        {
            if (dict.ContainsKey(i))
            {
                dict[i]--;
                for (int j = 0; j < 10; j++)
                {
                    if (dict.ContainsKey(j) && dict[j] > 0)
                    {
                        dict[j]--;
                        for (int k = 0; k < 9; k += 2)
                        {
                            if (dict.ContainsKey(k) && dict[k] > 0)
                            {
                                int cur = 100 * i + 10 * j + k;
                                final.Add(cur);
                            }
                        }
                        dict[j]++;
                    }
                }
                dict[i]++;
            }
        }

        return final.ToArray();
    }

    public ListNode DeleteMiddle(ListNode head)
    {

        if (head.next == null) return null;
        if (head.next.next == null)
        {
            head.next = null;
            return head;
        }
        int count = 0;
        var cur = head;
        while (cur != null)
        {
            cur = cur.next;
            count++;
        }
        cur = head;
        ListNode prev = head;
        int curCount = 0;
        while (curCount < count / 2)
        {
            prev = cur;
            cur = cur.next; curCount++;
        }
        prev.next = cur.next;
        return head;
    }

    public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
    {
        if (root == null || root == p || root == q) return root;
        TreeNode left = LowestCommonAncestor(root.left, p, q);
        TreeNode right = LowestCommonAncestor(root.right, p, q);
        return left == null ? right : right == null ? left : root;
    }

    public string GetDirections(TreeNode root, int startValue, int destValue)
    {
        TreeNode lca = LowestCommonAncestor(root, startValue, destValue);
        //search both side of root to look for start and end;
        //if end is in the right reverse a and prepend a to b, else set flag to false, as flag is false, reverse b and prepend to a
        if (lca.val == startValue)
        {
            //search in left
            StringBuilder buffer = new StringBuilder();
            string ifDestInLeft = PathFromRootToValue(new StringBuilder(), destValue, lca.left);
            if (ifDestInLeft == null)
            {
                string destInRight = PathFromRootToValue(new StringBuilder(), destValue, lca.right);
                buffer.Append("R");
                buffer.Append(destInRight);
                return buffer.ToString();
            }
            else
            {
                buffer.Append("L");
                buffer.Append(ifDestInLeft);
                return buffer.ToString();
            }
        }
        if (lca.val == destValue)
        {
            //search left for start
            StringBuilder buffer = new StringBuilder();
            buffer.Append("U"); //start now is not root so it must go up at least 1 node to be at root
            string ifStartInLeft = PathFromRootToValue(new StringBuilder(), startValue, lca.left);
            if (ifStartInLeft == null)
            {
                string startInRight = PathFromRootToValue(new StringBuilder(), startValue, lca.right);
                foreach (char chr in startInRight) buffer.Append("U");
                return buffer.ToString();
            }
            else
            {
                foreach (char chr in ifStartInLeft) buffer.Append("U");
                return buffer.ToString();
            }
        }

        string checkIfAIsInTheLeft = PathFromRootToValue(new StringBuilder(), startValue, lca.left);
        if (checkIfAIsInTheLeft == null)
        {
            //start is in the right branch
            string rightBranchSearchForA = PathFromRootToValue(new StringBuilder(), startValue, lca.right);
            StringBuilder buffer = new StringBuilder();
            buffer.Append("U");//had to go down to enter right branch
            foreach (char chr in rightBranchSearchForA) buffer.Append("U");
            buffer.Append("L");
            buffer.Append(PathFromRootToValue(new StringBuilder(), destValue, lca.left));
            return buffer.ToString();
        }
        else
        {//start in the left, reverse to all U
            StringBuilder buffer = new StringBuilder();
            foreach (char i in checkIfAIsInTheLeft) buffer.Append("U");
            buffer.Append("U");//go left to enter start's branch
            string noNeedToChangeForDest = PathFromRootToValue(new StringBuilder(), destValue, lca.right);
            buffer.Append("R");
            buffer.Append(noNeedToChangeForDest);
            return buffer.ToString();
        }
    }

    string PathFromRootToValue(StringBuilder sofar, int target, TreeNode cur)
    {//cur added in previous loop
        if (cur == null) return null;
        if (cur.val == target) return sofar.ToString();
        else
        {
            //left
            sofar.Append("L");
            string left = PathFromRootToValue(sofar, target, cur.left);
            if (left != null) return left;
            sofar.Remove(sofar.Length - 1, 1);
            //right
            sofar.Append("R");
            string right = PathFromRootToValue(sofar, target, cur.right);
            if (right != null) return right;
            sofar.Remove(sofar.Length - 1, 1);
        }
        //try all left and right and found shit
        return null;
    }


    public TreeNode LowestCommonAncestor(TreeNode root, int a, int b)
    {
        if (root == null || root.val == a || root.val == b) return root;
        TreeNode left = LowestCommonAncestor(root.left, a, b);
        TreeNode right = LowestCommonAncestor(root.right, a, b);
        return left == null ? right : right == null ? left : root;//if both left and right are nonnull mean each of them contains either a or b return root.
    }

    public int[][] ValidArrangement(int[][] pairs)
    {
        //https://leetcode.com/contest/weekly-contest-270/problems/valid-arrangement-of-pairs/
        List<int[]> adj = new List<int[]>();
        for (int i = 0; i < pairs.Length; i++)
        {
            List<int> curAdjOfI = new List<int>();
            for (int j = 0; j < pairs.Length; j++)
            {
                if (pairs[i][1] == pairs[j][0])
                    curAdjOfI.Add(j);
            }
            adj.Add(curAdjOfI.ToArray());
        }


        //this is just an adj list, use SCC to find such walk
        return adj.ToArray();
    }

    public int[] TwoSumSorted(int[] numbers, int target)
    {
        int l = 0, r = numbers.Length - 1;
        while (r > l)
        {
            if (numbers[l] + numbers[r] == target) return new int[2] { l, r };
            if (numbers[l] + numbers[r] > target) r--;
            else l++;
        }
        return new int[2];
    }

    bool IsBadVersion(int version)
    {
        return version >= 170;
    }
    public int FirstBadVersion(int n)
    {
        if (n == 1) return n;
        int l = 1, r = n;
        while (r > l)
        {
            int mid = l + (r - l) / 2;
            if (IsBadVersion(mid))
            {
                if (!IsBadVersion(mid - 1)) return mid;
                r = mid - 1;
            }
            else
            {
                l = mid + 1;
            }
        }
        return l;
    }
    public int[] Intersection(int[] nums1, int[] nums2)
    {
        //make sure first array is longer than second 
        if (nums1.Length < nums2.Length) return Intersection(nums2, nums1);
        Array.Sort(nums1);
        HashSet<int> set = new HashSet<int>();
        foreach (int i in nums2)
        {
            if (!set.Contains(i))
            {
                if (BinarySearch(nums1, i))
                    set.Add(i);
            }
        }
        int[] final = new int[set.Count];
        int cur = 0;
        foreach (int i in set)
        {
            final[cur++] = i;
        }
        return final;
    }
    bool BinarySearch(int[] nums, int target)
    {
        int l = 0, r = nums.Length - 1;
        while (r > l)
        {
            int mid = (r + l) / 2;
            if (nums[mid] == target) return true;
            if (nums[mid] > target) r = mid - 1;
            else l = mid + 1;
        }
        return nums[l] == target;
    }

    public bool IsPerfectSquare(int num)
    {
        if (num == 1 || num == 4) return true;
        if (num < 9) return false;
        long l = 1; long r = num;
        while (r > l)
        {
            long mid = l + (r - l) / 2;
            if (mid * mid == num) return true;
            if (mid * mid > num) r = mid - 1;
            else l = mid + 1;
        }
        return l * l == num;
    }

    public int LengthOfLIS(int[] nums)
    {
        //TODO:https://leetcode.com/problems/longest-increasing-subsequence/
        int[] start = new int[nums.Length], end = new int[nums.Length];
        end[0] = 1; start[nums.Length - 1] = 1;
        int max = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            max = Math.Max(max, LengthOfLISStart(nums, start, i) + LengthOfLISEnd(nums, end, i) - 1);
        }
        return max;
    }
    int LengthOfLISStart(int[] nums, int[] start, int i)
    {//find the longest lis if start at i
        if (start[i] == 0)
        {
            start[i]++;
            int max = 0;
            for (int j = i + 1; j < nums.Length; j++)
            {
                if (nums[j] > nums[i])
                {
                    max = Math.Max(max, LengthOfLISStart(nums, start, j));
                }
            }
            start[i] += max;
        }
        return start[i];
    }
    int LengthOfLISEnd(int[] nums, int[] end, int i)
    {
        if (end[i] == 0)
        {
            end[i]++;
            int max = 0;
            for (int j = i - 1; j >= 0; j--)
            {
                if (nums[j] < nums[i])
                {
                    max = Math.Max(max, LengthOfLISEnd(nums, end, j));
                }
            }
            end[i] += max;
        }
        return end[i];
    }

    public int[] FindRightInterval(int[][] intervals)
    {
        //TODO:https://leetcode.com/problems/find-right-interval/
        return new int[0];
    }

    public bool IsValidBST(TreeNode root)
    {
        //https://leetcode.com/problems/validate-binary-search-tree/
        if (root == null) return true;
        if (root.left == null)
        {
            if (root.right.val < root.val) return false;
            return IsValidBSTRight(root.right, root);
        }
        if (root.right == null)
        {
            if (root.left.val > root.val) return false;
            return IsValidBSTLeft(root.left, root);
        }
        if (root.right.val < root.val || root.left.val > root.val) return false;
        return IsValidBSTLeft(root.left, root) && IsValidBSTRight(root.right, root);
    }
    bool IsValidBSTLeft(TreeNode root, TreeNode parent)
    {
        //check if parent children of root satisfy parent, assumpt that the tree above parent is valid
        if (root == null) return true;
        if (root.left == null && root.right == null) return true;
        if (root.left == null)
        {
            if (root.right.val < root.val || root.right.val > parent.val) return false;
            return IsValidBSTRight(root.right, root);
        }
        else if (root.right == null)
        {
            if (root.left.val > root.val || root.left.val > parent.val) return false;
            return IsValidBSTLeft(root.left, root);
        }
        else
        {
            //both non-nil
            if (root.right.val < root.val || root.right.val > parent.val || root.left.val > root.val || root.left.val > parent.val) return false;
            return IsValidBSTRight(root.right, root) && IsValidBSTLeft(root.left, root); ;
        }

    }
    bool IsValidBSTRight(TreeNode root, TreeNode parent)
    {
        if (root == null) return true;
        if (root.left == null && root.right == null) return true;
        if (root.left == null)
        {
            if (root.right.val < root.val || root.right.val < parent.val) return false;
            return IsValidBSTRight(root.right, root);
        }
        else if (root.right == null)
        {
            if (root.left.val > root.val || root.left.val < parent.val) return false;
            return IsValidBSTLeft(root.left, root);
        }
        else
        {
            //both non-nil
            if (root.right.val < root.val || root.right.val < parent.val || root.left.val > root.val || root.left.val < parent.val) return false;
            return IsValidBSTRight(root.right, root) && IsValidBSTLeft(root.left, root); ;
        }
    }

    public int MinCostToMoveChips(int[] position)
    {
        int even = 0, odd = 0;
        for (int i = 0; i < position.Length; i++)
        {
            if (position[i] % 2 == 0) even++;
            else odd++;
        }
        return Math.Min(odd, even);
    }

    public int LongestConsecutive(int[] nums)
    {
        HashSet<int> set = new HashSet<int>();
        foreach (int num in nums) set.Add(num);
        int longestStreak = 1;
        foreach (int num in nums)
        {
            if (!set.Contains(num - 1))
            {
                int curNum = num, streak = 1;
                while (set.Contains(curNum + 1))
                {
                    curNum++;
                    streak++;
                }
                longestStreak = Math.Max(longestStreak, streak);
            }
        }
        return longestStreak;
    }



    public void SurroundedRegion(char[][] board)
    {
        //TODO:https://leetcode.com/problems/surrounded-regions/
        int rows = board.Length, cols = board[0].Length;
        int dummy = rows * cols;
        UnionFindSurroundedRegion uf = new UnionFindSurroundedRegion(rows * cols + 1);
        for (int i = 0; i < rows; i += 1)
        {
            for (int j = 0; j < cols; j++)
            {
                if (board[i][j] == 'O')
                {
                    if (i == 0 || j == 0 || i == rows - 1 || j == cols - 1) uf.Union(HashNode(i, j, cols), dummy);
                    else
                    {
                        if (board[i - 1][j] == 'O') uf.Union(HashNode(i, j, cols), HashNode(i - 1, j, cols));
                        if (board[i + 1][j] == 'O') uf.Union(HashNode(i, j, cols), HashNode(i + 1, j, cols));
                        if (board[i][j - 1] == 'O') uf.Union(HashNode(i, j, cols), HashNode(i, j - 1, cols));
                        if (board[i][1 + j] == 'O') uf.Union(HashNode(i, j, cols), HashNode(i, j + 1, cols));
                    }
                }
            }
        }
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (!uf.IsConnected(dummy, HashNode(i, j, cols)))
                {
                    board[i][j] = 'X';
                }
            }
        }
    }
    int HashNode(int row, int col, int cols)
    {
        return row * cols + col;
    }
    public int NumIslands(char[][] grid)
    {
        //TODO: https://leetcode.com/problems/number-of-islands/

        int rows = grid.Length, cols = grid[0].Length;
        if (rows == 1 && cols == 1) return grid[0][0] - 48;
        if (rows == 1)
        {
            int count = grid[0][0] - 48;
            for (int j = 1; j < cols; j++)
            {
                if (grid[0][j] == '1' && grid[0][j - 1] == '0') count++;
            }
            return count;
        }
        if (cols == 1)
        {
            int count = grid[0][0] - 48;
            for (int i = 1; i < rows; i++)
            {
                if (grid[i][0] == '1' && grid[i - 1][0] == '0') count++;
            }
            return count;
        }
        int Node(int row, int col)
        {
            return row * cols + col;
        }
        UnionFindSurroundedRegion uf = new UnionFindSurroundedRegion(rows * cols);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (grid[i][j] == '1')
                {
                    int cur = Node(i, j);
                    if (i == 0)
                    {

                        if (j != cols - 1 && grid[i][j + 1] == '1') uf.Union(cur, Node(i, j + 1));
                        if (grid[i + 1][j] == '1') uf.Union(cur, Node(i + 1, j));
                        if (j != 0 && grid[i][j - 1] == '1') uf.Union(cur, Node(i, j - 1));
                    }
                    else if (i == rows - 1)
                    {
                        if (j != cols - 1 && grid[i][j + 1] == '1') uf.Union(cur, Node(i, j + 1));
                        if (grid[i - 1][j] == '1') uf.Union(cur, Node(i - 1, j));
                        if (j != 0 && grid[i][j - 1] == '1') uf.Union(cur, Node(i, j - 1));
                    }
                    else if (j == 0)
                    {
                        if (grid[i][j + 1] == '1') uf.Union(cur, Node(i, j + 1));
                        if (i != rows - 1 && grid[i + 1][j] == '1') uf.Union(cur, Node(i + 1, j));
                        if (i != 0 && grid[i - 1][j] == '1') uf.Union(cur, Node(i - 1, j));
                    }
                    else if (j == cols - 1)
                    {
                        if (grid[i][j - 1] == '1') uf.Union(cur, Node(i, j - 1));
                        if (i != 0 && grid[i - 1][j] == '1') uf.Union(cur, Node(i - 1, j));
                        if (i != rows - 1 && grid[i + 1][j] == '1') uf.Union(cur, Node(i + 1, j));
                    }
                    else
                    {
                        if (grid[i][j + 1] == '1') uf.Union(cur, Node(i, j + 1));
                        if (grid[i + 1][j] == '1') uf.Union(cur, Node(i + 1, j));
                        if (grid[i][j - 1] == '1') uf.Union(cur, Node(i, j - 1));
                        if (grid[i - 1][j] == '1') uf.Union(cur, Node(i - 1, j));
                    }
                }
            }
        }
        HashSet<int> islands = new HashSet<int>();
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (grid[i][j] == '1')
                {
                    int parent = uf.FindRepresentative(Node(i, j));
                    if (!islands.Contains(parent))
                    {
                        islands.Add(parent);
                    }
                }
            }
        }
        return islands.Count;
    }


    public int MaxProfitStock(int[] prices)
    {
        //https://leetcode.com/problems/best-time-to-buy-and-sell-stock-iii/
        if (prices == null || prices.Length == 0) return 0;
        int[,] dp = new int[3, prices.Length];
        for (int k = 1; k <= 2; k++)
        {
            int min = prices[0];
            for (int i = 1; i < prices.Length; i++)
            {
                min = Math.Min(min, prices[i] - dp[k - 1, i - 1]);
                dp[k, i] = Math.Max(dp[k, i - 1], prices[i] - min);
            }
        }
        return dp[2, prices.Length - 1];
    }

    public bool CanReach(int[] arr, int start)
    {

        bool res = CanReachUtil(arr, start, new int[arr.Length], new bool[arr.Length]);
        return res;
    }

    bool CanReachUtil(int[] arr, int cur, int[] visited, bool[] calculating)
    {
        if (cur < 0 || cur >= arr.Length || calculating[cur]) return false;
        if (arr[cur] == 0) return true;
        if (visited[cur] != 0) return visited[cur] == 1;
        calculating[cur] = true;
        visited[cur] = (CanReachUtil(arr, cur + arr[cur], visited, calculating) || CanReachUtil(arr, cur - arr[cur], visited, calculating)) ? 1 : -1;
        return visited[cur] == 1;
    }

    public bool CanReachOneLiner(int[] arr, int start)
    {
        return 0 <= start && start <= arr.Length - 1 && arr[start] >= 0 && ((arr[start] = -arr[start]) == 0 || CanReachOneLiner(arr, start + arr[start]) || CanReachOneLiner(arr, start - arr[start]));
    }

    public int Knapsack01(int[] w, int[] v, int weight)
    {// find in w 
        int[,] dp = new int[w.Length + 1, weight + 1];
        //dp[i,w]: maxium value if a weight of w is allowed and we only work with the first i items
        //dp[0,w] = 0 since no shit is this. dp[i,0] is the same
        for (int i = 1; i < w.Length; i++)
        {
            for (int j = 1; j < weight; j++)
            {
                //weight jth and up to i
                if (w[i] > j)
                    dp[i, j] = dp[i - 1, j];
                else
                    dp[i, j] = dp[i - 1, j - w[i]] + v[i];
            }
        }
        return dp[w.Length, weight];
    }

    public int OptimizedKnapsack01(int[] w, int[] v, int weight)
    {
        int n = w.Length;
        int[,] dp = new int[n + 1, weight + 1];
        for (int i = 1; i <= n; i++)
            for (int j = 1; j <= weight; j++)
                dp[i, j] = -1;
        return KnapsackHelper(w, v, n, weight, dp);
    }
    int KnapsackHelper(int[] w, int[] v, int i, int weight, int[,] dp)
    {
        if (dp[i, weight] == -1)
        {
            if (w[i - 1] > weight)
                dp[i, weight] = KnapsackHelper(w, v, i - 1, weight, dp);
            else
                dp[i, weight] = Math.Max(KnapsackHelper(w, v, i - 1, weight, dp), KnapsackHelper(w, v, i - 1, weight - w[i - 1], dp) + v[i - 1]);
        }
        return dp[i, weight];
    }

    public bool CanPartition(int[] nums)
    {
        if (nums.Length < 2) return false;
        int n = nums.Length;
        int sum = 0;
        foreach (int i in nums) sum += i;
        if (sum % 2 != 0) return false;
        int[,] dp = new int[n + 1, sum / 2 + 1];
        for (int i = 1; i <= n; i++)
        {
            for (int j = 0; j <= sum / 2; j++)
            {
                dp[i, j] = -1;
            }
        }
        return CanPartitionUtil(nums, dp, sum / 2, n) == sum / 2;
    }

    int CanPartitionUtil(int[] w, int[,] dp, int weight, int i)
    {
        if (dp[i, weight] == -1)
        {
            if (w[i - 1] > weight)
                dp[i, weight] = CanPartitionUtil(w, dp, weight, i - 1);
            else
                dp[i, weight] = Math.Max(CanPartitionUtil(w, dp, weight, i - 1), w[i - 1] + CanPartitionUtil(w, dp, weight - w[i - 1], i - 1));
        }
        return dp[i, weight];
    }

    public List<int> SpiralOrder(int[][] matrix)
    {
        int m = matrix.Length; int n = matrix[0].Length;
        List<int> sofar = new List<int>();
        void Next(int i, int j, bool up)
        {
            //if up prioritize up -> right -> down ->left
            //else right - down- left- up
            //check if i and j can be moved next
            if (i >= m || j >= n || i < 0 || j < 0 || matrix[i][j] == 999) return;
            sofar.Add(matrix[i][j]);
            matrix[i][j] = 999;
            if (!up)//if right, prioritize down. if left, prioritize up
            {
                Next(i, j + 1, false);
                Next(i + 1, j, false);
                Next(i, j - 1, false);
                Next(i - 1, j, false);
            }
            else
            {
                Next(i - 1, j, true);
                Next(i, j + 1, true);
                Next(i + 1, j, true);
                Next(i, j - 1, true);
            }
        }
        Next(0, 0, false);
        return sofar;
    }


    public bool IsPalindrome(ListNode head)
    {
        //TODO: Snapchat
        return false;
    }
    public ListNode Reverse(ListNode head)
    {
        return null;
    }

    public int NumTilings(int n)
    {
        //dp[i-1] n.o ways to tile i
        long[] dp = new long[Math.Max(n, 3)];
        dp[0] = 1; dp[1] = 2; dp[2] = 5;
        if (n > 3)
        {
            for (int i = 3; i < n; i++)
            {
                dp[i] = 2 * dp[i - 1] + dp[i - 3];
            }
        }
        long res = dp[n - 1] % ((long)(Math.Pow(10, 9) + 7));
        return (int)res;
    }

    public bool WordBreak(string s, IList<string> wordDict)
    {
        //https://leetcode.com/problems/word-break/
        int[] dp = new int[s.Length];
        return WordBreakUtil(s, wordDict, dp, 0);

    }
    bool WordBreakUtil(string s, IList<string> dict, int[] dp, int i)
    {//check if starting at i is true
        if (i == s.Length) return true;
        if (i > s.Length) return false;
        if (dp[i] == 0)
        {
            dp[i] = -1;
            foreach (var str in dict)
            {
                if (i + str.Length <= s.Length && s.Substring(i, str.Length) == str && WordBreakUtil(s, dict, dp, i + str.Length))
                {
                    dp[i] = 1;
                    break;
                }
            }
        }
        return dp[i] == 1;
    }

    public int NthSuperUglyNumber(int n, int[] primes)
    {
        //TODO: Learn Heap and do this shit https://leetcode.com/problems/super-ugly-number/   
        return 1;
    }

    public int GetMoneyAmount(int n)
    {
        //https://leetcode.com/problems/guess-number-higher-or-lower-ii/
        int[,] dp = new int[n + 1, n + 1];

        int GetMoneyAmountUtil(int low, int high)
        {
            //get the minimalcost in the worst case senario in range (low, high) 
            if (low >= high) return 0;
            if (dp[low, high] == 0)
            {
                int max = int.MaxValue;
                for (int i = low; i < high; i++)
                {
                    //see each case if we choose i
                    max = Math.Min(max, i + Math.Max(GetMoneyAmountUtil(low, i - 1), GetMoneyAmountUtil(i + 1, high)));
                }
                dp[low, high] = max;
            }
            return dp[low, high];
        }
        return GetMoneyAmountUtil(1, n);
    }

    public int MaximalSquare(char[][] matrix)
    {
        //https://leetcode.com/problems/maximal-square/
        int m = matrix.Length; int n = matrix[0].Length;
        int[,] dp = new int[m, n];
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
                dp[i, j] = -1;
        }

        int MaximalSquareUtil(int i, int j)
        {
            if (dp[i, j] == -1)
            {
                if (matrix[i][j] == '0') dp[i, j] = 0;
                else if (i == 0 || j == 0) dp[i, j] = 1;
                else
                {
                    int a = (int)Math.Sqrt(MaximalSquareUtil(i, j - 1));
                    int b = (int)Math.Sqrt(MaximalSquareUtil(i - 1, j));
                    int min = Math.Min(a, b);
                    if (matrix[i - min][j - min] == '1') dp[i, j] = (min + 1) * (min + 1);
                    else dp[i, j] = min * min;
                }
            }
            return dp[i, j];
        }
        int max = 0;
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                max = Math.Max(MaximalSquareUtil(i, j), max);
            }
        }
        return max;

    }
    public int AtMostNGivenDigitSet(string[] digits, int n)
    {
        //https://leetcode.com/problems/numbers-at-most-n-given-digit-set/
        int length = GetNumberOfDigits(n);
        int[] dp = new int[length + 1]; //dp[i] = number of numbers that is of length i and of course smaller than n;
        int[] cumCom = new int[length + 1]; //cumulative sum of combination smaller than or equal to i
        int k = digits.Length;//number of given digits
        int first = n / (int)Math.Pow(10, length - 1);
        dp[0] = 1;
        for (int i = 1; i <= length; i++)
        {//find the number of combination of digit in digits for length i
            dp[i] = dp[i - 1] * k;
            cumCom[i] = cumCom[i - 1] + dp[i];
        }

        int Helper(int m)
        {
            if (m < 10)
            {
                int count = 0;
                foreach (var i in digits)
                {
                    int cur = Int32.Parse(i);
                    if (cur <= m) count++;
                    else break;
                }
                return count;
            }
            //get number of numbers that is smaller than n
            int lengthOfM = GetNumberOfDigits(m);
            int tillKminus1 = cumCom[lengthOfM - 1];//number of
            int firstDigitOfM = m / (int)Math.Pow(10, lengthOfM - 1);
            int next = m - (int)Math.Pow(10, lengthOfM - 1) * firstDigitOfM;
            int smallerThanFirstDigitOfM = 0;
            foreach (var i in digits)
            {
                int cur = Int32.Parse(i);
                if (cur <= firstDigitOfM) smallerThanFirstDigitOfM++;
                else break;
            }
            return tillKminus1 + smallerThanFirstDigitOfM * Helper(next);//get the rest of m. 
        }
        return Helper(n);

    }

    public int GetNumberOfDigits(int n)
    {
        if (n < 10) return 1;
        int count = 1;
        while (n > 9)
        {
            n /= 10;
            count++;
        }
        return count;
    }

    public string DecodeString(string s)
    {
        //TODO: https://leetcode.com/problems/decode-string/        
        return "";
    }

    public IList<IList<int>> MinimumAbsDifference(int[] arr)
    {
        Array.Sort(arr);
        int min = int.MaxValue;
        for (int i = 1; i < arr.Length; i++)
        {
            min = Math.Min(min, arr[i] - arr[i - 1]);
        }
        IList<IList<int>> final = new List<IList<int>>();
        for (int i = 1; i < arr.Length; i++)
        {
            if (arr[i] - arr[i - 1] == min) final.Add(new List<int>(new int[] { arr[i - 1], arr[i] }));
        }
        return final;
    }

    public TreeNode BalanceBST(TreeNode root)
    {
        List<int> sorted = InorderTraversal1(root);
        return BSTFromSortedArray(sorted, 0, sorted.Count - 1);
    }

    public TreeNode BSTFromSortedArray(List<int> arr, int left, int right)
    {
        if (right <= left) return new TreeNode(arr[left]);
        if (right - left == 1) return new TreeNode(arr[left], new TreeNode(arr[right]));
        int mid = (left + right) / 2;
        return new TreeNode(arr[mid], BSTFromSortedArray(arr, left, mid - 1), BSTFromSortedArray(arr, mid + 1, right));
    }

    public List<int> InorderTraversal1(TreeNode root)
    {
        List<int> final = new List<int>();
        void Helper(TreeNode cur)
        {
            if (cur == null) return;
            Helper(cur.left);
            final.Add(cur.val);
            Helper(cur.right);
        }
        return final;
    }

    public void WallsAndGates(int[][] rooms)
    {
        Queue<int[]> queue = new Queue<int[]>();
        int m = rooms.Length, n = rooms[0].Length;
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (rooms[i][j] == '0') queue.Enqueue(new int[2] { i, j });
            }
        }
        int dist = 0;
        int[][] dir = { new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { 0, -1 }, new int[] { -1, 0 } };
        while (queue.Count != 0)
        {
            int levelSize = queue.Count;
            for (int i = 0; i < levelSize; i++)
            {
                int[] cur = queue.Dequeue();
                int curR = cur[0], curC = cur[1];
                if (rooms[curR][curC] == int.MaxValue)
                    rooms[curR][curC] = dist;
                for (int j = 0; j < 4; j++)
                {
                    int newR = curR + dir[j][0], newC = curC + dir[j][1];
                    if (newR < 0 || newC < 0 || newR >= m || newC >= n || rooms[newR][newC] == -1 || rooms[newR][newC] != int.MaxValue) continue;
                    queue.Enqueue(new int[] { newR, newC });
                }
            }
            dist++;
        }
    }

    public string RemoveDuplicates(string s, int k)
    {
        if (k == 0) return s;
        if (k == 1) return "";
        if (s.Length < k) return s;
        Stack<int> count = new Stack<int>();
        count.Push(1);
        Stack<char> stack = new Stack<char>();
        stack.Push(s[0]);
        int cur = 1;
        while (cur != s.Length || stack.Count != 0)
        {
            if (cur == s.Length) break;
            char curChar = s[cur++];
            if (stack.Count == 0)
            {
                stack.Push(curChar);
                count.Push(1);
            }
            else
            {
                char prevChar = stack.Peek();
                if (curChar == prevChar)
                {
                    int curCount = count.Peek() + 1;   //check if stack is full            
                    count.Push(curCount);
                    stack.Push(curChar);
                    if (curCount == k)
                    {
                        for (int i = 0; i < k; i++)
                        {
                            stack.Pop();
                            count.Pop();
                        }
                    }
                }
                else
                {
                    stack.Push(curChar);
                    count.Push(1);
                }
            }
        }
        if (stack.Count == 0) return "";
        var temp = new Stack<char>();

        while (stack.Count != 0)
        {
            temp.Push(stack.Pop());
        }
        var buffer = new StringBuilder();
        while (temp.Count != 0) buffer.Append(temp.Pop());
        return buffer.ToString();

    }

    public IList<int> PreorderTraversal(TreeNode root)
    {
        if (root == null) return new List<int>();
        Stack<TreeNode> stack = new Stack<TreeNode>();
        stack.Push(root);
        List<int> final = new List<int>();
        while (stack.Count != 0)
        {
            var cur = stack.Pop();
            if (cur != null)
            {
                final.Add(cur.val);
                stack.Push(cur.right);
                stack.Push(cur.left);
            }
        }
        return final;
    }

    public void Flatten(TreeNode root)
    {
        TreeNode cur = root;
        while (cur != null)
        {
            if (cur.left != null)
            {
                var last = cur.left;
                while (last.right != null) last = last.right;
                last.right = cur.right;
                cur.right = cur.left;
                cur.left = null;
            }
            cur = cur.right;
        }
    }

    public bool IsValidSerialization(string preorder)
    {
        if (preorder.Length <= 2) return false;
        Stack<char> stack = new Stack<char>();
        stack.Push(preorder[0]);
        int lim = preorder.Length;
        int count = 1;
        while (count != lim && stack.Count != 0)
        {
            char cur = preorder[count];
            if (cur != ',')
            {
                if (cur != '#')
                {
                    stack.Push(cur);
                }
                else
                {
                    if (stack.Peek() == '#')//prevent while loop from auto entering
                    {
                        while (stack.Peek() == '#')
                        {
                            stack.Pop(); if (stack.Count == 0) return false;
                            stack.Pop(); if (stack.Count == 0) return count == lim - 1; //still # to be added but here the stack exhausted
                        }
                    }
                    stack.Push('#');
                }
            }
            count++;
        }
        return stack.Count == 0;

    }

    public bool IsPowerOfTwo(int n)
    {
        if (n == 0) return false;
        if (n == 1) return true;
        while (n % 2 == 0) n /= 2;
        return n == 1;
    }

    public IList<int> PostTraversal(TreeNode root)
    {
        if (root == null) return new List<int>();
        Stack<TreeNode> stack = new Stack<TreeNode>();
        stack.Push(root);
        List<int> final = new List<int>();
        while (stack.Count != 0)
        {
            var cur = stack.Pop();
            if (cur != null)
            {
                final.Add(cur.val);
                stack.Push(cur.left);
                stack.Push(cur.right);
            }
        }
        final.Reverse();
        return final;
    }

    public void ReorderList(ListNode head)
    {
        //https://leetcode.com/problems/reorder-list/
        if (head == null || head.next == null) return;
        ListNode slow = head, fast = head;
        while (fast != null && fast.next != null)
        {
            slow = slow.next;
            fast = fast.next.next;
        }
        Stack<ListNode> stack = new Stack<ListNode>();
        while (slow != null)
        {
            stack.Push(slow);
            slow = slow.next;
        }
        slow = head;
        while (stack.Count != 0)
        {
            var cur = stack.Pop();
            cur.next = slow.next;
            slow.next = cur;
            slow = slow.next.next;
        }
        slow.next = null;
    }

    public int[] AsteroidCollision(int[] asteroids)
    {
        Stack<int> stack = new Stack<int>();
        for (int i = 0; i < asteroids.Length; i++)
        {
            int cur = asteroids[i];
            if (stack.Count == 0)
            {
                stack.Push(cur);
                //first time enter or when the last one in stack is of the same size as the coming one
            }
            else
            {
                int last = stack.Peek();
                //if of the same direction
                if (last * cur > 0)
                {
                    stack.Push(cur);
                }
                else
                {
                    //if of different direction but moving away (peek is - and cur is +)
                    if (last < 0) stack.Push(cur);
                    //if of dirrent direction but moving against each}
                    else
                    {
                        while (stack.Count > 0)
                        {
                            int curPeek = stack.Peek();
                            if (curPeek * cur > 0 || curPeek < 0) { stack.Push(cur); break; } //ok no war
                            else if (curPeek < -cur) { stack.Pop(); if (stack.Count == 0) { stack.Push(cur); break; } } //stack is exhausted dealing with this shit
                            else if (curPeek == -cur) { stack.Pop(); break; } //pump, no war afterwards
                            else break; //curPeek outsmarts cur.
                        }
                    }
                }
            }
        }
        var final = stack.ToArray(); Array.Reverse(final);
        return final;
    }

    //rule 2 monostack: the minima/maximama element is useful when considering popping/pushing
    //rule 3 monotonic stack: when a height is popped from the stack, make sure it would never be used again
    public int LargestRectangleArea(int[] heights)
    {
        List<int> arr = new List<int>(heights); arr.Add(0);
        Stack<int> stack = new Stack<int>();//stack mission: store all the smaller height in ascending order. 
        int max = 0;
        int length = 1;
        for (int i = 0; i < arr.Count; i++)
        {
            while (stack.Count != 0 && arr[stack.Peek()] > arr[i])
            {
                //pop and update max;
                //what this loop is doing is that it will consider each case when the current height can form the max rec. by how? by calculating the length from i-1 to itself and the height is its own height (as it is the smallest ever since)
                //after updating this one, we are all safe to pop it. why then? As any one before it in the stack are sure to be smaller than itself. If again we choose some prior height to be the height for the rect, this height can still satisfy that needs when length is counted. 
                //another case when in the future we may see some height happens to 1) greater than this one, say index j, we repeat the same thing and 2) of smaller hieght, well , we can satisfy
                int height = arr[stack.Pop()];
                if (stack.Count != 0) length = i - stack.Peek() - 1;
                else length = i;
                max = Math.Max(height * length, max);
            }
            stack.Push(i);
        }
        return max;
    }

    public int[] SearchRange(int[] nums, int target)
    {
        //search the first index of target, get i
        int i = Array.BinarySearch(nums, target);
        int[] arr = new int[2];
        if (i < 0) return new int[] { -1, -1 };
        //search from 0 -> i -1 for the first target
        if (i == 0 || nums[i - 1] != target) arr[0] = i;
        else
        {
            int left = 0; int right = i - 1;
            int maybeFirst = Array.BinarySearch(nums, left, right - left + 1, target);
            while (maybeFirst != 0 && nums[maybeFirst - 1] == target)
            {
                maybeFirst = Array.BinarySearch(nums, left, maybeFirst - left, target);
            }
            arr[0] = maybeFirst;
        }
        //search from i+1-> n-1 for the last target
        if (i == arr.Length - 1 || nums[1 + i] != target) arr[1] = i;
        else
        {
            int left = i + 1; int right = arr.Length - 1;
            int maybeLast = Array.BinarySearch(nums, left, right - left + 1, target);
            while (maybeLast != arr.Length - 1 && nums[maybeLast + 1] == target)
            {
                maybeLast = Array.BinarySearch(nums, left, maybeLast + 2 - left, target);
            }
            arr[1] = maybeLast;
        }
        return arr;
    }

    public bool IsComplete(TreeNode root)
    {
        if (root.left != null && root.right != null)
        {
            return IsComplete(root.left) && IsComplete(root.right);
        }
        if (root.left == null) return false;
        return IsComplete(root.left);
    }

    public bool IsCompleteTree(TreeNode root)
    {
        if (root == null) return true;
        Queue<TreeNode> queue = new Queue<TreeNode>();
        queue.Enqueue(root);
        queue.Enqueue(new TreeNode(9999));
        bool nullMet = false;
        while (queue.Count != 0)
        {
            TreeNode cur = queue.Dequeue();
            if (cur == null)
            {
                if (nullMet == false) nullMet = true;
            }
            else
            {
                if (cur.val == 9999 && queue.Count > 0) { queue.Enqueue(new TreeNode(9999)); nullMet = false; }//level end;
                else if (cur.val != 9999)
                {
                    if (nullMet) return false;
                    queue.Enqueue(cur.left);
                    queue.Enqueue(cur.right);
                }
            }
        }
        return true;
    }

    public int[] NextGreaterElements(int[] nums)
    {
        //TODO: https://leetcode.com/problems/next-greater-element-ii/
        return new int[0];
    }

    public int TrappingRainWaterSample(int[] height)
    {
        Stack<int> stack = new Stack<int>();//decreasing stack
        int total = 0;
        for (int i = 0; i < height.Length; i++)
        {
            int curHeight = height[i];
            while (stack.Count > 0 && height[stack.Peek()] < curHeight)
            {
                //as now it is smaller than the last height
                int lastInd = stack.Pop();
                if (stack.Count != 0) break;
                int l = i - stack.Peek() - 1;
                int h = Math.Min(height[stack.Peek()], curHeight) - height[lastInd];
                total += l * h;
            }
            stack.Push(i);
        }
        return total;
    }

    public int TrappingWater(int[] height)
    {
        Stack<int> stack = new Stack<int>(); //decreasing stack
        int total = 0;
        for (int i = 0; i < height.Length; i++)
        {
            int curHeight = height[i];
            while (stack.Count > 0 && curHeight > height[stack.Peek()])
            {
                int lastInd = stack.Pop();
                if (stack.Count == 0) break;
                int l = i - stack.Peek() - 1;
                int h = Math.Min(height[stack.Peek()], curHeight) - height[lastInd];
                total += l * h;
            }
            stack.Push(i);
        }
        return total;
    }

    public bool Find132pattern(int[] nums)
    {
        if (nums.Length < 3) return false;
        Stack<Pair> stack = new Stack<Pair>();
        for (int i = 0; i < nums.Length; i++)
        {
            if (stack.Count == 0 || stack.Peek().min > nums[i]) stack.Push(new Pair(nums[i], nums[i]));
            else if (nums[i] > stack.Peek().min)
            {
                int cur = nums[i];
                var last = stack.Peek();
                if (last.max > cur) return true; //max>cur>min
                last.max = cur;
                while (stack.Count > 0 && stack.Peek().max <= cur) //that is, when some prior intervals which by definition has min smaller than last.min and max < cur, if somehow a new number is between its (the prior one) is found satisfied, it also satisfies the current interval
                    stack.Pop();
                if (stack.Count != 0 && stack.Peek().min < cur) return true; // min < cur < max
                stack.Push(last);
            }

        }
        return false;
    }

    public void RecoverTree(TreeNode root)
    {
        TreeNode first = null;
        TreeNode second = null;
        TreeNode prev = new TreeNode(int.MinValue);
        void Traverse(TreeNode root1)
        {
            if (root1 == null) return;
            Traverse(root1.left);
            if (first == null && prev.val >= root1.val)
            {
                first = prev;
            }
            if (first != null && prev.val >= root1.val)
            {
                second = root1;
            }
            Traverse(root1.right);
        }
        Traverse(root);
        int temp = first.val;
        first.val = second.val;
        second.val = first.val;
    }

    void ValidateAndSwap(TreeNode lowerBound, TreeNode upperBound, TreeNode root)
    {
        //checkcurrent node and maybe swap
        if (root == null) return;
        if (root.val < lowerBound.val)
        {
            int temp = root.val;
            root.val = lowerBound.val;
            lowerBound.val = temp;
            return;
        }
        if (root.val > upperBound.val)
        {
            int temp = root.val;
            root.val = upperBound.val;
            upperBound.val = temp;
            return;
        }
        //low < root < upper;
        ValidateAndSwap(lowerBound, root, root.left);
        ValidateAndSwap(root, upperBound, root.right);

    }

    public int FindJudege(int n, int[][] trust)
    {
        int[] trustee = new int[n + 1];//trustee of n
        bool[] truster = new bool[n + 1];//wether i trusts someone
        Array.Fill(truster, false);
        foreach (var pair in trust)
        {
            trustee[pair[1]]++;
            truster[pair[0]] = true;
        }
        for (int i = 1; i <= n; i++)
        {
            if (!truster[i] && trustee[i] == n - 1)//if trust no one and has n trustee
                return i;
        }
        return -1;
    }

    public bool CanVisitAllRooms(IList<IList<int>> rooms)
    {
        int n = rooms.Count;
        bool[] visited = new bool[n];
        Queue<int> queue = new Queue<int>();
        queue.Enqueue(0);
        int count = 1;
        while (queue.Count != 0)
        {
            int cur = queue.Dequeue();
            visited[cur] = true;
            foreach (int i in rooms[cur])
            {
                if (!visited[i])
                    queue.Enqueue(i);
            }
        }
        return count == n;
    }

    public int[] LoudAndRich(int[][] richer, int[] quiet)
    {
        int n = quiet.Length;
        List<int>[] adj = new List<int>[n];
        //TODO:https://leetcode.com/problems/loud-and-rich/
        return new int[0];
    }

    public int[] FindOrder(int numCourses, int[][] prerequisites)
    {
        List<int>[] adj = new List<int>[numCourses];
        for (int i = 0; i < numCourses; i++) adj[i] = new List<int>();
        bool[] visited = new bool[numCourses];
        Queue<int> queue = new Queue<int>();

        //first use visited to find where to start (ie the non prerequisite)
        bool[] hasPrerequisite = new bool[numCourses];
        foreach (var pair in prerequisites) { adj[pair[1]].Add(pair[0]); hasPrerequisite[pair[0]] = true; }//pair[0] has prerequesite
        List<int> final = new List<int>();
        for (int i = 0; i < numCourses; i++)
            if (!hasPrerequisite[i])
            { queue.Enqueue(i); visited[i] = true; }//add all no-prerequisite here
        if (queue.Count == 0) return new int[0];
        while (queue.Count != 0)
        {
            int cur = queue.Dequeue();
            final.Add(cur);
            foreach (int next in adj[cur])
                if (!visited[next])
                { queue.Enqueue(next); visited[next] = true; }
        }
        return final.ToArray();
    }

    public int[] TopSortFindOrder(int n, int[][] prerequisites)
    {
        Queue<int> q = new Queue<int>();//keep track of 0-indegree node
        Dictionary<int, List<int>> adj = new Dictionary<int, List<int>>();//reverse adj: adj maintains parents of the current node
        int[] indegree = new int[n];
        int[] topOrder = new int[n];
        int i = 0;
        for (i = 0; i < n; i++)
        {
            int dst = prerequisites[i][1];
            int src = prerequisites[i][0];
            List<int> lst = adj.GetValueOrDefault(src, new List<int>());
            lst.Add(dst);
            adj[src] = lst;
            indegree[dst] = src;
        }
        for (i = 0; i < n; i++)
        {
            if (indegree[i] == 0)
                q.Enqueue(i);
        }
        i = 0;
        while (q.Count != 0)
        {
            int cur = q.Dequeue();
            topOrder[i++] = cur;
            if (adj.ContainsKey(cur))
            {
                foreach (int child in adj[cur])
                {
                    indegree[child]--;
                    if (indegree[child] == 0)
                        q.Enqueue(child);
                }
            }
        }
        if (i == n) return topOrder;
        return new int[0];
    }

    public int MaxChunksToSorted(int[] arr)
    {
        if (arr.Length < 2) return 1;
        int max = 0;
        int count = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            max = Math.Max(arr[i], max);
            if (max == i) count++;
        }
        return count;
    }

    public int CarFleet(int target, int[] pos, int[] spe)
    {
        int n = 0;
        Dictionary<int, int> posSpe = new Dictionary<int, int>();
        for (int i = 0; i < n; i++)
        {
            posSpe.Add(pos[i], spe[i]);
        }
        Array.Sort(pos);
        int[] finish = new int[n];//time i reaches targer
        for (int i = 0; i < n; i++)
        {//posSpe[pos[i]] = speed of ith
            finish[i] = (target - pos[i]) / posSpe[pos[i]];
        }
        Stack<int> stack = new Stack<int>();
        stack.Push(finish[n - 1]);
        for (int i = n - 2; i >= 0; i--)
        {
            if (stack.Peek() < finish[i])
                stack.Push(finish[i]);
        }
        return stack.Count;
    }

    public int[][] Merge(int[][] intervals)
    {
        if (intervals.Length < 2) return intervals;
        CompareInterval comp = new CompareInterval();
        Array.Sort(intervals, comp);
        Stack<int[]> stack = new Stack<int[]>();
        stack.Push(intervals[0]);
        for (int i = 1; i < intervals.Length; i++)
        {
            var cur = intervals[i];
            if (cur[0] > stack.Peek()[1])
                stack.Push(cur);
            else
            {
                stack.Peek()[1] = Math.Max(cur[1], stack.Peek()[1]);
            }
        }
        return stack.ToArray();
    }

    public int MctFromLeafValues(int[] arr)
    {
        Stack<int> stack = new Stack<int>();
        stack.Push(int.MaxValue);
        int cost = 0;
        foreach (int i in arr)
        {
            while (stack.Peek() <= i)
            {
                //remove peek, keep a, and increment by min(last peek, a)*peek
                cost += stack.Pop() * Math.Min(stack.Peek(), i);
            }
            //now last peek is > i, push
            stack.Push(i);
        }
        //monostak is now descending, remove from right to left
        while (stack.Count > 2)
        {
            cost += stack.Pop() * stack.Peek();
        }
        return cost;

    }

    public int ScoreOfParentheses(string s)
    {
        //TODO: https://leetcode.com/problems/score-of-parentheses/
        Stack<char> stack = new Stack<char>();
        List<int> powOfTwo = new List<int>(); powOfTwo.Add(1);//powOfTwo[i] = 2^i
        bool closedBefore = false;
        int count = 0;
        int GetPowerOfTwo(int i)
        {
            if (powOfTwo.Count <= i)
            {
                int start = powOfTwo.Count;
                for (int j = start; j <= i; j++)
                {
                    powOfTwo.Add(2 * powOfTwo[j - 1]);
                }
            }
            return powOfTwo[i];

        }
        foreach (char chr in s)
        {
            if (chr == '(')
            {
                stack.Push('(');
                closedBefore = false;
            }
            else
            {
                if (!closedBefore)
                {
                    count += GetPowerOfTwo(stack.Count - 1);
                    closedBefore = true;
                }
                stack.Pop();
            }
        }
        return count;
    }

    public int ScoreParentheses(string s)
    {
        Stack<int> stack = new Stack<int>();
        int cur = 0;//current value, if stop at current index (disregard the preceding open paren)
        //that is, current value to addup if there come any number of the same depth
        foreach (char chr in s)
        {
            if (chr == '(')
            {
                stack.Push(cur);
                cur = 0;
            }
            else
            {
                cur = stack.Pop() + Math.Max(2 * cur, 1);
            }
        }
        return cur;
    }

    public int SumSubarrayMins(int[] arr)
    {
        //TODO: https://leetcode.com/problems/sum-of-subarray-minimums/
        int[] ContiguousBiggerLeft(int[] arr1)
        {
            int[] ans = new int[arr1.Length];//ans[i] is the number of strictly contiguously bigger numbers to the left of i 
            Stack<int[]> stack = new Stack<int[]>();
            for (int i = 0; i < arr1.Length; i++)
            {
                int count = 1;
                while (stack.Count != 0 && stack.Peek()[0] > arr1[i])
                {
                    count += stack.Pop()[1];
                }
                ans[i] = count;
                stack.Push(new int[] { arr1[i], ans[i] });
            }
            return ans;
        }
        int[] ContiguousBiggerRight(int[] arr1)
        {
            int[] ans = new int[arr1.Length];
            Stack<int[]> stack = new Stack<int[]>();
            for (int i = arr1.Length - 1; i >= 0; i--)
            {
                int count = 1;
                while (stack.Count != 0 && stack.Peek()[0] >= arr1[i])
                    count += stack.Pop()[1];
                ans[i] = count;
                stack.Push(new int[] { arr1[i], ans[i] });
            }
            return ans;
        }
        int[] left = ContiguousBiggerLeft(arr);
        int[] right = ContiguousBiggerRight(arr);
        long sum = 0;
        for (int i = 0; i < arr.Length; i++)
            sum += arr[i] * left[i] * right[i];
        sum %= (long)(Math.Pow(10, 9) + 7);
        return (int)sum;
    }

    public int Calculate(string s)
    {
        Stack<int> stack = new Stack<int>();
        char curOp = '+';
        int curNum = 0;
        for (int i = 0; i < s.Length; i++)
        {
            char curChar = s[i];
            if ('0' <= curChar && '9' >= curChar)
            {
                curNum = curNum * 10 + curChar - '0';
            }
            if (!Char.IsDigit(curChar) && curChar != ' ' || i == s.Length - 1)
            {
                if (curOp == '+') stack.Push(curNum);
                else if (curOp == '-') stack.Push(-curNum);
                else if (curOp == '*')
                {
                    stack.Push(stack.Pop() * curNum);
                }
                else if (curOp == '/')
                {
                    stack.Push(stack.Pop() / curNum);
                }
                curOp = curChar;
                curNum = 0;
            }
        }
        int res = 0;
        while (stack.Count != 0)
        {
            res += stack.Pop();
        }
        return res;
    }

    public int[][] KClosest(int[][] points, int k)
    {
        Dictionary<int, List<int[]>> dict = new Dictionary<int, List<int[]>>();
        foreach (int[] point in points)
        {
            int dis = point[0] * point[0] + point[1] * point[1];
            if (!dict.ContainsKey(dis))
            {
                var list = new List<int[]>(); list.Add(point);
                dict.Add(dis, list);
            }
            else
            {
                dict[dis].Add(point);
            }
        }
        // PriorityQueue<int, int> pq = new PriorityQueue<int, int>();
        return points;
        //TODO: Install .net4.8 and use pq. https://leetcode.com/problems/k-closest-points-to-origin/
    }

    public int[] NextSmallerElement(int[] arr)
    {
        int[] ans = new int[arr.Length];
        Stack<int> stack = new Stack<int>();
        for (int i = arr.Length - 1; i >= 0; i--)
        {
            //descreasing stack
            while (stack.Count != 0 && arr[stack.Peek()] >= arr[i])
            {
                stack.Pop();
            }
            ans[i] = stack.Count == 0 ? -1 : stack.Peek();
            stack.Push(i);
        }
        return ans;
    }

    public bool IsSameAfterReversals(int num)
    {
        if (num == 0) return true;
        if (num % 10 == 0) return false;
        return true;
    }

    public int[] ExecuteInstructions(int n, int[] startPos, string s)
    {
        int[,] dp = new int[n, n];//number
        int Execute(string s1)
        {
            int[] cur = (int[])startPos.Clone();
            int count = 0;
            foreach (char chr in s1)
            {
                if (chr == 'L')
                    cur[1]--;
                if (chr == 'R')
                    cur[1]++;
                if (chr == 'U')
                    cur[0]--;
                if (chr == 'D')
                    cur[0]++;
                if (cur[0] < 0 || cur[0] >= n || cur[1] < 0 || cur[1] >= n)
                    break;
                count++;
            }
            return count;
        }
        int[] ans = new int[s.Length];
        for (int i = 0; i < s.Length; i++)
            ans[i] = Execute(s.Substring(i));
        return ans;
    }

    public long[] GetDistances(int[] arr)
    {
        Dictionary<int, List<int>> dict = new Dictionary<int, List<int>>();
        for (int i = 0; i < arr.Length; i++)
        {
            if (!dict.ContainsKey(arr[i]))
            {
                dict.Add(arr[i], new List<int>(new int[] { i }));
            }
            else
            {
                dict[arr[i]].Add(i);
            }
        }
        long[] ans = new long[arr.Length];
        foreach (var pair in dict)
        {
            for (int i = 0; i < pair.Value.Count; i++)
            {
                for (int j = i + 1; j < pair.Value.Count; j++)
                {
                    int cur = Math.Abs(pair.Value[i] - pair.Value[j]);
                    ans[pair.Value[i]] += cur;
                    ans[pair.Value[j]] += cur;
                }
            }
        }
        return ans;
    }

    public int[] RecoverArray(int[] nums)
    {

        int[] Check(int k)
        {
            //so let's make life ezier. say nums contains lower and lower+k = higher. 
            Dictionary<int, int> counter = new Dictionary<int, int>();
            List<int> final = new List<int>();
            int halfK = k / 2;
            foreach (int i in nums)
            {
                if (!counter.ContainsKey(i)) counter.Add(i, 1);
                else counter[i]++;
            }
            foreach (int i in nums)
            {
                if (counter[i] != 0)
                {
                    if (!counter.ContainsKey(i + k) || counter[i + k] == 0) return new int[0];
                    counter[i + k]--; counter[i]--;
                    final.Add(i + halfK);
                }
            }
            return final.ToArray();
        }
        Array.Sort(nums);
        for (int i = 0; i < nums.Length; i++)
        {
            int k = nums[i] - nums[0];// actually k = 2k. as k is positive and nums[0] is smallest => nums[0]+k must exist!
            if (k != 0 && k % 2 == 0)
            {
                var res = Check(k);
                if (res.Length != 0) return res;
            }
        }
        return new int[0];
    }

    public bool CanReorderDoubled(int[] arr)
    {
        Array.Sort(arr);
        Dictionary<int, int> dict = new Dictionary<int, int>();
        foreach (int i in arr)
        {
            if (!dict.ContainsKey(i)) dict.Add(i, 1);
            else dict[i]++;
        }
        for (int i = arr.Length - 1; i >= 0; i--)
        {
            if (dict[arr[i]] != 0)
            {
                if (arr[i] == 0)
                {
                    if (dict[0] % 2 != 0) return false;
                }
                if (arr[i] > 0)
                {
                    if (arr[i] % 2 != 0 || !dict.ContainsKey(arr[i] / 2) || dict[arr[i] / 2] == 0) return false;
                    dict[arr[i]]--; dict[arr[i] / 2]--;
                }
                if (arr[i] < 0)
                {
                    if (!dict.ContainsKey(arr[i] * 2) || dict[arr[i]] == 0) return false;
                    dict[arr[i]]--; dict[arr[i] * 2]--;
                }
            }
        }
        return true;
    }

    public int FindComplement(int num)
    {
        //return ~num & (Integer.highestOneBit(num) - 1);
        return 1;
    }

    public IList<string> WordBreakII()
    {
        // PriorityQueue<int, int> pq = new PriorityQueue<int, int>();
        //TODO: Wordbreak 2
        return null;
    }

    public string LargestNumberII(int[] nums)
    {
        CompareLargestConcat comp = new CompareLargestConcat();
        Array.Sort(nums, comp);
        StringBuilder buffer = new StringBuilder();
        int start = nums.Length - 1;
        while (start >= 0 && nums[start] == 0) start--;
        if (start < 0) return "0";
        for (int i = start; i >= 0; i--)
            buffer.Append(nums[i].ToString());
        if (buffer.Length == 0) return "0";
        return buffer.ToString();
    }

    public int LeastInterval(char[] tasks, int n)
    {
        Dictionary<char, int> counter = new Dictionary<char, int>();
        int max = 0, maxCount = 0;
        foreach (char chr in tasks)
        {
            if (!counter.ContainsKey(chr)) counter.Add(chr, 1);
            else counter[chr]++;
            if (counter[chr] > max)
            {
                max = counter[chr]; maxCount = 1;
            }
            else if (counter[chr] == max)
            {
                maxCount++;
            }
        }

        int partCount = max - 1;
        int partLength = n - (maxCount - 1);
        int emptySlot = partCount * partLength;
        int availableTask = tasks.Length - max * maxCount;
        return Math.Max(0, emptySlot - availableTask) + tasks.Length;
    }

    public int[][] ReconstructQueue(int[][] people)
    {
        ReconstructQueueCompare comp = new ReconstructQueueCompare();
        Array.Sort(people, comp);
        List<int[]> res = new List<int[]>();
        foreach (var cur in people)
        {
            res.Insert(cur[1], cur);
        }
        return res.ToArray();
    }

    public ListNode MiddleNode(ListNode head)
    {
        if (head == null || head.next == null) return head;
        ListNode cur = head;
        int count = 0;
        while (cur != null)
        {
            cur = cur.next;
            count++;
        }
        cur = head;
        for (int i = 0; i < count / 2; i++) cur = cur.next;
        return cur;
    }

    public int EraseOverlapIntervals(int[][] intervals)
    {
        RemoveIntervalCompare comp = new RemoveIntervalCompare();
        Array.Sort(intervals, comp);
        int lastEnd = int.MinValue;
        int count = 0;
        for (int i = 0; i < intervals.Length;)
        {
            if (intervals[i][0] >= lastEnd) lastEnd = intervals[i][1];
            else count++;
        }
        return count;
    }

    public int TriangleNumber(int[] nums)
    {
        Array.Sort(nums);
        //so the idea is that we pick 2 index i < j. We find from j+1 to n-1 for the final number c (say index k) so that arr[i]+arr[j] > c (and >arr[j]-arr[i]). 
        //Next, use the last c in the prev j to find next c for j+1. just search from c-> n-1. 
        //in total: i loops n times, j times. c is found along j so it can never get more than j's time -> O(n^2)
        int count = 0;
        int BinarySearchLastSmallerIndex(int sum, int left)
        {
            //search for the final element's index in nums that is smaller than sum
            int right = nums.Length - 1;
            while (right > left)
            {
                int mid = (right + left) / 2;
                if (nums[mid] >= sum) right = mid - 1;
                else left = mid + 1;
            }
            while (nums[left] >= sum) left--;
            return left;
        }
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] == 0) continue;
            int lastK = i + 2; //index of C: arr[k] =c
            for (int j = i + 1; j < nums.Length - 1; j++)
            {
                if (nums[j] == 0) continue;
                if (lastK == nums.Length - 1)
                {
                    if (nums[i] + nums[j] > nums[lastK])
                        count += lastK - j;
                }
                else
                {
                    int k = BinarySearchLastSmallerIndex(nums[i] + nums[j], lastK);
                    if (k < j + 1) continue;//unfound
                    count += k - j; lastK = k;
                }
            }
        }
        return count;
    }

    public NexNode Connect(NexNode root)
    {
        Queue<NexNode> q = new Queue<NexNode>();
        q.Enqueue(root);
        q.Enqueue(new NexNode(int.MinValue));
        NexNode prev = null;
        while (q.Count != 0)
        {
            NexNode cur = q.Dequeue();
            if (cur != null && cur.val == int.MinValue && q.Count != 0)
            {//end of level;
                q.Enqueue(new NexNode(int.MinValue));
                prev = null;
            }
            else if (cur != null)
            {
                if (prev == null) prev = cur;
                else { prev.next = cur; prev = cur; }
                q.Enqueue(cur.left);
                q.Enqueue(cur.right);
            }
        }
        return root;
    }

    public int FindUnsortedSubarray(int[] nums)
    {
        int[] minRL = new int[nums.Length];
        int[] maxLR = new int[nums.Length];
        int temp = int.MaxValue;
        for (int i = nums.Length - 1; i >= 0; i--)
        {
            temp = Math.Min(nums[i], temp);
            minRL[i] = temp;
        }
        temp = int.MinValue;
        for (int i = 0; i < nums.Length; i++)
        {
            temp = Math.Max(nums[i], temp);
            maxLR[i] = temp;
        }
        int start = 0;//look for the first number that has value greater than its minRL
        while (start < nums.Length && nums[start] <= minRL[start]) start++;
        int end = nums.Length - 1;//look for the last number that has value greater than its maxLR
        while (end >= 0 && nums[end] >= maxLR[end]) end--;
        return Math.Max(0, end - start + 1);
    }

    public bool IncreasingTriplet(int[] nums)
    {
        int lastMin = nums[0];
        int lastMax = int.MaxValue;
        int dot = nums[0];

        int startInd = 0;
        while (startInd < nums.Length - 1 && nums[startInd] >= nums[startInd + 1]) startInd++;
        if (startInd >= nums.Length - 2) return false;
        lastMin = nums[startInd];
        lastMax = nums[startInd + 1];
        dot = nums[startInd];//dot <= lastMin
        for (int i = startInd + 2; i < nums.Length; i++)
        {
            int cur = nums[i];
            if (cur > lastMax) return true;
            if (cur > dot)
            {
                lastMin = dot;
                lastMax = cur;
            }
            else if (cur < dot)
            {
                dot = cur;
            }
        }
        return false;
    }

    public int SmallestRepunitDivByK(int k)
    {
        int remainder = 0;
        for (int i = 1; i <= k; i++)
        {
            remainder = (10 * remainder + 1) % k;
            if (remainder == 0) return i;
        }
        return -1;
    }

    public int FindMinArrowShots(int[][] points)
    {
        BaloonCompare comp = new BaloonCompare();
        Array.Sort(points, comp);
        int count =0;
        int pointer = 0;
        void FindIntervalAndMovePointer() {
            if (pointer>=points.Length) return;
            int start = points[pointer][0];
            int end = points[pointer][1];
            while (points[pointer][0]>=start && points[pointer][0]<=end) {               
                start = Math.Max(points[pointer][0], start);
                end = Math.Min(points[pointer][1], end);
                pointer++;
                if (pointer>=points.Length) break;
            }
            count++;
            FindIntervalAndMovePointer();
        } 
        FindIntervalAndMovePointer();
        return count;
    }

    int[] GetDigitArray (int n) {
        if (n<10) return new int[]{n};
        List<int> final = new List<int>();
        int numDig = GetNumberOfDigits(n);
        for (int i =numDig-1;i>=0;i--) {
            int cur = n % (int)Math.Pow(10, numDig);
            final.Add(cur);
            n -= cur*(int)Math.Pow(10, numDig);
        }
        return final.ToArray();
    }

    public int MonotoneIncreasingDigits(int n) {
        int numDig = GetNumberOfDigits(n);
        
    }

}

public class NexNode
{
    public int val;
    public NexNode left;
    public NexNode right;
    public NexNode next;

    public NexNode() { }

    public NexNode(int _val)
    {
        val = _val;
    }

    public NexNode(int _val, NexNode _left, NexNode _right, NexNode _next = null)
    {
        val = _val;
        left = _left;
        right = _right;
        next = _next;
    }

    public void InorderTraversal()
    {
        void Traverse(NexNode root)
        {
            if (root == null) return;
            Traverse(root.left);
            Console.Write(root.val + " ");
            Traverse(root.right);
        }
        Traverse(this);
    }
    public void SeeNext()
    {
        if (this == null) return;
        List<NexNode> allLeft = new List<NexNode>();
        var cur = this;
        while (cur != null)
        {
            allLeft.Add(cur);
            cur = cur.left;
        }
        foreach (var node in allLeft)
        {
            cur = node;
            while (cur != null)
            {
                Console.Write(cur.val + " ");
                cur = cur.next;
            }
            Console.WriteLine();
        }
    }
}
public class StockSpanner
{
    Stack<int[]> stack;
    public StockSpanner()
    {
        stack = new Stack<int[]>(); //keep [price, count]
    }

    public int Next(int price)
    {
        //number of prices that are smaller than today's price
        int res = 1;
        while (stack.Count != 0 && stack.Peek()[0] <= price)
        {
            res += stack.Pop()[1];
        }
        stack.Push(new int[] { price, res });
        return res;
    }
}

public class Pair
{
    public int min, max;
    public Pair(int min, int max) { this.min = min; this.max = max; }
}
public class BSTIterator
{
    Stack<int> stack = new Stack<int>();
    public BSTIterator(TreeNode root)
    {
        InorderTraversal(root);
    }
    void InorderTraversal(TreeNode root)
    {
        if (root == null) return;
        InorderTraversal(root.left);
        stack.Push(root.val);
        InorderTraversal(root.right);
    }

    public int Next()
    {
        return stack.Pop();
    }

    public bool HasNext()
    {
        return stack.Count != 0;
    }
}
public class MinStack
{
    Stack<int> stack = new Stack<int>();
    Stack<int> min = new Stack<int>();
    public MinStack()
    {
        min.Push(int.MaxValue);
    }

    public void Push(int val)
    {
        stack.Push(val);
        min.Push(Math.Min(val, min.Peek()));
    }

    public void Pop()
    {
        if (stack.Count != 0) { stack.Pop(); min.Pop(); }
    }

    public int Top()
    {
        if (stack.Count != 0) return stack.Peek();
        return -1;
    }

    public int GetMin()
    {
        return min.Peek();
    }
}
public class LRUCache
{

    int cap;
    int curTop = int.MinValue; //top level one
    int curLowest = int.MinValue;
    Dictionary<int, int> dict = new Dictionary<int, int>();//store present <key, value>
    Dictionary<int, int> kp = new Dictionary<int, int>(); //store priority of <key, priority>
    Dictionary<int, int> pk = new Dictionary<int, int>(); //store <priority, key>
    /*
    Whenever a new element is added
        - If not thing exceed cap, update dict, kp, pk with the value and the highest possible curTop+1 //even higher than the highst one and curLowest
        - If something exceeds cap, look at the pk[curLowest] to get the key, delete it in dict, also in kp

    */
    public LRUCache(int capacity)
    {
        this.cap = capacity;
    }

    public int Get(int key)
    {
        if (dict.ContainsKey(key))
        {
            pk.Remove(kp[key]);
            pk.Add(++curTop, key);
            kp[key] = curTop;
            return dict[key];
        }
        return -1;
    }

    public void Put(int key, int value)
    {
        if (!dict.ContainsKey(key))
        {
            if (dict.Count < cap)
            {
                dict.Add(key, value);
                kp.Add(key, ++curTop);
                pk.Add(curTop, key);
            }
            else
            {
                while (!pk.ContainsKey(curLowest)) curLowest++;
                int delKey = pk[curLowest];
                dict.Remove(delKey);
                kp.Remove(delKey);
                pk.Remove(curLowest++);
                Put(key, value);
            }
        }
        else
        {
            dict[key] = value;
            pk.Remove(kp[key]);
            pk.Add(++curTop, key);
            kp[key] = curTop;
        }
    }

}
public class LLNode
{
    public int val = 0; public LLNode next = null;
    public LLNode(int val, LLNode next) { this.val = val; this.next = next; }
    public LLNode() { }

}
class UnionFindSurroundedRegion
{
    int[] parents;
    public UnionFindSurroundedRegion(int numNodes)
    {
        parents = new int[numNodes];
        for (int i = 0; i < numNodes; i++) parents[i] = i;
    }

    public int FindRepresentative(int node)
    {
        while (node != parents[node])
        {
            parents[node] = parents[parents[node]];
            node = parents[node];
        }
        return node;
    }
    public void Union(int n1, int n2)
    {
        int r1 = FindRepresentative(n1);
        int r2 = FindRepresentative(n2);
        if (r1 != r2)
        {
            parents[r2] = r1;
        }
    }
    public bool IsConnected(int n1, int n2)
    {
        return FindRepresentative(n1) == FindRepresentative(n2);
    }
}
public class NestedInteger
{
    List<NestedInteger> list = new List<NestedInteger>();
    public NestedInteger() { }
    public NestedInteger(int value)
    {
        NestedInteger cur = new NestedInteger(value);
        list.Add(cur);
    }
    public bool IsInteger()
    {
        return false;
    }
    public int GetInteger() { return 1; }
    public void SetInteger(int value) { }
    public void Add(NestedInteger ni) { }
    public IList<NestedInteger> GetList()
    {
        return list;
    }
}
public class Nest
{
    public int DepthSum(IList<NestedInteger> nestedList)
    {
        int final = 0;
        foreach (var nest in nestedList)
        {
            final += DepthSumUtil(nest, 1);
        }
        return final;
    }
    int DepthSumUtil(NestedInteger nest, int curDepth)
    {
        if (nest.IsInteger()) return curDepth * nest.GetInteger();
        //if not a single integer, then it is a list
        int sum = 0;
        foreach (NestedInteger childnest in nest.GetList())
        {
            sum += DepthSumUtil(childnest, curDepth + 1);
        }
        return sum;
    }
}
public class RandomWeightPicker
{
    int[] weight;
    int sum = 0;
    Random seed = new Random();
    int[][] fromTo;

    public RandomWeightPicker(int[] w)
    {
        this.weight = w;
        fromTo = new int[w.Length][];
        int cur = 0;
        foreach (int i in w) sum += i;
        for (int i = 0; i < fromTo.Length; i++)
        {
            fromTo[i] = new int[2] { cur += 1, cur += weight[i] - 1 };
        }
    }

    public void LookInside()
    {
        foreach (var a in fromTo)
        {
            Console.WriteLine("Sum: " + sum);
            foreach (int i in a)
                Console.Write(i + " ");
            Console.WriteLine();
        }
    }


    public int PickIndex()
    {
        int index = seed.Next(0, sum + 1);
        int l = 0; int r = fromTo.Length - 1;
        while (r > l)
        {
            int mid = (l + r) / 2;
            if (fromTo[mid][0] <= index && index <= fromTo[mid][1]) return mid;
            if (index > fromTo[mid][1]) l = mid + 1;
            else r = mid - 1;
        }
        return l;
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
public class CombinationIterator
{
    //TODO1: Finish this shit
    public CombinationIterator(string characters, int combinationLength)
    {

    }

    public string Next()
    {
        return "";
    }

    public bool HasNext()
    {
        return false;
    }
}





