using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DennisCoreDemos.Csharp_8.Async_streams
{
    class Async_Streams
    {
        async IAsyncEnumerable<int> GetBigResultsAsync()
        {
            await foreach (var result in GetResultsAsync())
            {
                if (result > 20) yield return result;
            }
        }

        private async Task<IEnumerable<object>> GetResultsAsync()
        {
            //Task<string[]> result = await File.ReadAllLinesAsync(@"C:\Users\v-yanywu\Desktop\dennis\pic\test.txt", Encoding.UTF8);
            //return result;
            var res = File.ReadAllLines(@"C:\Users\v-yanywu\Desktop\dennis\pic\test.txt", Encoding.UTF8);
            return res.
        }
    }
}
