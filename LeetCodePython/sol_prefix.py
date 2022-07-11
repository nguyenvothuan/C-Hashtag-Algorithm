from collections import defaultdict
import heapq


class Soloution_Prefix:
    def minSubArrayLen(self, target: int, nums: list[int]) -> int:
        last = 0
        sum = 0
        res = 999999
        for i, num in enumerate(nums):
            sum += num
            while sum >= target:
                res = min(res, i - last + 1)
                sum -= nums[last]
                last += 1
        return 0 if res == 999999 else res

    def subarraySum(self, nums: list[int], k: int) -> int:
        sum = 0
        tudien = dict()
        res = 0
        tudien.setdefault(0, 1)
        for num in nums:
            sum += num
            if sum - k in tudien:
                res += tudien[sum - k]
            if sum in tudien:
                tudien[sum] += 1
            else:
                tudien.setdefault(sum, 1)
        return res

    def findMaxLength(self, nums: list[int]) -> int:
        balance = 0
        tudien = dict()
        res = 0
        tudien.setdefault(0, -1)
        for i, num in enumerate(nums):
            balance += 1 if num == 1 else -1
            if balance in tudien:
                res = max(res, i - tudien[balance])
            else:
                tudien.setdefault(balance, i)
        return res

    def numberOfArrays(self, differences: list[int], lower: int, upper: int) -> int:
        sum = 0
        ma = 0
        mi = 0
        for num in differences:
            sum += num
            ma = max(ma, sum)  # largest sum encountered so far
            mi = min(mi, sum)  # smallest sum
        # do we give a fuck whether max is behind min or not. no, max>min, and max-min is always the difference between max and min numbers, no big deal
        return max(0, upper - lower - (ma - mi) + 1)

    def platesBetweenCandles(self, s: str, queries: list[list[int]]) -> list[int]:
        n = len(str)
        left = [0] * n
        right = [0] * n
        res = [0] * n
        last = -1
        for i, chr in enumerate(s):
            if chr == '|':
                last = i
            right[i] = last
        last = -1
        for i, chr in reversed(list(enumerate(s))):
            if chr == '|':
                last = i
            left[i] = last
        candleCount = [0] * n  # so candle count[i] will be the no candle up to index i
        count = 0
        for i, chr in enumerate(s):
            if chr == '|':
                count += 1
            candleCount[i] = count
        for i, query in enumerate(queries):
            nearestRight = right[queries[0]]  # left bound
            nearestLeft = left[queries[1]]  # right bound, lmao
            if nearestLeft == -1 or nearestRight == -1: res[i] = 0
            d = nearestLeft - nearestRight
            if d < 1:
                res[i] = 0
            else:
                res[i] = d + 1 - (candleCount[nearestLeft] - candleCount[nearestRight] + 1)
        return res

    def subarraysDivByK(self, nums: list[int], k: int) -> int:
        tudien = dict()  # store number of num in list with same modulo by k
        res = 0
        tudien.setdefault(0, 1)
        sum = 0
        for num in nums:
            sum += num
            modulo = sum % k
            if modulo in tudien:
                res += tudien[modulo]
                tudien[modulo] += 1
            else:
                tudien.setdefault(modulo, 1)
        return res

    def corpFlightBookings(self, bookings: list[list[int]], n: int) -> list[int]:
        res = [0] * n
        for booking in bookings:
            res[booking[0] - 1] += booking[2]
            if booking[1] < n: res[booking[1]] -= booking[2]
        for i in range(1, n):
            res[i] += res[i - 1]
        return res

    def maxConsecutiveAnswers(self, answerKey: str, k: int) -> int:
        last = 0
        res = 0
        count = 0
        n = len(answerKey)
        temp = k
        # first loop, change F to T
        for i in range(n):
            if answerKey[i] == 'T':
                count += 1
            else:
                k -= 1
                if k == -1:
                    while answerKey[last] == 'T':
                        last += 1
                        count -= 1
                    last += 1
                    count -= 1
                    k = 0
                count += 1
            res = max(res, count)

        # second loop, change T to F
        last = 0
        count = 0
        k = temp
        for i in range(n):
            if answerKey[i] == 'F':
                count += 1
            else:
                k -= 1
                if k == -1:
                    while answerKey[last] == 'F':
                        last += 1
                        count -= 1
                    last += 1
                    count -= 1
                    k = 0
                count += 1
            res = max(res, count)
        return res

    def maximumWhiteTiles(self, tiles: list[list[int]], carpetLen: int) -> int:
        res = 0
        j = 0
        cover = 0
        i = 0
        tiles.sort(key=lambda x: x[0])
        while res < carpetLen and i < len(tiles):
            # if adding new tile still in bound of len
            if tiles[j][0] + carpetLen > tiles[i][1]:
                cover += tiles[i][1] - tiles[i][0] + 1
                res = max(res, cover)
                i += 1
            # try get partial tile from i
            else:
                partial = max(0, tiles[j][0] + carpetLen - tiles[i][0])
                res = max(res, cover + partial)
                # move sliding window
                cover -= (tiles[j][1] - tiles[j][0] + 1)
                j += 1
        return res

    def goodDaysToRobBank(self, security: list[int], time: int) -> list[int]:
        n = len(security)
        if time == 0: return [i for i in range(n)]
        gL = [0] * n
        gR = [0] * n
        res = []
        for i in range(n - 2, -1, -1):
            gR[i] = gR[i + 1] + 1 if security[i] <= security[i + 1] else 0
        for i in range(1, n):
            gL[i] = gL[i - 1] + 1 if security[i] <= security[i - 1] else 0
        for i in range(time - 1, n - time):
            if gL[i] >= time and gR[i] >= time:
                res.append(i)
        return res

    def splitPainting(self, segments: list[list[int]]) -> list[list[int]]:
        mix, res, last_i = defaultdict(int), [], 0
        for start, end, color in segments:
            mix[start] += color
            mix[end] -= color
        for i in sorted(mix.keys()):
            if last_i in mix and mix[last_i]:  # color changes, update segment
                res.append([last_i, i, mix[last_i]])
                mix[i] += mix[last_i]
            last_i = i
        return res

    def minimumRemoval(self, beans: list[int]) -> int:
        res = 9999
        beans.sort()
        sigma = sum(beans)
        n = len(beans)
        for i, bean in enumerate(beans):
            res = min(res, sigma - bean * (n - i))
        return res

    def numberOfWays(self, s: str) -> int:
        zeroLeft = [0] * (len(s))
        zeroRight = [0] * (len(s))
        oneLeft = [0] * (len(s))
        oneRight = [0] * (len(s))
        zero = 0
        one = 0
        for i, num in enumerate(s):
            zeroLeft[i] = zero
            oneLeft[i] = one
            if num == '0':
                zero += 1
            else:
                one += 1
        zero = 0
        one = 0
        for i, num in reversed(list(enumerate(s))):
            zeroRight[i] = zero
            oneRight[i] = one
            if num == '0':
                zero += 1
            else:
                one += 1
        res = 0
        for i in range(1, len(s) - 1):
            if s[i] == '0':
                res += oneLeft[i] * oneRight[i]
            else:
                res += zeroLeft[i] * zeroRight[i]
        return res

    def equalSubstring(self, s: str, t: str, maxCost: int) -> int:
        n = len(s)
        diffArr = [0] * n
        for i in range(n):
            diffArr[i] = abs(ord(s[i]) - ord(t[i]))
        res = 0
        cur = 0
        start = 0
        for i in range(n):
            cur += diffArr[i]
            while cur > maxCost:
                cur -= diffArr[start]
                start += 1
            res = max(i - start + 1, res)
        return res

    def largestMagicSquare(self, grid: list[list[int]]) -> int:
        m, n = len(grid), len(grid[0])
        psCol = [[0] * (n + 1) for _ in range(m)]
        psRow = [[0] * (m + 1) for _ in range(n)]
        for r in range(m):
            for c in range(n):
                psCol[c][r + 1] = psCol[c][r] + grid[r][c]
                psRow[r][c + 1] = psRow[r][c] + grid[c][r]

        def getColSum(c, l, r):  # get sum of col, from l to r inclusively
            return psCol[c][r + 1] - psCol[c][l]

        def getRowSum(r, d, u):
            return psRow[r][u + 1] - psRow[r][d]

        def check(k):
            # check if there is a magic square of size k in da grid
            for r in range(m - k + 1):
                for c in range(n - k + 1):
                    diag, antiDiag = 0, 0
                    for d in range(k):
                        diag += grid[r + d][c + d]
                        antiDiag += grid[r + d][c + k - 1 - d]
                    match = diag == antiDiag
                    nr, nc = r, c
                    # check row
                    while nr < r + k and match:
                        match = diag == getRowSum(nr, c, c + k - 1)
                        nr += 1
                    while nc < c + k and match:
                        match = diag == getColSum(nc, r, r + k - 1)
                        nc += 1
                    if match: return True
            return False

        for k in range(min(m, n), 3, -1):
            if check(k):
                return k
        return 1

    def kthLargestValue(self, matrix: list[list[int]], k: int) -> int:
        m, n = len(matrix), len(matrix[0])
        value = [[0] * n for _ in range(m)]
        heap = []
        value[0][0] = matrix[0][0]
        for c in range(1, n):
            value[0][c] = matrix[0][c] ^ value[0][c - 1]
        for r in range(1, m):
            value[r][0] = matrix[r][0] ^ value[r - 1][0]
        for r in range(1, m):
            for c in range(1, n):
                value[r][c] = matrix[r][c] ^ value[r - 1][c] ^ value[r][c - 1] ^ value[r - 1][c - 1]
        for r in range(m):
            for c in range(n):
                heap.append(value[r][c])
        return heapq.nlargest(k, heap)[-1]

    def countPalindromicSubsequence(self, s: str) -> int:
        keys = [chr(i) for i in range(97, 123)]
        d = dict()
        n = len(s)
        for char in keys:
            d.setdefault(char, [-1] * (n + 1))
        for i in range(n - 1, -1, -1):
            cur = s[i]
            # simply copy
            for char in d.keys():
                d[char][i] = d[char][i + 1]
            d[cur][i] = i
        s = set()
        for first in keys:
            for second in keys:
                for third in keys:
                    if first != third: continue
                    cur = first+second+third
                    if cur not in s:
                        fi = d[first][0]  # index of the first instance of s[first]
                        if fi != -1:
                            si = d[second][fi+1]
                            if si != -1:
                                ti = d[third][si+1]
                                if ti != -1:
                                    s.add(cur)
        return len(s)

    def maxFrequency(self, nums: list[int], k: int) -> int:
        return 1