﻿/********************************************************************
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
using System.Windows.Input;

namespace AvePoint.Migrator.Common.Controls
{
    /// <summary>
    ///     Provides information about a cell that has just entered edit mode.
    /// </summary>
    public class DataGridPreparingCellForEditEventArgs : EventArgs
    {
        /// <summary>
        ///     Constructs a new instance of these event arguments.
        /// </summary>
        /// <param name="column">The column of the cell that just entered edit mode.</param>
        /// <param name="row">The row container that contains the cell container that just entered edit mode.</param>
        /// <param name="editingEventArgs">The event arguments, if any, that led to the cell being placed in edit mode.</param>
        /// <param name="cell">The cell container that just entered edit mode.</param>
        /// <param name="editingElement">The editing element within the cell container.</param>
        public DataGridPreparingCellForEditEventArgs(DataGridColumn column, DataGridRow row, RoutedEventArgs editingEventArgs, FrameworkElement editingElement)
        {
            _dataGridColumn = column;
            _dataGridRow = row;
            _editingEventArgs = editingEventArgs;
            _editingElement = editingElement;
        }

        /// <summary>
        ///     The column of the cell that just entered edit mode.
        /// </summary>
        public DataGridColumn Column
        {
            get { return _dataGridColumn; }
        }

        /// <summary>
        ///     The row container that contains the cell container that just entered edit mode.
        /// </summary>
        public DataGridRow Row
        {
            get { return _dataGridRow; }
        }

        /// <summary>
        ///     The event arguments, if any, that led to the cell being placed in edit mode.
        /// </summary>
        public RoutedEventArgs EditingEventArgs
        {
            get { return _editingEventArgs; }
        }

        /// <summary>
        ///     The editing element within the cell container.
        /// </summary>
        public FrameworkElement EditingElement
        {
            get { return _editingElement; }
        }

        private DataGridColumn _dataGridColumn;
        private DataGridRow _dataGridRow;
        private RoutedEventArgs _editingEventArgs;
        private FrameworkElement _editingElement;
    }
}
