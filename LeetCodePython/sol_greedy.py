class Solution_Greedy:
    def minPartitions(self, n: str) -> int:
        # https://leetcode.com/problems/partitioning-into-minimum-number-of-deci-binary-numbers/
        res = 0
        for chr in n:
            res = max(int(chr), res)
        return res

