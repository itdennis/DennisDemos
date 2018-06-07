using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.Demos
{
    class NullAbleDemo
    {
        public void Run()
        {
            int? x = null;
            //x = 5;
            if (x != null)
            {
                int y = x.Value;
                Console.WriteLine(y);
            }
            int z = x ?? 10;
            Console.WriteLine(z);
        }
    }
}
