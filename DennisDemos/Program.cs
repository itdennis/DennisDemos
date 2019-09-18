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

            List<Tuple<DateTime, string>> cache = new List<Tuple<DateTime, string>>();
            for (int i = 0; i < 5; i++)
            {
                var date = DateTime.Now.AddDays(i);
                cache.Add(new Tuple<DateTime, string>(date, date.ToString("yyyy-MM-dd")));
            }
            cache = cache.OrderByDescending(c => c.Item1).ToList();

            var actuallyResults = cache.Select(a => a.Item1.ToString("yyyy-MM-dd")).ToList();
        }


        
    }
}
