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

namespace AvePoint.Migrator.Common.Controls
{
    #region ==using==
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;
    #endregion

    public class WizardItem : ContentControl
    {
        static WizardItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WizardItem), new FrameworkPropertyMetadata(typeof(WizardItem)));
        }

        Border leftLine;
        Border rightLine;
        Image image;
        TextBlock TitleTextBlock;
        Border border;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            TitleTextBlock = GetTemplateChild("TitleTextBlock") as TextBlock;
            leftLine = GetTemplateChild("Left_Line") as Border;
            rightLine = GetTemplateChild("Right_Line") as Border;
            image = GetTemplateChild("Image") as Image;
            border = GetTemplateChild("border") as Border;
            if (border != null)
            {
                this.ShowBorder(this.StepButtonStatus == Controls.StepButtonStatus.Selected);
            }
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(WizardItem));

        public bool IsFirst
        {
            get { return (bool)GetValue(IsFirstProperty); }
            set { SetValue(IsFirstProperty, value); }
        }
        public static readonly DependencyProperty IsFirstProperty = DependencyProperty.Register("IsFirst", typeof(bool), typeof(WizardItem), new PropertyMetadata(false));

        public bool IsLast
        {
            get { return (bool)GetValue(IsLastProperty); }
            set { SetValue(IsLastProperty, value); }
        }
        public static readonly DependencyProperty IsLastProperty = DependencyProperty.Register("IsLast", typeof(bool), typeof(WizardItem), new PropertyMetadata(false));

        public double LeftMargin
        {
            get { return (double)GetValue(LeftMarginProperty); }
            set { SetValue(LeftMarginProperty, value); }
        }
        public static readonly DependencyProperty LeftMarginProperty = DependencyProperty.Register("LeftMargin", typeof(double), typeof(WizardItem), new PropertyMetadata(30d));

        public double RightMargin
        {
            get { return (double)GetValue(RightMarginProperty); }
            set { SetValue(RightMarginProperty, value); }
        }
        public static readonly DependencyProperty RightMarginProperty = DependencyProperty.Register("RightMargin", typeof(double), typeof(WizardItem), new PropertyMetadata(30d));

        public StepButtonStatus StepButtonStatus
        {
            get { return (StepButtonStatus)GetValue(StepButtonStatusProperty); }
            set { SetValue(StepButtonStatusProperty, value); }
        }
        public static readonly DependencyProperty StepButtonStatusProperty =
            DependencyProperty.Register("StepButtonStatus", typeof(StepButtonStatus), typeof(WizardItem), new PropertyMetadata(StepButtonStatus.Normal, (d, e) => 
            {
                if (d != null)
                {
                    (d as WizardItem).OnStatusChanged((StepButtonStatus)e.NewValue);
                }
            }));

        private void SetGray(Border rec)
        {
            if (rec != null)
            {
                rec.Background = new System.Windows.Media.SolidColorBrush(Color.FromRgb(214, 214, 214));
            }
        }
        private void SetBlue(Border rec)
        {
            if (rec != null)
            {
                rec.Background = new System.Windows.Media.SolidColorBrush(Color.FromRgb(53, 89, 141));
            }
        }
        private void ShowBorder(bool show)
        {
            if (this.border != null)
            {
                //this.border.Visibility = show ? Visibility.Visible : System.Windows.Visibility.Collapsed;
                this.TitleTextBlock.Foreground = !show ? new SolidColorBrush(Colors.Black) : new SolidColorBrush(Colors.White);
                this.border.Background = show ? new SolidColorBrush(Color.FromRgb(53, 89, 141)) : new SolidColorBrush(Colors.Transparent);
            }
        }

        private void OnStatusChanged(StepButtonStatus to)
        {
            if (to == Controls.StepButtonStatus.Configured)
            {
                this.SetBlue(this.rightLine);
                this.SetBlue(this.leftLine);
                this.ShowBorder(false);
                this.ResetImageSize(WizardImageSize.Small);
            }
            else if (to == Controls.StepButtonStatus.Normal)
            {
                this.SetGray(this.rightLine);
                this.SetGray(this.leftLine);
                this.ShowBorder(false);
                this.ResetImageSize(WizardImageSize.Small);
            }
            else if (to == Controls.StepButtonStatus.Selected)
            {
                if (IsFirst)
                {
                    this.SetGray(this.rightLine);
                }
                if (IsLast)
                {
                    this.SetBlue(this.leftLine);
                }
                this.SetBlue(this.leftLine);
                this.SetGray(this.rightLine);
                this.ShowBorder(true);
                this.ResetImageSize(WizardImageSize.Normal);
            }
        }

        private void ResetImageSize(WizardImageSize size)
        {
            if (image != null)
            {
                if (size == WizardImageSize.Normal)
                {
                    image.Height = 27;
                    image.Margin = new Thickness(0, -4, 0, 0);
                }
                else
                {
                    image.Margin = new Thickness(0, 0, 0, 0);
                    image.Height = 23;
                }
            }
        }

        enum WizardImageSize
        {
            /// <summary>
            /// 23x27
            /// </summary>
            Normal = 0,

            /// <summary>
            /// 23x23
            /// </summary>
            Small = 1,
        }
    }

    public enum StepButtonStatus
    {
        Normal = 1,
        Selected = 2,
        Configured = 3,
    }
}
