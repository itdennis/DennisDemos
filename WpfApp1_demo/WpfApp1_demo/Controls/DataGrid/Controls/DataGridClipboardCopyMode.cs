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

namespace AvePoint.Migrator.Common.Controls
{
    /// <summary>
    /// Defines modes that indicate how DataGrid content is copied to the Clipboard. 
    /// </summary>
    public enum DataGridClipboardCopyMode
    {
        /// <summary>
        /// Copying to the Clipboard is disabled.
        /// </summary>
        None,

        /// <summary>
        /// The text values of selected cells can be copied to the Clipboard. Column header is not included. 
        /// </summary>
        ExcludeHeader,

        /// <summary>
        /// The text values of selected cells can be copied to the Clipboard. Column header is included for columns that contain selected cells.  
        /// </summary>
        IncludeHeader,
    }
}
