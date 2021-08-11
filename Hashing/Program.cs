using System;

namespace Hashing
{
    class Test {
        /// <summary>This method changes the point's location by
        ///    the given x- and y-offsets.
        /// <example>For example:
        /// <code>
        ///    Point p = new Point(3,5);
        ///    p.Translate(-1,3);
        /// </code>
        /// results in <c>p</c>'s having the value (2,8).
        /// </example>
        /// </summary>
        public int Translate(int xor, int yor)
        {
            return 1;
        }
    }
    class Program
    {
        
        public int Translate(int xor, int yor)
        {
            return 1;
        }
        static void Main(string[] args)
        {
            Test tester = new Test();
            tester.Translate(1,2);
        }
    }
}
