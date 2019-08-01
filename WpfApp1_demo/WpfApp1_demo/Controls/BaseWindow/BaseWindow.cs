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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;

namespace AvePoint.Migrator.Common.Controls
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:AvePoint.Box.GuiCommon.AUI.BaseWindow"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:AvePoint.Box.GuiCommon.AUI.BaseWindow;assembly=AvePoint.Box.GuiCommon.AUI.BaseWindow"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:BaseWindow/>
    ///
    /// </summary>
    public class BaseWindow : Window
    {

        #region --- DependencyProperty ---

        public ImageSource TitleImg
        {
            get { return (ImageSource)GetValue(TitleImgProperty); }
            set { SetValue(TitleImgProperty, value); }
        }

        public static readonly DependencyProperty TitleImgProperty =
            DependencyProperty.Register("TitleImg",
            typeof(ImageSource),
            typeof(BaseWindow),
            new UIPropertyMetadata(null));

        public string TitleText
        {
            get { return (string)GetValue(TitleTextProperty); }
            set { SetValue(TitleTextProperty, value); }
        }

        public static readonly DependencyProperty TitleTextProperty =
            DependencyProperty.Register("TitleText",
            typeof(string),
            typeof(BaseWindow),
            new UIPropertyMetadata(null));

        #endregion

        #region --- Construction ---

        public BaseWindow()
        {
            this.DefaultStyleKey = typeof(BaseWindow);
            FullScreenManager.RepairWpfWindowFullScreenBehavior(this);
        }

        #endregion

        private ImageButton mMinButton;
        private ImageButton mMaxButton;
        private ImageButton mRestoreButton;
        private ImageButton mCloseButton;
        private Grid mBorderTitle;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            mMinButton = this.GetTemplateChild("btnMin") as ImageButton;
            mMaxButton = this.GetTemplateChild("btnMax") as ImageButton;
            mRestoreButton = this.GetTemplateChild("btnRestore") as ImageButton;
            mCloseButton = this.GetTemplateChild("btnClose") as ImageButton;
            mBorderTitle = this.GetTemplateChild("titleArea") as Grid;
            if (mMinButton != null)
            {
                mMinButton.Click += delegate { this.WindowState = System.Windows.WindowState.Minimized; };
            }

            if (mMaxButton != null)
            {
                mMaxButton.Click += delegate
                {
                    this.WindowState = (this.WindowState == System.Windows.WindowState.Normal ? System.Windows.WindowState.Maximized : System.Windows.WindowState.Normal);
                };
            }

            if (mRestoreButton != null)
            {
                mRestoreButton.Click += delegate
                {
                    this.WindowState = (this.WindowState == System.Windows.WindowState.Normal ? System.Windows.WindowState.Maximized : System.Windows.WindowState.Normal);
                };
            }

            if (mCloseButton != null)
            {
                mCloseButton.Click += delegate { this.Close(); };
            }

            if (mBorderTitle != null)
            {
                mBorderTitle.MouseMove += delegate(object sender, MouseEventArgs e)
                {
                    if (e.LeftButton == MouseButtonState.Pressed)
                    {
                        this.DragMove();
                    }
                };
            }

            if (mBorderTitle != null)
            {
                mBorderTitle.MouseLeftButtonDown += delegate(object sender, MouseButtonEventArgs e)
                {
                    if (e.ClickCount >= 2)
                    {
                        mMaxButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                    }
                };
            }
        }
    }
}
