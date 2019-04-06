using System;
using System.Collections.Generic;
using System.Text;

namespace FactoryDemo
{
    public class OperationAdd : Operation
    {
        public override double GetResult()
        {
            return NumberA + NumberB;
        }
    }
}
