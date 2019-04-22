using System;
using System.Collections.Generic;
using System.Text;

namespace AccessDemo
{
    public class Woman : Human
    {
        public override void Accept(Action visitor)
        {
            visitor.GetWomanConclusion(this); 
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override void GetConclusion()
        {
            if (Action == "success")
            {
                Console.WriteLine("his behind will be a failed man.");
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
