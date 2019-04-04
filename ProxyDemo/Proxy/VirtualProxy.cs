using ProxyDemo.Interface;
using ProxyDemo.Target;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProxyDemo.Proxy
{
    /// <summary>
    /// 通过参数来实例化一个subject,而不是直接将subject通过参数传递.
    /// </summary>
    public class VirtualProxy : ISubject
    {
        private string name;
        private ISubject subject;
        public VirtualProxy() { }

        public VirtualProxy(string name)
        {
            this.name = name;
        }
        
        public void Visit()
        {
            subject = new RealSubject(this.name);
            subject.Visit();
        }
    }
}
