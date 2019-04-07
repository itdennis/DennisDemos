using System;
using System.Collections.Generic;
using System.Text;

namespace TemplateMethod
{
    public class ConcreateClass : AbstractClass
    {
        public override void Test1()
        {
            throw new NotImplementedException();
        }

        public override void Test2()
        {
            throw new NotImplementedException();
        }
    }
}
