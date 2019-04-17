using System;
using System.Collections.Generic;
using System.Text;

namespace BridgeDemo
{
    public class AbstractionB : Abstraction
    {
        public override void Operation()
        {
            base.implementor.Operation();
        }
    }
}
