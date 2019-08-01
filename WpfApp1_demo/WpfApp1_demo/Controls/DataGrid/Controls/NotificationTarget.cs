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
    /// Enum used to specify where we want an internal property change notification to be routed.
    /// </summary>
    [Flags]
    internal enum NotificationTarget
    {
        None                   = 0x00, // this means don't send it on; likely handle it on the same object that raised the event.
        Cells                  = 0x01,
        CellsPresenter         = 0x02,
        Columns                = 0x04,
        ColumnCollection       = 0x08,
        ColumnHeaders          = 0x10,
        ColumnHeadersPresenter = 0x20,
        DataGrid               = 0x40,
        DetailsPresenter       = 0x80,
        RefreshCellContent     = 0x100,
        RowHeaders             = 0x200,
        Rows                   = 0x400,
        All                    = 0xFFF,
    }
}
