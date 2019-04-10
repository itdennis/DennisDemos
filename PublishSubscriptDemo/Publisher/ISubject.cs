using System;
using System.Collections.Generic;
using System.Text;

namespace PublishSubscriptDemo
{
    public interface ISubject
    {
        string status { get; set; }
        IList<IObserver> observers { get; set; }
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify();
    }
}
