using System;
using System.Collections.Generic;
using System.Text;

namespace StatePatterns
{
    public class NoonState : State
    {
        public override void WriteProgram(Worker worker)
        {
            if (worker.Hour < 18)
            {
                Console.WriteLine("I am XiaoMing, now is afternoon, i am a little tired, but still working hard.");
            }
            else
            {

            }
        }
    }
}
