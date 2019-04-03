using Decorator.Component;
using Decorator.Decorator;
using System;

namespace Decorator
{
    class Program
    {
        /// <summary>
        /// （1）需要扩展一个类的功能、或者给一个类添加附加的职责时。
        /// （2）需要动态的给一个对象添加功能，这些功能可以再动态的撤销时。
        /// （3）需要增加由一些基本功能的排列组合而产生的非常大量的功能，从而使继承关系变得不现实。
        /// （4）当不能采用生成子类的方式进行扩充时。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            IComponent component = new ConcreateComponent("XiaoMing");
            ConcreateDecoratorA decoratorA = new ConcreateDecoratorA();
            decoratorA.decorator(component);

            ConcreateDecoratorB decoratorB = new ConcreateDecoratorB();
            decoratorB.decorator(decoratorA);

            decoratorB.Show("test");

            //IComponent component = new ConcreateDecoratorA(new ConcreateComponent("XiaoMing"));
            //ConcreateDecoratorA decoratorA = new ConcreateDecoratorA();
            //decoratorA.SetComponent(component);
            component.Show("decorator demo.");
            Console.ReadLine();
        }
    }
}
