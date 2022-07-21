class ListNode:
    def __init__(self, val=0, next=None):
        self.val = val
        self.next = next


class Solution_LinkedList:
    def reverseBetweenTrash(self, head: ListNode, left: int, right: int) -> ListNode:
        prev = head
        for _ in range(left - 2):
            prev = prev.next
        cur = prev.next
        last = cur
        for _ in range(left, right):
            last = last.next
        next = last.next
        last.next = None

        # reverse list then
        def reverse_list(node: ListNode):
            if not node or not node.next: return node
            n2 = reverse_list(node.next)
            node.next.next = node
            node.next = None
            return n2

        newNode = reverse_list(cur)
        prev.next = newNode
        cur.next = next
        return head

    def reverseBetween(self, head: ListNode, left: int, right: int) -> ListNode:
        if not head: return head
        dummy = ListNode(0)
        dummy.next = head
        prev = dummy
        for i in range(left - 1):
            prev = prev.next
        start = prev.next
        then = start.next
        for i in range(right - left):
            start.next = then.next
            then.next = prev.next
            prev.next = then
            then = start.next
        return dummy.next

    def printList(self, node: ListNode):
        while node:
            print(node.val)
            node = node.next
