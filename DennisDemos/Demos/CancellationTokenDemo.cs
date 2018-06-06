using System;
using System.Threading;
using System.Threading.Tasks;

namespace DennisDemos.Demos
{
    class CancellationTokenDemo
    {
        /// <summary>
        /// 代码在控制台程序中或从线程池线程调用时，均可运作良好。
        /// 但如果在Windows Forms UI线程（或其他单线程同步上下文）上执行这段代码，则会造成死锁。
        /// 想想在延迟任务完成时，DelayFor30Seconds方法会试图返回到哪个线程上？再想想task.Wait()调用运行在哪个线程上？
        /// 从根本上来说，问题在于调用了Wait()方法或Result属性。在相关任务完成前，二者均可阻塞线程。
        /// 我们应该总是使用await，来异步地等待任务的结果。
        /// </summary>
        public void Run()
        {
            var source = new CancellationTokenSource();
            var task = DelayFor30Seconds(source.Token); //❷ 调用异步方法
            source.CancelAfter(TimeSpan.FromSeconds(1));
            Console.WriteLine("Initail status : {0}", task.Status); //❸ 请求延迟的token取消操作
            try
            {
                task.Wait(); //❹ 等待完成（同步）可能会造成死锁.
            }
            catch (AggregateException e)
            {
                Console.WriteLine("Caught {0}", e.InnerExceptions[0]);
            }
        }
        static async Task DelayFor30Seconds(CancellationToken token)
        {
            Console.WriteLine("Waiting for 30 seconds...");
            await Task.Delay(TimeSpan.FromSeconds(30), token);   //❶ 启动一个异步的延迟操作
        }
    }
}
