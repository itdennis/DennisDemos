using System;
using System.Collections.Generic;
using System.Text;

namespace DennisCoreDemos.Interface_defaultImplementation
{
    class Class1 : Interface1
    {
        public void Log(int level, string message)
        {
            throw new NotImplementedException();
        }
    }
}
