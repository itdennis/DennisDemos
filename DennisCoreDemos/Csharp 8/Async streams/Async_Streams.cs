using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisCoreDemos.Csharp_8.Async_streams
{
    public class Async_Streams
    {
        public interface IAsyncEnumerable<out T>
        {
            IAsyncEnumerator<T> GetAsyncEnumerator();
        }
        public interface IAsyncEnumerator<out T> : IAsyncDisposable
        {
            Task<bool> MoveNextAsync();
            T Current { get; }
        }
        // Async Streams Feature 可以被异步销毁 
        public interface IAsyncDisposable
        {
            Task DiskposeAsync();
        }
    }


}
