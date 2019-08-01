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
    using System.Windows;
    using System.Windows.Controls;

    static class WizardHelper
    {
        public static int GetCurrentIndex(DependencyObject d)
        {
            return (int)d.GetValue(CurrentIndexProperty);
        }

        public static void SetCurrentIndex(DependencyObject d, int value)
        {
            d.SetValue(CurrentIndexProperty, value);
        }

        public static readonly DependencyProperty CurrentIndexProperty = DependencyProperty
            .RegisterAttached("CurrentIndex", typeof(int), typeof(WizardHelper), new PropertyMetadata(0, (d, e) =>
            {
                if (d != null)
                {
                    var grid = d as Grid;
                    if (grid == null)
                    {
                        return;
                    }
                    var gotoIndex = (int)e.NewValue;

                    for (int i = 0; i < grid.Children.Count; i++)
                    {
                        var item = grid.Children[i];
                        var wizard = item as WizardItem;
                        if (i < gotoIndex)
                        {
                            wizard.StepButtonStatus = StepButtonStatus.Configured;
                        }
                        else if (i == gotoIndex)
                        {
                            wizard.StepButtonStatus = StepButtonStatus.Selected;
                        }
                        else
                        {
                            wizard.StepButtonStatus = StepButtonStatus.Normal;
                        }
                    }
                }
            }));
    }
}
