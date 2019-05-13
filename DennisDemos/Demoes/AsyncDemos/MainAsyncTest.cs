using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DennisDemos.Demoes.AsyncDemos
{
    public class MainAsyncTest
    {
        public static void Run()
        {
            try
            {
                var dispatcher = new Dictionary<string, Func<Task>>
                {
                    { "a", MethodA },
                    { "b", MethodB },
                    { "c", MethodC }
                };
                List<Task> tasks = new List<Task>()
                {
                    dispatcher["a"](),
                    dispatcher["b"](),
                    dispatcher["c"]()
                };

                var res = Task.WhenAll(tasks);

                while (!res.IsCompleted)
                {
                    Console.WriteLine($"Current program is running for {DateTime.Now}");
                    Thread.Sleep(1000);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private async static Task MethodA()
        {
            var task = Task.Delay(1000 * 30);
            await Task.WhenAny(task);
            Console.WriteLine("MethodA finshed.");
        }

        private async static Task MethodB()
        {
            var task = Task.Delay(1000 * 10);
            await Task.WhenAny(task);
            Console.WriteLine("MethodB finshed.");
        }

        private async static Task MethodC()
        {
            var task = Task.Delay(1000 * 20);
            await Task.WhenAny(task);
            Console.WriteLine("MethodC finshed.");
        }

        
    }
}
