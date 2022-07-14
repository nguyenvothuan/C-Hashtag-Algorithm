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
