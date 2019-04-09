using System;
using System.Collections.Generic;
using System.Text;

namespace BuilderDemo
{
    public class Product
    {
        IList<string> parts = new List<string>();
        public void Add(string part)
        {
            parts.Add(part);
        }
        public void Show()
        {
            foreach (var item in parts)
            {
                Console.WriteLine(item);
            }
        }
    }
}
