using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1_demo.Interface;

namespace WpfApp1_demo.Resource
{
    internal class Repository
    {
        private static EventAggregator eventAggregator;

        internal static EventAggregator EventAggregator
        {
            get
            {
                return eventAggregator ?? (eventAggregator = new EventAggregator());
            }
        }

        internal static INavigator Navigator { get; set; }
    }
}
