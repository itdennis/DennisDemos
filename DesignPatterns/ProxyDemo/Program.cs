using ProxyDemo.Proxy;
using ProxyDemo.Target;
using System;

namespace ProxyDemo
{
    class Program
    {
        public static void Main(String[] args)
        {
            //ProxySubject subject = new ProxySubject(new RealSubject());
            VirtualProxy subject = new VirtualProxy("XiaoMing");

            subject.Visit();
        }
    }
}
