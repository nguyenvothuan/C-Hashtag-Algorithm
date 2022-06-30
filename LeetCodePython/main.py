from sol_stack import Solution_Stack
from sol_queue import Solution_Queue
from sol_greedy import Solution_Greedy
from sol_misc import Solution_Misc
from sol_prefix import Soloution_Prefix


def main():
    sol_stack = Solution_Stack()
    sol_queue = Solution_Queue()
    sol_greedy = Solution_Greedy()
    sol_misc = Solution_Misc()
    sol_prefix = Soloution_Prefix()
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
    print(sol_prefix.subarraySum([1,2,3],3))

main()
