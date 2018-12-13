using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DennisDemos.Demos;
using System.Net;
using System.IO;
using DennisDemos.Utils;

namespace DennisDemos
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("plz input a code to excute......"); //string.Format(\"Current time is: {0}\", DateTime.Now)
            string customerInput =  Console.ReadLine();
            var inputBeforeConvert = customerInput;
            var inputAfterConvert = CodeConvertor.Convertor(customerInput);





            GenericDemo genericDemo = new GenericDemo();
            genericDemo.Run();



            Console.ReadKey();
        }
    }
}
