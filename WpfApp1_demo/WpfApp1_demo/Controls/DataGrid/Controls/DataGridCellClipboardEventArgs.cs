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
using System.Collections.Generic;
using System.Text;

namespace AvePoint.Migrator.Common.Controls
{
    /// <summary>
    /// This class encapsulates a cell information necessary for CopyingCellClipboardContent and PastingCellClipboardContent events
    /// </summary>
    public class DataGridCellClipboardEventArgs : EventArgs
    {
        /// <summary>
        /// Construct DataGridCellClipboardEventArgs object
        /// </summary>
        /// <param name="item"></param>
        /// <param name="column"></param>
        /// <param name="content"></param>
        public DataGridCellClipboardEventArgs(object item, DataGridColumn column, object content)
        {
            _item = item;
            _column = column;
            _content = content;
        }

        /// <summary>
        /// Content of the cell to be set or get from clipboard
        /// </summary>
        public object Content
        {
            get { return _content; }
            set { _content = value; }
        }

        /// <summary>
        /// DataGrid row item containing the cell
        /// </summary>
        public object Item
        {
            get { return _item; }
        }

        /// <summary>
        /// DataGridColumn containing the cell
        /// </summary>
        public DataGridColumn Column
        {
            get { return _column; }
        }

        private object _content;
        private object _item;
        private DataGridColumn _column;
    }
}
