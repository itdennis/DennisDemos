using System;
using System.Collections.Generic;
using System.Text;

namespace AccessDemo
{
    public abstract class Human
    {
        private string action;
        public string Action { get => this.action; set => this.action = value; }
        public abstract void GetConclusion();

        public abstract void Accept(Action visitor);
    }
}
