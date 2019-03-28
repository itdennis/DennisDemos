using System;
using System.Collections.Generic;
using System.Text;

namespace Strategy.LogicLayer
{
    public class CashNomal : AlgorithmSuper
    {
        public override double GetResult(double money)
        {
            return money;
        }
    }
}
