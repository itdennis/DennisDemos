using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.Demos
{
    public class DelegateDemo3 : DemoBase
    {
        public void Show()
        {
            Console.WriteLine("This is show function in delegate demo in advance.");
        }

        public string SayHello(string name)
        {
            Console.WriteLine($"this is say hello function.");
            return $"{name}, hello.";
        }


        public override void Run()
        {
            new Action(() =>
            {
                Show();
                var res = SayHello("XiaoMing");
            }).SafeInvoke();
        }
    }
}
