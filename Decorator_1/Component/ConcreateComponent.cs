using System;
using System.Collections.Generic;
using System.Text;

namespace Decorator.Component
{
    public class ConcreateComponent : IComponent
    {
        private string name;
        public string Name
        {
            get { return this.name; }
            private set { this.name = value; }
        }
        public ConcreateComponent(string name)
        {
            this.Name = name;
        }
        public void Show(string str)
        {
            Console.WriteLine($"my name is {this.Name}, I want to say {str}");
        }
    }
}
