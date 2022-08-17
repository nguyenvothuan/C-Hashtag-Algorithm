import math
from collections import defaultdict
from typing import List


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

        return recurse((1 << n) - 1, 0)

    def findMaxForm(self, strs: list[str], m: int, n: int) -> int:
        L = len(strs)
        dp = [[[-1] * (m + 1) for _ in range(n + 1)] for _ in range(L)]

        def count(s: str):
            return s.count('0'), s.count('1')

        arr = [count(s) for s in strs]

        def util(i, a, b):
            if a < 0 or b < 0: return -1
            if i < 0: return -1
            if dp[i][a][b] == -1:
                dp[i][a][b] = max(util(i - 1, a, b), util(i - 1, a - arr[i][0], b - arr[i][1]) + 1)
            return dp[i][a][b]

        return util(L - 1, n, m)

    def PredictTheWinner(self, nums: list[int]) -> bool:
        n = len(nums)
        dp = [[-1] * n for _ in range(n)]
        ps = [0] * n
        sum = 0
        for i in range(n):
            sum += nums[i]
            ps[i] = sum  # up to but not includes i

        def sum_range(i, j):  # inclusively
            return ps[j] - ps[i] + nums[j]

        def util(i, j):  # max score if start first
            if i == j: return nums[i]
            if i > j: return -99999
            if dp[i][j] == -1:
                dp[i][j] = max(nums[i] + sum_range(i + 1, j) - util(i + 1, j),
                               nums[j] + sum_range(i, j - 1) - util(i, j - 1))
            return dp[i][j]

        return util(0, n - 1) > int(sum / 2)

    def champagneTower(self, poured: int, query_row: int, query_glass: int) -> float:
        dp = [[-1] * (query_row + 1) for _ in range(query_row + 1)]
        dp[0][0] = poured

        def ex(i, j):  # vol of glasses recieved by i,j
            if i < 0 or j < 0 or i > j: return 0
            if dp[i][j] == -1:
                dp[i][j] = max(ex(i - 1, j - 1) - 1, 0) / 2 + max(ex(i, j - 1) - 1, 0) / 2
            return dp[i][j]

        return min(1, ex(query_row, query_glass))

    def findPaths(self, m: int, n: int, maxMove: int, startRow: int, startColumn: int) -> int:
        dp = [[[-1] * n for _ in range(m)] for _ in range(maxMove + 1)]

        def util(move, i, j):
            if i < 0 or j < 0 or i >= m or j >= n: return 1
            if move == 0: return 0
            if dp[move][i][j] == -1:
                dp[move][i][j] = util(move - 1, i - 1, j) + util(move - 1, i, j - 1) \
                                 + util(move - 1, i + 1, j) + util(move - 1, i, j + 1)
            return dp[move][i][j]

        return util(maxMove, startRow, startColumn) % (pow(10, 9) + 7)

    def kInversePairs(self, n: int, k: int) -> int:
        dp = [[-1] * (k + 1) for _ in range(n)]

        def util(i, j):
            if i == 0: return 0
            if j == 0: return i
            if dp[i][j] == -1:
                dp[i][j] = util(i, j - 1) + util(i - 1, j) - util(i - 1, j - 1)
            return dp[i][j]

        return util(n - 1, k)

    def numSubmatrixSumTarget(self, matrix: list[list[int]], target: int) -> int:
        m = len(matrix)
        n = len(matrix[0])
        for row in matrix:
            for i in range(n - 1):
                row[i + 1] += row[i]
        res = 0
        for i in range(n):
            for j in range(i, n):
                # explore every submatrix that start at columns i and end at j
                c = defaultdict(int)
                c[0] = 1
                cur = 0
                for k in range(m):
                    cur += matrix[k][j] - (matrix[k][i - 1] if i > 0 else 0)
                    res += c[cur - target]
                    c[cur] += 1
        return res

    def generate(self, numRows: int) -> list[list[int]]:
        res = [[]]

        def pascal(i, j, last):
            if j == 0 or i == 0: return 1
            return last[j - 1] + last[j]

        for i in range(numRows):
            cur = []

            for j in range(i):
                cur.append(pascal(i, j, res[-1]))
            cur.append(1)
            res.append(cur)
        return res[1:]

    def divisorGame(self, n: int) -> bool:
        dp = [0] * 1001

        def util(i):
            if i == 1 or i == 3: return False
            if i == 2: return True
            if dp[i] == 0:
                dp[i] = -1
                for x in range(1, i):
                    if i % x == 0:
                        if not util(i - x):
                            dp[i] = 1
                            break
            return dp[i] == 1

        return util(n)

    def combinationSum4(self, nums: list[int], target: int) -> int:
        dp = [-1] * (target + 1)  # n.o way to reach i

        def util(i):
            if i < 0: return 0
            if i == 0: return 1
            if dp[i] == -1:
                dp[i] = 0
                for num in nums:
                    dp[i] += util(i - num)
            return dp[i]

        return util(target)

    def numRollsToTarget(self, n: int, k: int, target: int) -> int:
        # dp = [[-1] * (n + 1)] * (target + 1)
        dp = [[-1 for _ in range(n + 1)] for _ in range(target + 1)]

        def helper(s, count):
            if s < 0 or count < 0: return 0
            if s == 0: return 1
            if dp[s][count] == -1:
                dp[s][count] = 0
                for i in range(1, k + 1):
                    dp[s][count] += helper(s - i, count - 1)
            return dp[s][count]

        return int(helper(target, n) % (pow(10, 9) + 7))

    def numFactoredBinaryTrees(self, arr: List[int]) -> int:
        s = set(arr)
        n = len(arr)
        dp = dict()
        for i in arr:
            dp.setdefault(i, -1)

        def helper(i):
            if i not in s: return 0
            if dp[i] == -1:
                dp[i] = 1
                for j in range(1, 1+int(math.sqrt(i))):
                    if i % j == 0:
                        dp[i] += helper(j) * helper(i / j)
            return dp[i]

        sum = 0
        for i in arr:
            sum += helper(i)
        return sum