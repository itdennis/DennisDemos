using System;
using System.Collections.Generic;
using System.Text;

namespace AccessDemo
{
    public class Man : Human
    {
        public override void Accept(Action visitor)
        {
            visitor.GetManConclusion(this);
        }

        public override void GetConclusion()
        {
            if (Action == "success")
            {
                Console.WriteLine("his behind will be a success woman.");
            }
        }
    }
}
