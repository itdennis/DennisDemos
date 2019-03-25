using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DennisDemos.Demos;
using System.Net;
using System.IO;
using DennisDemos.Demoes;
using DennisDemos.Utils;

namespace DennisDemos
{
    class Program
    {
        static void Main(string[] args)
        {
            //ExecuteRectangle.Run();
            //SimpleDelegateUse.Run();


            Father ff = new Son();


            DemoBase runer;


            //runer = new RegexDemo();

            //runer = new TrimDemo();

            //runer = new JSONDemo();

            //runer = new OtherDemo();

            //runer = new Converter_demo();

            runer = new DateTimeDemo();


            runer.Run();

            Console.ReadKey();
        }
    }
}
