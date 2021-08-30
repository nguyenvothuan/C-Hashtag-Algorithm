using System;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace Dynamic_Programming
{
    class Program
    {
        static void Main(string[] args)
        {
            DynamicProgramming dp = new DynamicProgramming();
            int[] arr = {1,1,5};
            int[] proc = {8,10,8};
            int[] task = { 2,2,3,1,8,7,4,5,8,7,4,5};
            int[] arr2 = {1, -2};
            int[] test = dp.SubArrayWithMaxSum(arr2);
            Console.WriteLine(test[0] +", "+test[1]);
        }
    }
}
