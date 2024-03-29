from collections import deque, defaultdict
from typing import Optional, List


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

    def inorderSuccessor(self, root: TreeNode, p: TreeNode) -> TreeNode:
        if p.right:
            p = p.right
            while p.left:
                p = p.left
            return p
        candidate = None
        while root != p:
            if root.val < p.val:
                root = root.right
            else:
                candidate = root
                root = root.left
        return candidate

    def lowestCommonAncestor(self, root: TreeNode, p: TreeNode, q: TreeNode) -> TreeNode:
        def search(node):
            if not node: return None
            if node.val == p.val or node.val == q.val: return node
            l = search(node.left)
            r = search(node.right)
            if not l: return r
            if not r: return l
            return node

        return search(root)

    def widthOfBinaryTree(self, root: TreeNode) -> int:
        # https://leetcode.com/problems/maximum-width-of-binary-tree/
        return 1

    def flatten(self, root: TreeNode) -> None:
        cur = root
        while cur:
            if cur.left:
                last = cur.left
                while last.right:
                    last = last.right
                last.right = cur.right
                cur.right = cur.left
                cur.left = None
            cur = cur.right

    def isAnagram(self, s: str, t: str) -> bool:
        arr = [chr(i) for i in range(97, 123)]
        d = dict()
        for char in arr: d.setdefault(char, 0)
        for char in s: d[char] += 1
        for char in t: d[char] -= 1
        for val in d.values():
            if val != 0:
                return False
        return True

    class Node:
        def __init__(self, val):
            self.val = val
            self.left = None
            self.right = None
            self.parent = None

    def lowestCommonAncestor3(self, p: 'Node', q: 'Node') -> 'Node':
        p1, p2 = p, q
        while p1 != p2:
            p1 = p1.parent if p1.parent else q
            p2 = p2.parent if p2.parent else p
        return p1

    def findLeaves(self, root: TreeNode) -> list[list[int]]:
        parent = dict()
        outDeg = dict()
        zeroOut = []

        def check(node):
            outDeg.setdefault(node, 0)
            if node.left:
                parent.setdefault(node.left, node)
                outDeg[node] += 1
                check(node.left)
            if node.right:
                parent.setdefault(node.right, node)
                outDeg[node] += 1
                check(node.right)
            if outDeg[node] == 0: zeroOut.append(node)

        check(root)
        res = []
        next = []
        while len(zeroOut):
            res.append(zeroOut.copy())
            for node in zeroOut:
                outDeg[parent[node]] -= 1
                if outDeg[parent[node]] == 0:
                    next.append(parent[node])
            zeroOut = next.copy()
            next = []
        return res

    def verticalOrder(self, root: Optional[TreeNode]) -> List[List[int]]:
        memo = dict()
        least = 0
        most = 0

        def traverse(node, col):
            nonlocal least, most
            least = min(least, col)
            most = max(most, col)
            if not node: return
            if col not in memo.keys():
                memo.setdefault(col, [node.val])
            else:
                memo[col].append(node.val)
            traverse(node.left, col - 1)
            traverse(node.right, col + 1)

        traverse(root, 0)
        res = []
        for i in range(least, most + 1):
            if i in memo.keys():
                res.append(memo[i])
        return res

