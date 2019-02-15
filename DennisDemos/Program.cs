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
            Father ff = new Son();


            
            RegexDemo rd = new RegexDemo();
            rd.Run();

            Console.ReadKey();
        }
    }
}
