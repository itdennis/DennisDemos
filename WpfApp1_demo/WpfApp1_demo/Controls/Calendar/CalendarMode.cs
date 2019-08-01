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


namespace AvePoint.Migrator.Common.Controls
{
    /// <summary>
    /// Specifies values for the different modes of operation of a Calendar. 
    /// </summary>
    public enum CalendarMode
    {
        /// <summary>
        /// The Calendar displays a month at a time.
        /// </summary>
        Month = 0,

        /// <summary>
        ///  The Calendar displays a year at a time.
        /// </summary>
        Year = 1,

        /// <summary>
        /// The Calendar displays a decade at a time.
        /// </summary>
        Decade = 2,
    }
}
