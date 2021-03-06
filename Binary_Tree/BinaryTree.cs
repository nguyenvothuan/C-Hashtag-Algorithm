using System;
using System.Collections.Generic;
public class Node
{
    public int value { get; set; }
    public Node Left { get; set; }
    public Node Right { get; set; }
    public int childrenCount {get;set;}


    public int count { get; set; }
    public Node()
    {
        childrenCount=0;
        count = 1;
    }
    public Node(int value, Node left, Node right)
    {
        count = 1;
        childrenCount+=( left.childrenCount+right.childrenCount);
        this.value = value;
        this.Left = left;
        this.Right = right;
    }
    public Node(int value)
    {
        childrenCount=0;
        this.value = value;
        count = 1;
    }
    public int GetChildrenCount() {return childrenCount;}
}

class BinaryTree
{
    public Node Root { get; set; }
    private int Cardinality { get; set; }
    public BinaryTree() { Cardinality = 0; }
    public BinaryTree(Node Root)
    {
        this.Root = Root;
        Cardinality = 1;
    }

    public int GetCardinality() { return Cardinality; }

    ///<summary>Add a childless Node the value specified</summary>
    ///<param>An integer value to add to the tree.</param>
    ///<returns>True if there is no duplicate, False if there is a duplicate.<br/> In case of false, the node's augmented count will increment by one</returns>
    public bool Add(int value)
    {
        Node before = null, after = this.Root;
        List<Node> ancestorRecord = new List<Node>();
        while (after != null)
        {
            ancestorRecord.Add(after);
            before = after;
            if (value < after.value)
                after = after.Left;
            else if (value > after.value)
                after = after.Right;
            else
            {//meet the node
                after.count++;
                return false;
            }
        }

        Node newNode = new Node();
        newNode.value = value;
        foreach(Node ancestor in ancestorRecord){
            ancestor.childrenCount++;
        }
        if (this.Root == null)
            this.Root = newNode;
        else
        {
            if (value < before.value)
                before.Left = newNode;
            else
                before.Right = newNode;
        }
        Cardinality++;
        return true;
    }

    public Node Find(int value)
    {
        return this.Find(value, this.Root);
    }

    public void Remove(int value)
    {
        this.Root = Remove(this.Root, value);
    }

    private Node Remove(Node parent, int key)
    {

        if (parent == null) return parent; //when the rightful heir to the removed 
        if (key < parent.value) parent.Left = Remove(parent.Left, key);
        else if (key > parent.value) parent.Right = Remove(parent.Right, key);
        else
        {
            if (parent.Left == null)
                return parent.Right;
            if (parent.Right == null)
                return parent.Left;
            parent.value = MinValue(parent.Right);

            // Delete the inorder successor, because the minValue is to the utter left, it falls under the second case
            parent.Right = Remove(parent.Right, parent.value);
        }
        RefreshChildrenCount(parent);//refresh chilren
        Cardinality--;
        return parent;
    }
    private void RefreshChildrenCount(Node affected) {
        //TODO2: Refresh the cildrenCount property of all node of the tree when a node is removed.
    }

    private int MinValue(Node node)
    {
        int minv = node.value;

        while (node.Left != null)
        {
            minv = node.Left.value;
            node = node.Left;
        }

        return minv;
    }

    private Node Find(int value, Node parent)
    {
        if (parent != null)
        {
            if (value == parent.value) return parent;
            if (value < parent.value)
                return Find(value, parent.Left);
            else
                return Find(value, parent.Right);
        }

        return null;
    }

    public int GetTreeDepth()
    {
        return this.GetTreeDepth(this.Root);
    }

    private int GetTreeDepth(Node parent)
    {
        return parent == null ? 0 : Math.Max(GetTreeDepth(parent.Left), GetTreeDepth(parent.Right)) + 1;
    }

    public List<int> LevelOrderTraversal(Node parent)
    {
        List<int> traversal = new List<int>();
        Queue<Node> que = new Queue<Node>();
        que.Enqueue(parent);
        while (que.Count != 0)
        {
            Node cur = que.Dequeue();
            traversal.Add(cur.value);
            if (cur.Left != null) que.Enqueue(cur.Left);
            if (cur.Right != null) que.Enqueue(cur.Right);
        }
        return traversal;
    }
    public List<int> PostorderList()
    {
        List<int> list = new List<int>();
        PostorderUtil(Root, list);
        return list;
    }
    private void PostorderUtil(Node parent, List<int> list)
    {
        if (parent != null)
        {
            PostorderUtil(parent.Left, list);
            PostorderUtil(parent.Right, list);
            list.Add(parent.value);
        }
    }
    public List<int> InorderList()
    {
        List<int> list = new List<int>();
        InorderUtil(Root, list);
        return list;
    }
    private void InorderUtil(Node parent, List<int> list)
    {
        if (parent != null)
        {
            PreorderUtil(parent.Left, list);
            list.Add(parent.value);

            PreorderUtil(parent.Right, list);
        }
    }
    public List<int> PreorderList()
    {
        List<int> list = new List<int>();
        PreorderUtil(Root, list);
        return list;
    }
    private void PreorderUtil(Node parent, List<int> list)
    {
        if (parent != null)
        {
            list.Add(parent.value);
            PreorderUtil(parent.Left, list);
            PreorderUtil(parent.Right, list);
        }
    }
    public void TraversePreOrder(Node parent)
    {

        if (parent != null)
        {

            Console.Write(parent.value + " ");
            TraversePreOrder(parent.Left);
            TraversePreOrder(parent.Right);
        }
    }

    public void TraverseInOrder(Node parent)
    {
        if (parent != null)
        {
            TraverseInOrder(parent.Left);
            Console.Write(parent.value + " ");
            TraverseInOrder(parent.Right);
        }
    }

    public void TraversePostOrder(Node parent)
    {
        if (parent != null)
        {
            TraversePostOrder(parent.Left);
            TraversePostOrder(parent.Right);
            Console.Write(parent.value + " ");
        }
    }
}

class LiberalBinaryTree
{
    Node Root { get; set; }
    public LiberalBinaryTree(Node Root)
    {
        this.Root = Root;
    }
    ///<param>Node node: a node to add to this tree<param>
    ///<param>string dir: a node to specify to go left or right<param>

    public LiberalBinaryTree(int[] LevelOrder)
    {
        Node root = new Node(LevelOrder[0]);

        for (int i = 1; i < LevelOrder.Length; i++)
        {

        }
    }

    public void Add(Node node, string dir)
    {

    }
}


///<summary>A class contains methods written on the Binary Tree data structure</summary>
class BSTMethods
{
    public bool IsContinuous(BinaryTree bst)
    {
        return IsContinuousUtil(bst.Root);
    }
    private bool IsContinuousUtil(Node root)
    {
        if (root.Left == null && root.Right == null) return true;
        if (root.Left == null) return Math.Abs(root.value - root.Right.value) == 1 ? IsContinuousUtil(root.Right) : false;
        if (root.Right == null) return Math.Abs(root.value - root.Left.value) == 1 ? IsContinuousUtil(root.Left) : false;
        return (Math.Abs(root.value - root.Right.value) == 1 && Math.Abs(root.value - root.Left.value) == 1) ? (IsContinuousUtil(root.Left) && IsContinuousUtil(root.Right)) : false;
    }

    ///<summary>A tree is "foldable" if it left child's structure, not value, is the same as the right one</summary>
    ///<example>
    ///                   /\
    /// Foldable:        /  \                  
    ///                 /\  /\
    ///
    ///                     /\
    ///Unfoldable:         /  \
    ///                   /\
    ///</example>
    public bool IsFoldable(BinaryTree bst)
    {
        if (bst.Root == null) return true;
        return CompareStructure(bst.Root.Left, bst.Root.Right);
    }
    private bool CompareStructure(Node left, Node right)
    {
        if (right == null && left == null)
            return true;
        if (right != null && left != null)
            return CompareStructure(left.Left, right.Right) && CompareStructure(left.Right, right.Left);
        return false;
    }

    public bool IsMirror(BinaryTree bst)
    {
        return bst.Root.Left == bst.Root.Right;
    }

    ///<summary>Return the depth of a full binary, giver the preorder traversal string</summary>
    ///<param>String tree: a string of two characters 'n' and 'l' represent the nodes and leafs of the tree in preorder traversal
    ///</param>
    int FindDepthRec(char[] tree, int n, int index)
    {
        if (index >= n || tree[index] == 'l')
            return 0;

        // calc height of left subtree (In preorder
        // left subtree is processed before right)
        index++;
        int left = FindDepthRec(tree, n, index);

        // calc height of right subtree
        index++;
        int right = FindDepthRec(tree, n, index);

        return Math.Max(left, right) + 1;
    }

    public int FindDepth(char[] tree)
    {
        int n = tree.Length;
        int index = 0;
        return (FindDepthRec(tree, n, index));
    }

    public BinaryTree BuildTreeFromPreorder(int[] pre)
    {
        return new BinaryTree(FromPreorder(pre, 0, pre.Length - 1));
    }
    private Node FromPreorder(int[] pre, int index, int cut)//pre[index:cut]
    {
        Node cur = new Node(pre[index]);
        //segment with only 1 node 
        if (index == cut)
            return cur;//root

        //segment with at least 2 node: index < cut
        Node left;
        Node right;
        int rightIndex = IndexOfTheFirstGreater(pre, index, cut);//find the index of right node of cur
        int leftIndex = index + 1;
        if (rightIndex == -1 && pre[index + 1] > pre[index]) //no heir; very unlikely is this case
            return cur;
        //case one: no right child so the left child will span from the root of the left subtree pre[index+1] to pre[cut]
        if (rightIndex == -1)
        {
            left = FromPreorder(pre, leftIndex, cut);
            cur.Left = left;
            return cur;
        }
        //case two: no left child so the left right child will span from the root of the right subtree to pre[cut]
        if (pre[index + 1] > pre[index])
        {
            right = FromPreorder(pre, rightIndex, cut);
            cur.Right = right;
            return cur;
        }
        left = FromPreorder(pre, leftIndex, rightIndex - 1);
        right = FromPreorder(pre, rightIndex, cut);
        cur.Left = left; cur.Right = right;
        return cur;
    }
    private int IndexOfTheFirstGreater(int[] pre, int index, int cut)
    {//pass in a segment of the string from pre[index:cut]. Return the first the index of the first element greater than pre[index], -1 if there is no such thing
        int cur = pre[index];


        while (index <= cut) //smaller than the right starting point
        {//so index can be equal to cut: pre[cut]>pre[index]

            if (pre[index] > cur) return index;
            index++;
        }
        return -1;//pre[index] is the greatest from index forward
    }

    public BinaryTree ReplaceNodeWithSumPredecessorAndSuccessorInInorder(BinaryTree bst)
    {
        List<int> inorder = bst.InorderList();
        inorder.Insert(inorder.Count, 0);
        inorder.Insert(0, 0);

        Node cur = bst.Root;
        ReplaceWithSum(cur, inorder, 1);
        BinaryTree newTree = new BinaryTree(cur);
        return newTree;
    }
    private void ReplaceWithSum(Node cur, List<int> inorder, int i)
    {//inorder traversal and replace with sum
        //TODO1: Fix this shit
        if (cur != null)
        {
            i++;
            Console.WriteLine(i);
            ReplaceWithSum(cur.Left, inorder, i);
            cur.value = inorder[i - 1] + inorder[i + 1];

            ReplaceWithSum(cur.Right, inorder, i);

        }

    }

    public bool SameLevel(BinaryTree bst, int a, int b)
    {

        int levela = FindLevel(bst, a);
        int levelb = FindLevel(bst, b);

        return levela == levelb;
    }
    ///<summary>Return the level of the node with value a, -1 if no such thing found</summary>
    public int FindLevel(BinaryTree bst, int a)
    {
        Node cur = bst.Root;
        int level = 0;
        while (cur != null)
        {
            if (a == cur.value) return level;
            if (a < cur.value)
            {
                cur = cur.Left;
                level++;
            }
            else
            {
                cur = cur.Right;
                level++;
            }
        }
        return -1;
    }

    public bool AllLeavesOfTheSameLevel(BinaryTree bst)
    {
        return AllLeavesUtil(bst.Root) != -1;


    }
    private int AllLeavesUtil(Node root)
    {//return the depth of the tree if all leaves of the same level, return -1 else.
        if (root.Left == null && root.Right == null) return 0;
        if (root.Left != null && root.Right != null)
        {
            int left = AllLeavesUtil(root.Left); int right = AllLeavesUtil(root.Right);
            if (left == -1 || right == -1) return -1;
            if (left != right) return -1;
            return left + 1;
        }
        return -1;

    }
    public int[] LeftDiagonalSum(BinaryTree bst)
    {
        List<int> levelSum = new List<int>();
        LeftDiagonalUtil(bst.Root, levelSum, 0);
        int[] sum = levelSum.ToArray();
        return sum;
    }
    private void LeftDiagonalUtil(Node root, List<int> levelSum, int curLevel)
    {
        if (root == null) return;
        if (levelSum.Count <= curLevel) levelSum.Add(0);//index out of bound
        levelSum[curLevel] += root.value;
        LeftDiagonalUtil(root.Right, levelSum, curLevel);
        LeftDiagonalUtil(root.Left, levelSum, curLevel + 1);
    }
    public int LongestPathRootToLeaf(BinaryTree bst)
    {
        int sum = 0;
        List<Node> path = LongestPathUtil(bst.Root);
        foreach (Node n in path)
        {
            sum += n.value;
        }
        return sum;
    }
    private List<Node> LongestPathUtil(Node root)
    {
        if (root == null) return null;
        List<Node> left; List<Node> right;
        if (root.Left == null && root.Right == null)
        {
            List<Node> path = new List<Node>(); path.Add(root);
            return path;
        }
        if (root.Right == null)
        {
            left = LongestPathUtil(root.Left);
            left.Insert(0, root);
            return left;

        }
        if (root.Left == null)
        {
            right = LongestPathUtil(root.Right);
            right.Insert(0, root);
            return right;

        }
        left = LongestPathUtil(root.Left);
        right = LongestPathUtil(root.Right);
        if (left.Count > right.Count)
        {
            left.Insert(0, root);
            return left;
        }
        right.Insert(0, root);
        return right;
    }

    public int LargestSumNonAdjacentNodes(BinaryTree bst)
    {
        int count = bst.GetCardinality();
        int[] red = new int[count];
        int[] black = new int[count];
        Array.Fill(red, -1); Array.Fill(black, -1);
        int[] pre = bst.PreorderList().ToArray();
        int chosen = Red(bst.Root, red, black, pre);
        int unchosen = Black(bst.Root, red, black, pre);
        return Math.Max(chosen, unchosen);

    }

    private int Red(Node n, int[] red, int[] black, int[] pre)
    {//node n chosen
        int index = HashNodeToPreorderIndex(n, pre);
        if (red[index] != -1) return red[index];//calculated
        else if (n.Left == null && n.Right == null)
        {
            red[index] = n.value;
        }
        else if (n.Left == null)
        {
            red[index] = n.value + Black(n.Right, red, black, pre);
        }
        else if (n.Right == null)
        {
            red[index] = n.value + Black(n.Left, red, black, pre);
        }
        else
        {
            red[index] = n.value + Black(n.Right, red, black, pre) + Black(n.Left, red, black, pre);
        }
        return red[index];
    }

    private int Black(Node n, int[] red, int[] black, int[] pre)
    {//node n not selected, but noting to  do with its descendants
        int index = HashNodeToPreorderIndex(n, pre);
        if (index == -1) return -1;
        if (black[index] != -1) return black[index];//calculated
        if (n.Left == null && n.Right == null)
        {
            black[index] = 0;
            return 0;//because n is not chosen and it has no child
        };
        if (n.Left == null)
        {
            black[index] = Math.Max(Black(n.Right, red, black, pre), Red(n.Right, red, black, pre));
            return black[index];
        }
        if (n.Right == null)
        {
            black[index] = Math.Max(Black(n.Left, red, black, pre), Red(n.Left, red, black, pre));
            return black[index];
        }
        int redLeft = Red(n.Left, red, black, pre);
        int redRight = Red(n.Right, red, black, pre);
        int blackLeft = Black(n.Left, red, black, pre);
        int blackRight = Black(n.Right, red, black, pre);
        black[index] = MultipleNumberMax(blackLeft + redRight, redLeft + blackRight, redLeft + redRight, blackLeft + blackRight);
        return black[index];
    }
    private int HashNodeToPreorderIndex(Node n, int[] pre)
    { //O(n)
        for (int i = 0; i < pre.Length; i++)
        {
            if (pre[i] == n.value) return i;

        }
        return -1;
    }

    private int MultipleNumberMax(int a, int b, int c, int d)
    {
        return Math.Max(Math.Max(a, b), Math.Max(c, d));
    }
    private class MapNodeToNum
    {
        Node n { get; set; }
        int i { get; set; }
    }
    public int AllNodeSum(BinaryTree bst)
    {
        Node cur = bst.Root;
        return AllNodeSumUtil(bst.Root);
    }
    private int AllNodeSumUtil(Node n)
    {
        if (n == null) return 0;
        return n.value + AllNodeSumUtil(n.Left) + AllNodeSumUtil(n.Right);
    }

    public int MaxSumNonAdjacentLevels(BinaryTree bst)
    {
        Node root = bst.Root;
        int oddLevelSum = 0;
        int evenLevelSum = 0;
        MaxSumNonAdjacentLevelUtil(root,ref oddLevelSum,ref evenLevelSum, 1);
        return Math.Max(oddLevelSum, evenLevelSum);
    }
    private void MaxSumNonAdjacentLevelUtil(Node cur, ref int sumOdd,  ref int sumEven, int status)
    {
        if (status == 0) //first level being 1, odd to 1, even to 0
        {
            if (cur == null)
            {
                
                return;
            }
            sumEven += cur.value;
            MaxSumNonAdjacentLevelUtil(cur.Left,ref sumOdd,ref sumEven, 1);
            MaxSumNonAdjacentLevelUtil(cur.Right,ref sumOdd,ref sumEven, 1);
        }
        else
        {//odd level
            if (cur == null)
            {
                
                return;
            }
            sumOdd += cur.value;
            MaxSumNonAdjacentLevelUtil(cur.Left,ref sumOdd,ref sumEven, 0);
            MaxSumNonAdjacentLevelUtil(cur.Right,ref sumOdd,ref sumEven, 0);
        }
    }

    public int RemoveNodeEqualHalves(BinaryTree bst) {
        int allNode = bst.GetCardinality();
        if (allNode%2!=0) return -1;
        Node n = NodeToBeRemoved(allNode, bst.Root);
        return n==null?-1:n.value;
    }
    private Node NodeToBeRemoved(int allNode, Node cur) {
        if (cur==null) return null;
        if (cur.childrenCount<allNode%2) return null;
        if (cur.childrenCount>allNode%2) {
            Node left = NodeToBeRemoved(allNode, cur.Left);
            Node right=NodeToBeRemoved(allNode, cur.Right);
            if (left!=null) return left;
            if(right!=null) return right;
        }
        return cur;
    }

    public Node LargestSubtreeSum(BinaryTree bst) {
        List<Node> allNode = new List<Node>();
        List<int> subtreeSum = new List<int>();//subtreeSum[i] is the subtree sum of allNode[i]
        return bst.Root;
        //TODO3: Largest Subtree Sum
    }
    
    public bool IsHeightBalanced(BinaryTree bst) {
        return HeightBalancedUtil(bst.Root)!=-1;
    }
    private int HeightBalancedUtil(Node cur) {//return -1 if not heightbalanced, return height otherwise
        if (cur==null) return 0;
        int left=HeightBalancedUtil(cur.Left);
        int right = HeightBalancedUtil(cur.Right);
        if (left==-1||right==-1||Math.Abs(left-right)<2) return -1;
        return left+1;
    }
}

