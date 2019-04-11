using System;
using System.Collections.Generic;
using System.Text;

namespace Strategy.LogicLayer
{
    public class Context
    {
        AlgorithmSuper algorithmSuper;
        public Context(string type)
        {
            switch (type)
            {
                case "normal":
                    this.algorithmSuper = new CashNomal();
                    break;
                case "rebate 0.8":
                    this.algorithmSuper = new CashRebate(0.8);
                    break;
                default:
                    break;
            }
            
        }

        public double GetResult(double money)
        {
            return this.algorithmSuper.GetResult(money);
        }
    }
}
