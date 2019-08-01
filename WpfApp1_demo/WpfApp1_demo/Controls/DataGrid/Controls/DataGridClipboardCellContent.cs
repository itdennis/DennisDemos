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
    /// This structure encapsulate the cell information necessary when clipboard content is prepared
    /// </summary>
    public struct DataGridClipboardCellContent
    {
        /// <summary>
        /// Creates a new DataGridClipboardCellValue structure containing information about DataGrid cell
        /// </summary>
        /// <param name="row">DataGrid row item containing the cell</param>
        /// <param name="column">DataGridColumn containing the cell</param>
        /// <param name="value">DataGrid cell value</param>
        public DataGridClipboardCellContent(object item, DataGridColumn column, object content)
        {
            _item = item;
            _column = column;
            _content = content;
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

        /// <summary>
        /// Cell content
        /// </summary>
        public object Content
        {
            get { return _content; }
        }

        /// <summary>
        /// Field-by-field comparison to avoid reflection-based ValueType.Equals 
        /// </summary>
        /// <param name="obj"/>
        /// <returns>True iff this and data are equal</returns>
        public override bool Equals(object obj)
        {
            DataGridClipboardCellContent clipboardCellContent;
            if (obj is DataGridClipboardCellContent)
            {
                clipboardCellContent = (DataGridClipboardCellContent)obj;
                            
                return 
                    (_column == clipboardCellContent._column) &&
                    (_content == clipboardCellContent._content) &&
                    (_item == clipboardCellContent._item);
            }

            return false;
        }

        /// <summary>
        /// Return a deterministic hash code
        /// </summary>
        /// <returns>Hash value</returns>
        public override int GetHashCode()
        {
            return 
                _column.GetHashCode() ^
                _content.GetHashCode() ^
                _item.GetHashCode();
        } 

        /// <summary>
        /// Field-by-field comparison to avoid reflection-based ValueType.Equals 
        /// </summary>
        /// <param name="clipboardCellContent1"/>
        /// <param name="clipboardCellContent2"/>
        /// <returns>True iff clipboardCellContent1 and clipboardCellContent2 are equal</returns>
        public static bool operator ==(
            DataGridClipboardCellContent clipboardCellContent1,
            DataGridClipboardCellContent clipboardCellContent2)
        {
            return 
                (clipboardCellContent1._column == clipboardCellContent2._column) &&
                (clipboardCellContent1._content == clipboardCellContent2._content) &&
                (clipboardCellContent1._item == clipboardCellContent2._item);
        }

        /// <summary>
        /// Field-by-field comparison to avoid reflection-based ValueType.Equals 
        /// </summary>
        /// <param name="clipboardCellContent1"/>
        /// <param name="clipboardCellContent2"/>
        /// <returns>True iff clipboardCellContent1 and clipboardCellContent2 are NOT equal</returns>
        public static bool operator !=(
            DataGridClipboardCellContent clipboardCellContent1, 
            DataGridClipboardCellContent clipboardCellContent2)
        {
            return
                (clipboardCellContent1._column != clipboardCellContent2._column) ||
                (clipboardCellContent1._content != clipboardCellContent2._content) ||
                (clipboardCellContent1._item != clipboardCellContent2._item);
        }

        private object _item;
        private DataGridColumn _column;
        private object _content;
    }
}
