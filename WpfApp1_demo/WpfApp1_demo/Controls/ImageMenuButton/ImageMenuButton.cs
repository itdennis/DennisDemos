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
using System.Windows.Media.Imaging;

namespace AvePoint.Migrator.Common.Controls
{
    public class ImageMenuButton : ContentControl
    {
        #region    ==Fields==

        public event RoutedEventHandler Click;

        private Border backgroundBorder;

        private Popup popup;

        private Border submenuBorder;

        private Grid parentGrid;

        #endregion ==Fields==

        #region    ==Properties==

        #region    ==ImagePath==

        public string ImagePath
        {
            get { return (string)GetValue(ImagePathProperty); }
            set { SetValue(ImagePathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImagePath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImagePathProperty =
            DependencyProperty.Register("ImagePath", typeof(string), typeof(ImageMenuButton), new PropertyMetadata(""));
        
        #endregion ==ImagePath==

        #region    ==MenuContent==

        public object MenuContent
        {
            get { return (object)GetValue(MenuContentProperty); }
            set { SetValue(MenuContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MenuContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MenuContentProperty =
            DependencyProperty.Register("MenuContent", typeof(object), typeof(ImageMenuButton), new PropertyMetadata(null));

        #endregion ==MenuContent==

        #region    ==IsSelected==

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelctedProperty); }
            set { SetValue(IsSelctedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsSelcted.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSelctedProperty =
            DependencyProperty.Register("IsSelcted", typeof(bool), typeof(ImageMenuButton), new PropertyMetadata(false, OnIsSelectedChanged));

        private static void OnIsSelectedChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ImageMenuButton source = o as ImageMenuButton;
            if (source.backgroundBorder != null)
            {
                source.backgroundBorder.Opacity = (bool)e.NewValue ? 1 : 0;
            }
        }

        #endregion ==IsSelected==

        #region    ==ImageMargin==

        public Thickness ImageMargin
        {
            get { return (Thickness)GetValue(ImageMarginProperty); }
            set { SetValue(ImageMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageMarginProperty =
            DependencyProperty.Register("ImageMargin", typeof(Thickness), typeof(ImageMenuButton), new PropertyMetadata(new Thickness(0)));

        #endregion ==ImageMargin


        public double ImageHeight
        {
            get { return (double)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageHeightProperty =
            DependencyProperty.Register("ImageHeight", typeof(double), typeof(ImageMenuButton), new PropertyMetadata(24.0));



        public double ImageWidth
        {
            get { return (double)GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageWidthProperty =
            DependencyProperty.Register("ImageWidth", typeof(double), typeof(ImageMenuButton), new PropertyMetadata(24.0));


        #endregion ==Properties==

        #region    ==Constructors

        static ImageMenuButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageMenuButton), new FrameworkPropertyMetadata(typeof(ImageMenuButton)));
        }

        #endregion ==Constructors

        #region    ==Methods==

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            Image image = this.GetTemplateChild("image") as Image;
            if (image != null)
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.UriSource = new Uri("pack://application:,,,/CRMTimelineUI;component" + ImagePath, UriKind.RelativeOrAbsolute);
                bitmap.EndInit();
                bitmap.Freeze();
                image.Source = bitmap;
            }

            parentGrid = this.GetTemplateChild("parentGrid") as Grid;
            if (parentGrid != null)
            {
                parentGrid.MouseLeftButtonDown -= new MouseButtonEventHandler(parentGrid_MouseLeftButtonDown);
                parentGrid.MouseEnter -= new MouseEventHandler(parentGrid_MouseEnter);
                parentGrid.MouseLeave -= new MouseEventHandler(parentGrid_MouseLeave);
                parentGrid.MouseLeftButtonDown += new MouseButtonEventHandler(parentGrid_MouseLeftButtonDown);
                parentGrid.MouseEnter += new MouseEventHandler(parentGrid_MouseEnter);
                parentGrid.MouseLeave += new MouseEventHandler(parentGrid_MouseLeave);
            }

            backgroundBorder = this.GetTemplateChild("Background") as Border;
            backgroundBorder.Opacity = this.IsSelected ? 1 : 0;

            popup = GetTemplateChild("Popup") as Popup;
            if (popup != null)
            {
                popup.Closed -= popup_Closed;
                popup.Opened -= popup_Opened;
                popup.Closed += popup_Closed;
                popup.Opened += popup_Opened;
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
        }

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

        private void MainWindow_LocationChanged(object sender, EventArgs e)
        {
            if (popup != null)
            {
                popup.IsOpen = false;
            }
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (popup != null)
            {
                popup.IsOpen = false;
            }
        }

        private void popup_Closed(object sender, EventArgs e)
        {
            popup.StaysOpen = true;
        }

        private void popup_Opened(object sender, EventArgs e)
        {
            if (parentGrid != null)
            {
                popup.VerticalOffset = parentGrid.ActualHeight;
                popup.HorizontalOffset = 0;
                popup.HorizontalOffset = parentGrid.ActualWidth - submenuBorder.ActualWidth;
            }
        }

        private void parentGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            VisualStateManager.GoToState(this, "Normal", true);
            if (!popup.IsMouseOver)
            {
                popup.StaysOpen = false;
            }
        }

        private void parentGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            VisualStateManager.GoToState(this, "MouseOver", true);
            popup.StaysOpen = true;
        }

        private void parentGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Click != null)
            {
                Click(this, e);
            }
            VisualStateManager.GoToState(this, "Pressed", true);
            this.Focus();
            if (MenuContent != null)
            {
                popup.IsOpen = !popup.IsOpen;
            }
        }

        #endregion ==Methods==
    }
}
