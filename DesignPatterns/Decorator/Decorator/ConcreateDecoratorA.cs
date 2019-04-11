using Decorator.Component;
using System;
using System.Collections.Generic;
using System.Text;

namespace Decorator.Decorator
{
    class ConcreateDecoratorA : Decorator
    {
        //public ConcreateDecoratorA(IComponent component) : base(component) { }
        public string AddState(string str)
        {
            return $"this is a decorated str :{str}.";
        }
        public override void Show(string str)
        {
            base.Show(AddState(str));
            
        }
    }
}
