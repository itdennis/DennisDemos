using System;
using System.Collections.Generic;
using System.Text;

namespace AdapterDemo
{
    /// <summary>
    /// 是第三方的类, 并且无法通过继承的方式实现`Target`
    /// </summary>
    public class Adaptee
    {
        public void SpecifiedRequest() { }
    }
}
