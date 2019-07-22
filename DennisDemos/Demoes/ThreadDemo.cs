using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace DennisDemos.Demoes
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

            Thread thread = new Thread(new ThreadStart(Run));
            var res = Task.Run<string>(()=> { return GetReturnResult(); });
        }
        static string GetReturnResult()
        {
            Thread.Sleep(2000);
            return "我是返回值";
        }
    }
}
