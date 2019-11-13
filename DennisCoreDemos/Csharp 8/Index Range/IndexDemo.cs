using System;
using System.Collections.Generic;
using System.Text;

namespace DennisCoreDemos.Csharp_8.Index_Range
{
    public class IndexDemo
    {
        public static void Run() 
        {

            Index i1 = 3;  // number 3 from beginning  

            Index i2 = ^4; // number 4 from end  

            int[] a = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            Console.WriteLine($"{a[i1]}, {a[i2]}"); // "3, 6"  
        }
    }
}
