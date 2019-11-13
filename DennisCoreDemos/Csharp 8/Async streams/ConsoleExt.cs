using System;
using System.Collections.Generic;
using System.Text;

namespace DennisCoreDemos.Csharp_8.Async_streams
{
    public static class ConsoleExt
    {
        public static void WriteLine(string str)
        {
            Console.WriteLine($"Time: {DateTime.Now}, thread: {System.Threading.Thread.GetCurrentProcessorId()} : {str}");
        }

        public async static void WriteLineAsync(string str) 
        {
            Action action = new Action(() => { Console.WriteLine(str); });
            //Action<string> action = new Action<string>((str => { Console.WriteLine(str); }));
            var consoleWriter =  new System.Threading.Tasks.Task(action);
            await consoleWriter.ConfigureAwait(false);
        }
    }
}
