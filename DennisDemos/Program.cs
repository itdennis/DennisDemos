using DennisDemos.Demoes;
using DennisDemos.Demoes.Concurrency;
using DennisDemos.Demoes.CSharp_basis;
using DennisDemos.Demoes.Delegate_Demos;
using DennisDemos.Interview;
using DennisDemos.JustForTest;
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
            //var task = Github.GetRepo("itdennis", "dennis-pics", "5a084fcbe76dfb51752a9222bac085d32cdb4c00");
            //task.Wait();
            //var dir = task.Result;

            //var task = BearerToken.GetBearerToken("https://microsoft.onmicrosoft.com/flighter");
            //task.Wait();
            //var res = task.Result;

            string referrer = "WoodblocksV4.US.Slot18.newbee.dat";


            referrer = referrer.Substring(0, referrer.Substring(0, referrer.LastIndexOf(".")).LastIndexOf("."));



            var task = AzureDevOpsServices.GetProjects();

            task.Wait();
        }
    }
}
