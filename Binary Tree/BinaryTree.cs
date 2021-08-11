using System;

public class Node 
{
    public int value {get; set;}
    public Node Left {get; set;}
    public Node Right {get; set;}
    public int count {get;set;}
    public Node() {
        count=1;
    }
    public Node (int value, Node left, Node right){
        count=1;
        this.value=value;
        this.Left=left;
        this.Right=right;
    }
    public Node (int value) {
        this.value=value;
        count=1;
    }
    
}

class BinaryTree
{
    public Node Root { get; set; }


    ///<summary>Add a childless Node the value specified</summary>
    ///<param>An integer value to add to the tree.</param>
    ///<returns>True if there is no duplicate, False if there is a duplicate.<br/> In case of false, the node's augmented count will increment by one</returns>
    public bool Add(int value)
    {
        Node before = null, after = this.Root;
 
        while (after != null)
        {
            before = after;
            if (value < after.value) 
                after = after.Left; 
            else if (value > after.value)
                after = after.Right;
            else
            {
                after.count++;
                return false;
            }
        }
 
        Node newNode = new Node();
        newNode.value = value;
 
        if (this.Root == null)
            this.Root = newNode;
        else
        {
            if (value < before.value)
                before.Left = newNode;
            else
                before.Right = newNode;
        }
 
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
 
        return parent;
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
                return Find(value, parent.Left );
            else
                return Find(value, parent.Right );
        }
 
        return null;
    }
 
    public int GetTreeDepth()
    {
        return this.GetTreeDepth(this.Root);
    }
 
    private int GetTreeDepth(Node parent)
    {
        return parent == null ? 0 : Math.Max(GetTreeDepth(parent.Left ), GetTreeDepth(parent.Right )) + 1;
    }
 
    public void TraversePreOrder(Node parent)
    {
        if (parent != null)
        {
            Console.Write(parent.value + " ");
            TraversePreOrder(parent.Left );
            TraversePreOrder(parent.Right );
        }
    }
 
    public void TraverseInOrder(Node parent)
    {
        if (parent != null)
        {
            TraverseInOrder(parent.Left );
            Console.Write(parent.value + " ");
            TraverseInOrder(parent.Right );
        }
    }
 
    public void TraversePostOrder(Node parent)
    {
        if (parent != null)
        {
            TraversePostOrder(parent.Left );
            TraversePostOrder(parent.Right );
            Console.Write(parent.value + " ");
        }
    }
}

///<summary>A class contains methods written on the Binary Tree data structure</summary>
class BSTMethods {
    public bool IsContinuous(BinaryTree bst) {
        return IsContinuousUtil(bst.Root);
    }
    private bool IsContinuousUtil(Node root){
        if (root.Left==null&&root.Right==null) return true;
        if (root.Left==null) return Math.Abs(root.value-root.Right.value)==1?IsContinuousUtil(root.Right):false;
        if (root.Right==null) return Math.Abs(root.value-root.Left.value)==1?IsContinuousUtil(root.Left):false;
        return (Math.Abs(root.value-root.Right.value)==1&&Math.Abs(root.value-root.Left.value)==1)?(IsContinuousUtil(root.Left)&&IsContinuousUtil(root.Right)):false;
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
    public bool IsFoldable(BinaryTree bst) {
        if(bst.Root==null) return true;
        return CompareStructure(bst.Root.Left, bst.Root.Right);
    }
    private bool CompareStructure(Node left, Node right) {
        if (right==null && left==null) 
            return true;
        if (right!=null&&left!=null) 
            return CompareStructure(left.Left, right.Right)&&CompareStructure(left.Right, right.Left);
        return false;
    }

    public bool IsMirror(BinaryTree bst) {
        return bst.Root.Left==bst.Root.Right;
    }
    

}