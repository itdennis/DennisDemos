using System;

namespace TemplateMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            TestPaper studentAPaper = new StudentPaper();
            studentAPaper.TestCase1();
            Console.ReadLine();
        }
    }
}
