using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DennisDemos.Demos;
using System.Net;
using System.IO;

namespace DennisDemos
{
    class Program
    {
        static void Main(string[] args)
        {
            //DelegateDemo delegateDemo = new DelegateDemo();
            ////delegateDemo.Run();
            //delegateDemo.Run2();
            //WebRequest request = WebRequest.Create("http://www.baidu.com");
            //using (WebResponse response = request.GetResponse())
            //using (Stream responseStream = response.GetResponseStream())
            //using (FileStream output = File.Create("response.dat"))
            //{
            //    responseStream.CopyTo(output);
            //}

            new ManualResetEventDemo().Run();




            Console.ReadKey();
        }
    }
}
