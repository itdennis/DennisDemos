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
using System.Windows.Input;

namespace AvePoint.Migrator.Common.Controls
{
    /// <summary>
    ///     Provides information just before a cell exits edit mode.
    /// </summary>
    public class DataGridCellEditEndingEventArgs : EventArgs
    {
        /// <summary>
        ///     Instantiates a new instance of this class.
        /// </summary>
        /// <param name="column">The column of the cell that is about to exit edit mode.</param>
        /// <param name="row">The row container of the cell container that is about to exit edit mode.</param>
        /// <param name="editingElement">The editing element within the cell.</param>
        /// <param name="editingUnit">The editing unit that is about to leave edit mode.</param>
        public DataGridCellEditEndingEventArgs(DataGridColumn column, DataGridRow row, FrameworkElement editingElement, DataGridEditAction editAction)
        {
            _dataGridColumn = column;
            _dataGridRow = row;
            _editingElement = editingElement;
            _editAction = editAction;
        }

        /// <summary>
        ///     When true, prevents the cell from exiting edit mode.
        /// </summary>
        public bool Cancel
        {
            get { return _cancel; }
            set { _cancel = value; }
        }

        /// <summary>
        ///     The column of the cell that is about to exit edit mode.
        /// </summary>
        public DataGridColumn Column
        {
            get { return _dataGridColumn; }
        }

        /// <summary>
        ///     The row container of the cell container that is about to exit edit mode.
        /// </summary>
        public DataGridRow Row
        {
            get { return _dataGridRow; }
        }

        /// <summary>
        ///     The editing element within the cell. 
        /// </summary>
        public FrameworkElement EditingElement
        {
            get { return _editingElement; }
        }

        /// <summary>
        ///     The edit action when leave edit mode.
        /// </summary>
        public DataGridEditAction EditAction
        {
            get { return _editAction; }
        }

        private bool _cancel;
        private DataGridColumn _dataGridColumn;
        private DataGridRow _dataGridRow;
        private FrameworkElement _editingElement;
        private DataGridEditAction _editAction;
    }
}
