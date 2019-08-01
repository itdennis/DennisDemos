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
    ///     Converts Boolean to SelectiveScrollingOrientation based on the given parameter.
    /// </summary> 
    [Localizability(LocalizationCategory.NeverLocalize)]
    internal sealed class BooleanToSelectiveScrollingOrientationConverter : IValueConverter
    {
        /// <summary>
        ///     Convert Boolean to SelectiveScrollingOrientation
        /// </summary>
        /// <param name="value">Boolean</param>
        /// <param name="targetType">SelectiveScrollingOrientation</param>
        /// <param name="parameter">SelectiveScrollingOrientation that should be used when the Boolean is true</param>
        /// <param name="culture">null</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool && parameter is SelectiveScrollingOrientation)
            {
                var valueAsBool = (bool)value;
                var parameterSelectiveScrollingOrientation = (SelectiveScrollingOrientation)parameter;

                if (valueAsBool)
                {
                    return parameterSelectiveScrollingOrientation;
                }
            }

            return SelectiveScrollingOrientation.Both;
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
