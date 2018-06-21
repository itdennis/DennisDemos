using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace DennisDemos.Demos
{
    class ThreadDemo
    {
        public void Run()
        {
            var result = ThreadPool.QueueUserWorkItem(
                state =>
                {
                    Console.WriteLine("Hello from thread pool.");
                });
            Console.WriteLine("main thread does some work, then sleeps.");
            Thread.Sleep(1000);
            Console.WriteLine(result);
        }

    }
}
