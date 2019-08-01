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


namespace AvePoint.Migrator.Common.Controls
{
    /// <summary>
    /// Provides data for the DisplayModeChanged event.
    /// </summary>
    public class CalendarModeChangedEventArgs : System.Windows.RoutedEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the CalendarModeChangedEventArgs class.
        /// </summary>
        /// <param name="oldMode">Previous value of the property, prior to the event being raised.</param>
        /// <param name="newMode">Current value of the property at the time of the event.</param>
        public CalendarModeChangedEventArgs(CalendarMode oldMode, CalendarMode newMode)
        {
            this.OldMode = oldMode;
            this.NewMode = newMode;
        }

        /// <summary>
        /// Gets the new mode of the Calendar.
        /// </summary>
        public CalendarMode NewMode
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the previous mode of the Calendar.
        /// </summary>
        public CalendarMode OldMode
        {
            get;
            private set;
        }
    }
}
