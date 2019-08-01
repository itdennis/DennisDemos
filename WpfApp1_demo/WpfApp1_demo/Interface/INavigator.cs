using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp1_demo.Interface
{
    public interface INavigator : IAppDispatcher
    {
        /// <summary>
        /// Go back to landing page
        /// </summary>
        void NavigateBack();

        void Navigate(NavigationType mode);

        void NavigateSpecifyTab(int tabIndex);

        void EnableAllTabs();

        void DisableAllTabsExcept(int tabIndex);

        /// <summary>
        /// Remove tabitem by index
        /// </summary>
        /// <param name="tabIndex"></param>
        void Remove(int tabIndex);

        /// <summary>
        /// Remove tabitem by item
        /// </summary>
        /// <param name="item"></param>
        void Remove(TabItem item);

        NavigationType CurrentMode { get; }

        TabControl TabController { get; }
    }


    public enum NavigationType
    {
        LandingPage = 0,
        FileMigration = 1,
        LicenseManager,
    }
}
