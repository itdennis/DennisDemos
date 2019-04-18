using System;
using System.Collections.Generic;
using System.Text;

namespace DutyChainDemo
{
    class Employee : AbstractEmployee
    {
        public override TitleLevel Level { get ; set ; }
        public override AbstractEmployee Manager { get; set; }
        public override string Name { get ; set ; }

        public Employee(string name, TitleLevel titleLevel)
        {
            Name = name;
            Level = titleLevel;
            Manager = ManagerArchitecture.SetManager(Level);
        }

        public override void CheckRequest(Request request)
        {
            Console.Write($"Hello, I am {Name}.");
            if (TitlePermissionMap.titlePermissionMapping[Level].Contains(request.RequestType))
            {
                Console.WriteLine("I have permission to deal with this request.");
            }
            else
            {
                Console.WriteLine("I donnot have permission to do with this request, i need to send this request to my manager.");
                Manager.CheckRequest(request);
            }
        }
    }
}
