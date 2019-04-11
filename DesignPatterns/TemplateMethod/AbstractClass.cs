using System;
using System.Collections.Generic;
using System.Text;

namespace TemplateMethod
{
    public abstract class AbstractClass
    {
        public abstract void Test1();
        public abstract void Test2();

        public void TemplateMethod()
        {
            Test1();
            Test2();
            Console.WriteLine("...");
        }
    }
}
