using System;
using System.Collections.Generic;
using System.Text;

namespace PublishSubscriptDemo
{
    /// <summary>
    /// 通过事件订阅来将INotify与Observer之间解耦 
    /// </summary>
    public interface INotifier
    {
        string Status { get; set; }
        void Notify();
    }
}
