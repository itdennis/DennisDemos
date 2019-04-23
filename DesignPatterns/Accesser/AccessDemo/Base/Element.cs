using System;
using System.Collections.Generic;
using System.Text;

namespace AccessDemo.Base
{
    public abstract class Element
    {
        public abstract void Accept(Visitor visitor);
    }
}
