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

//---------------------------------------------------------------------------
//
// Copyright (C) Microsoft Corporation.  All rights reserved.
//
//---------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace AvePoint.Migrator.Common.Controls.Primitives
{
    /// <summary>
    /// Subclass of Grid that knows how to freeze certain cells in place when scrolled.
    /// Used as the panel for the DataGridRow to hold the header, cells, and details.
    /// </summary>
    public class SelectiveScrollingGrid : Grid
    {
        /// <summary>
        /// Attached property to specify the selective scroll behaviour of cells
        /// </summary>
        public static readonly DependencyProperty SelectiveScrollingOrientationProperty =
            DependencyProperty.RegisterAttached(
                "SelectiveScrollingOrientation",
                typeof(SelectiveScrollingOrientation),
                typeof(SelectiveScrollingGrid),
                new FrameworkPropertyMetadata(SelectiveScrollingOrientation.Both, new PropertyChangedCallback(OnSelectiveScrollingOrientationChanged)));

        /// <summary>
        /// Getter for the SelectiveScrollingOrientation attached property
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static SelectiveScrollingOrientation GetSelectiveScrollingOrientation(DependencyObject obj)
        {
            return (SelectiveScrollingOrientation)obj.GetValue(SelectiveScrollingOrientationProperty);
        }

        /// <summary>
        /// Setter for the SelectiveScrollingOrientation attached property
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetSelectiveScrollingOrientation(DependencyObject obj, SelectiveScrollingOrientation value)
        {
            obj.SetValue(SelectiveScrollingOrientationProperty, value);
        }

        /// <summary>
        /// Property changed call back for SelectiveScrollingOrientation property
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnSelectiveScrollingOrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UIElement element = d as UIElement;
            SelectiveScrollingOrientation orientation = (SelectiveScrollingOrientation)e.NewValue;
            ScrollViewer scrollViewer = DataGridHelper.FindVisualParent<ScrollViewer>(element);
            if (scrollViewer != null && element != null)
            {
                Transform transform = element.RenderTransform;

                if (transform != null)
                {
                    BindingOperations.ClearBinding(transform, TranslateTransform.XProperty);
                    BindingOperations.ClearBinding(transform, TranslateTransform.YProperty);
                }

                if (orientation == SelectiveScrollingOrientation.Both)
                {
                    element.RenderTransform = null;
                }
                else
                {
                    TranslateTransform translateTransform = new TranslateTransform();

                    // Add binding to XProperty of transform if orientation is not horizontal
                    if (orientation != SelectiveScrollingOrientation.Horizontal)
                    {
                        Binding horizontalBinding = new Binding("ContentHorizontalOffset");
                        horizontalBinding.Source = scrollViewer;
                        BindingOperations.SetBinding(translateTransform, TranslateTransform.XProperty, horizontalBinding);
                    }

                    // Add binding to YProperty of transfrom if orientation is not vertical
                    if (orientation != SelectiveScrollingOrientation.Vertical)
                    {
                        Binding verticalBinding = new Binding("ContentVerticalOffset");
                        verticalBinding.Source = scrollViewer;
                        BindingOperations.SetBinding(translateTransform, TranslateTransform.YProperty, verticalBinding);
                    }

                    element.RenderTransform = translateTransform;
                }
            }
        }
    }
}
