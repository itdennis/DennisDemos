using System;

namespace StatePatterns
{
    class Program
    {
        static void Main(string[] args)
        {
            Worker worker = new Worker();
            worker.Hour = 9;
            worker.WriteProgram();

            Console.ReadKey();
        }
    }
}
