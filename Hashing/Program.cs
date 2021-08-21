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

            int[][] points =new int[6][];
            points[0]=new int[2] {-1, 1}; 
            points[1]=new int[2] {0,0};
            points[2] = new int[2] {1, 1};
            points[3] = new int[2] {2,2};
            points[4] = new int[2] {3,3};
            points[5] = new int[2]{3,4};

            int[] arr4 = {1, 2, 3, 1, 4, 5};
            bool test = tester.DuplicateInKDistance(3, arr4);
            Console.WriteLine(test);


        }
    }
}
