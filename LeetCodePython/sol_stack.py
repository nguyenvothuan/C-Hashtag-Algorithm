class Solution_Stack:
    def decodeString(self, s: str):
        stack = []
        curNum = 0
        curString = ''
        for chr in s:
            if chr == '[':
                stack.append(curNum)
                stack.append(curString)
                curString = ''
                curNum = 0
            elif chr == ']':
                num = int(stack.pop())
                prevStr = stack.pop()
                curString = prevStr + num * curString
            elif chr.isdigit():
                curNum = curNum * 10 + chr
            else:
                curString += chr
        return curString

    def nextGreaterElements(self, nums):
        stack = []
        n = len(nums)
        res = [-1] * n
        for i in range(2 * n - 1, -1, -1):
            while len(stack) > 0 and nums[stack[-1]] < nums[i % len(nums)]:
                stack.pop()
            res[i % len(nums)] = stack[-1] if len(stack) > 0 else -1
            stack.append(i % n)
        return res

    def numberOfWeakCharacters(self, properties: list[list[int]]) -> int:
        properties.sort(key=lambda x: (x[0], -x[1]))
        stack = []
        ans = 0
        for a, d in properties:
            while stack and stack[-1] < d:
                ans += 1
                stack.pop()
            stack.append(d)
        return ans

    def findUnsortedSubarray(self, nums: list[int]) -> int:
        stack = []
        n = len(nums)
        l = n
        r = 0
        # find l: the left bound for min
        for i in range(n):
            while len(stack) > 0 and nums[stack[-1]] > nums[i]:
                l = min(stack.pop(), l)
            stack.append(i)
        stack.clear()
        for i in range(n - 1, -1, -1):
            while len(stack) > 0 and nums[stack[-1]] < nums[i]:
                r = max(stack.pop(), r)
            stack.append(i)
        return 0 if l == r else r - l + 1

    def numSubmat(self, mat: list[list[int]]) -> int:
        m = len(mat)
        n = len(mat[0])
        res = 0
        h = [0] * n

        def util(h):
            stack = []
            sum = [0] * n
            for i, cur in enumerate(h):
                while stack and h[stack[-1]] >= cur: stack.pop()
                if stack:
                    preIndex = stack[-1]
                    sum[i] = sum[preIndex]
                    sum[i] += h[i] * (i - preIndex)
                else:
                    sum[i] = h[i] * (i + 1)
                stack.append(i)
            res = 0
            for s in sum: res += s
            return res

        for i in range(m):
            for j in range(n):
                h[j] = h[j] + 1 if mat[i][j] == 1 else 0
            res += util(h)
        return res

