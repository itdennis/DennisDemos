using System;
using System.Collections.Generic;
using System.Text;

namespace AccessDemo
{
    public class Success : Action
    {
        public override void GetManConclusion(Man man)
        {
            Console.WriteLine($"{man.GetType().Name} {this.GetType().Name}, his behind will be a success woman.");
        }
        public override void GetWomanConclusion(Woman woman)
        {
            Console.WriteLine($"{woman.GetType().Name} {this.GetType().Name}, his behind will be a failed man.");
        }
    }
}
