using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DennisCoreDemos.Csharp_8.Async_streams
{
    public class Producter
    {
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
    }
}
