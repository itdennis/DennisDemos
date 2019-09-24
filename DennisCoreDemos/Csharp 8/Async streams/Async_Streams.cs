using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisCoreDemos.Csharp_8.Async_streams
{
    public  class Async_Streams
    {
        static int SumFromOneToCount(int count)
        {
            ConsoleExt.WriteLine("SumFromOneToCount called!");

            var sum = 0;
            for (var i = 0; i <= count; i++)
            {
                sum = sum + i;
            }
            return sum;
        }
    }


}
