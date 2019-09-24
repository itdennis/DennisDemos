using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
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
    }
}
