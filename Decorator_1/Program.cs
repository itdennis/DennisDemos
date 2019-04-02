using Decorator.Component;
using Decorator.Decorator;
using System;

namespace Decorator
{
    class Program
    {
        static void Main(string[] args)
        {
            IComponent component = new ConcreateDecoratorA(new ConcreateComponent("XiaoMing"));
            //ConcreateDecoratorA decoratorA = new ConcreateDecoratorA();
            //decoratorA.SetComponent(component);
            component.Show("decorator demo.");
            Console.ReadLine();
        }
    }
}
