using System;
using System.Collections.Generic;
using System.Text;

namespace StatePatterns
{
    public class Worker
    {

        public Worker()
        {
            //this.currentState = state;
        }

        private State currentState;
        public State State { get => this.currentState; set => currentState = value; }

        private int hour;
        public int Hour { get => this.hour; set => this.hour = value; }

        private bool finished = false;
        public bool Finished { get => this.finished; set => this.finished = value; }

        public void WriteProgram()
        {
            currentState.WriteProgram(this);
        }

        public void SetState(State targetState)
        {
            currentState = targetState;
        }
    }
}
