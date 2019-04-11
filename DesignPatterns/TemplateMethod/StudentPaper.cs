using System;
using System.Collections.Generic;
using System.Text;

namespace TemplateMethod
{
    public class StudentPaper : TestPaper
    {
        public override string Answer1()
        {
            return "a";
        }        
    }
}
