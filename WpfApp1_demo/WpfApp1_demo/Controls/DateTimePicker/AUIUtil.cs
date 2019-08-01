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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Media;
    #endregion

    public static class AUIUtil
    {
        /// <summary>
        /// 取得Application的宽高
        /// example:AUIUtil.GetAppSize()
        /// </summary>
        /// <returns>Point(x:宽;y:高)</returns>
        public static System.Windows.Point GetAppSize()
        {
            Point size = new Point();
            if (System.Windows.Application.Current != null && System.Windows.Application.Current.MainWindow != null)
            {
                size.X = (System.Windows.Application.Current.MainWindow.Content as FrameworkElement).ActualWidth;
                size.Y = (System.Windows.Application.Current.MainWindow.Content as FrameworkElement).ActualHeight;
            }
            return size;
        }

        /// <summary>
        /// 取得控件相对Application的left&top
        /// example:ui.GetUIPosition();
        /// </summary>
        /// <returns>Point(x:left;y:top)</returns>
        public static Point GetUIPosition(this UIElement ui)
        {
            if (Application.Current != null && Application.Current.MainWindow != null)
            {
                return ui.GetUIPosition(Application.Current.MainWindow);
            }
            return new Point();
        }
        /// <summary>
        /// 取得控件相对指定容器panel的left&top
        /// example:ui.GetUIPosition(this.LayoutRoot);
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="panel">相对定位的容器</param>
        /// <returns>Point(x:left;y:top)</returns>
        public static Point GetUIPosition(this UIElement ui, DependencyObject panel)
        {
            Point Pos = new Point();
            if (Application.Current != null && Application.Current.MainWindow != null)
            {
                GeneralTransform gt = ui.TransformToVisual(panel as UIElement);
                if (gt != null)
                {
                    Pos = gt.Transform(new Point(0, 0));
                }
            }
            return Pos;
        }
    }
}
