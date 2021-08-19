using System;
using System.Threading.Tasks;
namespace Dynamic_Programming
{
    class Program
    {
        static void Main(string[] args)
        {
            DynamicProgramming dp = new DynamicProgramming();
            int[] arr = {1,1,5};
           
            int steps = dp.CandyEqual(arr);
            Console.WriteLine(steps);
        }
    }
}
