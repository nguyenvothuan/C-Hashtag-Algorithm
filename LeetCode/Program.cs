using System;
using System.Collections.Generic;
namespace LeetCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Solution sol = new Solution();
            // int[] nums = {9,4,1,7};
            // string[] nums = {"683339452288515879","7846081062003424420","4805719838","4840666580043","83598933472122816064","522940572025909479","615832818268861533","65439878015","499305616484085","97704358112880133","23861207501102","919346676","60618091901581","5914766072","426842450882100996","914353682223943129","97","241413975523149135","8594929955620533","55257775478129","528","5110809","7930848872563942788","758","4","38272299275037314530","9567700","28449892665","2846386557790827231","53222591365177739","703029","3280920242869904137","87236929298425799136","3103886291279"};
            // string[] num1 = {"2","21","12","1"};
            int[][] arr = new int[4][];
            arr[0] = new int[6] { 1, 1, 2, 2, 4, 4 };
            arr[1] = new int[4] { 3, 4, 10, 4 };
            arr[2] = new int[4] { 2, 10, 1, 2 };
            arr[3] = new int[4] { 5, 4, 4, 5 };
            int[] trash = new int[6] { 1, 0, 0, 1, 0, 0 };
            List<int> dummyList = new List<int>(arr[0]);
            int[] rec1 = {0, 0, 1, 1}; int[] rec2 = {2, 2, 3, 3};
            //string s = "ababcbacade";
            Tool<char> tool = new Tool<char>();
            Dictionary<int, char> dict = new Dictionary<int, char>();
            dict.Add(2,'b');
            dict.Add(3,'a');
            dict.Add(1,'c');
            // foreach (var key in dict.Keys){
            //     Console.WriteLine(key+": "+dict[key]);
            // }
            // var newDict = tool.SortDictWithKey(dict);
            // foreach (var key in newDict.Keys){
            //     Console.WriteLine(key+": "+newDict[key]);
            // }
            int[] shita = {1,3,5};
            // Console.WriteLine(sol.kDifference(new List<int>(shita), 2));
            //Console.W
            int[] j = {2,3,1,1,4};
            int[] sorted = {1,2,3,4,5,7,9};
            Console.WriteLine(Array.BinarySearch(sorted, 3));
            }
    }
}
