using DennisDemos.Demoes;
using DennisDemos.Demoes.Concurrency;
using DennisDemos.Demoes.CSharp_basis;
using DennisDemos.Demoes.Delegate_Demos;
using DennisDemos.Interview;
using DennisDemos.JustForTest;
using DennisDemos.LeetCodes;
using DennisDemos.Utils;
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
            LeetCode448 leet = new LeetCode448();
            leet.FindDisappearedNumbers(new int[] { 4, 3, 2, 7, 8, 2, 3, 1 });
        }
    }
}
