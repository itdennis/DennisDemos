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

namespace AvePoint.Migrator.Common.Controls
{
    /// <summary>
    ///     Struct which holds block of realized column indices.
    /// </summary>
    internal struct RealizedColumnsBlock
    {
        public RealizedColumnsBlock(int startIndex, int endIndex, int startIndexOffset) : this()
        {
            StartIndex = startIndex;
            EndIndex = endIndex;
            StartIndexOffset = startIndexOffset;
        }

        /// <summary>
        ///     Starting index of the block
        /// </summary>
        public int StartIndex 
        { 
            get; private set; 
        }

        /// <summary>
        ///     Ending index of the block
        /// </summary>
        public int EndIndex 
        { 
            get; private set; 
        }

        /// <summary>
        ///     The count of realized columns before this block
        /// </summary>
        public int StartIndexOffset 
        { 
            get; private set; 
        }
    }
}
