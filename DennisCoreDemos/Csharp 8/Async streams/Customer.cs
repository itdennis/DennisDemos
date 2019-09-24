using System;
using System.Collections.Generic;
using System.Text;

namespace DennisCoreDemos.Csharp_8.Async_streams
{
    public class Customer
    {
        /// <summary>
        /// we use this method to get the data from producter, but if the producter's logic is complexity enough, 
        /// the main thread will be blocked for a long time, so that why we need the async to slove this problem.
        /// </summary>
        public void GetDataFromProducter1() 
        {
            const int count = 5;
            ConsoleExt.WriteLine($"Starting the application with count: {count}!");
            ConsoleExt.WriteLine("Classic sum starting.");
            ConsoleExt.WriteLine($"Classic sum result: {Producter.SumFromOneToCount(count)}");
            ConsoleExt.WriteLine("Classic sum completed.");
            ConsoleExt.WriteLine("################################################");
            ConsoleExt.WriteLine(Environment.NewLine);
        }

        /// <summary>
        /// in this function, the result was splited by servral results and displayed in the consle.
        /// this is the benifit of yield feature. we can get some of the result before we get the whole result.
        /// but we can also see, the producter's logic still block the main thread.
        /// </summary>
        public void GetDataFromProducter2() 
        {
            const int count = 5;
            ConsoleExt.WriteLine("Sum with yield starting.");
            foreach (var i in Producter.SumFromOneToCountYield(count))
            {
                ConsoleExt.WriteLine($"Yield sum: {i}");
            }
            ConsoleExt.WriteLine("Sum with yield completed.");

            ConsoleExt.WriteLine("################################################");
            ConsoleExt.WriteLine(Environment.NewLine);
        }

        /// <summary>
        /// the async feature help us to slove the main thread block problem. 
        /// but we still got the whole result at one time.
        /// we need some solution to fix both main threads block and split the results.
        /// how to fix that requirement? 
        /// </summary>
        public async void GetDataFromProducter3() 
        {
            const int count = 5;
            ConsoleExt.WriteLine("async example starting.");
            var result = await Producter.SumFromOneToCountAsync(count);
            ConsoleExt.WriteLine("async Result: " + result);
            ConsoleExt.WriteLine("async completed.");

            ConsoleExt.WriteLine("################################################");
            ConsoleExt.WriteLine(Environment.NewLine);
        }
    }
}
