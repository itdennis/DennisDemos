using System;

namespace FactoryDemo
{
    class Program
    {

        /// <summary>
        /// 这里需要解释一下, 简单工厂模式和工厂模式之间的区别.
        /// 简单工厂模式:
        /// 一句话其实就是简单工厂模式违背了设计模式的原则, 开闭原则.
        /// 如果简单工厂模式中, 需要添加一个Operation Mul, 则需要对OperationFactory类进行修改. 在其中添加一种case.
        /// 
        /// 然而工厂模式中, 
        /// 如果添加了一个算法模式, 比如Mul, 只需要添加一个继承自Operation的类, 实现getResult方法, 在Factorys扩展一个实现了IFactory的类即可.
        /// 符合开闭原则.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            {
                IFactory factory = new AddFactory();
                Operation oper = factory.CreateOperation();
                oper.NumberA = 0.1;
                oper.NumberB = 2;
                var res = oper.GetResult();
            }


            {
                ILeiFengFactory factory = new StudentFactory();
                LeiFeng leiFeng = factory.CreateLeiFeng();
                leiFeng.ChuiBei();
                leiFeng.SaoDi();
            }

        }
    }
}
