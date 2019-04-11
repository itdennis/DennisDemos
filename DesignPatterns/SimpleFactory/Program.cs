using SimpleFactory.LogicLayer;
using System;

namespace SimpleFactory
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("plz input number a:");
            double numberA;
            double.TryParse(Console.ReadLine(), out numberA);
            Console.WriteLine("plz input number b:");
            double numberB;
            double.TryParse(Console.ReadLine(), out numberB);
            Console.WriteLine("plz input a operation like '+', '-'...");
            string input = Console.ReadLine();
            var operationWorker = OperationFactory.CreateOperation(input);
            if (operationWorker == null)
            {
                Console.WriteLine("Invalid input.");
            }
            else
            {
                operationWorker.NumberA = numberA;
                operationWorker.NumberB = numberB;
                Console.WriteLine($"result is {operationWorker.GetResult()}");
            }
        }
    }
}
