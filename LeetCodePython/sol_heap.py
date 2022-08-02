from heapq import heapify, heappop, heappush


class Solution_Heap:
    def kthSmallestTrash(self, matrix: list[list[int]], k: int) -> int:
        m, n = len(matrix), len(matrix[0])
        maxHeap = []
        for r in range(m):
            for c in range(n):
                heappush(maxHeap, -matrix[r][c])  # remove the current largest one,
                if len(maxHeap) > k:
                    heappop(maxHeap)
        return -heappop(maxHeap)

    def kthSmallest(self, matrix: list[list[int]], k: int) -> int:
        m, n = len(matrix), len(matrix[0])
        maxHeap = []
        for i in range(min(k, m)):
            heappush(maxHeap, (matrix[i][0], i, 0))
        # remove the k smallest numbers from the heap
        ans = -1
        for i in range(k):
            ans, r, c = heappop(maxHeap)
            if c + 1 < n:
                heappush(maxHeap, (matrix[r][c + 1], r, c + 1))
        return ans
