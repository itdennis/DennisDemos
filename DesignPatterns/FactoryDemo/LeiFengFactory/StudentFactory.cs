using System;
using System.Collections.Generic;
using System.Text;

namespace FactoryDemo
{
    public class StudentFactory : ILeiFengFactory
    {
        public LeiFeng CreateLeiFeng()
        {
            return new Student();
        }
    }
}
