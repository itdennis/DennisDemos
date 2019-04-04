using ProxyDemo.Proxy;
using ProxyDemo.Target;
using System;

namespace ProxyDemo
{
    class Program
    {
        public static void main(String[] args)
        {
            ProxySubject subject = new ProxySubject(new RealSubject());
            subject.Visit();
        }
    }
}
