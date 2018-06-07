using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.Demos
{
    class GenericityDemo
    {
        public void Run()
        {
            List<int> intergers = new List<int>();
            intergers.Add(1);
            intergers.Add(2);
            intergers.Add(3);
            intergers.Add(4);
            intergers.Add(5);
            Converter<int, double> converter = TakeSquareRoot;
        }

        static double TakeSquareRoot(int x)
        {
            return Math.Sqrt(x);
        }
    }
}
