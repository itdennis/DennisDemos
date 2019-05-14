using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.Demoes
{
    class CheckWeekday : DemoBase
    {
        public override void Run()
        {
            DateTime date = DateTime.Now;
            DayOfWeek day = date.DayOfWeek;

            if ((day == DayOfWeek.Saturday) || (day == DayOfWeek.Sunday))
            {
                Console.WriteLine("This is a weekend.");
            }
            else
            {
                Console.WriteLine("Today is a weekday.");
            }
        }
    }
}
