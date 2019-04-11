using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DennisDemos.DemosHelper;

namespace DennisDemos.Demos
{
    public delegate void EventHandler();
    public class DelegateDemo : DemoBase
    {
        public event EventHandler Update;

        public override void Run()
        {
            Update += new EventHandler(UpLoadFile2Server);
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            //使用invoke会阻塞主线程. 所以在调用invoke之后会直接等待方法执行结束. 
            Update.Invoke();
            stopWatch.Stop();
            Console.WriteLine($"Invoke use time {stopWatch.Elapsed.TotalSeconds}");

            //使用BeginInvoke不会阻塞主线程. 主线程可以继续做自己的事情.
            stopWatch.Restart();
            AsyncCallback callback = (ar) => 
            {
                Stopwatch temp = (Stopwatch)ar.AsyncState;
                Update.EndInvoke(ar);
                temp.Stop();
                Console.WriteLine($"BeginInvoke use time {temp.Elapsed.TotalSeconds}");
            };
            IAsyncResult asyncResult =  Update.BeginInvoke(callback, stopWatch);

            Console.WriteLine("do something else....");
            Console.WriteLine("do something else....");
            Console.WriteLine("do something else....");
            Console.WriteLine("do something else....");
            Console.WriteLine("do something else....");

            asyncResult.AsyncWaitHandle.WaitOne();
            Console.WriteLine($"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} is finished at {DateTime.UtcNow}");
        }

        public void UpLoadFile2Server()
        {
            Console.WriteLine($"Uploading file 2 server by thread id:{Thread.CurrentThread.Name}.");
            Thread.Sleep(1000 * 10);
        }
    }
}
