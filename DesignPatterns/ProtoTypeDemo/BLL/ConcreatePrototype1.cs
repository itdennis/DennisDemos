using System;
using System.Collections.Generic;
using System.Text;

namespace ProtoTypeDemo
{
    public class ConcreatePrototype1 : Prototype
    {
        public ConcreatePrototype1(string id) : base(id) { }
        public override Prototype Clone()
        {
            return (Prototype)this.MimberwiseClone();
        }

        private Prototype MimberwiseClone()
        {
            throw new NotImplementedException();
        }
    }
}
