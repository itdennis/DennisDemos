using System;
using System.Collections.Generic;
using System.Text;
using Decorator.Component;

namespace Decorator.Decorator
{
    public abstract class Decorator : IComponent
    {
        public void decorator(IComponent component)
        {
            this.component = component;
        }
        protected IComponent component { get; set; }

        public void SetComponent(IComponent component)
        {
            this.component = component;
        }

        public virtual void Show(string str)
        {
            this.component.Show(str);
        }
    }
}
