from sol_stack import Solution_Stack
from sol_queue import Solution_Queue
from sol_greedy import Solution_Greedy


def main():
    sol_stack = Solution_Stack()
    sol_queue = Solution_Queue()
    sol_greedy = Solution_Greedy()
    # print(sol.nextGreaterElements([5,4,3,2,1]))
    # print(sol_queue.maxSlidingWindow([1, 3, -1, -3, 5, 3, 6, 7], k=3))
    # print(sol_queue.maxResult([10, -5, -2, 4, 0, 3], k=3))
    # print(sol_stack.findUnsortedSubarray([2,6,4,8,10,9,15]))
    # print(sol_queue.shortestSubarray([2,-1,2], 3))
    # print(sol_queue.constrainedSubsetSum([-1, -2, -3], 1))
    # print(sol_queue.maxScore([1, 2, 3, 4, 5, 6, 1], 3))
    # print(sol_queue.totalFruit([1,0,1,4,1,4,1,2,3]))
    # print(sol_queue.maxSatisfied([1, 0, 1, 2, 1, 1, 7, 5], [0, 1, 0, 1, 0, 1, 0, 1], 3))
    print(sol_greedy.minPartitions("27346209830709182346"))


main()
