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
using System.Windows.Media.Imaging;
using AvePoint.Migrator.Common.Controls;
using MigratorTool.WPF.I18N;

namespace MigratorTool.WPF.View.Controls.Alert
{
    /// <summary>
    /// Interaction logic for Confirm.xaml
    /// </summary>
    public partial class Confirm : ChildWindow
    {
        private Action okAction;
        public Confirm(AlertType type, string message, Action okCallback)
        {
            InitializeComponent();
            this.okAction = okCallback;
            this.AlertMessage.Text = message;
            this.SetImageSource(type);
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            base.Close();
            this.okAction.Execute();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            base.Close();
        }

        private void SetImageSource(AlertType type)
        {
            switch (type)
            {
                case AlertType.Error:
                    this.Caption = I18NEntity.Get("ER_2d7aa343_d093_4791_835f_03001ce08419");
                    this.Image.Source = new BitmapImage(new Uri(@"pack://application:,,,/Resource/Image/Controls/Validation/error_32x32.png", UriKind.RelativeOrAbsolute));
                    break;
                case AlertType.Info:
                    this.Caption = I18NEntity.Get("Common_26b8668a_09b0_45af_a04d_4d9bad83b44f");
                    this.Image.Source = new BitmapImage(new Uri(@"pack://application:,,,/Resource/Image/Controls/Validation/information_32x32.png", UriKind.RelativeOrAbsolute));
                    break;
                case AlertType.Warnning:
                    this.Caption = I18NEntity.Get("Common_2d97a38c_911f_4bc1_88e6_5b599a093009");
                    this.Image.Source = new BitmapImage(new Uri(@"pack://application:,,,/Resource/Image/Controls/Validation/warning_32x32.png", UriKind.RelativeOrAbsolute));
                    break;
                default:
                    this.Caption = I18NEntity.Get("ER_dd74c7d2_6de1_4fd7_b43a_28ea6d15343b");
                    this.Image.Source = new BitmapImage(new Uri(@"pack://application:,,,/Resource/Image/Controls/Validation/information_32x32.png", UriKind.RelativeOrAbsolute));
                    break;
            }
        }
    }
}
