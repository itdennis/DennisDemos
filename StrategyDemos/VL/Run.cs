using Strategy.LogicLayer;
using System;

namespace Strategy
{
    public class Run
    {
        /// <summary>
        /// 这种方式对于客户端来说, 只暴露了context类, 对于算法核心部分根本不用了解.
        /// 很好的解耦了client, context, algorithm三个类.
        /// </summary>
        /// <param name="args"></param>
        public static void Runner()
        {
            
            Context context = new Context("normal");
            
            Console.WriteLine(context.GetResult(1000));
        }
    }
}
