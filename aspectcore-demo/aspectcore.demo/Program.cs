using AspectCore.DynamicProxy;
using System;

namespace aspectcore.demo
{
    class Program
    {
        static void Main(string[] args)
        {
            ProxyGeneratorBuilder proxyGeneratorBuilder = new ProxyGeneratorBuilder();
            using (IProxyGenerator proxyGenerator = proxyGeneratorBuilder.Build())
            {
                Person p = proxyGenerator.CreateClassProxy<Person>();
                p.Say("denniswu.cnblogs.com");
            }
            Console.ReadKey();
        }
    }
}
