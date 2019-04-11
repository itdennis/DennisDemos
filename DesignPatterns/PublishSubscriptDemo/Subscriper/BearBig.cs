using System;
using System.Collections.Generic;
using System.Text;

namespace PublishSubscriptDemo
{
    public class BearBig
    {
        private string name;
        private string state;
        private INotifier notifier;
        public BearBig(string name, INotifier notifier)
        {
            this.name = name;
            this.notifier = notifier;
        }
        public void ProtectTree()
        {
            this.state = notifier.Status;
            Console.WriteLine($"My name is {this.name}, I am notified {state}.");
        }
    }
}
