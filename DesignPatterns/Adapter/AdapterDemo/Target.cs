using System;
using System.Collections.Generic;
using System.Text;

namespace AdapterDemo
{
    /// <summary>
    /// target, 可以为实际的类也可以是抽象类.
    /// </summary>
    public abstract class Target
    {
        public abstract void Request();
        
    }
}
