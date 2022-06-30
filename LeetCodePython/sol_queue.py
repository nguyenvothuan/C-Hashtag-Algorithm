from collections import deque


class Solution_Queue:
    def maxSlidingWindow1(self, nums: list[int], k: int) -> list[int]:
        n = len(nums)
        if n == 0: return nums
        res = [0] * (n - k + 1)
        dq = deque()
        for i in range(n):
            if len(dq) > 0 and dq[0] < i - k + 1:
                dq.popleft()
            while len(dq) > 0 and nums[i] >= nums[dq[-1]]:
                dq.pop()
            dq.append(i)
            if i - k + 1 >= 0:
                res[i - k + 1] = nums[dq[0]]
        return res

    def maxSlidingWindow(self, nums: list[int], k: int) -> list[int]:
        n = len(nums)
        res = [0] * (n - k + 1)
        pointer = 0
        dq = deque()
        for i in range(n):
            if len(dq) > 0 and dq[0] < i - k + 1: dq.popleft()
            while len(dq) > 0 and nums[dq[-1]] < nums[i]: dq.pop()
            dq.append(i)
            if i - k + 1 >= 0: res[i - k + 1] = nums[dq[0]]
        return res

    def maxResult(self, nums: list[int], k: int) -> int:
        n = len(nums)
        dp = [0] * n
        deck = deque()
        for i in range(n - 1, -1, -1):
            # if the right most one is out of bound
            if len(deck) > 0 and deck[-1] > min(n - 1, i + k):
                deck.pop()
            if len(deck) > 0:
                dp[i] = nums[i] + dp[deck[-1]]
            else:
                dp[i] = nums[i]
            while len(deck) > 0 and dp[i] > dp[deck[0]]: deck.popleft()
            deck.appendleft(i)
        return dp[0]

    def shortestSubarray(self, nums: list[int], k: int) -> int:
        n = len(nums)
        deck = deque()
        p = [0] * n
        # algorithm: keep an increasing deck. x_i<x_i+1 and p[x_i] < p[x_i+1]
        for i, num in enumerate(nums):
            if i == 0:
                p[i] = nums[0]
            else:
                p[i] = p[i - 1] + nums[i]
        ans = n + 1
        for i in range(n):
            while len(deck) > 0 and p[i] <= p[deck[-1]]: deck.pop()
            while len(deck) > 0 and p[i] - p[deck[0]] >= k:
                ans = min(ans, i - deck.popleft())
            deck.append(i)
        return ans if ans < n else -1

    def constrainedSubsetSum(self, nums: list[int], k: int) -> int:
        # https://leetcode.com/problems/constrained-subsequence-sum/
        deck = deque()
        n = len(nums)
        dp = [0] * n
        res = -999
        for i in range(n - 1, -1, -1):
            if deck and deck[-1] > min(n - 1, i + k):
                deck.pop()
            if deck:
                dp[i] = dp[deck[-1]] + nums[i]
            else:
                dp[i] = nums[i]
            cur = max(dp[i], nums[i])
            while deck and dp[deck[0]] <= cur:
                deck.popleft()
            deck.appendleft(i)
            res = max(res, dp[i], nums[i])
        return res

    def longestSubarray(self, nums: list[int], lim: int) -> int:
        # https://leetcode.com/problems/longest-continuous-subarray-with-absolute-diff-less-than-or-equal-to-limit/
        mind = deque()  # increasing deck, keep min of current window at its left most
        maxd = deque()  # decrease deck, max at the left most
        # either i is max or min's index,
        i = 0
        n = len(nums)
        res = 0
        for j in range(n):
            # update max deck
            while maxd and nums[i] > maxd[-1]: maxd.pop()
            while mind and nums[i] < mind[-1]: mind.pop()
            maxd.append(nums[i])
            mind.append(nums[i])
            # if max - min is larger than lim, update res
            if maxd[0] - mind[0] > lim:
                if maxd[0] == nums[i]:
                    maxd.popleft()
                if mind[0] == nums[i]:
                    mind.popleft()
                i += 1
            res = max(res, j - i + 1)
        return res

    def numberOfSubstrings(self, s: str) -> int:
        res = i = 0
        count = {c: 0 for c in 'abc'}
        for j in range(len(s)):
            count[s[j]] += 1
            while all(count.values()):
                count[s[i]] -= 1
                i += 1
            res += i
        return res

    def numberOfSubarrays(self, nums: list[int], k: int) -> int:
        count = 0
        i = 0
        res = 0
        for j in range(len(nums)):
            if nums[j] % 2 == 1:
                k -= 1
                count = 0
            while k == 0:
                k += nums[i] & 1
                count += 1
                i += 1
            res += count
        return res

    def totalFruit(self, fruits: list[int]) -> int:
        n = len(fruits)
        i = 0
        res = 0
        dic = dict()
        for j in range(n):
            if fruits[j] in dic:
                dic[fruits[j]] += 1
            else:
                dic.setdefault(fruits[j], 1)
            if len(dic) > 2:
                res = max(res, j - i)
                # delete the first one that reaches zero
                while len(dic) > 2:
                    dic[fruits[i]] -= 1
                    if dic[fruits[i]] == 0:
                        del dic[fruits[i]]
                    i += 1
        res = max(res, j - i + 1)
        return res

    def maxSatisfied(self, customers: list[int], grumpy: list[int], minutes: int) -> int:
        sum = 0  # sum is the no more customers if owner chooses to be not grumpy
        n = len(customers)
        for i in range(0, minutes):
            sum += customers[i] if grumpy[i] == 1 else 0
        res = sum

        for i in range(0, n - minutes):
            sum -= customers[i] if grumpy[i] == 1 else 0
            sum += customers[i + minutes] if grumpy[i + minutes] == 1 else 0
            res = max(res, sum)

        for i in range(n):
            res += customers[i] if grumpy[i] == 0 else 0  # fill the res
        return res

    def maxSubarraySumCircular(self, nums: list[int]) -> int:
        # https://leetcode.com/problems/maximum-sum-circular-subarray/
        return -1

    def maxScore(self, cardPoints: list[int], k: int) -> int:
        n = len(cardPoints)

        def ind(i: int):
            if i >= n: return i % (n - 1)
            return i

        sum = 0
        for i in range(n - k, n):
            sum += cardPoints[i]
        res = sum
        for i in range(n - k + 1, n + 1):  # if start at n-k to n or 0
            sum = sum - cardPoints[ind(i)] + cardPoints[ind(i + n - 1)]
            res = max(res, sum)
        return res

    def findKthLargest(self, nums: list[int], k: int) -> int:
        # https://leetcode.com/problems/kth-largest-element-in-an-array/
        return -1

    def minMoves(self, nums: list[int], k: int) -> int:
        p = [i for i, v in enumerate(nums) if v == 1]
        n = len(nums)
        pre = [0] * (n + 1)  # pre[i+1] = sum(p[0], p[1],...,p[i])
        for i in range(n):
            pre[i + 1] = pre[i] + nums[i]
        res = 99999
        if k & 1:
            radius = (k - 1) // 2
            for i in range(radius, n - radius):
                right = pre[i + radius + 1] - pre[i + 1]
                left = pre[i] - pre[i - radius]
                res = min(right - left, res)
            return res - radius * (radius + 1)
        else:
            radius = (k - 2) // 2
            for i in range(radius, n - radius - 1):
                right = pre[i + radius + 2] - pre[i + 1]
                left = pre[i] - pre[i - radius]
                res = min(right - left - p[i], res)
            return res - (radius + 1) * (radius - 1)

    def maxTurbulenceSize(self, arr: list[int]) -> int:
        up = True
        count = 0
        res = 0
        last = -9999999
        for i, cur in enumerate(arr):
            if up:
                if last > cur:
                    up = False
                    count += 1
                else:
                    res = max(res, count)
                    count = 2
            else:
                if last < cur:
                    up = True
                    count += 1
                else:
                    res = max(res, count)
                    count = 2
            last = cur
        res = max(res, count)
        return res
