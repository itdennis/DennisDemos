using ProxyDemo.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProxyDemo.Target
{
    
    public class RealSubject : Subject
    {
        private string name = "Xiong2";
        public void Visit()
        {
            Console.WriteLine(name);
        }
    }
}
