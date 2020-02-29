using System;
using System.Collections.Generic;
using System.Text;

namespace aspectcore.demo
{
    public class Person
    {
        [CustomInterceptor]
        public virtual void Say(string message) => Console.WriteLine($"Service calling ... => {message}");
    }
}
