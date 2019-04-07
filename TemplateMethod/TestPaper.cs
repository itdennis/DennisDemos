using System;
using System.Collections.Generic;
using System.Text;

namespace TemplateMethod
{
    public class TestPaper
    {
        public void TestCase1()
        {
            Console.WriteLine("apple, banana, pear?a.banana b.apple c.pear d.others");
            Console.WriteLine($"the answer is: {Answer1()}");
        }

        public virtual string Answer1()
        {
            return "";
        }
    }
}
