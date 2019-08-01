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

namespace AvePoint.Migrator.Common.Controls
{
    /// <summary>
    ///     The accepted selection units used in selection on a DataGrid.
    /// </summary>
    public enum DataGridSelectionUnit
    {
        /// <summary>
        ///     Only cells are selectable.
        ///     Clicking on a cell will select the cell.
        ///     Clicking on row or column headers does nothing.
        /// </summary>
        Cell,

        /// <summary>
        ///     Only full rows are selectable.
        ///     Clicking on row headers or on cells will select the whole row.
        /// </summary>
        FullRow,

        /// <summary>
        ///     Cells and rows are selectable.
        ///     Clicking on a cell will select the cell. Selecting all cells in the row will not select the row.
        ///     Clicking on a row header will select the row and all cells in the row.
        /// </summary>
        CellOrRowHeader
    }
}
