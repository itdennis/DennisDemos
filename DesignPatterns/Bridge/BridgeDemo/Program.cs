using System;

namespace BridgeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Abstraction abstraction = new AbstractionA();
            abstraction.SetImplementor(new ConcreteImplementorA());
            abstraction.Operation();
        }
    }
}
