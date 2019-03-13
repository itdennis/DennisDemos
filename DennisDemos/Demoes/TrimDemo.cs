using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.Demoes
{
    class TrimDemo : DemoBase
    {
        public override void Run()
        {

            string s = "/Woodblocks.US.Slot1.compact.dat";
            s = s.TrimStart('/');
            Console.WriteLine(s);
            Console.ReadKey();
        }
    }
}
