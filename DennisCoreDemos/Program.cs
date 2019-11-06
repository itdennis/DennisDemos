using DennisCoreDemos.Csharp_8.Async_streams;
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
            Console.WriteLine(await Lc35.Run(new int[] { 1, 3, 5 }, 3));

            Console.ReadKey();
        }
    }
}
