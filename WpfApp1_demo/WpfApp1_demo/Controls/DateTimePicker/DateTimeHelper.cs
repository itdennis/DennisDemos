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
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

namespace AvePoint.Migrator.Common.Controls
{
    // NOTICE: This date time helper assumes it is working in a Gregorian calendar
    //         If we ever support non Gregorian calendars this class would need to be redesigned
    internal static class DateTimeHelper
    {
        private static System.Globalization.Calendar cal = new GregorianCalendar();

        public static DateTime? AddDays(DateTime time, int days)
        {
            try
            {
                return cal.AddDays(time, days);
            }
            catch (System.ArgumentException)
            {
                return null;
            }
        }

        public static DateTime? AddMonths(DateTime time, int months)
        {
            try
            {
                return cal.AddMonths(time, months);
            }
            catch (System.ArgumentException)
            {
                return null;
            }
        }

        public static DateTime? AddYears(DateTime time, int years)
        {
            try
            {
                return cal.AddYears(time, years);
            }
            catch (System.ArgumentException)
            {
                return null;
            }
        }

        public static DateTime? SetYear(DateTime date, int year)
        {
            return DateTimeHelper.AddYears(date, year - date.Year);
        }

        public static DateTime? SetYearMonth(DateTime date, DateTime yearMonth)
        {
            DateTime? target = SetYear(date, yearMonth.Year);
            if (target.HasValue)
            {
                target = DateTimeHelper.AddMonths(target.Value, yearMonth.Month - date.Month);
            }

            return target;
        }

        public static int CompareDays(DateTime dt1, DateTime dt2)
        {
            return DateTime.Compare(DiscardTime(dt1).Value, DiscardTime(dt2).Value);
        }

        public static int CompareYearMonth(DateTime dt1, DateTime dt2)
        {
            return ((dt1.Year - dt2.Year) * 12) + (dt1.Month - dt2.Month);
        }

        public static int DecadeOfDate(DateTime date)
        {
            return date.Year - (date.Year % 10);
        }

        public static DateTime DiscardDayTime(DateTime d)
        {
            return new DateTime(d.Year, d.Month, 1, 0, 0, 0);
        }

        public static DateTime? DiscardTime(DateTime? d)
        {
            if (d == null)
            {
                return null;
            }

            return d.Value.Date;
        }

        public static int EndOfDecade(DateTime date)
        {
            return DecadeOfDate(date) + 9;
        }

        public static DateTimeFormatInfo GetCurrentDateFormat()
        {
            return GetDateFormat(CultureInfo.CurrentCulture);
        }

        internal static CultureInfo GetCulture(FrameworkElement element)
        {
            CultureInfo culture;
            if (DependencyPropertyHelper.GetValueSource(element, FrameworkElement.LanguageProperty).BaseValueSource != BaseValueSource.Default)
            {
                culture = GetCultureInfo(element);
            }
            else
            {
                culture = CultureInfo.CurrentCulture;
            }
            return culture;
        }

        // ------------------------------------------------------------------
        // Retrieve CultureInfo property from specified element.
        // ------------------------------------------------------------------
        internal static CultureInfo GetCultureInfo(DependencyObject element)
        {
            XmlLanguage language = (XmlLanguage)element.GetValue(FrameworkElement.LanguageProperty);
            try
            {
                return language.GetSpecificCulture();
            }
            catch (InvalidOperationException)
            {
                // We default to en-US if no part of the language tag is recognized.
                return CultureInfo.ReadOnly(new CultureInfo("en-us", false));
            }
        }

        internal static DateTimeFormatInfo GetDateFormat(CultureInfo culture)
        {
            if (culture.Calendar is GregorianCalendar)
            {
                return culture.DateTimeFormat;
            }
            else
            {
                GregorianCalendar foundCal = null;
                DateTimeFormatInfo dtfi = null;

                foreach (System.Globalization.Calendar cal in culture.OptionalCalendars)
                {
                    if (cal is GregorianCalendar)
                    {
                        // Return the first Gregorian calendar with CalendarType == Localized
                        // Otherwise return the first Gregorian calendar
                        if (foundCal == null)
                        {
                            foundCal = cal as GregorianCalendar;
                        }

                        if (((GregorianCalendar)cal).CalendarType == GregorianCalendarTypes.Localized)
                        {
                            foundCal = cal as GregorianCalendar;
                            break;
                        }
                    }
                }


                if (foundCal == null)
                {
                    // if there are no GregorianCalendars in the OptionalCalendars list, use the invariant dtfi
                    dtfi = ((CultureInfo)CultureInfo.InvariantCulture.Clone()).DateTimeFormat;
                    dtfi.Calendar = new GregorianCalendar();
                }
                else
                {
                    dtfi = ((CultureInfo)culture.Clone()).DateTimeFormat;
                    dtfi.Calendar = foundCal;
                }

                return dtfi;
            }
        }

        // returns if the date is included in the range
        public static bool InRange(DateTime date, CalendarDateRange range)
        {
            return InRange(date, range.Start, range.End);
        }

        // returns if the date is included in the range
        public static bool InRange(DateTime date, DateTime start, DateTime end)
        {
            Debug.Assert(DateTime.Compare(start, end) < 1);

            if (CompareDays(date, start) > -1 && CompareDays(date, end) < 1)
            {
                return true;
            }

            return false;
        }

        public static string ToDayString(DateTime? date, CultureInfo culture)
        {
            string result = string.Empty;
            DateTimeFormatInfo format = GetDateFormat(culture);

            if (date.HasValue && format != null)
            {
                result = date.Value.Day.ToString(format);
            }

            return result;
        }

        public static string ToDecadeRangeString(int decade, CultureInfo culture)
        {
            string result = string.Empty;
            DateTimeFormatInfo format = culture.DateTimeFormat;

            if (format != null)
            {
                int decadeEnd = decade + 9;
                result = decade.ToString(format) + "-" + decadeEnd.ToString(format);
            }

            return result;
        }

        public static string ToYearMonthPatternString(DateTime? date, CultureInfo culture)
        {
            string result = string.Empty;
            DateTimeFormatInfo format = GetDateFormat(culture);

            if (date.HasValue && format != null)
            {
                result = date.Value.ToString(format.YearMonthPattern, format);
            }

            return result;
        }

        public static string ToYearString(DateTime? date, CultureInfo culture)
        {
            string result = string.Empty;
            DateTimeFormatInfo format = GetDateFormat(culture);

            if (date.HasValue && format != null)
            {
                result = date.Value.Year.ToString(format);
            }

            return result;
        }

        public static string ToAbbreviatedMonthString(DateTime? date, CultureInfo culture)
        {
            string result = string.Empty;
            DateTimeFormatInfo format = GetDateFormat(culture);

            if (date.HasValue && format != null)
            {
                string[] monthNames = format.AbbreviatedMonthNames;
                if (monthNames != null && monthNames.Length > 0)
                {
                    result = monthNames[(date.Value.Month - 1) % monthNames.Length];
                }
            }

            return result;
        }

        public static string ToLongDateString(DateTime? date, CultureInfo culture)
        {
            string result = string.Empty;
            DateTimeFormatInfo format = GetDateFormat(culture);

            if (date.HasValue && format != null)
            {
                result = date.Value.Date.ToString(format.LongDatePattern, format);
            }

            return result;
        }

        public static int CompareTime(DateTime d1, DateTime d2)
        {
            DateTime today = DateTime.Today;
            DateTime d11 = new DateTime(today.Year, today.Month, today.Day, d1.Hour, d1.Minute, d1.Second);
            DateTime d21 = new DateTime(today.Year, today.Month, today.Day, d2.Hour, d2.Minute, d2.Second);
            return DateTime.Compare(d11, d21);
        }

        public static int CompareDate(DateTime d1, DateTime d2)
        {
            DateTime d11 = new DateTime(d1.Year, d1.Month, d1.Day, 0, 0, 0);
            DateTime d21 = new DateTime(d2.Year, d2.Month, d2.Day, 0, 0, 0);

            return DateTime.Compare(d11, d21);
        }

        public static DateTime FixDateAndTime(DateTime date, DateTime time)
        {
            return new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);
        }

        public static string AppendZero(int i)
        {
            if (i < 10)
            {
                return "0" + i.ToString();
            }
            else
            {
                return i.ToString();
            }
        }
    }

    public class KeyEventHelper
    {
        /// <summary>
        /// 数字按钮的集合
        /// </summary>
        private static readonly List<Key> NumberKeys =
            new List<Key> { Key.D0, Key.D1, Key.D2, Key.D3, 
                Key.D4, Key.D5, Key.D6, Key.D7, Key.D8, Key.D9 ,
                Key.NumPad0, Key.NumPad1, Key.NumPad2, Key.NumPad3, 
                Key.NumPad4, Key.NumPad5, Key.NumPad6, Key.NumPad7, 
                Key.NumPad8, Key.NumPad9 };

        /// <summary>
        /// 方向键按钮的集合
        /// </summary>
        private static readonly List<Key> Directionkeys =
            new List<Key> { Key.Left, Key.Up, Key.Down, Key.Right };

        /// <summary>
        /// 判断key是否是数字key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsNumberKey(Key key)
        {
            return NumberKeys.Contains(key);
        }

        /// <summary>
        /// 判断key是否是方向键
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsDirectionkey(Key key)
        {
            return Directionkeys.Contains(key);
        }

        /// <summary>
        /// 判断shift是否被按下
        /// </summary>
        /// <returns></returns>
        public static bool IsShiftPushedDown()
        {
            return (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift;
        }

        /// <summary>
        /// 判断Control是否被按下
        /// </summary>
        /// <returns></returns>
        public static bool IsControlPushedDown()
        {
            return (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control;
        }

        /// <summary>
        /// 判断Alt是否被按下
        /// </summary>
        /// <returns></returns>
        public static bool IsAltPushedDown()
        {
            return (Keyboard.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt;
        }
    }
}
