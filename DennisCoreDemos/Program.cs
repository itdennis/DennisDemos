﻿using DennisCoreDemos.Csharp_8.Async_streams;
using DennisCoreDemos.LeetCodes;
using System;
using System.Threading.Tasks;
using static System.Console;

namespace DennisCoreDemos
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //await Customer.AsyncStreamsDemo();
            //await Lc14.Run(new string[] { "flower", "flow", "flight"});
            //await Lc15.Run("[");
            //await Lc26.Run(new int[] { 1, 1, 2 });
            //await Lc15.Run("[");
            //await Lc27.Run(new int[] { 1,2,3,2,4 }, 2);
            //await Lc28.Run("hello", "ll");
            //Console.WriteLine(await Lc35.Run(new int[] { 1, 3, 5 }, 3));
            //Console.WriteLine(await Lc704.Run(new int[] { -1, 0, 3, 5, 9, 12 }, 2));
            Console.WriteLine(await Lc300.Run(new int[] { 10, 9, 2, 5, 3, 7, 101, 18 }));

            Console.ReadKey();
        }
    }
}
