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
using System.ComponentModel;
using System.Globalization;
using System.Windows;

namespace AvePoint.Migrator.Common.Controls
{
    /// <summary>
    ///     Used to indicate the type of value that DataGridLength is holding.
    /// </summary>
    public enum DataGridLengthUnitType 
    {
        // Keep in sync with DataGridLengthConverter.UnitStrings

        /// <summary>
        ///     The value indicates that content should be calculated based on the 
        ///     unconstrained sizes of all cells and header in a column.
        /// </summary>
        Auto,

        /// <summary>
        ///     The value is expressed in pixels.
        /// </summary>
        Pixel,

        /// <summary>
        ///     The value indicates that content should be be calculated based on the
        ///     unconstrained sizes of all cells in a column.
        /// </summary>
        SizeToCells,

        /// <summary>
        ///     The value indicates that content should be calculated based on the
        ///     unconstrained size of the column header.
        /// </summary>
        SizeToHeader,

        /// <summary>
        ///     The value is expressed as a weighted proportion of available space.
        /// </summary>
        Star,
    }
}
