using System;
using System.Collections.Generic;
using System.Text;

namespace BridgeDemo
{
    public class ConcreteImplementorA : Implementor
    {
        public override void Operation()
        {
            Console.WriteLine("this is operation in ConcreteImplementor A.");
        }
    }
}
