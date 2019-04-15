using System;
using System.Collections.Generic;
using System.Text;

namespace StatePatterns
{
    public class ConcreateStateA : State
    {
        public override void Handel(Context context)
        {
            context.State = new ConcreateStateB();
        }
    }
}
