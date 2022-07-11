class Solution_DynamicProgramming:
    def minCostClimbingStairs(self, cost: list[int]) -> int:
        n = len(cost)
        dp = [0]*(n+2) # dp[i] smallest sum start at i
        for i in range(n-1, -1,-1):
            dp[i] = min(dp[i+1], dp[i+2]) + cost[i]
        return min(dp[0], dp[1])
