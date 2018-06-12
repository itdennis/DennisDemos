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
            delegateDemo.Run2();

            //APMDemo test = new APMDemo();
            //test.testAwait1();

            //ReflectDemo reflectDemo = new ReflectDemo();
            //reflectDemo.Run();

            //AsyncDemo test = new AsyncDemo();
            //test.Run();

            //LambdaDemo ld = new LambdaDemo();
            //ld.Run();

            //NullityDemo na = new NullityDemo();
            //na.Run();

            //CancellationTokenDemo ctTest = new CancellationTokenDemo();
            //ctTest.Run();
            //YieldDemo ydDemo = new YieldDemo();
            //ydDemo.Run();


            Console.ReadKey();
        }
    }
}
