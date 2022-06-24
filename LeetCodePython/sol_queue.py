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
        #either i is max or min's index,
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
                i+=1
            res = max(res, j - i + 1)
        return res

    def maxSubarraySumCircular(self, nums: list[int]) -> int:
        # https://leetcode.com/problems/maximum-sum-circular-subarray/
        return -1;

    def findKthLargest(self, nums: list[int], k: int) -> int:
        # https://leetcode.com/problems/kth-largest-element-in-an-array/
        return -1;
