using System;
using System.Collections.Generic;
using System.Text;

namespace PublishSubscriptDemo
{
    public class ConcreateObserverA : IObserver
    {
        private string name;
        private string observerState;
        private ISubject subject;

        public ConcreateObserverA(ISubject subject, string name)
        {
            this.subject = subject;
            this.name = name;
        }
        public void Update()
        {
            observerState = this.subject.status;
            Console.WriteLine($"observer {this.name}'s new state is {this.observerState}.");
        }
    }
}
