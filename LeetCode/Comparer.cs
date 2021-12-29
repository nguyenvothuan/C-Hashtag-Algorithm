using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

public class CompareInt : Comparer<int>
{
    public override int Compare([AllowNull] int x, [AllowNull] int y)
    {
        if (x > y) return 1;
        if (x < y) return -1;
        return 0;
    }
}

public class CompareLexiString : Comparer<string>
{
    public override int Compare([AllowNull] string x, [AllowNull] string y)
    {
        if (x.Length == y.Length) return String.Compare(x, y);
        if (x.Length == 0) return -1; if (y.Length == 0) return 1;
        int smallerLen = Math.Min(x.Length, y.Length);
        for (int i = 0; i < smallerLen; i++)
        {
            if (x[i] < y[i]) return -1;
            if (x[i] > y[i]) return 1;
        }
        return x.Length == smallerLen ? Compare(x, y.Substring(smallerLen + 1)) : Compare(x.Substring(smallerLen + 1), y);
    }
    public int StringToInt(string str) { return Int32.Parse(str); }


}

public class CompareThreeArray : Comparer<int[]>
{
    public override int Compare([AllowNull] int[] x, [AllowNull] int[] y)
    {
        Array.Sort(x); Array.Sort(y);
        for (int i = 0; i < 3; i++)
        {
            if (x[i] < y[i]) return -1;
            if (x[i] > y[i]) return 1;
        }
        return 0;
    }
}

public class EmailComparer : Comparer<string>
{
    public override int Compare([AllowNull] string x, [AllowNull] string y)
    {
        for (int i = 0; i < Math.Min(x.Length, y.Length); i++)
        {
            if (x[i] > y[i]) return 1;
            if (x[i] < y[i]) return -1;
        }
        return 0;
    }
}

public class CompareConcatString : Comparer<string>
{
    public override int Compare([AllowNull] string x, [AllowNull] string y)
    {
        string str1 = String.Concat(x, y);
        string str2 = string.Concat(y, x);
        return String.Compare(str1, str2);
    }
}

public class CompareInterval : Comparer<int[]>
{
    public override int Compare(int[] a, int[] b)
    {
        if (a[0] == b[0]) return 0;
        if (a[0] > b[0]) return 1;
        return -1;
    }
}

public class CompareLargestConcat : Comparer<int>
{
    int NumberOfDigit(int x)
    {
        int count = 1;
        while (x >= 10)
        {
            x /= 10;
            count++;
        }
        return count;
    }
    public override int Compare(int x, int y)
    {
        if (x == y) return 0;
        int numDigX = NumberOfDigit(x), numDigY = NumberOfDigit(y);
        long xy = x * (long)Math.Pow(10, numDigY) + y; //xy
        long yx = y * (long)Math.Pow(10, numDigX) + x; //yx
        return xy > yx ? 1 : -1;
    }
}

public class ReconstructQueueCompare : Comparer<int[]>
{
    public override int Compare(int[] o1, int[] o2)
    {
        //of same height, the one with higher number oftaller ppl goes later
        //of differnt height, the taller guy goes first
        return o1[0] != o2[0] ? -o1[0] + o2[0] : o1[1] - o2[1];
    }
}

public class RemoveIntervalCompare : Comparer<int[]> {
    public override int Compare(int[] x, int[] y)
    {  
        return -x[1] + y[1]; 
    }
}