using System;
using System.Collections.Generic;
using System.Text;

namespace SingletonDemo
{
    public class SingletonClass
    {
        private SingletonClass singletonInstance = null;
        private static readonly object locker = new object();
        private SingletonClass() { }
        public SingletonClass GetInstance()
        {
            if (singletonInstance == null)
            {
                lock (locker)
                {
                    if (singletonInstance == null)
                    {
                        singletonInstance = new SingletonClass();
                    }
                } 
            }
            return singletonInstance;
        }
    }
}
