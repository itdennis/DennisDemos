using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1_demo.Controls
{
    public class HomeTabItem : TabItem
    {
        static HomeTabItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HomeTabItem), new FrameworkPropertyMetadata(typeof(HomeTabItem)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }
    }
}
