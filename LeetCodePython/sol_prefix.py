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
        tudien.setdefault(0,1)
        for num in nums:
            sum += num
            if sum - k in tudien:
                res += tudien[sum - k]
            if sum in tudien:
                tudien[sum] += 1
            else:
                tudien.setdefault(sum, 1)
        return res
