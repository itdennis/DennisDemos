using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace anc.middleware.test
{
    public class NoLogsAttriteFilter : Attribute
    {
        public string Message = "";

        public NoLogsAttriteFilter(string message)
        {
            Message = message;
        }
    }
}
