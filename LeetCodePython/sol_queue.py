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
