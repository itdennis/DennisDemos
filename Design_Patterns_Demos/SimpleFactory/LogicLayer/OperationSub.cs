using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleFactory.LogicLayer
{
    public class OperationSub : Operation
    {
        public override double GetResult()
        {
            return NumberA - NumberB;
        }
    }
}
