using System;
using System.Collections.Generic;
using System.Text;

namespace BuilderDemo
{
    public class BuilderDrictor
    {
        public void Construct(Builder builder)
        {
            builder.BuildPartA();
            builder.BuildPartB();
        }
    }
}
