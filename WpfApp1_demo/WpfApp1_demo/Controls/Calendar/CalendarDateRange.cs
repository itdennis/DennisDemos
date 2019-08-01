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

using System;
using System.ComponentModel;

namespace AvePoint.Migrator.Common.Controls
{
    /// <summary>
    /// Specifies a DateTime range class which has a start and end.
    /// </summary>
    public sealed class CalendarDateRange : INotifyPropertyChanged
    {
        #region Data
        private DateTime _end;
        private DateTime _start;
        #endregion Data

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the CalendarDateRange class.
        /// </summary>
        public CalendarDateRange() :
            this(DateTime.MinValue, DateTime.MaxValue)
        {
        }

        /// <summary>
        /// Initializes a new instance of the CalendarDateRange class which creates a range from a single DateTime value.
        /// </summary>
        /// <param name="day"></param>
        public CalendarDateRange(DateTime day) :
            this(day, day)
        {
        }

        /// <summary>
        /// Initializes a new instance of the CalendarDateRange class which accepts range start and end dates.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public CalendarDateRange(DateTime start, DateTime end)
        {
            _start = start;
            _end = end;
        }
        #endregion Constructors

        #region Public Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Public Properties

        /// <summary>
        /// Specifies the End date of the CalendarDateRange.
        /// </summary>
        public DateTime End
        {
            get
            {
                return CoerceEnd(_start, _end);
            }

            set
            {
                DateTime newEnd = CoerceEnd(_start, value);
                if (newEnd != End)
                {
                    OnChanging(new CalendarDateRangeChangingEventArgs(_start, newEnd));
                    _end = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("End"));
                }
            }
        }

        /// <summary>
        /// Specifies the Start date of the CalendarDateRange.
        /// </summary>
        public DateTime Start
        {
            get
            {
                return _start;
            }

            set
            {
                if (_start != value)
                {
                    DateTime oldEnd = End;
                    DateTime newEnd = CoerceEnd(value, _end);

                    OnChanging(new CalendarDateRangeChangingEventArgs(value, newEnd));

                    _start = value;

                    OnPropertyChanged(new PropertyChangedEventArgs("Start"));

                    if (newEnd != oldEnd)
                    {
                        OnPropertyChanged(new PropertyChangedEventArgs("End"));
                    }
                }
            }
        }

        #endregion Public Properties

        #region Internal Events

        internal event EventHandler<CalendarDateRangeChangingEventArgs> Changing;

        #endregion Internal Events

        #region Internal Methods

        /// <summary>
        /// Returns true if any day in the given DateTime range is contained in the current CalendarDateRange.
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        internal bool ContainsAny(CalendarDateRange range)
        {
            return (range.End >= this.Start) && (this.End >= range.Start);
        }

        #endregion Internal Methods

        #region Private Methods

        private void OnChanging(CalendarDateRangeChangingEventArgs e)
        {
            EventHandler<CalendarDateRangeChangingEventArgs> handler = this.Changing;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Coerced the end parameter to satisfy the start &lt;= end constraint
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns>If start &lt;= end the end parameter otherwise the start parameter</returns>
        private static DateTime CoerceEnd(DateTime start, DateTime end)
        {
            return (DateTime.Compare(start, end) <= 0) ? end : start;
        }

        #endregion Private Methods
    }
}
