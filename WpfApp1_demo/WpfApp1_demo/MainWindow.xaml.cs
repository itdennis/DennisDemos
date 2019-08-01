using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfApp1_demo.Interface;
using WpfApp1_demo.LandingPage;
using WpfApp1_demo.Logger;
using WpfApp1_demo.Resource;

namespace WpfApp1_demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INavigator
    {
        public MainWindow()
        {
            InitializeComponent();
            UIStaticResource.AppDispatcher = this;
            Repository.Navigator = this;
            this.Closed += (s, e) =>
            {
                CloseCurrentProcess();
            };
        }
        Logger.Logger mLog = Logger.Logger.CreateInstance();

        private void CloseCurrentProcess()
        {
            try
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
            catch (Exception ex)
            {
                mLog.Log(LogLevel.Warning, "Close current process error: {0}", ex.ToString());
            }

        }

        private LandingPageViewModel viewModel;
        private LandingPageViewModel ViewModel
        {
            get { return this.viewModel ?? (this.viewModel = new LandingPageViewModel(this)); }
        }

        public void NavigateBack()
        {
            var count = this.TabController.Items.Count;
            while (count != 1)
            {
                count--;
                var canDispose = ((this.TabController.Items[count] as TabItem).Content as FrameworkElement).DataContext as IDisposable;
                if (canDispose != null)
                {
                    canDispose.Dispose();
                }
                this.TabController.Items.RemoveAt(count);
            }
            this.FixedHomeTab.Visibility = System.Windows.Visibility.Visible;
            this.FixedHomeTab.IsSelected = true;
            this.currentMode = NavigationType.LandingPage;
        }

        private NavigationType currentMode;
        public void Navigate(NavigationType mode)
        {
            this.currentMode = mode;
            this.FixedHomeTab.Visibility = System.Windows.Visibility.Collapsed;
            UIStaticResource.AppDispatcher.AppDispatcher.BeginInvoke(new Action(() => this.viewModel.ExecuteNavigation(mode)));
        }

        public void NavigateSpecifyTab(int tabIndex)
        {
            if (this.TabController.Items.Count > 1 && this.TabController.Items.Count >= tabIndex)
            {
                var tab = this.TabController.Items[tabIndex] as TabItem;
                tab.IsSelected = true;
            }
        }

        public NavigationType CurrentMode
        {
            get { return this.currentMode; }
        }

        public void EnableAllTabs()
        {
            var count = this.TabController.Items.Count;
            for (var i = 0; i < count; i++)
            {
                (this.TabController.Items[i] as TabItem).IsEnabled = true;
            }
        }

        public void DisableAllTabsExcept(int tabIndex)
        {
            var count = this.TabController.Items.Count;
            for (var i = 0; i < count; i++)
            {
                (this.TabController.Items[i] as TabItem).IsEnabled = i == tabIndex;
            }
        }

        public TabControl TabController
        {
            get { return this.Tabs; }
        }

        public void Remove(int tabIndex)
        {
            if (this.Tabs.Items.Count <= tabIndex)
            {
                return;
            }
            this.Tabs.Items.RemoveAt(tabIndex);
        }

        public void Remove(TabItem item)
        {
            if (this.Tabs.Items.Contains(item))
            {
                this.Tabs.Items.Remove(item);
            }
        }

        public System.Windows.Threading.Dispatcher AppDispatcher
        {
            get { return this.BreadCrum.Dispatcher; }
        }
    }
}
