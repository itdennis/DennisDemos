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
            Thread thread1 = new Thread(new ThreadStart(() => { Console.WriteLine("this is threadstart delegate"); }));
            thread1.Start();


            Thread thread2 = new Thread(new ParameterizedThreadStart((a) => { Console.WriteLine($"this is ParameterizedThreadStart delegate, {a}"); }));
            thread2.Start("canshu");

            Task task = new Task(() => { Console.WriteLine("this is task's action parameter delegate."); });
            task.Start();
            task.ConfigureAwait(false);

            List<Task> tasks = new List<Task>();

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
