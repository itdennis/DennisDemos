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
    public class SubmenuButton : Button
    {
        static SubmenuButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SubmenuButton), new FrameworkPropertyMetadata(typeof(SubmenuButton)));
        }

        private Popup popup;

        private Path arrow;

        public object MenuContent
        {
            get { return (object)GetValue(MenuContentProperty); }
            set { SetValue(MenuContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MenuContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MenuContentProperty =
            DependencyProperty.Register("MenuContent", typeof(object), typeof(SubmenuButton), new PropertyMetadata(null, new PropertyChangedCallback(OnMenuContentChanged)));

        private static void OnMenuContentChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            SubmenuButton source = o as SubmenuButton;
            if (source != null && source.arrow != null)
            {
                source.arrow.Visibility = e.NewValue == null ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public Visibility SubMenuVisibility
        {
            get { return (Visibility)GetValue(SubMenuVisibilityProperty); }
            set { SetValue(SubMenuVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SubMenuVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SubMenuVisibilityProperty =
            DependencyProperty.Register("SubMenuVisibility", typeof(Visibility), typeof(SubmenuButton), new PropertyMetadata(Visibility.Visible, OnSubMenuVisibilityChanged));

        private static void OnSubMenuVisibilityChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            SubmenuButton source = o as SubmenuButton;
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

        protected override void OnClick()
        {
            base.OnClick();
            if (SubMenuVisibility == Visibility.Collapsed || MenuContent == null)
            {
                ClosePopup();
            }
        }

        public void ClosePopup()
        {
            Border border = MigrationVisualTreeHelper.GetParent<Border>(this);
            if (border != null && border.Tag != null)
            {
                Popup popup = border.Tag as Popup;
                if (popup != null && popup.IsOpen)
                {
                    popup.IsOpen = false;
                    if (popup.Tag != null)
                    {
                        RibbonButton btn = popup.Tag as RibbonButton;
                        ImageMenuButton imageMenuButton = popup.Tag as ImageMenuButton;
                        if (btn != null)
                        {
                            btn.Focus();
                        }
                        else if (imageMenuButton != null)
                        {
                            imageMenuButton.Focus();
                        }
                        else
                        {
                            SubmenuButton subMenuBtn = popup.Tag as SubmenuButton;
                            if (subMenuBtn != null)
                            {
                                subMenuBtn.ClosePopup();
                            }
                        }
                    }
                }
            }
        }

        private Popup GetParentPopup()
        {
            Border border = MigrationVisualTreeHelper.GetParent<Border>(this);
            if (border != null && border.Tag != null)
            {
                return border.Tag as Popup;
            }
            return null;
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Grid parentGrid = this.GetTemplateChild("back") as Grid;
            if (parentGrid != null)
            {
                parentGrid.MouseLeftButtonDown -= new MouseButtonEventHandler(parentGrid_MouseLeftButtonDown);
                parentGrid.MouseEnter -= new MouseEventHandler(parentGrid_MouseEnter);
                parentGrid.MouseLeave -= new MouseEventHandler(parentGrid_MouseLeave);
                parentGrid.MouseLeftButtonDown += new MouseButtonEventHandler(parentGrid_MouseLeftButtonDown);
                parentGrid.MouseEnter += new MouseEventHandler(parentGrid_MouseEnter);
                parentGrid.MouseLeave += new MouseEventHandler(parentGrid_MouseLeave);
            }

            popup = GetTemplateChild("Popup") as Popup;
            if (popup != null)
            {
                popup.Closed -= popup_Closed;
                popup.Closed += popup_Closed;
                //Popup的Tag表示Popup中的控件是否处于获取焦点状态
                popup.Tag = this;
            }

            Border submenuBorder = GetTemplateChild("SubmenuBorder") as Border;
            if (submenuBorder != null)
            {
                submenuBorder.Tag = popup;
            }

            arrow = GetTemplateChild("Arrow") as Path;
            if (arrow != null && SubMenuVisibility == Visibility.Visible)
            {
                arrow.Visibility = MenuContent == null ? Visibility.Collapsed : Visibility.Visible;
            }

            Window rootWindow = MigrationVisualTreeHelper.GetAppRootVisual();
            if (rootWindow != null)
            {
                rootWindow.SizeChanged -= MainWindow_SizeChanged;
                rootWindow.LocationChanged -= MainWindow_LocationChanged;
                rootWindow.SizeChanged += MainWindow_SizeChanged;
                rootWindow.LocationChanged += MainWindow_LocationChanged;
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
            //VisualStateManager.GoToState(this, "Normal", true);
            if (!popup.IsMouseOver)
            {
                popup.StaysOpen = false;
            }
        }

        void parentGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            //VisualStateManager.GoToState(this, "MouseOver", true);
            popup.StaysOpen = true;
        }

        void parentGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (MenuContent != null && SubMenuVisibility == Visibility.Visible)
            {
                popup.IsOpen = !popup.IsOpen;
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Key == System.Windows.Input.Key.Enter)
            {
                parentGrid_MouseLeftButtonDown(null, null);
                ClosePopup();
            }
            else if (e.Key == System.Windows.Input.Key.Escape)
            {
                ClosePopup();
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
    }
}
