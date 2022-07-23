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


