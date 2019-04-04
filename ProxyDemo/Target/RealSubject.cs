using ProxyDemo.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProxyDemo.Target
{
    
    public class RealSubject : ISubject
    {
        private string name = "";

        public RealSubject() { }

        /// <summary>
        /// for virtual proxy mode
        /// </summary>
        /// <param name="name"></param>
        public RealSubject(string name)
        {
            this.name = name;
        }

        public void Visit()
        {
            Console.WriteLine(name);
        }
    }
}
