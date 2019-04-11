using Strategy.LogicLayer;
using System;

namespace Strategy
{
    class Program
    {
        /// <summary>
        /// 这种方式对于客户端来说, 只暴露了context类, 对于算法核心部分根本不用了解.
        /// 很好的解耦了client, context, algorithm三个类.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            Run.Runner();
        }
    }
}
