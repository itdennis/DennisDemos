using ProxyDemo.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProxyDemo.Proxy
{
    public class ProxySubject : Subject
    {
        private Subject subject;

        public ProxySubject(Subject subject)
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
