using System;
using System.Collections.Generic;
using System.Text;

namespace PublishSubscriptDemo
{
    public class ConcreateSubject1 : ISubject
    {
        public IList<IObserver> observers { get; set; }
        public string status { get; set; }

        public ConcreateSubject1()
        {
            observers = new List<IObserver>();
        }
        public void Attach(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in this.observers)
            {
                observer.Update();
            }
        }
    }
}
