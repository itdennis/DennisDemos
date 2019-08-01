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
    ///     Enum to specify the scroll orientation of cells in selective scroll grid
    /// </summary>
    public enum SelectiveScrollingOrientation
    {
        /// <summary>
        /// The cell will not be allowed to get
        /// sctolled in any direction
        /// </summary>
        None = 0,

        /// <summary>
        /// The cell will be allowed to
        /// get scrolled only in horizontal direction
        /// </summary>
        Horizontal = 1,

        /// <summary>
        /// The cell will be allowed to
        /// get scrolled only in vertical directions
        /// </summary>
        Vertical = 2,

        /// <summary>
        /// The cell will be allowed to get
        /// scrolled in all directions
        /// </summary>
        Both = 3
    }
}
