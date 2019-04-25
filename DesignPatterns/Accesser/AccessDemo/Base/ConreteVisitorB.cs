using System;
using System.Collections.Generic;
using System.Text;

namespace AccessDemo.Base
{
    public class ConreteVisitorB : Visitor
    {
        public override void VisitConcreteElementA(ConcreteElementA elementA)
        {
            Console.WriteLine($"{elementA.GetType().Name} is accessed by {this.GetType().Name}");
        }

        public override void VisitConcreteElementB(ConcreteElementB elementB)
        {
            Console.WriteLine($"{elementB.GetType().Name} is accessed by {this.GetType().Name}");
        }
    }
}
