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
using System.Windows.Documents;

namespace AvePoint.Migrator.Common.Controls
{
    public class AUITimeZone
    {
        static AUITimeZone()
        {
            GetAllTimeZones();
        }

        public AUITimeZone()
        {
      
        }

        public AUITimeZone(string id)
            : this()
        {
            List<AUITimeZone> zones = GetAllTimeZones();
            foreach (AUITimeZone z in zones)
            {
                if (z.Id.Equals(id))
                {
                    this.Id = z.Id;
                    this.DisplayName = z.DisplayName;
                    this.SupportsDaylightSavingTime = z.SupportsDaylightSavingTime;
                    this.BaseUtcOffset = z.BaseUtcOffset;
                    this.Zone = z.Zone;
                    this.HashCode = z.HashCode;
                    this.X64HashCode = z.X64HashCode;
                    break;
                }
            }
        }

        public static AUITimeZone Local
        {
            get
            {
                return AUITimeZone.GetCurrentTimeZone();
            }
        }

        public bool SupportsDaylightSavingTime
        {
            get;
            set;
        }

        public string DisplayName
        {
            get;
            set;
        }

        public string Zone
        {
            get;
            set;
        }

        public string Id
        {
            get;
            set;
        }

        public TimeSpan BaseUtcOffset
        {
            get;
            set;
        }

        public long HashCode
        {
            get;
            set;
        }

        public long X64HashCode
        {
            get;
            set;
        }

        public static List<AUITimeZone> AllTimeZones
        {
            get;
            set;
        }

        //AveTimeZone已去掉
        //public bool IsDaylightSavingTime { get; set; }

        public static AUITimeZone GetCurrentTimeZone()
        {
            List<AUITimeZone> allZones = GetAllTimeZones();
            TimeZoneInfo localTimeZone = TimeZoneInfo.Local;
            int curHashCode = localTimeZone.GetHashCode();
            foreach (AUITimeZone zone in allZones)
            {
                // modified by jptian: 修改匹配逻辑为根据HashCode匹配 -- 2011.10.10
                if (zone.HashCode == curHashCode || zone.X64HashCode == curHashCode)
                {
                    return zone;
                }
            }

            // 当没有匹配上HashCode的时候，使用时区匹配
            TimeSpan curTimeSpan = localTimeZone.BaseUtcOffset;
            foreach (AUITimeZone zone in allZones)
            {
                if (zone.BaseUtcOffset == curTimeSpan)
                {
                    return zone;
                }
            }

            // 都未匹配上则使用格林尼治时间
            return GetTimeZone("GMT Standard Time");
        }

        /// <summary>
        /// 根据时区的ID获取到时区信息
        /// </summary>
        /// <param name="id">需要获取的TimeZone的ID，实际值对应Windows中时区信息中的StandardName</param>
        /// <returns>对应时区信息，如果没有找到，则返回null</returns>
        public static AUITimeZone GetTimeZone(string id)
        {
            List<AUITimeZone> list = GetAllTimeZones();
            foreach (AUITimeZone zone in list)
            {
                if (zone.Id == id)
                {
                    return zone;
                }
            }
            return null;
        }

        /// <summary>
        /// 根据原始时间和时区，转到对应时区的时间
        /// </summary>
        /// <param name="sourceDate">原始时间</param>
        /// <param name="sourceTimeZone">原始时区</param>
        /// <param name="desTimeZone">目标时区</param>
        /// <returns>目标时区的时间</returns>
        public static DateTime ConvertDate(DateTime sourceDate, AUITimeZone sourceTimeZone, AUITimeZone desTimeZone)
        {
            // 转到UTC时间
            DateTime utcDate = sourceDate.AddHours(sourceTimeZone.BaseUtcOffset.Hours * (-1))
                    .AddMinutes(sourceTimeZone.BaseUtcOffset.Minutes * (-1));
            // 转到目标时间
            return utcDate.AddHours(desTimeZone.BaseUtcOffset.Hours)
                    .AddMinutes(desTimeZone.BaseUtcOffset.Minutes);
        }

        public override bool Equals(object obj)
        {
            if (obj is AUITimeZone)
            {
                return ((AUITimeZone)obj).Id.Equals(this.Id);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool IsUsingDaylightSavingTime()
        {
            DateTime summer = new DateTime(DateTime.Now.Year, 7, 1);
            DateTime winter = new DateTime(DateTime.Now.Year, 1, 1);
            return TimeZoneInfo.Local.IsDaylightSavingTime(summer) || TimeZoneInfo.Local.IsDaylightSavingTime(winter);
        }

        //key为TimeZone的Id值（唯一），Value为32位HashCode的值
        //原来使用X64HashCode不可用原因：不同机器的X64HashCode可能不同，故不能作为key
        public static Dictionary<string, long> HashCodeDic;

        public static void InitHashCodeDic()
        {
            HashCodeDic =
            new Dictionary<string, long>(){
             // ============================= 000 ==============================
            {"Dateline Standard Time",-514555984},
            {"UTC-11",-1086848243},            
            {"Samoa Standard Time",-758128916},
            {"Hawaiian Standard Time",-1369885288},
            {"Alaskan Standard Time",1929871915},
            {"Pacific Standard Time (Mexico)",1966587549},
            {"Pacific Standard Time",-20620119},
            {"US Mountain Standard Time",-1011079097},
            {"Mountain Standard Time (Mexico)",-1549238612},
            {"Mountain Standard Time",867061357},
            // ============================== 010 ==============================
            {"Central America Standard Time",-2030645691},
            {"Central Standard Time",-1472553907},
            {"Central Standard Time (Mexico)",-774670651},
            {"Canada Central Standard Time",398800922},
            {"SA Pacific Standard Time",-51503002},
            {"Eastern Standard Time",1575136615},
            {"US Eastern Standard Time",-1920434229},
            {"Venezuela Standard Time",-1759550415},
            {"Paraguay Standard Time",781531414},
            {"Atlantic Standard Time",1688170723},
             // ============================== 020 ==============================
            {"Central Brazilian Standard Time",780897488},
            {"SA Western Standard Time",277435212},
            {"Pacific SA Standard Time",672784620},
            {"Newfoundland Standard Time",-1290608544},
            {"E. South America Standard Time",1061339055},
            {"Argentina Standard Time",1409949984},
            {"SA Eastern Standard Time",849007863},
            {"Greenland Standard Time",-1010907336},
            {"Montevideo Standard Time",-91072651},
            {"UTC-02",-1087044852},
             // ============================== 030 ==============================
            {"Mid-Atlantic Standard Time",1857440091},
            {"Azores Standard Time",1289301972},
            {"Cape Verde Standard Time",524845130},
            {"Morocco Standard Time",-985979243},
            {"UTC",-884914970},
            {"GMT Standard Time",1243702734},
            {"Greenwich Standard Time",-2134877238},
            {"W. Europe Standard Time",417657061},
            {"Central Europe Standard Time",-1072506847},
            {"Romance Standard Time",1951788994},
            // ============================== 040 ==============================
            {"Central European Standard Time",587654972},
            {"W. Central Africa Standard Time",1596262695},
            {"Namibia Standard Time",390533655},
            {"Jordan Standard Time",1796499939},
            {"GTB Standard Time",1141185203},
            {"Middle East Standard Time",-152181505},
            {"Egypt Standard Time",137358981},
            {"Syria Standard Time",187161848},
            {"South Africa Standard Time",-1896308101},
            {"FLE Standard Time",-1437804775},
            // ============================== 050 ==============================
            //{-492405966,new DisplayNameX64HashCode(){DisplayName = I18NEntity.Get("Common", "(UTC+02:00) Istanbul"),HashCode = -1339675810}}, //AveTimeZone.DisplayName=""
            {"Turkey Standard Time",-1339675810},
            {"Israel Standard Time",203046616},
            {"E. Europe Standard Time",567328507},
            {"Arabic Standard Time",2030831431},
            //{-1998590704,new DisplayNameX64HashCode(){DisplayName = I18NEntity.Get("Common", "(UTC+03:00) Kaliningrad"),HashCode = 2046974450}},//AveTimeZone.DisplayName=""
            {"Kaliningrad Standard Time",2046974450},
            {"Arab Standard Time",-2011598098},
            {"E. Africa Standard Time",-72981115},
            {"Iran Standard Time",2018412928},
            {"Arabian Standard Time",271408839},
            {"Azerbaijan Standard Time",457374418},
            // ============================== 060 ==============================
            {"Russian Standard Time",-581402749},
            {"Mauritius Standard Time",-1818159890},
            {"Georgian Standard Time",1022731794},
            {"Caucasus Standard Time",1602929463},
            {"Afghanistan Standard Time",858250651},
            {"Pakistan Standard Time",-1714669205},
            {"West Asia Standard Time",153346398},
            {"India Standard Time",-270438407},
            {"Sri Lanka Standard Time",-2049351688},
            {"Nepal Standard Time",296234527},
            // ============================== 070 ==============================
            {"Central Asia Standard Time",199744320},
            {"Bangladesh Standard Time",-862257735},
            {"Ekaterinburg Standard Time",-74019787},
            {"Myanmar Standard Time",-851594192},
            {"SE Asia Standard Time",1782768786},
            {"N. Central Asia Standard Time",-1887523373},
            {"China Standard Time",779965156},
            {"North Asia Standard Time",1141291926},
            {"Singapore Standard Time",-981867726},
            {"W. Australia Standard Time",-963623640},
            // ============================== 080 ==============================
            {"Taipei Standard Time",-1152086148},
            {"Ulaanbaatar Standard Time",-578634598},
            {"North Asia East Standard Time",-1853844139},
            {"Tokyo Standard Time",2058205788},
            {"Korea Standard Time",1859384373},
            {"Cen. Australia Standard Time",-409381159},
            {"AUS Central Standard Time",-1265531735},
            {"E. Australia Standard Time",-873538331},
            {"AUS Eastern Standard Time",13992051},
            {"West Pacific Standard Time",1703400787},
            // ============================== 090 ==============================
            {"Tasmania Standard Time",556097360},
            {"Yakutsk Standard Time",-846996468},
            {"Central Pacific Standard Time",-1503639322},
            {"Vladivostok Standard Time",-1534558847},
            {"New Zealand Standard Time",2053458808},
            {"UTC+12",-704707827},
            {"Fiji Standard Time",-1078326823},
            {"Magadan Standard Time",-1553516668},
            {"Kamchatka Standard Time",-1759089867},
            {"Tonga Standard Time",1773130869},
            // ============================== 100 ==============================
            //通过AveTimeZone取值后新加的
            //{1208864541,new DisplayNameX64HashCode(){DisplayName = I18NEntity.Get("Common", "(UTC-03:00) Salvador"),HashCode = 1248183795}},//AveTimeZone.DisplayName=""
            {"Bahia Standard Time",1248183795},
            };
        }

        public static List<AUITimeZone> GetAllTimeZones()
        {
            //注：X64HashCode个别不准或者为空，以HashCodeDic里的值为准
            if (AllTimeZones == null)
            {
                #region 废弃代码

                var timeZones = TimeZoneInfo.GetSystemTimeZones();

                var rv = new List<AUITimeZone>();
                foreach (var item in timeZones)
                {
                    rv.Add(new AUITimeZone 
                    {
                        Id = item.Id,
                        DisplayName = item.DisplayName,
                        Zone = item.StandardName,
                        BaseUtcOffset = item.BaseUtcOffset,
                        SupportsDaylightSavingTime = item.SupportsDaylightSavingTime,
                        HashCode = item.GetHashCode()
                    });
                }

                AllTimeZones = rv;
                #endregion 废弃代码
                return  rv;
            }
            return AllTimeZones;

        }

        /// <summary>
        /// 为支持VPAT，重写ToString方法
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.DisplayName;
        }
    }
}
