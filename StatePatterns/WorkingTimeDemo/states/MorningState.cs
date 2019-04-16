using System;
using System.Collections.Generic;
using System.Text;

namespace StatePatterns
{
    public class MorningState : State
    {
        public override void WriteProgram(Worker worker)
        {
            if (worker.Hour < 12)
            {
                Console.WriteLine("I am XiaoMing, I am working very hard now.");
            }
            else
            {
                worker.SetState(new NoonState());
                worker.WriteProgram();
            }
        }
    }
}
