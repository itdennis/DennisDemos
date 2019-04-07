using System;
using System.Collections.Generic;
using System.Text;

namespace ProtoTypeDemo
{
    public abstract class Prototype
    {
        private string id;
        public Prototype(string id)
        {
            this.id = id;
        }

        public string Id { get => this.id; }

        public abstract Prototype Clone();
    }
}
