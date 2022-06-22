from sol_stack import Solution_Stack
from sol_queue import Solution_Queue


def main():
    sol_stack = Solution_Stack()
    sol_queue = Solution_Queue()
    # print(sol.nextGreaterElements([5,4,3,2,1]))
    # print(sol_queue.maxSlidingWindow([1, 3, -1, -3, 5, 3, 6, 7], k=3))
    # print(sol_queue.maxResult([10, -5, -2, 4, 0, 3], k=3))
    # print(sol_stack.findUnsortedSubarray([2,6,4,8,10,9,15]))
    print(sol_queue.shortestSubarray([2,-1,2], 3))

main()

