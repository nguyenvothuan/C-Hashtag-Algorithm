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
        while len(q) > 1 :
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
