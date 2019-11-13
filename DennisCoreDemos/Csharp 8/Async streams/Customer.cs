using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DennisCoreDemos.Csharp_8.Async_streams
{
    public class Customer
    {
        /// <summary>
        /// we use this method to get the data from producter, but if the producter's logic is complexity enough, 
        /// the main thread will be blocked for a long time, so that why we need the async to slove this problem.
        /// </summary>
        public static void SumFromOneToCount() 
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
        public static void SumFromOneToCountYield() 
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
        public static async Task SumFromOneToCountAsync() 
        {
            const int count = 5;
            ConsoleExt.WriteLine("async example starting.");
            var result = await Producter.SumFromOneToCountAsync(count);
            ConsoleExt.WriteLine("async Result: " + result);
            ConsoleExt.WriteLine("async completed.");

            ConsoleExt.WriteLine("################################################");
            ConsoleExt.WriteLine(Environment.NewLine);
        }

        /// <summary>
        /// we used async method and will avoid to block the main thread, 
        /// also we can get the every iteration of the value, but it is come from one collection, and this collection will be get at one time.
        /// now we need a new mode: every time the producter create a product, this product should get back to the customer.
        /// </summary>
        public async void SumFromOneToCountTaskIEnumerable() 
        {
            const int count = 5;
            ConsoleExt.WriteLine("SumFromOneToCountAsyncIEnumerable started!");
            var scs = await Producter.SumFromOneToCountTaskIEnumerable(count);
            ConsoleExt.WriteLine("SumFromOneToCountAsyncIEnumerable done!");

            foreach (var sc in scs)
            {
                ConsoleExt.WriteLine($"AsyncIEnumerable Result: {sc}");
            }

            ConsoleExt.WriteLine("################################################");
            ConsoleExt.WriteLine(Environment.NewLine);
        }

        public static async Task SumFromOneToCountAsyncYield() 
        {
            const int count = 5;
            ConsoleExt.WriteLine("Sum with yield starting.");
            await foreach  (var i in Producter.SumFromOneToCountAsyncYield(count))
            {
                  ConsoleExt.WriteLine($"Yield sum: {i}");
            }
            for (int i = 0; i < 9; i++)
            {
                ConsoleExt.WriteLine($"thread id: {System.Threading.Thread.GetCurrentProcessorId()},  current time: {DateTime.Now}");
                Task.Delay(TimeSpan.FromSeconds(1)).Wait();
            }
            ConsoleExt.WriteLine("Sum with yield completed.");

            ConsoleExt.WriteLine("################################################");
            ConsoleExt.WriteLine(Environment.NewLine);
        }


        public static async Task RunAsyncEnumerableDemo()
        {
            const int count = 5;
            ConsoleExt.WriteLine("Starting Async Streams Demo!");

            // Start a new task. Used to produce async sequence of data!
            IAsyncEnumerable<int> pullBasedAsyncSequence = Producter.ProduceAsyncSumSeqeunc(count);

            // Start another task; Used to consume the async data sequence!
            var consumingTask = Task.Run(() => Producter.ConsumeAsyncSumSeqeunc(pullBasedAsyncSequence));

            await Task.Delay(TimeSpan.FromSeconds(3));

            ConsoleExt.WriteLineAsync("X#X#X#X#X#X#X#X#X#X# Doing some other work X#X#X#X#X#X#X#X#X#X#");

            // Just for demo! Wait until the task is finished!
            await consumingTask;

            ConsoleExt.WriteLineAsync("Async Streams Demo Done!");
        }

        public static async Task AsyncStreamsDemo() 
        {
            await foreach (var item in Producter.GetBigData())
            {
                Console.WriteLine($"we need to handle the part of the result: {item}.");
            }
            ConsoleExt.WriteLine("################################################");
            IAsyncEnumerable<int> asyncEnumerableObject = Producter.GetBigData();
            IAsyncEnumerator<int> asyncEnumeratorObject = asyncEnumerableObject.GetAsyncEnumerator();
            while (await asyncEnumeratorObject.MoveNextAsync())
            {
                var i = asyncEnumeratorObject.Current;
                Console.WriteLine($"we need to handle the part of the result: {i}.");
            }
            await asyncEnumeratorObject.DisposeAsync();
        }
    }
}
