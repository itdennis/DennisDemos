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

namespace AvePoint.Migrator.Common.Controls
{
    /// <summary>
    /// Interaction logic for Alert.xaml
    /// </summary>
    public partial class Alert : ChildWindow
    {
        public Alert()
        {
            InitializeComponent();
        }

        private bool hasCopy = true;
        public bool HasCopy
        {
            get { return hasCopy; }
            set
            {
                if (value != hasCopy)
                {
                    hasCopy = value;
                    this.CopyBtn.Visibility = value ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
                }
            }
        }

        private AlertType type;
        public AlertType Type
        {
            get { return type; }
            set
            {
                if (value != type)
                {
                    type = value;
                    SetImageSource(value);
                }
            }
        }

        private void SetImageSource(AlertType type)
        {
            switch (type)
            {
                case AlertType.Error:
                    this.Image.Source = new BitmapImage(new Uri(@"pack://application:,,,/Resource/Image/Controls/Validation/error_32x32.png", UriKind.RelativeOrAbsolute));
                    break;
                case AlertType.Info:
                    this.Image.Source = new BitmapImage(new Uri(@"pack://application:,,,/Resource/Image/Controls/Validation/information_32x32.png", UriKind.RelativeOrAbsolute));
                    break;
                case AlertType.Warnning:
                    this.Image.Source = new BitmapImage(new Uri(@"pack://application:,,,/Resource/Image/Controls/Validation/warning_32x32.png", UriKind.RelativeOrAbsolute));
                    break;
                default:
                    this.Image.Source = new BitmapImage(new Uri(@"pack://application:,,,/Resource/Image/Controls/Validation/information_32x32.png", UriKind.RelativeOrAbsolute));
                    break;
            }
        }

        private string message;
        public string Message
        {
            get { return message; }
            set
            {
                if (value != message)
                {
                    message = value;
                    this.AlertMessage.Text = value;
                }
            }
        }

        private void OKBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CopyBtn_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(this.AlertMessage.Text);
        }
    }
}
