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
using System.Globalization;
using System.Linq;
using System.Diagnostics.CodeAnalysis;

namespace AvePoint.Migrator.Common.Controls
{
    internal class DateTimeParser
    {
        public static bool TryParse(string value, string format, DateTime currentDate, CultureInfo cultureInfo, out DateTime result)
        {
            bool success = false;
            result = currentDate;

            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(format))
                return false;

            var dateTimeString = ResolveDateTimeString(value, format, currentDate, cultureInfo).Trim();

            if (!String.IsNullOrEmpty(dateTimeString))
                success = DateTime.TryParse(dateTimeString, cultureInfo.DateTimeFormat, DateTimeStyles.None, out result);

            if (!success)
                result = currentDate;

            return success;
        }

        [SuppressMessage("FxCopCustomRules", "C100007:SpellCheckStringValues", Justification = "These are special tag in MigratorTool-Control")]
        private static string ResolveDateTimeString(string dateTime, string format, DateTime currentDate, CultureInfo cultureInfo)
        {
            Dictionary<string, string> dateParts = GetDateParts(currentDate, cultureInfo);
            string[] timeParts = new string[3] { currentDate.Hour.ToString(), currentDate.Minute.ToString(), currentDate.Second.ToString() };
            string designator = "";
            string[] dateTimeSeparators = new string[] { ",", " ", "-", ".", "/", cultureInfo.DateTimeFormat.DateSeparator, cultureInfo.DateTimeFormat.TimeSeparator };

            ResolveSortableDateTimeString(ref dateTime, ref format, cultureInfo);

            var dateTimeParts = dateTime.Split(dateTimeSeparators, StringSplitOptions.RemoveEmptyEntries).ToList();
            var formats = format.Split(dateTimeSeparators, StringSplitOptions.RemoveEmptyEntries).ToList();

            //something went wrong
            if (dateTimeParts.Count != formats.Count)
                return string.Empty;

            for (int i = 0; i < formats.Count; i++)
            {
                var f = formats[i];
                if (!f.Contains("ddd") && !f.Contains("GMT"))
                {
                    if (f.Contains("M"))
                        dateParts["Month"] = dateTimeParts[i];
                    else if (f.Contains("d"))
                        dateParts["Day"] = dateTimeParts[i];
                    else if (f.Contains("y"))
                    {
                        dateParts["Year"] = dateTimeParts[i];

                        if (dateParts["Year"].Length == 2)
                            dateParts["Year"] = string.Format("{0}{1}", currentDate.Year / 100, dateParts["Year"]);
                    }
                    else if (f.Contains("h") || f.Contains("H"))
                        timeParts[0] = dateTimeParts[i];
                    else if (f.Contains("m"))
                        timeParts[1] = dateTimeParts[i];
                    else if (f.Contains("s"))
                        timeParts[2] = dateTimeParts[i];
                    else if (f.Contains("t"))
                        designator = dateTimeParts[i];
                }
            }

            var date = string.Join(cultureInfo.DateTimeFormat.DateSeparator, dateParts.Select(x => x.Value).ToArray());
            var time = string.Join(cultureInfo.DateTimeFormat.TimeSeparator, timeParts);

            return String.Format("{0} {1} {2}", date, time, designator);
        }

        private static void ResolveSortableDateTimeString(ref string dateTime, ref string format, CultureInfo cultureInfo)
        {
            if (format == cultureInfo.DateTimeFormat.SortableDateTimePattern)
            {
                format = format.Replace("'", "").Replace("T", " ");
                dateTime = dateTime.Replace("'", "").Replace("T", " ");
            }
            else if (format == cultureInfo.DateTimeFormat.UniversalSortableDateTimePattern)
            {
                format = format.Replace("'", "").Replace("Z", "");
                dateTime = dateTime.Replace("'", "").Replace("Z", "");
            }
        }

        private static Dictionary<string, string> GetDateParts(DateTime currentDate, CultureInfo cultureInfo)
        {
            Dictionary<string, string> dateParts = new Dictionary<string, string>();
            var dateFormatParts = cultureInfo.DateTimeFormat.ShortDatePattern.Split(new string[] { cultureInfo.DateTimeFormat.DateSeparator }, StringSplitOptions.RemoveEmptyEntries).ToList();
            dateFormatParts.ForEach(item =>
            {
                string key = string.Empty;
                string value = string.Empty;

                if (item.Contains("M"))
                {
                    key = "Month";
                    value = currentDate.Month.ToString();
                }
                else if (item.Contains("d"))
                {
                    key = "Day";
                    value = currentDate.Day.ToString();
                }
                else if (item.Contains("y"))
                {
                    key = "Year";
                    value = currentDate.Year.ToString();
                }
                dateParts.Add(key, value);
            });
            return dateParts;
        }
    }
}
