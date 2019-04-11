using System;
using System.Collections.Generic;
using System.Text;

namespace Strategy.LogicLayer
{
    public class CashRebate : AlgorithmSuper
    {
        private double moneyRebate = 1d;

        public CashRebate(double rebate)
        {
            this.moneyRebate = rebate;
        }
        public override double GetResult(double money)
        {
            return money * this.moneyRebate;
        }
    }
}
