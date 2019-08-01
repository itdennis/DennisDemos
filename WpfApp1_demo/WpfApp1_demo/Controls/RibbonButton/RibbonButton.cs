/********************************************************************
 *
 *  PROPRIETARY and CONFIDENTIAL
 *
 *  This file is licensed from, and is a trade secret of:
 *
 *                   AvePoint, Inc.
 *                   Harborside Financial Center
 *                   9th Fl.   Plaza Ten
 *                   Jersey City, NJ 07311
 *                   United States of America
 *                   Telephone: +1-800-661-6588
 *                   WWW: www.avepoint.com
 *
 *  Refer to your License Agreement for restrictions on use,
 *  duplication, or disclosure.
 *
 *  RESTRICTED RIGHTS LEGEND
 *
 *  Use, duplication, or disclosure by the Government is
 *  subject to restrictions as set forth in subdivision
 *  (c)(1)(ii) of the Rights in Technical Data and Computer
 *  Software clause at DFARS 252.227-7013 (Oct. 1988) and
 *  FAR 52.227-19 (C) (June 1987).
 *
 *  Copyright © 2001-2014 AvePoint® Inc. All Rights Reserved. 
 *
 *  Unpublished - All rights reserved under the copyright laws of the United States.
 *  $Revision:  $
 *  $Author:  $        
 *  $Date:  $
 */

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Shapes;

namespace AvePoint.Migrator.Common.Controls
{
    public class RibbonButton : Control
    {
        static RibbonButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RibbonButton), new FrameworkPropertyMetadata(typeof(RibbonButton)));
        }

        public RibbonButton()
        {
            this.Unloaded += RibbonButton_Unloaded;
        }

        void RibbonButton_Unloaded(object sender, RoutedEventArgs e)
        {
            Window rootWindow = MigrationVisualTreeHelper.GetAppRootVisual();
            if (rootWindow != null)
            {
                rootWindow.SizeChanged -= MainWindow_SizeChanged;
                rootWindow.LocationChanged -= MainWindow_LocationChanged;
            }
        }

        //private Border backgroundBorder;

        private Popup popup;

        private Border submenuBorder;

        private Path arrow;

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(RibbonButton), new PropertyMetadata(string.Empty));

        public object MenuContent
        {
            get { return (object)GetValue(MenuContentProperty); }
            set { SetValue(MenuContentProperty, value); }
        }

        public static readonly DependencyProperty MenuContentProperty =
            DependencyProperty.Register("MenuContent", typeof(object), typeof(RibbonButton), new PropertyMetadata(null, new PropertyChangedCallback(OnMenuContentChanged)));

        private static void OnMenuContentChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            RibbonButton source = o as RibbonButton;
            if (source != null && source.arrow != null)
            {
                source.arrow.Visibility = e.NewValue == null ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public Visibility MenuContentVisibility
        {
            get { return (Visibility)GetValue(MenuContentVisibilityProperty); }
            set { SetValue(MenuContentVisibilityProperty, value); }
        }

        public static readonly DependencyProperty MenuContentVisibilityProperty =
            DependencyProperty.Register("MenuContentVisibility", typeof(Visibility), typeof(RibbonButton), new PropertyMetadata(Visibility.Visible, OnMenuContentVisibilityChanged));

        private static void OnMenuContentVisibilityChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            RibbonButton source = o as RibbonButton;
            if (source != null && source.arrow != null)
            {
                if ((Visibility)e.NewValue == Visibility.Collapsed)
                {
                    source.arrow.Visibility = Visibility.Collapsed;
                }
                else
                {
                    source.arrow.Visibility = source.MenuContent == null ? Visibility.Collapsed : Visibility.Visible;
                }
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Grid parentGrid = this.GetTemplateChild("parentGrid") as Grid;
            if (parentGrid != null)
            {
                parentGrid.MouseLeftButtonDown -= new MouseButtonEventHandler(parentGrid_MouseLeftButtonDown);
                parentGrid.MouseEnter -= new MouseEventHandler(parentGrid_MouseEnter);
                parentGrid.MouseLeave -= new MouseEventHandler(parentGrid_MouseLeave);
                parentGrid.MouseLeftButtonDown += new MouseButtonEventHandler(parentGrid_MouseLeftButtonDown);
                parentGrid.MouseEnter += new MouseEventHandler(parentGrid_MouseEnter);
                parentGrid.MouseLeave += new MouseEventHandler(parentGrid_MouseLeave);
            }

            //backgroundBorder = this.GetTemplateChild("Background") as Border;
            //backgroundBorder.Opacity = this.IsSelected ? 1 : 0;

            popup = GetTemplateChild("Popup") as Popup;
            if (popup != null)
            {
                popup.Closed -= popup_Closed;
                popup.Closed += popup_Closed;
                //Popup的Tag表示Popup中的控件是否处于获取焦点状态
                popup.Tag = this;
            }

            submenuBorder = GetTemplateChild("SubmenuBorder") as Border;
            if (submenuBorder != null)
            {
                submenuBorder.Tag = popup;
            }
            Window rootWindow = MigrationVisualTreeHelper.GetAppRootVisual();
            if (rootWindow != null)
            {
                rootWindow.SizeChanged -= MainWindow_SizeChanged;
                rootWindow.LocationChanged -= MainWindow_LocationChanged;
                rootWindow.SizeChanged += MainWindow_SizeChanged;
                rootWindow.LocationChanged += MainWindow_LocationChanged;
            }

            arrow = GetTemplateChild("Arrow") as Path;
            if (arrow != null && MenuContentVisibility == Visibility.Visible)
            {
                arrow.Visibility = MenuContent == null ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        void MainWindow_LocationChanged(object sender, EventArgs e)
        {
            if (popup != null)
            {
                popup.IsOpen = false;
            }
        }

        void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (popup != null)
            {
                popup.IsOpen = false;
            }
        }

        void popup_Closed(object sender, EventArgs e)
        {
            popup.StaysOpen = true;
        }

        void parentGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            VisualStateManager.GoToState(this, "Normal", true);
            if (!popup.IsMouseOver)
            {
                popup.StaysOpen = false;
            }
        }

        void parentGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            VisualStateManager.GoToState(this, "MouseOver", true);
            popup.StaysOpen = true;
        }

        void parentGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Command != null)
            {
                Command.Execute(null);
            }
            VisualStateManager.GoToState(this, "Pressed", true);
            this.Focus();
            //if (MenuContent != null && MenuContentVisibility == Visibility.Visible)
            {
                popup.IsOpen = !popup.IsOpen;
            }
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(RibbonButton));

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.Key == Key.Enter)
            {
                parentGrid_MouseLeftButtonDown(null, null);
            }
            else if (e.Key == Key.Down)
            {
                if (popup.IsOpen)
                {
                    Panel panel = this.MenuContent as Panel;
                    if (panel != null)
                    {
                        foreach (var item in panel.Children)
                        {
                            if ((item as FrameworkElement).Focusable)
                            {
                                (item as FrameworkElement).Focus();
                                e.Handled = true;
                                break;
                            }
                        }
                    }
                }
            }
        }

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelctedProperty); }
            set { SetValue(IsSelctedProperty, value); }
        }

        public static readonly DependencyProperty IsSelctedProperty =
            DependencyProperty.Register("IsSelcted", typeof(bool), typeof(RibbonButton), new PropertyMetadata(false, OnIsSelectedChanged));

        private static void OnIsSelectedChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            //RibbonButton source = o as RibbonButton;
            //if (source.backgroundBorder != null)
            //{
            //    source.backgroundBorder.Opacity = (bool)e.NewValue ? 1 : 0;
            //}
        }
    }
}
