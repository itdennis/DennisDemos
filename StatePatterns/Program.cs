using System;

namespace StatePatterns
{
    class Program
    {
        static void Main(string[] args)
        {
            State state = new ConcreateStateA();
            Context context = new Context(state);
            context.Request();
            context.Request();
            context.Request();
            context.Request();
            context.Request();

            Console.ReadKey();
        }
    }
}
