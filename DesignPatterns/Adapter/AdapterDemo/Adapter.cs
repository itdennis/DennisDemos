using System;
using System.Collections.Generic;
using System.Text;

namespace AdapterDemo
{
    /// <summary>
    /// 此时client如果希望调用特殊的第三方类的方法的时候,
    /// 需要一个适配器adapter来重写target中的的方法, 
    /// 再重写的方法里调用第三方的类方法.
    /// </summary>
    public class Adapter : Target
    {
        private Adaptee adaptee = new Adaptee();
        public override void Request()
        {
            adaptee.SpecifiedRequest();
        }
    }
}
