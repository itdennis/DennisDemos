using System;
using System.Collections.Generic;
using System.Text;

namespace AccessDemo.Base
{
    public class Client
    {
        public static void Run()
        {
            ObjectStucture o = new ObjectStucture();
            o.Attach(new ConcreteElementA());
            o.Attach(new ConcreteElementB());

            ConcreteVisitorA visitorA = new ConcreteVisitorA();
            ConreteVisitorB visitorB = new ConreteVisitorB();

            o.Accept(visitorA);
            o.Accept(visitorB);

            Console.ReadKey();
        }
    }
}
