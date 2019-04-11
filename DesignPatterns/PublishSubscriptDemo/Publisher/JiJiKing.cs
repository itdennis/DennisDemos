using System;
using System.Collections.Generic;
using System.Text;

namespace PublishSubscriptDemo
{
    public delegate void EventHandler();
    public class JiJiKing : INotifier
    {
        public event EventHandler Update;
        public string Status { get; set ; }

        public void Notify()
        {
            Update();
        }
    }
}
