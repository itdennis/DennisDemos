using System;
using System.Collections.Generic;
using System.Text;

namespace FactoryDemo
{
    public class OperationSub : Operation
    {
        public override double GetResult()
        {
            return NumberA - NumberB;
        }
    }
}
