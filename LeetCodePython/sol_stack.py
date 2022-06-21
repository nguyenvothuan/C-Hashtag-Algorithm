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

