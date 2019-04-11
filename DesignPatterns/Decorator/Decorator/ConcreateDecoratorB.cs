using System;
using System.Collections.Generic;
using System.Text;

namespace Decorator.Decorator
{
    public class ConcreateDecoratorB : Decorator
    {
        public string RemoveState(string str)
        {
            return $"[RemoveState] this is a Decorator str :{str}.";
        }
        public override void Show(string str)
        {
            base.Show(RemoveState(str));

        }
    }
}
