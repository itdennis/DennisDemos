using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1_demo.Controls
{
    public class CloseableTabItem : TabItem
    {
        static CloseableTabItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CloseableTabItem),
                new FrameworkPropertyMetadata(typeof(CloseableTabItem)));
        }

        Button closeButton;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            closeButton = base.GetTemplateChild("PART_Close") as Button;
            if (closeButton != null)
            {
                closeButton.Click += new System.Windows.RoutedEventHandler(closeButton_Click);
            }
        }

        public bool CanClose
        {
            get { return (bool)GetValue(CanCloseProperty); }
            set { SetValue(CanCloseProperty, value); }
        }

        public static DependencyProperty CanCloseProperty =
            DependencyProperty.Register("CanClose", typeof(bool), typeof(CloseableTabItem), new PropertyMetadata(false, (d, e) =>
        {
            if (d != null)
            {
                var self = d as CloseableTabItem;
                if (self.closeButton != null)
                {
                    self.closeButton.Visibility = (bool)e.NewValue ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }));

        void closeButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Close != null)
            {
                this.Close(this, e);
            }
            var parent = this.Parent as TabControl;
            if (parent != null)
            {
                parent.Items.Remove(this);
            }
        }

        public event EventHandler Close;
    }
}