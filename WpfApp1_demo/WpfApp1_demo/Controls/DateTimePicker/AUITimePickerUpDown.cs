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

using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace AvePoint.Migrator.Common.Controls
{
    public class AUITimePickerUpDown : Control
    {
        private TextBox TB;

        private RepeatButton UpRepeatButton;
        private RepeatButton DownRepeatButton;

        public event TextChangedEventHandler TextChanged;

        static AUITimePickerUpDown()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AUITimePickerUpDown), new FrameworkPropertyMetadata(typeof(AUITimePickerUpDown)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            TB = GetTemplateChild("TB") as TextBox;
            UpRepeatButton = GetTemplateChild("UpRepeatButton") as RepeatButton;
            DownRepeatButton = GetTemplateChild("DownRepeatButton") as RepeatButton;
            UpRepeatButton.Click -= new RoutedEventHandler(UpRepeatButton_Click);
            UpRepeatButton.Click += new RoutedEventHandler(UpRepeatButton_Click);
            DownRepeatButton.Click -= new RoutedEventHandler(DownRepeatButton_Click);
            DownRepeatButton.Click += new RoutedEventHandler(DownRepeatButton_Click);
            Binding b = new Binding() 
            {
                Source = this,
                Path = new PropertyPath("Text"),
                Mode = BindingMode.TwoWay
            };
            TB.SetBinding(TextBox.TextProperty, b);
        }

        void DownRepeatButton_Click(object sender, RoutedEventArgs e)
        {
            var value = -1;
            if (int.TryParse(this.Text, out value))
            {
                this.Text = DateTimeHelper.AppendZero(value - 1);
            }
        }

        void UpRepeatButton_Click(object sender, RoutedEventArgs e)
        {
            var value = -1;
            if (int.TryParse(this.Text, out value))
            {
                this.Text = DateTimeHelper.AppendZero(value + 1);
            }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(AUITimePickerUpDown), new PropertyMetadata(string.Empty, OnTextChanged));

        private static void OnTextChanged(DependencyObject o,DependencyPropertyChangedEventArgs e)
        {
            AUITimePickerUpDown source = o as AUITimePickerUpDown;
            if(source.TextChanged != null)
            {
                source.TextChanged(source, null);
            }
        }

        public void SelectAll()
        { 
            if(this.TB != null)
            {
                this.TB.SelectAll();
            }
        }
    }
}
