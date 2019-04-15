using System;
using System.Collections.Generic;
using System.Text;

namespace StatePatterns
{
    public class ConcreateStateB : State
    {
        public override void Handel(Context context)
        {
            context.State = new ConcreateStateA();
        }
    }
}
