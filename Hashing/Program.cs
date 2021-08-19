using System;
using System.Collections.Generic;
using System.Collections;

namespace Hashing
{
    class Program
    {
        
        public int Translate(int xor, int yor)
        {
            return 1;
        }
        static void Main(string[] args)
        {

            GeneralHashing tester = new GeneralHashing();
            int[] M = { 11, 1, 13, 21, 3, 7 };
            int[] N = {11, 7, 3};
            int[] arr2 = {4,4,2,3,4, 3,5};
            int[] arr3 = {3, 2, 1, 2, 1, 4, 5, 8, 6, 7, 4, 2};
            BinaryTree bst = new BinaryTree();
            bst.Add(4);
            bst.Add(2);
            bst.Add(3);
            bst.Add(1);
            bst.Add(8);
            bst.Add(6);
            bst.Add(9);

            int[][] points =new int[6] {{-1, 1}, {0, 0}, {1, 1}, {2, 2}, {3, 3}, {3, 4} };
            
            int test = tester.MaxPointsContainedOnALine(points);
            Console.WriteLine(test);


        }
    }
}
