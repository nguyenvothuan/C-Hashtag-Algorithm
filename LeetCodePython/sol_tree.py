from collections import deque


class TreeNode:
    def __init__(self, val=0, left=None, right=None):
        self.val = val
        self.left = left
        self.right = right


class Solution_Tree:
    def rightSideView(self, root: TreeNode) -> list[int]:
        return []

    def levelOrder(self, root: TreeNode) -> list[list[int]]:
        res = []
        cur = []
        dummy = -10001
        q = deque()
        q.appendleft(root)
        q.appendleft(dummy)
        while len(q) > 1:
            node: TreeNode = q.pop()
            if node == dummy:
                res.append(cur.copy())
                cur = []
                q.appendleft(dummy)
            else:
                cur.append(node.val)
                if node.left is not None: q.appendleft(node.left)
                if node.right is not None: q.appendleft(node.right)
        if cur: res.append(cur)
        return res

    def btreeGameWinningMove(self, root: TreeNode, n: int, x: int) -> bool:
        # count if left and right are equal
        q: deque[TreeNode | int] = deque()
        dummy = -1
        q.appendleft(root)
        q.appendleft(dummy)

        def countDescendants(node: TreeNode):
            if node is None: return 0
            return 1 + countDescendants(node.left) + countDescendants(node.right)

        while len(q) > 1 or (len(q) == 1 and q[0] != dummy):
            cur = q.pop()
            if cur == dummy:
                q.appendleft(dummy)
            else:
                if cur.val == x:
                    return countDescendants(cur) - 1 == (n - 1) / 2
                else:
                    if cur.left is not None: q.appendleft(cur.left)
                    if cur.right is not None: q.appendleft(cur.right)
        return True

    def treeFromBFS(self, arr):
        # assuming all rows are full probably except the last one
        return TreeNode()

    def isEvenOddTree(self, root: TreeNode) -> bool:
        # even decreasing, odd increasing
        q = deque()
        dummyOdd = -1  # append at the end of the even layer
        dummyEven = -2
        q.appendleft(root)
        q.appendleft(dummyOdd)
        isAtOdd = False
        inf = 99999999
        last = inf
        while len(q) > 1:
            cur = q.pop()
            if cur == dummyEven:
                q.appendleft(dummyOdd)
                isAtOdd = False
                last = inf
            elif cur == dummyOdd:
                q.appendleft(dummyEven)
                isAtOdd = True
                last = -inf
            else:
                if isAtOdd:
                    if last >= cur.val: return False
                else:
                    if last <= cur.val: return False
                if cur.left: q.appendleft(cur.left)
                if cur.right: q.appendleft(cur.right)
                last = cur.val
        return True

    def longestUnivaluePath(self, root: TreeNode) -> int:
        if root is None: return None
        res = 1
        if root.left and root.left.val == root.val: res = max(res, self.longestUnivaluePath(root.left))
        if root.right and root.right.val == root.val: res = max(res, self.longestUnivaluePath(root.right))
        return res

    def largestValues(self, root: TreeNode) -> list[int]:
        q = deque()
        dummy = -1
        q.appendleft(root)
        q.appendleft(dummy)
        inf = 999999999999999
        m = -inf
        res = []
        while len(q) > 1:
            cur = q.pop()
            if cur == dummy:
                res.append(m)
                m = -inf
                q.appendleft(dummy)
            else:
                m = max(m, cur.val)
                if cur.left: q.appendleft(cur.left)
                if cur.right: q.appendleft(cur.right)
        if m != -inf: res.append(m)
        return res

    def trimBST(self, root: TreeNode, low: int, high: int) -> TreeNode:
        if not root: return None
        if root.val > high: return self.trimBST(root.left)
        if root.val < low: return self.trimBST(root.right)
        root.left = self.trimBST(root.left)
        root.right = self.trimBST(root.right)
        return root

    def findBottomLeftValue(self, root: TreeNode) -> int:
        q = deque()
        dummy = -1
        q.appendleft(root)
        q.appendleft(dummy)
        head = True
        last = root.val
        while len(q) > 1:
            cur = deque.pop()
            if cur == dummy:
                q.appendleft(dummy)
                head = True
            else:
                if head: last = cur.val
                if cur.left: q.appendleft(cur.left)
                if cur.right: q.appendleft(cur.right)
                head = False
        return last

    def isSubtree(self, root: TreeNode, subRoot: TreeNode) -> bool:
        def equal(a: TreeNode, b: TreeNode):
            if not a and not b: return True
            if not a or not b: return False
            return a.val == b.val and equal(a.left, b.left) and equal(a.right, b.right)

        if not subRoot: return True
        if not root: return False
        if equal(root, subRoot): return True
        return self.isSubtree(root.left, subRoot) or self.isSubtree(root.right, subRoot)
