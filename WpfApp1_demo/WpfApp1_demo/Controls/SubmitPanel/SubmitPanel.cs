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

namespace AvePoint.Migrator.Common.Controls
{
    public class SubmitPanel : ItemsControl
    {
        static SubmitPanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SubmitPanel), new FrameworkPropertyMetadata(typeof(SubmitPanel)));
        }

        /// <summary>
        /// ready for items
        /// </summary>
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            ButtonBase _item = item as ButtonBase;
            if (null != _item)
            {
                Type _type = item.GetType();
                if (null != _type && _type.Equals(typeof(Button)))
                {
                    _item.MinWidth = 60;
                }
                _item.Height = 20;
                _item.Margin = new Thickness(5, 0, 0, 0);
            }
            base.PrepareContainerForItemOverride(element, item);
        }

        public static readonly DependencyProperty TopLineVisibilityProperty
             = DependencyProperty.Register("TopLineVisibility", typeof(Visibility), typeof(SubmitPanel), new PropertyMetadata(Visibility.Collapsed));

        public Visibility TopLineVisibility
        {
            get { return (Visibility)GetValue(TopLineVisibilityProperty);}
            set { SetValue(TopLineVisibilityProperty, value); }
        }
    }
}
