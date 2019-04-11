using System;

namespace BuilderDemo
{
    /// <summary>
    /// 建造者模式
    /// 建造者模式的目的是抽象一个builder类, 专门用于维护生产product的核心代码. 
    /// 然后对于不同的业务场景, 需要实现不同的concreate builder类继承自builder.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            BuilderDrictor drictor = new BuilderDrictor();
            Builder builder1 = new ConcreateBuilderA();
            drictor.Construct(builder1);
            Product product = builder1.GetResults();
            product.Show();
        }
    }
}
