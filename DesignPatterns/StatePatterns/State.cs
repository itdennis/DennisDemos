using System;
using System.Collections.Generic;
using System.Text;

namespace StatePatterns
{
    public abstract class State
    {
        public abstract void WriteProgram(Worker context);
    }
}
