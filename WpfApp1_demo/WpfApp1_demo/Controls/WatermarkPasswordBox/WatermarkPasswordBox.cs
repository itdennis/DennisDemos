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

using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace AvePoint.Migrator.Common.Controls
{
    [TemplatePart(Name = PART_CONTENTCONTROL, Type = typeof(ContentControl))]
    [TemplatePart(Name = PART_PASSWORD, Type = typeof(PasswordBox))]
    public class WatermarkPasswordBox : TextBox
    {
        #region ==const==

        public const string PART_CONTENTCONTROL = "PART_Watermark";
        public const string PART_PASSWORD = "PART_PasswordBox";

        #endregion ==const==

        #region    ==Members==

        private ContentControl WatermarkContent;
        private PasswordBox PasswordContent;

        #endregion ==Members==

        #region    ==Constructors==

        static WatermarkPasswordBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WatermarkPasswordBox), new FrameworkPropertyMetadata(typeof(WatermarkPasswordBox)));
        }

        #endregion ==Constructors==

        #region    ==Properties==

        #region    ==Watermark==

        /// <summary>
        /// Gets or Sets the Watermark
        /// </summary>
        public object Watermark
        {
            get { return base.GetValue(WatermarkProperty) as object; }
            set { base.SetValue(WatermarkProperty, value); }
        }
        public static readonly DependencyProperty WatermarkProperty =
        DependencyProperty.Register("Watermark", typeof(object), typeof(WatermarkPasswordBox), new PropertyMetadata("Password", OnWatermarkPropertyChanged));

        private static void OnWatermarkPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            WatermarkPasswordBox watermarkTextBox = sender as WatermarkPasswordBox;
            if (watermarkTextBox != null && watermarkTextBox.WatermarkContent != null)
            {
                watermarkTextBox.DetermineWatermarkContentVisibility();
            }

            Debug.WriteLine("OnWatermarkPropertyChanged");
        }

        #endregion ==Watermark==

        #region    ==WatermarkStyle==

        /// <summary>
        /// Gets or Sets the WatermarkStyle
        /// </summary>
        public Style WatermarkStyle
        {
            get { return base.GetValue(WatermarkStyleProperty) as Style; }
            set { base.SetValue(WatermarkStyleProperty, value); }
        }
        public static readonly DependencyProperty WatermarkStyleProperty =
        DependencyProperty.Register("WatermarkStyle", typeof(Style), typeof(WatermarkPasswordBox), null);

        #endregion ==WatermarkStyle==

        #endregion ==Properties==

        #region    ==Methods==
        #region    ==Base Override Methods==

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.WatermarkContent = this.GetTemplateChild(PART_CONTENTCONTROL) as ContentControl;
            this.PasswordContent = this.GetTemplateChild(PART_PASSWORD) as PasswordBox;

            if (PasswordContent != null)
            {
                PasswordContent.GotFocus -= PasswordContent_GotFocus;
                PasswordContent.LostFocus -= PasswordContent_LostFocus;
                PasswordContent.GotFocus += PasswordContent_GotFocus;
                PasswordContent.LostFocus += PasswordContent_LostFocus;
            }

            if (WatermarkContent != null)
            {
                PasswordContent.PasswordChanged -= new RoutedEventHandler(PasswordContent_PasswordChanged);
                PasswordContent.PasswordChanged += new RoutedEventHandler(PasswordContent_PasswordChanged);
                DetermineWatermarkContentVisibility();
            }

            this.Loaded += WatermarkPasswordBox_Loaded;
            this.TextChanged -= WatermarkPasswordBox_TextChanged;
            this.TextChanged += WatermarkPasswordBox_TextChanged;
        }

        void WatermarkPasswordBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PasswordContent != null && PasswordContent.Password != this.Text)
            {
                PasswordContent.Password = this.Text;
            }

            if (PasswordContent != null && !string.IsNullOrEmpty(PasswordContent.Password) && WatermarkContent != null)
            {
                WatermarkContent.Visibility = Visibility.Collapsed;
            }
            if (PasswordContent != null && WatermarkContent != null)
            {
                if (!string.IsNullOrEmpty(PasswordContent.Password))
                {
                    WatermarkContent.Visibility = Visibility.Collapsed;
                }
                else
                {
                    WatermarkContent.Visibility = Visibility.Visible;
                }
            }
        }

        void WatermarkPasswordBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (!this.Text.Equals(string.Empty) && PasswordContent != null)
            {
                PasswordContent.Password = this.Text;
            }

            if (PasswordContent != null && !string.IsNullOrEmpty(PasswordContent.Password) && WatermarkContent != null)
            {
                WatermarkContent.Visibility = Visibility.Collapsed;
            }
        }

        #endregion ==Base Override Methods==

        #region    ==private Methods==

        private void DetermineWatermarkContentVisibility()
        {
            if (string.IsNullOrEmpty(this.PasswordContent.Password))
            {
                this.WatermarkContent.Visibility = Visibility.Visible;
            }
            else
            {
                this.WatermarkContent.Visibility = Visibility.Collapsed;
            }
        }

        #endregion ==Private Methods==
        #endregion ==Methods==

        #region    ==Events==

        private void PasswordContent_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordContent != null && WatermarkContent != null && string.IsNullOrEmpty(this.PasswordContent.Password))
            {
                this.WatermarkContent.Visibility = Visibility.Visible;
            }
        }

        private void PasswordContent_GotFocus(object sender, RoutedEventArgs e)
        {
            if (WatermarkContent != null)
            {
                this.WatermarkContent.Visibility = Visibility.Collapsed;
            }
        }

        private void PasswordContent_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwdBx = sender as PasswordBox;
            if (passwdBx != null && this.Text != passwdBx.Password)
            {
                this.Text = passwdBx.Password;
            }
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            if (WatermarkContent != null)
            {
                this.WatermarkContent.Visibility = Visibility.Collapsed;
            }
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            if (PasswordContent != null && WatermarkContent != null && string.IsNullOrEmpty(this.PasswordContent.Password))
            {
                this.WatermarkContent.Visibility = Visibility.Visible;
            }
            base.OnLostFocus(e);
        }

        #endregion ==Events==
    }
}
