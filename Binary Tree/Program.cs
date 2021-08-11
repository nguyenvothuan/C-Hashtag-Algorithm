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
            Console.Write(tester.IsMirror(bst));
        }
    }
}
