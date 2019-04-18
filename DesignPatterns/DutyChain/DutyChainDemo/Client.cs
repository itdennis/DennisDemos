using System;

namespace DutyChainDemo
{
    class Client
    {
        static void Main(string[] args)
        {
            Employee workerA = new Employee("XiaoMing",TitleLevel.worker);
            workerA.CheckRequest(new Request()
            {
                RequestType = RequestType.RaiseSalary,
                RequestContent = "I need to raise salary.",
                Nunber = 1
            });

            Console.ReadKey();
        }
    }
}
