using System;
using System.Collections.Generic;
using System.Text;

namespace AccessDemo.Base
{
    class ObjectStucture
    {
        IList<Element> elements = new List<Element>();

        public void Attach(Element element)
        {
            elements.Add(element);
        }

        public void Deattach(Element element)
        {
            elements.Remove(element);
        }

        public void Accept(Visitor visitor)
        {
            foreach (var ele in elements)
            {
                ele.Accept(visitor);
            }
        }
    }
}
