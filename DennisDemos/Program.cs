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
        private enum TimeComparison
        {
            Earlier = -1,
            Same = 0,
            Later = 1
        };
        static void Main(string[] args)
        {
            //CheckTwoCollectionEqual.Run();
            //CheckTwoCollectionEqual.Demo();

            {
                DateTimeOffset firstTime = new DateTimeOffset(2019, 9, 15, 8, 59, 59,
                                 new TimeSpan(-7, 0, 0));

                DateTimeOffset secondTime = firstTime;
                Console.WriteLine("Comparing {0} and {1}: {2}",
                                  firstTime, secondTime,
                                  (TimeComparison)firstTime.CompareTo(secondTime));

                secondTime = new DateTimeOffset(2007, 9, 1, 6, 45, 0,
                                 new TimeSpan(-6, 0, 0));
                Console.WriteLine("Comparing {0} and {1}: {2}",
                                 firstTime, secondTime,
                                 (TimeComparison)firstTime.CompareTo(secondTime));

                secondTime = new DateTimeOffset(2007, 9, 1, 8, 45, 0,
                                 new TimeSpan(-5, 0, 0));
                Console.WriteLine("Comparing {0} and {1}: {2}",
                                 firstTime, secondTime,
                                 (TimeComparison)firstTime.CompareTo(secondTime));
            }


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
