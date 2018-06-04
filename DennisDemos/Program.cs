using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DennisDemos.Demos;

namespace DennisDemos
{
    class Program
    {
        static void Main(string[] args)
        {
            DelegateDemo delegateDemo = new DelegateDemo();
            //delegateDemo.Run();
            delegateDemo.RunForFunc();

            //ReflectDemo reflectDemo = new ReflectDemo();
            //reflectDemo.Run();
            Console.ReadKey();
        }
    }
}
