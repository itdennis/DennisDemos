using System;
using System.Collections.Generic;
using System.Text;

namespace DennisCoreDemos.Csharp_8.Index_Range
{
    /// <summary>
    /// Range 的概念基本相同，其由两个 Index 值构成，
    /// 其一代表开始、其二代表结束，且可以用 x…y 的范围表达式编写。
    /// </summary>
    public class RangeDemo
    {
        public static void Run() 
        {
            Index i1 = 3;  // number 3 from beginning  

            Index i2 = ^4; // number 4 from end  

            int[] a = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            Console.WriteLine($"{a[i1]}, {a[i2]}"); // "3, 6"  

            var slice = a[i1..i2];
            // { 3, 4, 5 }  
        }
    }
}
