from collections import defaultdict


class Solution_DynamicProgramming:
    def minCostClimbingStairs(self, cost: list[int]) -> int:
        n = len(cost)
        dp = [0] * (n + 2)  # dp[i] smallest sum start at i
        for i in range(n - 1, -1, -1):
            dp[i] = min(dp[i + 1], dp[i + 2]) + cost[i]
        return min(dp[0], dp[1])

    def findTargetSumWays(self, nums: list[int], target: int) -> int:
        n = len(nums)
        dp = []
        for i in range(n):
            dp.append(dict())

        def cache(i, sum):
            if i == 0:
                if sum == nums[i] or sum == -nums[i]:
                    return 1
                else:
                    return 0
            if sum not in dp[i]:
                dp[i].setdefault(sum, cache(i - 1, sum + nums[i]) + cache(i - 1, sum - nums[i]))
            return dp[i][sum]

        return cache(n - 1, target)

    def orderOfLargestPlusSign(self, n: int, mines: list[list[int]]) -> int:
        dp = [[0] * n for _ in range(n)]
        ans = 0
        banned = [tuple(mine) for mine in mines]
        for r in range(n):
            count = 0
            for c in range(n):
                count = 0 if (r, c) in banned else count + 1
                dp[r][c] = count
            count = 0
            for c in range(n - 1, -1, -1):
                count = 0 if (r, c) in banned else count + 1
                if dp[r][c] > count: dp[r][c] = count

        for c in range(n):
            for r in range(n):
                count = 0 if (r, c) in banned else count + 1
                if dp[r][c] > count: dp[r][c] = count
            for r in range(n - 1, -1, -1):
                count = 0 if (r, c) in banned else count + 1
                if dp[r][c] > count: dp[r][c] = count
                if dp[r][c] > ans: ans = dp[r][c]

        return ans

    def makesquare(self, matchsticks: list[int]) -> bool:
        if not matchsticks: return False
        n = len(matchsticks)
        perimeter = sum(matchsticks)
        if perimeter % 4 != 0: return False
        possible_side = perimeter / 4
        dp = {}

        def recurse(mask, side_dones):
            total = 0
            for i in range(n - 1, -1, -1):
                if not (mask & (1 << i)):
                    total += matchsticks[i]  # total sum of matchstick used so far
            if total % possible_side == 0: side_dones += 1
            if side_dones == 3: return True
            if (mask, side_dones) in dp: return dp[(mask, side_dones)]

            ans = False
            c = int(total / possible_side)
            rem = possible_side * (c + 1) - total  # availablespace in the current side
            for i in range(n - 1, -1, -1):
                # if current one fit and not used
                if matchsticks[i] < rem and mask & (1 << i):
                    if recurse(mask ^ (1 << i), side_dones):
                        ans = True
                        break
            dp[(mask, side_dones)] = ans
            return ans

        return recurse((1<<n) -1, 0)