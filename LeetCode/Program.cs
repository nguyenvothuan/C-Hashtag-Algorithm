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
            // int[][] arr = new int[4][];
            // arr[0] = new int[6] { 1, 1, 2, 2, 4, 4 };
            // arr[1] = new int[4] { 3, 4, 10, 4 };
            // arr[2] = new int[4] { 2, 10, 1, 2 };
            // arr[3] = new int[4] { 5, 4, 4, 5 };
            // int[] trash = new int[6] { 1, 0, 0, 1, 0, 0 };
            // List<int> dummyList = new List<int>(arr[0]);
            // int[] rec1 = { 0, 0, 1, 1 }; int[] rec2 = { 2, 2, 3, 3 };
            // //string s = "ababcbacade";
            // Tool<char> tool = new Tool<char>();
            // Dictionary<int, char> dict = new Dictionary<int, char>();
            // dict.Add(2, 'b');
            // dict.Add(3, 'a');
            // dict.Add(1, 'c');
            // foreach (var key in dict.Keys){
            //     Console.WriteLine(key+": "+dict[key]);
            // }
            // var newDict = tool.SortDictWithKey(dict);
            // foreach (var key in newDict.Keys){
            //     Console.WriteLine(key+": "+newDict[key]);
            // }
            // Console.WriteLine(sol.kDifference(new List<int>(shita), 2));
            //Console.W
            // int[] j = { 2, 3, 1, 1, 4 };
            // int[] sorted = { 1, 2, 3, 4, 5, 7, 9 };
            // List<string> a = new List<string>(new string[5] { "a", "jk", "abb", "mn", "abc" });
            // List<string> b = new List<string>(new string[5] { "bb", "kj", "bbc", "op", "def" });

            // var test = sol.getMinimumDifference(a, b);
            // List<int> cal = new List<int>(new int[5] { 2, 3, 15, 1, 16 });
            // int[][] obs = new int[1][];
            // obs[0] = new int[2] { 1, 1 };
            // int[][] tel = new int[2][];
            // tel[0] = new int[4] { 0, 2, 0, 1 };
            // tel[1] = new int[4] { 0, 3, 2, 0 };


            // int[] shit = { 0, 1, 2, 2, 3, 3, 5, 8, 9 };
            // List<int> testshit = new List<int>(shit);
            // // tool.BinaryInsertSortedList(testshit, 6);
            // List<List<int>> testfuck = new List<List<int>>();
            // testfuck.Add(new List<int>(new int[3] { 1, 2, 100 }));
            // testfuck.Add(new List<int>(new int[3] { 2, 5, 100 }));
            // testfuck.Add(new List<int>(new int[3] { 3, 4, 100 }));

            // Console.Write(sol.RemoveElement(shit, 2));
            // Console.WriteLine(sol.camelCaseSeparation(new string[1]{"a"}, "AAAAAA"));
            // Console.Write(sol.EnoughParentheses("({}}])))"));
            // var threesome = sol.ThreeSum(new int[6] { -1, 0, 1, 2, -1, -4 });
            // foreach (var list in threesome)
            // {
            //     foreach (var item in list)
            //         Console.Write(item +", ");
            //     Console.WriteLine();
            // }
            // Console.WriteLine(sol.testStuff());
            // Console.WriteLine(sol.StrStr("mississippi", "issip"));
            // var genpar = sol.GenerateParenthesis(4);
            // foreach(string str in genpar)
            //     Console.WriteLine(str);
            // int[] nums = {3,30,34,5,9};
            // ListNode l1 = new ListNode(2, new ListNode(4, new ListNode(3)));
            // ListNode l2 = new ListNode(5, new ListNode(6, new ListNode(4)));
            // var test1 = sol.AddTwoNumbers(l1, l2);
            // var cur = test1;
            // while (cur!=null)
            // {
            //     Console.WriteLine(cur.val);
            //     cur = cur.next;
            // }
            // Console.Write(sol.IsPalindrome(1200021));
            // var head = new ListNode(1, new ListNode(2, new ListNode(3, new ListNode(4))));
            // sol.SwapPairs(head);
            // char[][] board = new char[9][];
            // board[0] = new char[9] { '5', '3', '.', '.', '7', '.', '.', '.', '.' };
            // board[1] = new char[9] { '6', '.', '.', '1', '9', '5', '.', '.', '.' };
            // board[2] = new char[9] { '.', '9', '8', '.', '.', '.', '.', '6', '.' };
            // board[3] = new char[9] { '8', '.', '.', '.', '6', '.', '.', '.', '3' };
            // board[4] = new char[9] { '4', '.', '.', '8', '.', '3', '.', '.', '1' };
            // board[5] = new char[9] { '7', '.', '.', '.', '.', '.', '.', '.', '6' };
            // board[6] = new char[9] { '.', '6', '.', '.', '.', '.', '2', '8', '.' };
            // board[7] = new char[9] { '.', '.', '.', '4', '1', '9', '.', '.', '5' };
            // board[8] = new char[9] { '.', '.', '.', '.', '2', '.', '.', '7', '9' };
            // // Console.Write(sol.IsValidSudoku(board));
            // // Console.Write(sol.Multiply("12","10" ));
            // // int[][] matrix = new int[2][];
            // // matrix[0] = new int[2] {1,1};
            // // matrix[1] = new int[2] {2,2};
            // sol.SolveSudoku(board);
            // foreach(var row in board ){
            //     foreach (int i in row)
            //         Console.Write(i+ " ");
            //     Console.WriteLine();
            // }


            //Console.Write(sol.SearchMatrix(matrix, 0));
            // ListNode head = new ListNode(1, new ListNode(2, new ListNode(3)));
            // var test1 = sol.RemoveNthFromEnd(head, 3);
            // while (test1!=null) {
            //     Console.Write(test1.val);
            //     test1 = test1.next;
            // }
            // var test1 = sol.CommaSeperator(",,,");
            // foreach (var str in test1) 
            //     Console.WriteLine(str);
            // var test1 = new List<string>(new string[5] { "A,B,G", "A,B,C", "D,E,", "A,B,", "D,E,F" });
            // var test3 = new List<string>(new string[3] { "A,,", ",,C", "A,B,C" });
            // var test4 = new List<string>(new string[4] {"BACELNXGH9,TGZ7MNVJTY,W2H3B0ZJ6S",
            // "BACELNXGH9,TGZ7MNVJTY,W2H3B0ZJ6S",
            // "9A9AH5OTJ4,AM70384WO1,K281AQCQ1O",
            // "9A9AH5OTJ4,AM70384WO1,K281AQCQ1O"
            // });
            //  var test2 = sol.match_records(test4);
            // foreach (var list in test2)
            // {
            //     foreach (int i in list)
            //         Console.Write(i + ", ");
            //     Console.WriteLine();
            // }
            // int[] arr1 = {1,2,3,1};
            // var test = sol.Rob(arr1);
            // Console.WriteLine(test);
            //     int[][] edges = new int[3][];
            //     edges[1]=new int[2]{1,2};
            //     edges[2]=new int[2]{1,3};
            //     edges[0]=new int[2]{1,0}; 
            //    var test = sol.FindMinHeightTrees(4, edges);
            //    foreach (int i in test) Console.WriteLine(test);
            //Console.Write(sol.MinStartValue(new int[2]{1,2}));
            // Console.Write(sol.BalancedStringSplit("RLRRRLLRLL"));
            // Console.WriteLine(sol.Maximum69Number(9969));
            // Console.Write(sol.LemonadeChange(new int[5]{5,5,10,10,20}));
            // var test = new ListNode(7, new ListNode(7, new ListNode(7, new ListNode(7))));
            // var test1 = sol.RemoveElements(test, 7);
            // var cur = test1;
            // while(cur!=null) {
            //     Console.WriteLine(cur.val);
            //     cur =cur.next;
            // }
            // }   
            // Console.WriteLine(sol.CanPlaceFlowers(new int[5] { 1, 0, 0, 0, 1 }, 2));
            // Console.Write(sol.FindContentChildren(new int[4]{10, 9, 8,7}, new int[4]{5,6,7,8}));
            // var test= sol.DailyTemperatures(new int[8]{73,74,75,71,69,72,76,73});
            // var test = sol.NextGreaterElement(new int[3]{4,1,2}, new int[5]{2,1,2,4,3});
            //var test = sol.FinalPrices(new int[5]{8,4,6,2,3});
            // int test = sol.NumberOfWaysHash(new int[7]{1,2,3,3,4,3,5}, 6);
            // foreach(int i in test) Console.WriteLine(i);
            // Console.Write(sol.EncryptFacebook("abc"));
            //Console.Write(sol.CheckAlmostEquivalent("cccddabba", "babababab"));
            //Console.Write(sol.BalancedSplitExists(new int[6]{1,2,3,4,5,5}));
            // int[] test ={1,2,3,4,5,6,7,8};
            // var test1 = sol.LargestDivisibleSubset(test);
            // foreach (int i in test1) Console.WriteLine(i);
            // Console.Write(sol.MinCostClimbingStairs(new int[3]{10,15,20}));
            // int[] test = {0,1,2,0,12,0,3,0};
            // sol.MoveZeroes(test);
            // foreach (int i in test) Console.WriteLine(i);
            // var root = new TreeNode(5, new TreeNode(4, new TreeNode(11, new TreeNode(7), new TreeNode(2))), new TreeNode(8, new TreeNode(13), new TreeNode(4, null, new TreeNode(1))));
            //Console.WriteLine(sol.HasPathSum(root, 22));
            // var test = sol.Permute(new int[3]{1,2,3});
            // foreach (var list in test) {
            //     foreach (int i in list)
            //         Console.Write(i + " ");
            //     Console.WriteLine();
            // }
            // Console.Write(sol.CanJump(new int[5]{3,2,1,0,4}));
            // var test = sol.Combine(5,3);
            // foreach (var i in test) {
            //     foreach (int j in i)
            //         Console.Write(j+" ");
            //     Console.WriteLine();
            // }
            // Console.WriteLine("A: " + (int)'A');
            // Console.WriteLine("a: "+(int)'a');
            // Console.WriteLine("-A: "+(int)-'A');
            // char[][] matrix = new char[3][];
            // matrix[0] = new char[4] { 'A', 'B', 'C', 'E' };
            // matrix[1] = new char[4] { 'S', 'F', 'C', 'S' };
            // matrix[2] = new char[4] { 'A', 'D', 'E', 'E' };
            // Console.Write(sol.WordExist(matrix, "ABCCD"));

            // var list = new ListNode(1, new ListNode(4, new ListNode(3, new ListNode(2, new ListNode(5, new ListNode(2))))));
            // var test = sol.Partition(list,3);
            // while (test!=null) {
            //     Console.WriteLine(test.val);
            //     test = test.next;
            // }
            // var test = sol.ConstantSpaceMissingNumber(new int[8]{4,3,2,7,8,2,3,1});
            // foreach (int i in test) Console.Write(i+ " ");

            // var test = sol.LetterCombinations("");
            // foreach (var str in test)
            //     Console.WriteLine(str);
            // Console.Write(sol.HammingDistance2(4,1));
            // Console.Write(sol.CanThreePartsEqualSum(new int[11]{0,2,1,-6,6,-7,9,1,2,0,1}));
            // Console.Write(sol.SingleNonDuplicate1(new int[9]{3,3,7,7,9,9,10,11,11}));
            // Console.Write(sol.SearchInsert(new int[4]{1,3,5,6}, 5));
            // Console.Write(sol.SqrtWithBinarySearch(0));
            var str = new char[5] { 'h', 'e', 'l', 'l', 'o' };
            sol.ReverseString(str);
            foreach (char chr in str)
                Console.WriteLine(chr);
        }

    }
}
