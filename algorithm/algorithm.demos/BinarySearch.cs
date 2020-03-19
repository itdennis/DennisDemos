using System;
using System.Collections.Generic;
using System.Text;

namespace algorithm.demos
{
    public class BinarySearch
    {

        public static void Run() 
        {
            int[] seqList = { 32, 25, 8, 10, 13, 21, 36, 51, 57, 62, 69 };
            Console.WriteLine("-------------Array.BinarySearch-------------");
            Array.Sort(seqList);
            Console.WriteLine("查找51：{0}", Array.BinarySearch(seqList, 51));
            Console.WriteLine("查找69：{0}", Array.BinarySearch(seqList, 69));
            Console.WriteLine("查找15：{0}", Array.BinarySearch(seqList, 15));
        }
    }
}
