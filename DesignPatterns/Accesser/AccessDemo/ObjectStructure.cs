using System;
using System.Collections.Generic;
using System.Text;

namespace AccessDemo
{
    public class ObjectStructure
    {
        private IList<Human> humen = new List<Human>();
        public void Add(Human human)
        {
            this.humen.Add(human);
        }
        public void Delete(Human human)
        {
            this.humen.Remove(human);
        }

        public void Display(Action visitor)
        {
            foreach (var item in humen)
            {
                item.Accept(visitor);
            }
        }
    }
}
