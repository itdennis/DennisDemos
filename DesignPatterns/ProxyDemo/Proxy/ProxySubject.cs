using ProxyDemo.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProxyDemo.Proxy
{
    public class ProxySubject : ISubject
    {
        private ISubject subject;

        public ProxySubject(ISubject subject)
        {
            this.subject = subject;
        }
        public void Visit()
        {
            Console.WriteLine("do some other operations.");
            subject.Visit();
        }
    }
}
