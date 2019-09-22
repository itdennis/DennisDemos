using DennisDemos.Demoes;
using DennisDemos.Demoes.Delegate_Demos;
using DennisDemos.Demoes.WeChat;
using DennisDemos.Interview;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DennisDemos
{
    class Program
    {
        static void Main(string[] args)
        {
            //CheckTwoCollectionEqual.Run();
            //CheckTwoCollectionEqual.Demo();
            List<string> lA = new List<string>() { "111", "222", "222", "333" };
            List<string> lB = new List<string>() { "111", "222", "333", "333" };
            bool res = lA.All(lB.Contains) && lA.Count == lB.Count;
            if (res)
            {
                Console.WriteLine("完全一致");
            }
            else
            {
                Console.WriteLine("不完全一致");
            }



        }
    }
}
