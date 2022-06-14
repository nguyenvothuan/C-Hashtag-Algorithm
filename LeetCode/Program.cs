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
            // var str = new char[5] { 'h', 'e', 'l', 'l', 'o' };
            // sol.ReverseString(str);
            // foreach (char chr in str)
            //     Console.WriteLine(chr);
            // Console.Write(sol.IsPalindrome(".,"));
            // int[][] list1 = new int[4][];
            // list1[0] = new int[2] { 0, 2 };
            // list1[1] = new int[2] { 5, 10 };
            // list1[2] = new int[2] { 13, 23 };
            // list1[3] = new int[2] { 24, 25 };
            // int[][] list2 = new int[4][];
            // list2[0] = new int[2] { 1, 5 };
            // list2[1] = new int[2] { 8, 12 };
            // list2[2] = new int[2] { 15, 24 };
            // list2[3] = new int[2] { 25, 26 };
            // var test = sol.IntervalIntersection(list1, list2);
            // foreach (var arr in test)
            // {
            //     foreach (int i in arr) { Console.Write(i + " ");}
            //     Console.WriteLine();
            // }
            // int[] test1 = {1,2,1};
            // int[] test2 = {-3,-3};
            // Console.Write(sol.CastleInTheSky(test1));
            // Console.Write(sol.RetrieveTaskName("codi3lity"));
            // string[] T = {"test1a", "test2","test1b","test1c","test3"};
            // string[] R = {"Wrong answer", "OK", "RE", "OK", "TL"};
            // Console.Write(sol.ScoreFromTestCase(T, R));
            // int[] A = {3,2,4,3};
            // int F = 2; int M = 4;
            // var test = sol.ForgottenShootingDice(A, F, M);
            // foreach(int i in test) 
            //     Console.WriteLine(i);
            //    Console.Write(sol.IsHappy(19));
            // Console.Write(sol.IsValidAbbreviation("solution", "s6n"));
            //    / int a =1;
            //     Console.Write(a+=2);
            // var Picker = new RandomWeightPicker(new int[3]{1,3,4});
            // int count0 =0, count1=0,count2=0;
            // for(int i =0;i<8000;i++) {
            //     int cur = Picker.PickIndex();
            //     if(cur==0) count0++;
            //     if (cur==1) count1++;
            //     if (cur==2) count2++;
            // }
            // Console.WriteLine("Number of 0:" + count0);
            // Console.WriteLine("Number of 1:" + count1);
            // Console.WriteLine("Number of 2:" + count2);
            UUGraph uugraph = new UUGraph(5);
            uugraph.AddEdge(0, 1); uugraph.AddEdge(0, 2); uugraph.AddEdge(0, 3);
            uugraph.AddEdge(1, 4); uugraph.AddEdge(4, 2);
            // graph.BFS(0, true);
            // graph.DFS(0, true);
            UDGraph udGraph = new UDGraph(5);
            udGraph.AddEdge(0, 2);
            udGraph.AddEdge(2, 3);
            udGraph.AddEdge(4, 3);
            udGraph.AddEdge(0, 1);
            // Console.Write(udGraph.MotherVertex());
            // udGraph.TransitiveClosure(true);
            // Console.Write('0'==48);
            // Console.Write(sol.CountBinarySubstrings("101011100"));
            // Console.Write(Convert.ToChar(1+48));
            // Console.Write(sol.AddStrings("132", "128"));
            // Console.Write(new int[0] == null);
            // int[][] graph = new int[5][];
            // graph[0] = new int[3]{4,3,1};
            // graph[1] = new int[3]{3,2,4};
            // graph[2] = new int[1]{3};
            // graph[3] = new int[1]{4};
            // graph[4] = new int[0];
            // var test = sol.AllPathsSourceTarget(graph);
            // foreach (var list in test)
            // {
            //     foreach(int i in list)
            //         Console.Write(i+" ");
            //     Console.WriteLine();
            // }
            // var test = sol.SplittingArray(new int[6]{2, 1, 2, 3, 3, 4});
            // foreach(var a in test)
            // {
            //     foreach (int i in a)
            //         Console.Write(i+" ");
            //     Console.WriteLine();
            // }
            // var test = sol.FullJustify(new string[7]{"This", "is", "an", "example", "of", "text", "justification."}, 16);
            // foreach 
            //IList<IList<string>> test = new List<IList<string>>();
            // test.Add(new List<string>(new string[4]{"Gabe","Gabe0@m.co","Gabe3@m.co","Gabe1@m.co"}));
            // test.Add(new List<string>(new string[4]{"Kevin","Kevin3@m.co","Kevin5@m.co","Kevin0@m.co"}));
            // test.Add(new List<string>(new string[4]{"Ethan","Ethan5@m.co","Ethan4@m.co","Ethan0@m.co"}));
            // test.Add(new List<string>(new string[4]{"Hanzo","Hanzo3@m.co","Hanzo1@m.co","Hanzo0@m.co"}));
            // test.Add(new List<string>(new string[4]{"Fern","Fern5@m.co","Fern1@m.co","Fern0@m.co"}));
            // test.Add(new List<string>(new string[3] {"John","johnsmith@mail.com","john_newyork@mail.com"}));
            // test.Add(new List<string>(new string[3] {"John","johnsmith@mail.com","john00@mail.com"}));
            // test.Add(new List<string>(new string[2] {"Mary","mary@mail.com"}));
            // test.Add(new List<string>(new string[2] {"John","johnnybravo@mail.com"}));


            // var res = sol.AccountsMerge(test);
            // foreach (var arr in res) {
            //     foreach (string str in arr)
            //         Console.Write(str + " ");
            //     Console.WriteLine();
            // }
            // var test = sol.FindKSmallest(3,new int[8]{1,9,2,4,5,6,3,7});
            // foreach (int i in test)
            //     Console.WriteLine(i);
            // TreeNode root =new TreeNode(1,new TreeNode(2, null, new TreeNode(5)), new TreeNode(3, null, new TreeNode(4)));
            // var test = sol.RightSideView(root);
            // foreach(int i in test)
            //     Console.Write(i+" ");
            // var test = new ListNode(1, new ListNode(2, new ListNode(3, new ListNode(4, new ListNode(5)))));
            // var res = sol.OddEvenList(test);
            // while (res!=null) {
            //     Console.Write(res.val +" ");
            //     res = res.next;
            // }
            // Console.Write(sol.IsInterleave("aabcc", "dbbca", "aadbbcbcac"));
            // Console.Write(sol.MaxProduct(new int[3]{-2,-1,-1}));
            // Console.Write(sol.CompareVersion("7.5.2", "7.5.2.4"));
            // Console.Write(sol.FindDuplicate(new int[5]{1,3,4,2,2}));
            // List<IList<int>> triangle = new List<IList<int>>();
            // triangle.Add(new List<int>(new int[1]{2}));
            // triangle.Add(new List<int>(new int[2]{3,4}));
            // triangle.Add(new List<int>(new int[3]{6,5,7}));
            // triangle.Add(new List<int>(new int[4]{4,1,8,3}));
            // Console.Write(sol.MinimumTotal(triangle));
            // TreeNode t1 = new TreeNode(3, new TreeNode(5,new TreeNode(6), new TreeNode(2, new TreeNode(7), new TreeNode(4))), new TreeNode(1, new TreeNode(9), new TreeNode(8)));
            // TreeNode t2 = new TreeNode(3, new TreeNode(5, new TreeNode(6), new TreeNode(7)), new TreeNode(1, new TreeNode(4), new TreeNode(2, new TreeNode(9), new TreeNode(8))));
            // Console.Write(sol.LeafSimilar(t1, t2));
            // Console.Write(sol.ReverseVowels("aA"));
            // Console.Write(sol.Translate("hello, secret meeting tongith"));
            // int[] from = {4,9,6,1};
            // int[] to = {9,5,1,4};
            // Console.Write(sol.FindNetworkEndpoint(4, from, to));
            // int[] test = {1,6,4,8,2};
            // Console.Write(sol.FindSmallestInterval(test));
            // Console.Write(sol.IsNumber("123.-"));
            // Console.Write(sol.FindPeakElement(new int[9]{8,1,3,4,5,6,7,8,9}));
            // var test = sol.FindEvenNumbers(new int[4]{2,1,3,0});
            // foreach (int i in test) 
            //     Console.WriteLine(i);
            // var test = sol.DeleteMiddle(new ListNode(1, new ListNode(2, new ListNode(3, new ListNode(4, new ListNode(5, new ListNode(6, new ListNode(7))))))));
            // var cur = test;
            // while (cur!=null)
            // {
            //     Console.Write(cur.val);
            //     cur = cur.next;
            // }

            // var tree = new TreeNode(5, new TreeNode(1, new TreeNode(3)), new TreeNode(2, new TreeNode(6, new TreeNode(4))));
            // Console.Write(sol.GetDirections(tree, 5,6));
            // int[][] test = new int[4][];
            // test[0] = new int[2]{5,1};
            // test[1] = new int[2]{4,5};
            // test[2] = new int[2]{11,9};
            // test[3] = new int[2]{9,4};
            // var aftertest = sol.ValidArrangement(test);
            // for(int i =0;i<test.Length;i++) {
            //     Console.Write(i +": ");
            //     foreach(int j in aftertest[i]) {
            //         Console.Write(j+" ");
            //     }
            //     Console.WriteLine();
            // } 
            // var test=sol.TwoSumSorted(new int[6]{1,2,3,4,6,20}, 10);
            // Console.Write(test[0]+", "+test[1]);

            // Console.Write(sol.FirstBadVersion(212675));
            // int[] n1 = {1,2,2,3,1}, n2 = {2,3,2};
            // int[] test = sol.Intersection(n1, n2);
            // foreach (int i in test) Console.WriteLine(i);
            // Console.Write(sol.IsPerfectSquare(808201));
            // Console.Write(sol.MinCostToMoveChips(new int[5]{2,2,2,3,3}));
            // Console.Write(sol.CanReachOneLiner(new int[5]{3,0,2,1,2},2));
            // char[][] grid = new char[4][];
            // grid[0] = new char[5] {'1','1','0','0','0'};
            // grid[1] = new char[5] {'1','1','0','0','0'};
            // grid[2] = new char[5] {'0','0','1','0','0'};
            // grid[3] = new char[5] {'0','0','0','1','1'};
            // Console.Write(sol.NumIslands(grid));
            // Console.Write(sol.MaxProfitStock(new int[8]{3,3,5,0,0,3,1,4}));
            // LRUCache lru = new LRUCache(2);
            // lru.Put(1,1);
            // lru.Put(2,2);

            // Console.WriteLine(lru.Get(2));
            // lru.Put(3,3);
            // Console.WriteLine(lru.Get(3));
            // Console.WriteLine(lru.Get(1));
            // Console.Write(sol.NumTilings(60));
            // Console.Write(sol.OptimizedKnapsack01(new int[10]{23,26,20,18,32,27,29,26,30,27}, new int[10]{505, 352, 458, 220, 354, 414,498,545, 473, 543}, 67));
            // Console.Write(sol.CanPartition(new int[4]{1,5,11,9}));
            // int[][] matrix = new int[4][];
            // matrix[0] = new int[4]{1,2,3,4};
            // matrix[1] = new int[4]{5,6,7,8};
            // matrix[2] = new int[4]{9,10,11,12};
            // matrix[3]=new int[4]{13,14,15,16};
            // var res = sol.SpiralOrder(matrix);
            // foreach (int i in res) Console.Write(i +" ");
            // Console.Write(sol.WordBreak("catsandog", new List<string>(new string[5]{"cats","dog","sand","and","cat"})));
            // Console.Write(sol.WordBreak("leetcode", new List<string>(new string[2]{"leet", "code"})));
            // Console.Write(sol.LengthOfLIS(new int[6]{0,1,0,3,2,3}));
            // var tree = new TreeNode(5, new TreeNode(1), new TreeNode(4, new TreeNode(3), new TreeNode(6)));
            // var tree = new TreeNode(2, new TreeNode(1), new TreeNode(3));
            // Console.Write(sol.IsValidBST(tree));
            // Console.Write(sol.GetMoneyAmount(10));
            // char[][] mat = new char[4][];
            // mat[0] = new char[5]{'1','0','1','0','0'};
            // mat[1] = new char[5]{'1','0','1','1','1'};
            // mat[2] = new char[5]{'1', '1','1','1','1'};
            // mat[3] = new char[5]{'1','0','0','1','0'};
            // Console.Write(sol.MaximalSquare(mat));
            //  Console.WriteLine(sol.AtMostNGivenDigitSet(new string[4]{"3", "4", "5", "6"},64));
            // Console.Write(sol.AtMostNGivenDigitSet(new string[3]{"5","7","8"}, 59));
            // Console.Write(sol.GetNumberOfDigits(6988));
            // TreeNode tree = new TreeNode(1, new TreeNode(2, new TreeNode(3, new TreeNode(4))));
            // var test =sol.InorderTraversal1( sol.BalanceBST(tree));
            // foreach(int i in test) Console.Write(i);
            // Console.Write(sol.RemoveDuplicates("abcd", 2));
            // var test = sol.MinimumAbsDifference(new int[4]{4,2,1,3});
            // foreach(var i in test)
            // {
            //     foreach(int j in i) Console.Write(j+" ");
            //     Console.WriteLine();
            // }
            // var test = sol.Flatten(new TreeNode(1, null, new TreeNode(2, new TreeNode(3))));
            // var test = new TreeNode(1, null, new TreeNode(2, new TreeNode(3), new TreeNode(4)));
            // sol.Flatten(test);
            // var res = sol.InorderTraversal(test);
            // foreach(int i in res) Console.Write(i);
            // Console.Write(sol.IsValidSerialization("9,3,4,#,#,1,#,#,2,#,6,#,#"));
            // Console.Write(sol.IsPowerOfTwo(2));
            // ListNode list = new ListNode(1,new ListNode(2, new ListNode(3, new ListNode(4))));
            // sol.ReorderList(list);
            // while (list!=null) {
            //     Console.Write(list.val + " ");
            //     list = list.next;
            // }
            // var test = sol.AsteroidCollision(new int[]{-2, -2, 1, -2});
            // foreach(int i in test) Console.Write(i+" ");
            // var test = sol.SearchRange(new int[]{5,7,7,8,8,10}, 6);
            // foreach(int i in test) Console.Write(i +" ");
            // TreeNode tree = new TreeNode(1, new TreeNode(2, new TreeNode(4), new TreeNode(5)), new TreeNode(3, new TreeNode(6),new TreeNode(7)));
            // Console.Write(sol.IsCompleteTree(tree));
            // Console.Write(sol.TrappingWater(new int[]{0,1,0,2,1,0,1,3,2,1,2,1}));
            // Console.Write(sol.Find132pattern(new int[]{3,5,0,3,4}));
            // int [][] arr = new int[6][];
            // arr[0] = new int[]{1,0};
            // arr[1] = new int[]{2,1};
            // arr[2] = new int[]{0,3};
            // arr[3] = new int[]{4,3};
            // arr[4] = new int[]{3,5};
            // arr[5] = new int[]{4,5};
            // var test = sol.FindOrder(6, arr);
            // foreach (int i in test) Console.Write(i + " ");
            // StockSpanner test=new StockSpanner();
            // Console.WriteLine(test.Next(100));
            // Console.WriteLine(test.Next(80));
            // Console.WriteLine(test.Next(60));
            // Console.WriteLine(test.Next(70));
            // Console.WriteLine(test.Next(60));
            // Console.WriteLine(test.Next(75));
            // Console.WriteLine(test.Next(85));
            // Console.WriteLine(test.Next(0));
            // Console.Write(sol.MctFromLeafValues(new int[]{6,2,4}));
            // Console.Write(sol.ScoreParentheses("((()))"));
            // Console.Write(sol.SumSubarrayMins(new int[]{71,55,82,55}));
            // Console.Write(sol.Calculate("3+2 *9"));
            // var test = sol.ExecuteInstructions(3, new int[]{0,1}, "RRDDLU");
            // foreach (int i in test)
            //     Console.Write(i +" ");
            // var test= sol.GetDistances(new int[]{2,1,3,1,2,3,3});
            // foreach (int i in test)
            //     Console.Write(i+" ");
            // Console.WriteLine(sol.CanReorderDoubled(new int[]{2,1,2,6}));
            // var test = sol.RecoverArray(new int[]{2,10,6,4,8,12});
            // foreach (int i in test)
            // Console.Write(i +" ");
            // CompareLargestConcat comp = new CompareLargestConcat();
            // Console.Write(comp.Compare(10,2));
            // Console.Write(sol.LargestNumberII(new int[]{0,0}));
            // var test = sol.MiddleNode(new ListNode(1, new ListNode(2, new ListNode(3, new ListNode(4, new ListNode(5))))));
            // Console.Write(test.val);
            // var test = new int[]{24,3,82,22,35,84,19};
            // Console.Write(sol.TriangleNumber(test));
            // var tree = new NexNode(1, new NexNode(2, new NexNode(4), new NexNode(5)), new NexNode(3, new NexNode(6), new NexNode(7)));
            // var test = sol.Connect(tree);
            // test.SeeNext();
            // Console.Write(sol.FindUnsortedSubarray(new int[]{1,2,3,4}));
            // Console.Write(sol.IncreasingTriplet(new int[]{20,100,10,12,5,13}));
            // Console.Write(sol.SmallestRepunitDivByK(3));
            // var arr = new int[4][];
            // arr[0] = new int[]{10,16};
            // arr[1] = new int[]{2,8};
            // arr[2] = new int[]{1,6};
            // arr[3] = new int[]{7,12};
            // Console.Write(sol.FindMinArrowShots(arr));
            // var arr = sol.GetDigitArray(10);
            // foreach (int i in arr) Console.Write(i + " ");
            // Console.Write(sol.MonotoneIncreasingDigits(100000));
            // var test = new int[3][];
            // test[0] = new int[]{1,2};
            // test[1] = new int[]{2,3};
            // test[2] = new int[]{3,4};
            // Console.Write(sol.FindLongestChain(test));
            // Console.Write(sol.Candy(new int[]{1,2,2}));
            // Console.Write(sol.NumPairsDivisibleBy60(new int[]{15,63,451,213,209,343,319}));
            // var arr = new int[]{1,2,3,3,4,5};
            // Console.Write(sol.IsPossible(arr));
            // int[] a1 = {2,4,5,8,10};
            // int[] a2 = {4,6,8,9};
            // Console.Write(sol.MaxSum(a1, a2));
            // Console.Write(sol.MaxCoins2(new int[]{2,4,1,2,7,8}));
            // var tree = new TreeNode(3, new TreeNode(9), new TreeNode(20, new TreeNode(15), new TreeNode(7)));
            // var test = sol.LevelOrder(tree);
            // foreach (var lv in test)
            // {
            //     foreach (int i in lv) Console.Write(i+" ");
            //     Console.WriteLine();
            // }    
            // var arr = new int[4][];
            // arr[0] = new int[]{9,3,4};
            // arr[1] = new int[]{9,1,7};
            // arr[2] = new int[]{4,2,4};
            // arr[3] = new int[]{7,4,5};
            // Console.Write(sol.CarPooling(arr, 23));
            // int[][] clips = new int[16][];
            // clips[0] = new int[2]{0,10};
            // clips[1] = new int[2]{6,8};
            // clips[2] = new int[2]{0,2};
            // clips[3] = new int[2]{5,6};
            // clips[4] = new int[2]{0,4};
            // clips[5] = new int[2]{0,3};
            // clips[6] = new int[2]{6,7};
            // clips[7] = new int[2]{1,3};
            // clips[8] = new int[2]{4,7};
            // clips[9] = new int[2]{1,4};
            // clips[10] = new int[2]{2,5};
            // clips[11] = new int[2]{2,6};
            // clips[12] = new int[2]{3,4};
            // clips[13] = new int[2]{4,5};
            // clips[14] = new int[2]{5,7};
            // clips[15] = new int[2]{6,9};


            // int[] INPUT = {4};
            // Console.WriteLine(sol.MaxSumDivThree(INPUT));
            // int[] tokens = { 26 };
            // Console.Write(sol.BagOfTokensScore(tokens, 51));
            // Console.Write(sol.EarliestFullBloom(new int[] { 1, 2, 3, 2 }, new int[] { 2, 1, 2, 1 }));   
            // sol.Merge(new int[] { 1, 2, 3, 0, 0, 0 }, 3, new int[] { 2, 5, 6 }, 3);
            // Console.Write(sol.NumRescueBoats(new int[] { 3, 2, 2, 1 }, 3));
            // Console.Write(sol.RemovePalindromeSub("baabb"));
            // Console.Write(sol.CoinChange(new int[]{186, 419, 83, 408}, 6249));
            // Console.Write(sol.NumRabbits(new int[]{1,1,2}));
            // int[][] arr = new int[4][];
            // arr[0] = new int[2]{4,3};
            // arr[1] = new int[2]{1,4};
            // arr[2] = new int[2]{4,6};
            // arr[3] = new int[2]{1,7};
            // Console.Write(sol.MaxNumberOfFamilies(4, arr));
            // Console.Write(sol.MinOperations(new int[]{3,2,20,1,1,3}, 10));
            // Console.WriteLine(sol.NumberOfArithmeticSlices(new int[]{1,2,3,4,1,-2,-5}));
            // int[][] arr = new int[3][];
            // arr[0] =new int[3]{1,2,3};
            // arr[1] =new int[3]{4,5,6};
            // arr[2] =new int[3]{7,8,9};
            // int[][] arr2 = new int[2][];
            // arr2[0] = new int[2]{1,2};
            // arr2[1] = new int[2] {3,4};
            // int[] res = sol.FindDiagonalOrder(arr);
            // foreach(int i in res)
            //     Console.WriteLine(i);
            // int[] arr = new int[] { 1, 2, 3, 4 };
            // sol.Rotate(arr, 2);
            // foreach (int i in arr)
            // {
            //     Console.WriteLine(i);
            // }
            // Console.Write(sol.CountCollisions("RLRSLL"));
            // CustomStack cs = new CustomStack(3);
            // cs.Push(3);
            // cs.Push(2);
            // cs.Push(1);
            // cs.Push(0);
            // cs.Increment(2,100);
            // while(true) {
            //     int cur = cs.Pop();
            //     if (cur==-1) break;
            //     Console.WriteLine(cur);
            // }
            // Console.Write(sol.MaxWidthRamp(new int[]{6,0,8,2,1,5}));
            int[] res = sol.MostCompetitive(new int[]{71,18,52,29,55,73,24,42,66,8,80,2}, 3);
            foreach(int i in res)
            Console.WriteLine(i);
        }
    }

}
