using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DennisCoreDemos.Csharp_8.Async_streams
{
    public class Producter
    {
        /// <summary>
        /// simple foreach then return the result
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
       public  static int SumFromOneToCount(int count)
        {
            ConsoleExt.WriteLine("SumFromOneToCount called!");

            var sum = 0;
            for (var i = 0; i <= count; i++)
            {
                sum = sum + i;
            }
            return sum;
        }

        /// <summary>
        /// simple foreach and yield return the result
        /// the caller can only get one result object is sum
        /// but will get the every iteration sum vaule because of yield statement
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
       public static IEnumerable<int> SumFromOneToCountYield(int count)
        {
            ConsoleExt.WriteLine("SumFromOneToCountYield called!");

            var sum = 0;
            for (var i = 0; i <= count; i++)
            {
                sum = sum + i;

                yield return sum;
            }
        }

        /// <summary>
        /// async to get the sum value after a long foreach logic.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static async Task<int> SumFromOneToCountAsync(int count)
        {
            ConsoleExt.WriteLine("SumFromOneToCountAsync called!");
            var result = await Task.Run(() =>
            {
                var sum = 0;
                for (var i = 0; i <= count; i++)
                {
                    Thread.Sleep(3000);
                    sum = sum + i;
                }
                return sum;
            });
            return result;
        }

        /// <summary>
        /// async method to get the every iteration of the sum value
        /// because all the iteration's sum value was added into a collection.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<int>> SumFromOneToCountTaskIEnumerable(int count)
        {
            ConsoleExt.WriteLine("SumFromOneToCountAsyncIEnumerable called!");
            var collection = new Collection<int>();

            var result = await Task.Run(() =>
            {
                var sum = 0;

                for (var i = 0; i <= count; i++)
                {
                    sum = sum + i;
                    collection.Add(sum);
                }
                return collection;
            });

            return result;
        }
        /// <summary>
        /// 1. make this method to be an async method.
        /// 2. create task.delay to intent a long work for get the sum result.
        /// 3. use yield return to return the temp sum value to customer.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public async static IAsyncEnumerable<int> SumFromOneToCountAsyncYield(int count)
        {
            ConsoleExt.WriteLine("SumFromOneToCountYield called!");
            var sum = 0;
            for (var i = 0; i <= count; i++)
            {
                ConsoleExt.WriteLine($"thread id: {System.Threading.Thread.GetCurrentProcessorId()},  current time: {DateTime.Now}");
                sum = sum + i;
                await Task.Delay(TimeSpan.FromSeconds(2));
                yield return sum;
            }
        }

        /// <summary>
        /// 1. you need to know the yield return's usage
        /// 2. this method will be executed when the caller use the result.
        /// </summary>
        /// <returns></returns>
        public static async IAsyncEnumerable<int> GetBigData() 
        {
            var r = new Random();
            while (true)
            {
                var res = r.Next(300);
                await Task.Delay(res);
                yield return res;
            }
        }

        public static async Task ConsumeAsyncSumSeqeunc(IAsyncEnumerable<int> sequence)
        {
            ConsoleExt.WriteLineAsync("ConsumeAsyncSumSeqeunc Called");

            await foreach (var value in sequence)
            {
                ConsoleExt.WriteLineAsync($"Consuming the value: {value}");

                // simulate some delay!
                await Task.Delay(TimeSpan.FromSeconds(1));
            };
        }

        public static async IAsyncEnumerable<int> ProduceAsyncSumSeqeunc(int count)
        {
            ConsoleExt.WriteLineAsync("ProduceAsyncSumSeqeunc Called");
            var sum = 0;

            for (var i = 0; i <= count; i++)
            {
                sum = sum + i;

                // simulate some delay!
                await Task.Delay(TimeSpan.FromSeconds(0.5));

                yield return sum;
            }
        }
    }
}
