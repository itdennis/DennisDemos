using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.Demoes.IOCDemos
{
    interface ISqlHelper
    {
        int Add();
        int Delete();
        int Update();
        int Query();
    }
}
