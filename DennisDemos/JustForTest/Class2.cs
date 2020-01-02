using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.JustForTest
{
    public class Class2 : Class1
    {
        private bool testB = false;
        public bool TestB => testB;

        public Class2()
        {
            testB = true;
        }
        public void Run() 
        {
            var a = base.MyProperty;
        }
    }
}
