using System;
using System.Collections.Generic;
using System.Text;

namespace StatePatterns
{
    public class Context
    {
        private State state;
        public Context(State state)
        {
            this.state = state;
        }

        public State State
        {
            get => this.state;
            set
            {
                state = value;
                Console.WriteLine($"current state is {state.GetType().Name}");
            }
        }

        public void Request()
        {
            state.Handel(this);
        }
    }
}
