using System;
using System.Collections.Generic;
using System.Text;

namespace BridgeDemo
{
    public abstract class Abstraction
    {
        public Implementor implementor;
        public void SetImplementor(Implementor i)
        {
            implementor = i;
        }

        public abstract void Operation();
    }
}
