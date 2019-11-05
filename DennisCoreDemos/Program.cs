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

            await Lc27.Run(new int[] { 0, 1, 2, 2, 3, 0, 4, 2 }, 2);
        }
    }
}
