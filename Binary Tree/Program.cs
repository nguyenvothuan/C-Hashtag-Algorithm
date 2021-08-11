using System;
using Binary_Tree;
namespace Binary_Tree
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryTree bst = new BinaryTree();
            bst.Add(4);
            bst.Add(2);
            bst.Add(6);
            bst.Add(3);
            bst.Add(-9);
            bst.Add(5);
            bst.Add(7);
            BSTMethods tester =  new BSTMethods();
            int[] pre = {10,5,1,7,40,50};

            BinaryTree tree = tester.BuildTreeFromPreorder(pre);
            tree.TraversePreOrder(tree.Root);
        }
    }
}
