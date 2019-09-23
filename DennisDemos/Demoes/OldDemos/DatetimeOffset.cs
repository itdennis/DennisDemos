using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.Demoes
{

    class DatetimeOffset
    {
        private enum TimeComparison
        {
            Earlier = -1,
            Same = 0,
            Later = 1
        };
        public void Run()
        {
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
        }
    }
}
