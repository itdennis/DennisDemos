using System;
using System.Collections.Generic;
using System.Text;

namespace DennisCoreDemos.Interface_defaultImplementation
{
    interface Interface1
    {
        void Log(int level, string message);
        void Log(Exception ex) => Log(1, ex.ToString()); // New overload
    }
}
