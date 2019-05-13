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
using DennisDemos.Demoes.AsyncDemos;

namespace DennisDemos
{
    class Program
    {
        static void Main(string[] args)
        {
            //DemoBase runer;
            //runer.Run();
            //MainAsyncTest.Run().Wait();
            //AsyncDemo asyncDemo = new AsyncDemo();
            //asyncDemo.Run();
            JSONDemo jSONDemo = new JSONDemo();
            jSONDemo.Run();
        }
    }
}
