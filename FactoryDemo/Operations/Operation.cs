using System;
using System.Collections.Generic;
using System.Text;

namespace FactoryDemo
{
    public class Operation
    {
        public double NumberA { get; set; }
        public double NumberB { get; set; }
        public virtual double GetResult()
        {
            double result = 0;
            return result;
        }
    }
}
