using System;
using System.Collections.Generic;
using System.Text;

namespace AccessDemo.Base
{
    public abstract class Visitor
    {
        public abstract void VisitConcreteElementA(ConcreteElementA elementA);
        public abstract void VisitConcreteElementB(ConcreteElementB elementB);
    }
}
