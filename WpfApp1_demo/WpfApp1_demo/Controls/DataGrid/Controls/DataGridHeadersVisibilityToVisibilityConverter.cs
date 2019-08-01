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
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AvePoint.Migrator.Common.Controls
{
    /// <summary>
    ///     Converts DataGridHeadersVisibility to Visibility based on the given parameter.
    /// </summary> 
    [Localizability(LocalizationCategory.NeverLocalize)]
    internal sealed class DataGridHeadersVisibilityToVisibilityConverter : IValueConverter
    {
        /// <summary>
        ///     Convert DataGridHeadersVisibility to Visibility
        /// </summary>
        /// <param name="value">DataGridHeadersVisibility</param>
        /// <param name="targetType">Visibility</param>
        /// <param name="parameter">DataGridHeadersVisibility that represents the minimum DataGridHeadersVisibility that is needed for a Visibility of Visible</param>
        /// <param name="culture">null</param>
        /// <returns>Visible or Collapsed based on the value & converter mode</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var visible = false;

            if (value is DataGridHeadersVisibility && parameter is DataGridHeadersVisibility)
            {
                var valueAsDataGridHeadersVisibility = (DataGridHeadersVisibility)value;
                var parameterAsDataGridHeadersVisibility = (DataGridHeadersVisibility)parameter;

                switch (valueAsDataGridHeadersVisibility)
                {
                    case DataGridHeadersVisibility.All:
                        visible = true;
                        break;
                    case DataGridHeadersVisibility.Column:
                        visible = parameterAsDataGridHeadersVisibility == DataGridHeadersVisibility.Column || 
                                    parameterAsDataGridHeadersVisibility == DataGridHeadersVisibility.None;
                        break;
                    case DataGridHeadersVisibility.Row:
                        visible = parameterAsDataGridHeadersVisibility == DataGridHeadersVisibility.Row || 
                                    parameterAsDataGridHeadersVisibility == DataGridHeadersVisibility.None;
                        break;
                }
            }

            if (targetType == typeof(Visibility))
            {
                return visible ? Visibility.Visible : Visibility.Collapsed;
            }
            else
            {
                return DependencyProperty.UnsetValue;
            }
        }

        /// <summary>
        ///     Not implemented
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
           throw new NotImplementedException();
        }
    }
}
