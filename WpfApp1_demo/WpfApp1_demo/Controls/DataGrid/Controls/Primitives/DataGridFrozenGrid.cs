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

namespace AvePoint.Migrator.Common.Controls.Primitives
{
    /// <summary>
    /// Represents a non-scrollable grid that contains <see cref="T:System.Windows.Controls.DataGrid" /> row headers.
    /// </summary>
    /// <QualityBand>Mature</QualityBand>
    public class DataGridFrozenGrid : Grid
    {
        /// <summary>
        /// A dependency property that indicates whether the grid is frozen.
        /// </summary>
        public static readonly DependencyProperty IsFrozenProperty = DependencyProperty.RegisterAttached(
            "IsFrozen",
            typeof(Boolean),
            typeof(DataGridFrozenGrid),
            null);

        /// <summary>
        /// Gets a value that indicates whether the grid is frozen.
        /// </summary>
        /// <param name="element">
        /// The object to get the <see cref="P:System.Windows.Controls.Primitives.DataGridFrozenGrid.IsFrozen" /> value from.
        /// </param>
        /// <returns>true if the grid is frozen; otherwise, false. The default is true.</returns>
        public static bool GetIsFrozen(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (bool)element.GetValue(IsFrozenProperty);
        }

        /// <summary>
        /// Sets a value that indicates whether the grid is frozen.
        /// </summary>
        /// <param name="element">The object to set the <see cref="P:System.Windows.Controls.Primitives.DataGridFrozenGrid.IsFrozen" /> value on.</param>
        /// <param name="value">true if <paramref name="element" /> is frozen; otherwise, false.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="element" /> is null.</exception>
        public static void SetIsFrozen(DependencyObject element, bool value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(IsFrozenProperty, value);
        }
    }
}
