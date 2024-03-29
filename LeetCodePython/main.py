import time
from datetime import datetime
from calendar import timegm

from sol_stack import Solution_Stack
from sol_queue import Solution_Queue
from sol_greedy import Solution_Greedy
from sol_misc import Solution_Misc, Solution
from sol_prefix import Soloution_Prefix
from sol_dp import Solution_DynamicProgramming
from sol_tree import Solution_Tree, TreeNode
from sol_linkedlist import Solution_LinkedList, ListNode
from sol_heap import Solution_Heap


def main():
    sol_stack = Solution_Stack()
    sol_queue = Solution_Queue()
    sol_greedy = Solution_Greedy()
    sol_misc = Solution_Misc()
    sol_prefix = Soloution_Prefix()
    sol_dp = Solution_DynamicProgramming()
    sol_tree = Solution_Tree()
    sol_linkedlist = Solution_LinkedList()
    sol_heap = Solution_Heap()
    # print(sol.nextGreaterElements([5,4,3,2,1]))
    # print(sol_queue.maxSlidingWindow([1, 3, -1, -3, 5, 3, 6, 7], k=3))
    # print(sol_queue.maxResult([10, -5, -2, 4, 0, 3], k=3))
    # print(sol_stack.findUnsortedSubarray([2,6,4,8,10,9,15]))
    # print(sol_queue.shortestSubarray([2,-1,2], 3))
    # print(sol_queue.constrainedSubsetSum([-1, -2, -3], 1))
    # print(sol_queue.maxScore([1, 2, 3, 4, 5, 6, 1], 3))
    # print(sol_queue.totalFruit([1,0,1,4,1,4,1,2,3]))
    # print(sol_queue.maxSatisfied([1, 0, 1, 2, 1, 1, 7, 5], [0, 1, 0, 1, 0, 1, 0, 1], 3))
    # print(sol_greedy.minPartitions("27346209830709182346"))
    # print(sol_misc.minMoves2([1, 0, 0, 8, 6]))
    # print(sol_queue.maxTurbulenceSize([9,4,2,10,7,8,8,1,9]))
    # print(sol_prefix.minSubArrayLen(7,[2, 3, 1, 2, 4, 3]))
    # print(sol_prefix.subarraySum([1,2,3],3))
    # print(sol_prefix.findMaxLength([0,1,0]))
    # print(sol_prefix.numberOfArrays([83702, -5216], -82788, 14602))
    # print(sol_misc.maxArea(5, 4, [3,1], [1]))
    # print(sol_prefix.subarraysDivByK([5], 9))
    # print(sol_prefix.corpFlightBookings([[1, 2, 10], [2, 3, 20], [2, 5, 25]], 5))
    # print(sol_prefix.maxConsecutiveAnswers(answerKey="TFFT", k=1))
    # print(sol_prefix.maximumWhiteTiles(tiles=[[1, 5], [10, 11], [12, 18], [20, 25], [30, 32]], carpetLen=10))
    # print(sol_prefix.goodDaysToRobBank(security = [1,2,3,4,5,6], time = 0))
    # print(sol_prefix.splitPainting(segments=[[1, 4, 5], [4, 7, 7], [1, 7, 9]]))
    # print(sol_prefix.minimumRemoval(beans=[2, 10, 3, 2]))
    # print(sol_misc.longestConsecutive(nums = [100,4,200,1,3,2,1]))
    # print(sol_misc.fib(11))
    # print(sol_prefix.numberOfWays(s = "11100"))
    # print(sol_prefix.equalSubstring(s = "abcd", t = "bcdf", maxCost = 3))
    # print(sol_prefix.kthLargestValue(matrix = [[5,2],[1,6]], k = 2))
    # print(sol_prefix.countPalindromicSubsequence(s="bbcbaba"))
    # print(sol_dp.minCostClimbingStairs(cost = [1,100,1,1,1,100,1,1,100,1]))
    # print(sol_dp.findTargetSumWays(nums = [0,0,0,0,0,0,0,0,1], target = 1))
    # print(sol_tree.levelOrder(TreeNode(3, TreeNode(9), TreeNode(20, TreeNode(15), TreeNode(7)))))
    # print(sol_dp.findMaxForm(strs = ["00","000"], m = 1, n = 10))
    # print(sol_dp.PredictTheWinner(nums = [1,5,2]))
    # print(sol_dp.champagneTower(poured=
    #                             100000009, query_row=33, query_glass=17))
    # print(sol_dp.findPaths(m = 1, n = 3, maxMove = 3, startRow = 0, startColumn = 1))
    # print(sol_dp.kInversePairs(n = 3, k = 0)) print(sol_dp.generate(6)) print(sol_stack.numSubmat(mat = [[1,0,1],
    # [1,1,0],[1,1,0]])) print(sol_misc.numMatchingSubseq(s = "dsahjpjauf", words = ["ahjpjau","ja","ahbwzgqnuk",
    # "tnmlanowax"])) print(sol_misc.buddyStrings(s="aa", goal = "aa")) print(sol_misc.diagonalSort(mat = [[3,3,1,1],
    # [2,2,1,2],[1,1,1,2]])) print(sol_linkedlist.printList( sol_linkedlist.reverseBetween(ListNode(1, ListNode(2,
    # ListNode(3, ListNode(4, ListNode(5))))), 2, 4))) print(sol_misc.reverseVowels("leetcode")) print(
    # sol_linkedlist.printList(sol_linkedlist.partition(ListNode(1, ListNode(4, ListNode(3, ListNode(2, ListNode(5,
    # ListNode(2)))))), 3)))

    # print(sol_misc.countSmaller([5,2,6,1]))
    # print(sol_prefix.dietPlanPerformance(calories = [6,5,0,0], k = 2, lower = 1, upper = 5))
    # print(sol_misc.searchRange(nums = [5,7,7,8,8,10], target = 6))
    # arr = [1,2,3,0,0,0]
    # sol_misc.merge(nums1 = arr, m = 3, nums2 = [2,5,6], n = 3)
    # print(arr)
    p = TreeNode(5, TreeNode(6), TreeNode(2, TreeNode(7), TreeNode(4)))
    q = TreeNode(1, TreeNode(0), TreeNode(8))
    # print(sol_tree.lowestCommonAncestor(TreeNode(3, p, q)))
    # node = TreeNode(3, p, q)
    # sol_tree.flatten(node)
    # print(sol_misc.removeOuterParentheses('(()())(())'))
    # print(sol_dp.divisorGame(5))
    # print(sol_misc.wordSubsets(words1 = ["amazon","apple","facebook","google","leetcode"], words2 = ["l","e"]))
    # print(sol_heap.kthSmallest( matrix = [[1,5,9],[10,11,13],[12,13,15]], k = 7))
    # calendar = sol_misc.MyCalendar()
    # calls = [[47,50],[33,41],[39,45],[33,42],[25,32],[26,35],[19,25],[3,8],[8,13],[18,27]]
    # for call in calls:
    #     print(calendar.book(call[0], call[1]))
    # sol_tree.findLeaves(TreeNode(3, p, q))
    # print(sol_misc.pancakeSort([1,5,2,3,4]))
    # print(sol_dp.numRollsToTarget(n = 2, k = 6, target = 7))
    # sol_tree.verticalOrder(TreeNode(3, p, q))
    # print(sol_dp.numFactoredBinaryTrees(arr = [2,4]))
    # print(sol_misc.twoSum(
    #     [3, 2, 4], 6))
    # print(sol_misc.prisonAfterNDays(cells = [1,0,0,1,0,0,1,0], n = 1000))
    # print(sol_misc.expandedString("((ab){3}(cd){2}){4}"))
    # sol = Solution()
    # data = [['gutvzg', '2022-01-10', '15:12:54', 'Akuna31', 'CME', 'TSLA', 'C', '2022-05-24', 950, 880, 'BUY'], [
    #     '377tvk', '2022-01-10', '15:12:55', 'Broker', 'CME', 'TSLA', 'C', '2022-05-24', 900, 880, 'BUY']]
    # for trade in data:
    #     sol.process_raw_trade(trade)
    print(sol_misc.findSubstring("wordgoodgoodgoodbestword", ["word", "good", "best", "good"]))


main()

# datetime.datetime.utcfromtimestamp(1347517370).strftime('%Y-%m-%d %H:%M:%S')
