using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.Demoes.AsyncDemos
{
    public static class DeadlockDemo
    {
        private static async Task DelayAsync()
        {
            await Task.Delay(1000).ConfigureAwait(false);
        }
        // This method causes a deadlock when called in a GUI or ASP.NET context.
        public static void Test()
        {
            // Start the delay.
            var delayTask = DelayAsync();
            // Wait for the delay to complete.
            delayTask.Wait();
        }


    }
}
