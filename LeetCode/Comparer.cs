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
