import math
from collections import OrderedDict
from bisect import bisect_left, bisect_right, bisect


class Solution_Misc:
    def minMoves2(self, nums: list[int]) -> int:
        # choose median, not mean
        nums.sort()
        res = 0
        i = 0
        j = len(nums) - 1
        while j > i:
            res += (nums[j] - nums[i])
            j -= 1
            i += 1
        return res

    def maxArea(self, h: int, w: int, horizontalCuts: list[int], verticalCuts: list[int]) -> int:
        horizontalCuts.append(h)
        verticalCuts.append(w)
        n = len(horizontalCuts)
        m = len(verticalCuts)
        horizontalCuts.sort()
        verticalCuts.sort()
        maxGapH = horizontalCuts[0]
        maxGapV = verticalCuts[0]
        for i in range(1, n):
            maxGapH = max(maxGapH, horizontalCuts[i] - horizontalCuts[i - 1])
        for i in range(1, m):
            maxGapV = max(maxGapV, verticalCuts[i] - verticalCuts[i - 1])
        return (maxGapV * maxGapH) % (pow(10, 9) + 7)

    def longestConsecutive(self, nums: list[int]) -> int:
        nums = list(set(nums))
        s, d = set(), dict()
        for num in nums:
            if num + 1 in s:
                d.setdefault(num, d[num + 1] + 1)
                del d[num + 1]
                s.remove(num + 1)
            else:
                d.setdefault(num, 1)
            s.add(num)
        maxi = 0
        unchanged = False
        while not unchanged:
            b4 = len(s)
            for num in s:
                maybe = num + d[num]
                if maybe in s:
                    d[num] += d[maybe]
                    del d[maybe]
                    s.remove(maybe)
                    maxi = max(d[num], maxi)
                    break
            unchanged = b4 == len(s)
        return maxi

    fibCache = dict()
    fibCache.setdefault(0, 0)
    fibCache.setdefault(1, 1)

    def fib(self, n: int) -> int:
        if n not in self.fibCache:
            self.fibCache.setdefault(n, self.fib(n - 1) + self.fib(n - 2))
        return self.fibCache[n]

    def pushDominoes(self, dominoes: str) -> str:
        symbols = [(i, x) for i, x in enumerate(dominoes)]
        symbols = [(-1, 'L') + symbols + (len(symbols), 'R')]
        ans = list(dominoes)
        for (i, x), (j, y) in zip(symbols, symbols[1:]):
            if x == y:
                for k in range(i + 1, j):
                    ans[k] = x
            elif x > y:
                for k in range(i + 1, j):
                    if k < (i - j) / 2:
                        ans[k] = x
                    elif k > (i - j) / 2:
                        ans[k] = y
        return "".join(ans)

    def numMatchingSubseq(self, s: str, words: list[str]) -> int:
        d = dict()
        n = len(s)
        alphabet = [chr(i) for i in range(97, 123)]
        for char in alphabet:
            d.setdefault(char, [0] * (n + 1))
            d[char][n] = -1
        # fill d[char]
        for char in alphabet:
            for i in range(n - 1, -1, -1):
                if s[i] == char:
                    d[char][i] = i
                else:
                    d[char][i] = d[char][i + 1]
        res = 0
        for word in words:
            cur = 0
            for i, char in enumerate(word):
                cur = d[char][cur]  # next occuring of char.
                if cur == -1:
                    break
                else:
                    cur += 1
                # end of word
                if i == len(word) - 1: res += 1
                if cur >= n: break
        return res

    def buddyStrings(self, s: str, goal: str) -> bool:
        alphabet = [chr(i) for i in range(97, 97 + 26)]
        if len(s) != len(goal): return False
        if s == goal:
            se = set()
            for i in range(0, min(len(s), 27)):
                if s[i] in se: return True
                se.add(s[i])
            # no duplicate so far
            return False
        n = len(s)
        diff = []
        for i in range(n):
            if s[i] != goal[i]:
                diff.append(i)
                if len(diff) > 2: return False
        if len(diff) == 1: return False
        if s[diff[0]] != goal[diff[1]] or s[diff[1]] != goal[diff[0]]: return False
        return True

    def diagonalSort(self, mat: list[list[int]]) -> list[list[int]]:
        m = len(mat)
        n = len(mat[0])

        def generator(i):
            # start is i
            r, c = i
            res = []
            while r < m and c < n:
                res.append((r, c))
                r += 1
                c += 1
            return res

        diagonal = [(i, 0) for i in range(m - 1, -1, -1)]
        for i in range(1, n): diagonal.append((0, i))
        for i in diagonal:
            indexArr = generator(i)
            arr = [mat[r][c] for r, c in indexArr]
            arr.sort()
            for j, (r, c) in enumerate(indexArr):
                mat[r][c] = arr[j]
        return mat

    def reverseVowels(self, s: str) -> str:
        vowels = ['a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U']
        l, r = 0, len(s) - 1
        n = len(s)
        while l < n and s[l] not in vowels: l += 1
        while r < n and s[r] not in vowels: r -= 1
        arr = list(s)
        while l < r:
            temp = arr[r]
            arr[r] = arr[l]
            arr[l] = temp
            r -= 1
            l += 1
            while l < n and arr[l] not in vowels: l += 1
            while r < n and arr[r] not in vowels: r -= 1
        return ''.join(arr)

    def countSmaller(self, nums: list[int]) -> list[int]:
        smaller = [0] * len(nums)

        def sort(enum):
            half = int(len(enum) / 2)
            if half:
                left, right = sort(enum[:half]), sort(enum[half:])
                for i in range(len(enum))[::-1]:
                    if not right or left and left[-1][1] > right[-1][1]:
                        smaller[left[-1][0]] += len(right)
                        enum[i] = left.pop()
                    else:
                        enum[i] = right.pop()
            return enum

        sort(list(enumerate(nums)))
        return smaller

    def searchMatrix(self, matrix: list[list[int]], target: int) -> bool:
        if not matrix: return False
        m, n = len(matrix), len(matrix[0])
        if m < 1 or n < 1: return False
        row, col = 0, n - 1
        while col >= 0 and row < m - 1:
            if matrix[row][col] == target:
                return True
            elif matrix[row][col] > target:
                col -= 1
            elif matrix[row][col] < target:
                row += 1
        return False

    def searchRange(self, nums: list[int], target: int) -> list[int]:
        n = len(nums)

        def findBound(leftBound):
            i, j = 0, n - 1
            while i <= j:
                mid = int((i + j) / 2)
                if nums[mid] == target:
                    if leftBound:
                        if mid == i or nums[mid - 1] != target: return mid
                        j = mid - 1
                    else:
                        if mid == j or nums[mid + 1] != target: return mid
                        i = mid + 1
                elif nums[mid] > target:
                    j = mid - 1
                else:
                    i = mid + 1

        if n == 0: return [-1, -1]
        return [findBound(True) or -1, findBound(False) or -1]

    def merge(self, nums1: list[int], m: int, nums2: list[int], n: int) -> None:
        if m == 0:
            for i in range(n):
                nums1[i] = nums2[i]
        elif n == 0:
            return
        else:
            p1, p2, nextEmpty = m - 1, n - 1, m + n - 1
            for _ in range(m + n):
                if p2 < 0 or p1 < 0: break
                if nums2[p2] >= nums1[p1]:
                    nums1[nextEmpty] = nums2[p2]
                    p2 -= 1
                    nextEmpty -= 1
                else:
                    nums1[nextEmpty] = nums1[p1]
                    nextEmpty -= 1
                    p1 -= 1
            while p2 >= 0:
                nums1[nextEmpty] = nums2[p2]
                p2 -= 1
                nextEmpty -= 1

    def removeOuterParentheses(self, s: str) -> str:
        sum = 0
        last = 0
        res = ""
        for i, char in enumerate(s):
            if char == '(':
                sum += 1
            else:
                sum -= 1
                if sum == 0:
                    if i - last > 1:
                        res += s[last + 1:i]
                    else:
                        res += s[last:i + 1]
                    last = i + 1
        return res

    def wordSubsets(self, words1: list[str], words2: list[str]) -> list[str]:
        alphabet = [chr(i) for i in range(97, 123)]
        requirement = dict(zip(alphabet, [0] * 26))
        for word in words2:
            count = dict(zip(alphabet, [0] * 26))
            for char in word: count[char] += 1
            for char in alphabet: requirement[char] = max(requirement[char], count[char])
        res = []
        for word in words1:
            count = dict(zip(alphabet, [0] * 26))
            for char in word: count[char] += 1
            flag = False
            for char in alphabet:
                if requirement[char] > count[char]:
                    flag = True
                    break
            if not flag: res.append(word)
        return res

    def uniquePaths(self, m: int, n: int) -> int:
        return int(math.factorial(m + n - 2) / (math.factorial(n - 1) * math.factorial(m - 1)))

    class MyCalendar:

        def __init__(self):
            self.od = OrderedDict()

        def book(self, start: int, end: int) -> bool:
            end -= 1
            if len(self.od) != 0:
                insert_point = bisect_left(list(self.od.keys()), start)
                # conditions to add: left tail < start<end<right head
                left = -9999999 if insert_point == 0 else self.od[list(self.od.keys())[insert_point - 1]]
                right = 9999999 if insert_point == len(self.od) else list(self.od.keys())[insert_point]
                if left >= start or start > end or end >= right: return False
            self.od.setdefault(start, end)
            return True

    def mirrorReflection(self, p: int, q: int) -> int:
        while p % 2 == 0 and q % 2 == 0: p /= 2; q /= 2
        return 1 - p % 2 + q % 2

    def pancakeSort(self, arr: list[int]) -> list[int]:
        def flipFirstK(array, k):
            array[0:k] = array[0:k][::-1]

        sarr = sorted(arr)
        res = []

        def fsort(i):  # sort i
            if i == 0: return

            while i>0 and arr[i] == sarr[i]:
                i -= 1
                if i == 0: return
            a = sarr[i]
            b = arr[i]
            # a>b
            trueIndex = bisect(arr, a)-1
            flipFirstK(arr, trueIndex + 1)
            res.append(trueIndex + 1)
            if arr[i] != a:
                flipFirstK(arr, i + 1)
                res.append(i + 1)


            fsort(i-1)

        fsort(len(arr) - 1)
        return res
