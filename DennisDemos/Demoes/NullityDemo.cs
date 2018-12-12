using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.Demos
{
    class NullityDemo
    {
        /// <summary>
        /// system.nullable<t> 
        /// 语法糖?
        /// </summary>
        public void Run()
        {
            Nullable<int> xx = 5;
            xx = new Nullable<int>(5);
            Console.WriteLine("Instance with value:");
            Display(xx);
            xx = new Nullable<int>();
            Display(xx);

            int? x = null;
            //x = 5;
            if (x != null)
            {
                int y = x.Value;
                Console.WriteLine(y);
            }
            int z = x ?? 10;
            Console.WriteLine(z);
            System.Nullable<int> s = null;
            if (s.HasValue)
            {
                Console.WriteLine(s.Value); 
            }

            System.Nullable<DateTime> dateTime = null;
        }

        static void Display(Nullable<int> x)
        {
            Console.WriteLine("has value: {0}", x.HasValue);
            if (x.HasValue)
            {
                Console.WriteLine("Value: {0}", x.Value);
                Console.WriteLine("Explict conversion: {0}", (int)x);
            }
        }
    }
}
