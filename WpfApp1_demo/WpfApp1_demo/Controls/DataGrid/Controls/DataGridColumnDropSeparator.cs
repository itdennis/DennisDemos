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
using AvePoint.Migrator.Common.Controls.Primitives;

using MS.Internal;

namespace AvePoint.Migrator.Common.Controls
{
    /// <summary>
    /// The separator used to indicate drop location during column header drag-drop
    /// </summary>
    internal class DataGridColumnDropSeparator : Separator
    {
        #region Constructors

        static DataGridColumnDropSeparator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(DataGridColumnDropSeparator), 
                new FrameworkPropertyMetadata(typeof(DataGridColumnDropSeparator)));

            WidthProperty.OverrideMetadata(
                typeof(DataGridColumnDropSeparator), 
                new FrameworkPropertyMetadata(null, new CoerceValueCallback(OnCoerceWidth)));
            
            HeightProperty.OverrideMetadata(
                typeof(DataGridColumnDropSeparator), 
                new FrameworkPropertyMetadata(null, new CoerceValueCallback(OnCoerceHeight)));
        }

        #endregion

        #region Static Methods

        private static object OnCoerceWidth(DependencyObject d, object baseValue)
        {
            double width = (double)baseValue;
            if (DoubleUtil.IsNaN(width))
            {
                return 2.0;
            }

            return baseValue;
        }

        private static object OnCoerceHeight(DependencyObject d, object baseValue)
        {
            double height = (double)baseValue;
            DataGridColumnDropSeparator separator = (DataGridColumnDropSeparator)d;
            if (separator._referenceHeader != null && DoubleUtil.IsNaN(height))
            {
                return separator._referenceHeader.ActualHeight;
            }

            return baseValue;
        }

        #endregion

        #region Properties

        internal DataGridColumnHeader ReferenceHeader
        {
            get
            {
                return _referenceHeader;
            }

            set
            {
                _referenceHeader = value;
            }
        }

        #endregion

        #region Data

        private DataGridColumnHeader _referenceHeader;

        #endregion
    }
}
