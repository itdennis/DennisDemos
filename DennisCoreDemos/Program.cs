using DennisCoreDemos.Csharp_8.Async_streams;
using System;
using static System.Console;

namespace DennisCoreDemos
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Hello World!");

            Async_Streams worker = new Async_Streams();
            var result = worker.GetBigResultsAsync().GetAsyncEnumerator();
            Console.WriteLine(result.Current);
        }
    }
}
