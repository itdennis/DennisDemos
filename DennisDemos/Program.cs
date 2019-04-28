using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DennisDemos.Demoes;
using System.Net;
using System.IO;
using DennisDemos.Demoes;
using DennisDemos.Utils;
using DennisDemos.Demoes.XiaoJuHua;

namespace DennisDemos
{
    class Program
    {
        static void Main(string[] args)
        {
            DemoBase runer;
            //ExecuteRectangle.Run();
            //SimpleDelegateUse.Run();
            //Father ff = new Son();
            //runer = new RegexDemo();
            //runer = new TrimDemo();
            //runer = new JSONDemo();
            //runer = new OtherDemo();
            //runer = new Converter_demo();
            //runer = new DateTimeDemo();
            //runer = new DelegateDemo();
            //runer.Run();
            //Console.ReadKey();
            runer = new SequenceEqualDemo();

            runer.Run();
        }
    }
}
