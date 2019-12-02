using DennisDemos.Demoes;
using DennisDemos.Demoes.CSharp_basis;
using DennisDemos.Demoes.Delegate_Demos;
using DennisDemos.Demoes.WeChat;
using DennisDemos.Interview;
using DennisDemos.JustForTest;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DennisDemos
{
    class Program
    {
        static void Main(string[] args)
        {
            var res = Math.Round(((decimal)1 / 12), 2);
            //Ref_Out.Out_demo();
            Solution solution = new Solution();
            //solution.Reverse(-2147483648); 
            solution.Reverse(1563847412);

        }
    }
}
