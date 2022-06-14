using System;
using System.Collections.Generic;

class Solution2
{
    public int MinDistance(string w1, string w2)
    {
        int n = w1.Length, m = w2.Length;
        int[,] dp = new int[n, m]; // dp[i,j] lcs that ends at i of w1 and j of w2
        for (int i=0;i<n;i++)
            for(int j=0;j<m;j++)
                dp[i,j]=-1;
        int LCS(int i, int j) {
            if (i<0 || j<0) return 0;
            if (dp[i, j]==-1) {
                if (w1[i]==w2[j]) dp[i,j] = 1+LCS(i-1, j-1);
                else dp[i,j] = Math.Max(LCS(i-1, j), LCS(i, j-1));
            }
            return dp[i,j];
        }
        int lcs = LCS(n-1, m-1);
        return m+n-2*lcs;
    }
}