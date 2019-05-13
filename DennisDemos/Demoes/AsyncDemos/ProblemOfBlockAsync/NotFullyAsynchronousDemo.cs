using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DennisDemos.Demoes.AsyncDemos
{
    public static class NotFullyAsynchronousDemo
    {
        // This method synchronously blocks a thread.
        public static async Task TestNotFullyAsync()
        {
            await Task.Yield();
            Thread.Sleep(5000);
        }
    }
}
