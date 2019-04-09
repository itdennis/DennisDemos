using System;
using System.Collections.Generic;
using System.Text;

namespace BuilderDemo
{
    public class ConcreateBuilderA : Builder
    {
        private Product product = new Product();
        
        public override void BuildPartA()
        {
            this.product.Add("A");
        }

        public override void BuildPartB()
        {
            this.product.Add("B");
        }

        public override Product GetResults()
        {
            return this.product;
        }
    }
}
