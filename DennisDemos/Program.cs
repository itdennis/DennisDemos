﻿using DennisDemos.Demoes;
using DennisDemos.Demoes.Concurrency;
using DennisDemos.Demoes.CSharp_basis;
using DennisDemos.Demoes.Delegate_Demos;
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
            Class2 class2 = new Class2();
            if (class2.TestB)
            {

            }

            //ConcurrentDictionaryDemo.Run();
            JSONDemo demo = new JSONDemo();
            demo.Run();
        }
    }
}
