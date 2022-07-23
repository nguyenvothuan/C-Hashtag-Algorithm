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

    def partition(self, head: ListNode, x: int) -> ListNode:
        d1 = ListNode(-1)  # dummy node to keep track of the less list
        d2 = ListNode(-1)
        cur1 = d1
        cur2 = d2
        cur = head
        while cur:
            if cur.val < x:
                cur1.next = cur
                cur1 = cur1.next
            else:
                cur2.next = cur
                cur2 = cur2.next
            cur = cur.next
        cur2.next = None
        cur1.next = d2.next
        return d1.next

