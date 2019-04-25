using System;
using System.Collections.Generic;
using System.Text;

namespace AccessDemo.Base
{
    public class ConcreteElementB : Element
    {
        public override void Accept(Visitor visitor)
        {
            visitor.VisitConcreteElementB(this);
        }
    }
}
