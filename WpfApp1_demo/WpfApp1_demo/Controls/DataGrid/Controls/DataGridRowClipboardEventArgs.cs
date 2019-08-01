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
using System.Collections.ObjectModel;
using System.Text;

namespace AvePoint.Migrator.Common.Controls
{
    /// <summary>
    /// This class encapsulates a selected row information necessary for CopyingRowClipboardContent event
    /// </summary>
    public class DataGridRowClipboardEventArgs : EventArgs
    {
        /// <summary>
        /// Creates DataGridRowClipboardEventArgs object initializing the properties.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="startColumnDisplayIndex"></param>
        /// <param name="endColumnDisplayIndex"></param>
        /// <param name="isColumnHeadersRow"></param>
        public DataGridRowClipboardEventArgs(object item, int startColumnDisplayIndex, int endColumnDisplayIndex, bool isColumnHeadersRow)
        {
            _item = item;
            _startColumnDisplayIndex = startColumnDisplayIndex;
            _endColumnDisplayIndex = endColumnDisplayIndex;
            _isColumnHeadersRow = isColumnHeadersRow;
        }

        internal DataGridRowClipboardEventArgs(object item, int startColumnDisplayIndex, int endColumnDisplayIndex, bool isColumnHeadersRow, int rowIndexHint) :
            this(item, startColumnDisplayIndex, endColumnDisplayIndex, isColumnHeadersRow)
        {
            _rowIndexHint = rowIndexHint;
        }

        /// <summary>
        /// DataGrid row item for which we prepare ClipboardRowContent
        /// </summary>
        public object Item
        {
            get { return _item; }
        }

        /// <summary>
        /// This list should be used to modify, add ot remove a cell content before it gets stored into the clipboard.
        /// </summary>
        public List<DataGridClipboardCellContent> ClipboardRowContent
        {
            get
            {
                if (_clipboardRowContent == null)
                {
                    _clipboardRowContent = new List<DataGridClipboardCellContent>();
                }

                return _clipboardRowContent;
            }
        }

        /// <summary>
        /// This method serialize ClipboardRowContent list into string using the specified format.
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public string FormatClipboardCellValues(string format)
        {
            StringBuilder sb = new StringBuilder();
            int count = ClipboardRowContent.Count;
            for (int i = 0; i < count; i++)
            {
                ClipboardHelper.FormatCell(ClipboardRowContent[i].Content, i == 0 /* firstCell */, i == count - 1 /* lastCell */, sb, format);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Represents the DisplayIndex of the first selected column
        /// </summary>
        public int StartColumnDisplayIndex
        {
            get { return _startColumnDisplayIndex; }
        }

        /// <summary>
        /// Represents the DisplayIndex of the last selected column
        /// </summary>
        public int EndColumnDisplayIndex
        {
            get { return _endColumnDisplayIndex; }
        }

        /// <summary>
        /// This property is true when the ClipboardRowContent represents column headers. In this case Item is null.
        /// </summary>
        public bool IsColumnHeadersRow
        {
            get { return _isColumnHeadersRow; }
        }

        /// <summary>
        ///     If the row index was known at creation time, this will be non-negative.
        /// </summary>
        internal int RowIndexHint
        {
            get { return _rowIndexHint; }
        }

        private int _startColumnDisplayIndex;
        private int _endColumnDisplayIndex;
        private object _item;
        private bool _isColumnHeadersRow;
        private List<DataGridClipboardCellContent> _clipboardRowContent;
        private int _rowIndexHint = -1;
    }
}
