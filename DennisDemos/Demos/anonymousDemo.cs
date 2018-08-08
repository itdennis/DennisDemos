using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.Demos
{
    public class AnonymousDemo
    {
        delegate void TestEnClosing();

        public void EnclosingTest()
        {
            int a = 5;
            Console.WriteLine("before delegate method, a's value is : {0}", a);
            TestEnClosing x = delegate () { a = 3; Console.WriteLine("delegate method a's value: {0}", a); };
            x();
            Console.WriteLine("after delegate method, a's value is : {0}", a);

        }
    }
}
