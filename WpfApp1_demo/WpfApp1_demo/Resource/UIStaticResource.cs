using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1_demo.Interface;

namespace WpfApp1_demo.Resource
{
    public class UIStaticResource
    {
        public static IAppDispatcher AppDispatcher { get; set; }

        public static void Dispose()
        {
            AppDispatcher = null;
        }
    }
}
