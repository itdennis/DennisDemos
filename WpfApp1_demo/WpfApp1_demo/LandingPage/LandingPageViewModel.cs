using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfApp1_demo.Interface;
using WpfApp1_demo.ViewModel;

namespace WpfApp1_demo.LandingPage
{
    public class LandingPageViewModel : ViewModelBase
    {
        #region Constructor
        protected INavigator Navigator;
        public LandingPageViewModel(INavigator navigator)
        {
            this.Navigator = navigator;
        }
        #endregion

        [SuppressMessage("FxCopCustomRules", "C100007:SpellCheckStringValues", Justification = "屏蔽掉对Manangement的检查")]
        public void ExecuteNavigation(NavigationType mode)
        {
            if (mode == NavigationType.FileMigration)
            {
            }
        }

    }
}
