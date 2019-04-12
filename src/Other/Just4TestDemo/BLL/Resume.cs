using System;
using System.Collections.Generic;
using System.Text;

namespace Just4TestDemo
{
    public class Resume : IResume
    {
        public string Name { get; set; }
        public bool Sex { get; set; }
        public int Age { get; set; }
        public string Experince { get; set; }
        public Resume(string name)
        {
            this.Name = name;
        }
    }
}
