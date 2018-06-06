using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.Demos
{
    class LambdaDemo
    {
        public void Run()
        {
            Func<int> func = () => { return 1; };
        }
    }
}
