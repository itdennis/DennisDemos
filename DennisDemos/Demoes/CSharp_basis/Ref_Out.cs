using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.Demoes.CSharp_basis
{
    class Ref_Out
    {
        public static void Ref_demo()
        {
            string errMsg = string.Empty; // ref parameter need to be initialized at the beginning.
            Ref_Run(ref errMsg);
            Console.WriteLine(errMsg);
        }

        public static void Ref_Run(ref string errMsg)
        {
            errMsg = "this is ref demo in C# basis";
        }

        public static void Out_demo()
        {
            string errMsg = string.Empty; // out parameter not required to be initialized at the beginning.
            Out_Run(out errMsg);
            Console.WriteLine(errMsg);
        }

        public static void Out_Run(out string errMsg)
        {
            errMsg = "this is out demo in C# basis";
        }
    }
}
