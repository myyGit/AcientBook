using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Common
{
    public static class DateTimeUtils
    {
        private static readonly ChineseLunisolarCalendar ChineseCalendar = new ChineseLunisolarCalendar();
        /// <summary>
        /// 返回两个日期间共有几天
        /// </summary>
        /// <param name="datetimeBegin"></param>
        /// <param name="datetimeEnd"></param>
        /// <returns></returns>
        public static int GetWeek(DateTime datetimeBegin, DateTime datetimeEnd)
        {
            int weekstr = 0;

            TimeSpan ts1 = new TimeSpan(datetimeBegin.Ticks);
            TimeSpan ts2 = new TimeSpan(datetimeEnd.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();

            int a1 = ts.Days;

            if (a1 <= 7)
            {
                weekstr = a1;
            }
            else
            {
                int w = a1 / 7;
                int d = a1 % 7;
                if (d == 0)
                {
                    weekstr = w*7;
                }
                else
                {
                    weekstr = w * 7 + d;
                }
            }
            return weekstr;
        }

        /// <summary>
        /// 根据两个日期获得XX年XX月XX天
        /// </summary>
        /// <param name="dtday"></param>
        /// <param name="dtNow"></param>
        /// <returns></returns>
        public static string GetYearMonDay(DateTime dtday, DateTime dtNow)
        {
            string strAge = string.Empty;
            int intYear = 0;
            int intMonth = 0;
            int intDay = 0;

            intDay = dtNow.Day - dtday.Day;
            if (intDay < 0)
            {
                dtNow = dtNow.AddMonths(-1);
                intDay += DateTime.DaysInMonth(dtNow.Year, dtNow.Month);
            }
            intMonth = dtNow.Month - dtday.Month;
            if (intMonth < 0)
            {
                intMonth += 12;
                dtNow = dtNow.AddYears(-1);
            }
            intYear = dtNow.Year - dtday.Year;
            if (intYear >= 1)
            {
                strAge = string.Format("{0}年", intYear.ToString());
            }
            if (intMonth > 0 && intYear <= 100)
            {
                strAge += string.Format("{0}月", intMonth.ToString());
            }
            if (intDay >= 0)
            {
                if (strAge.Length == 0 || intDay > 0)
                {
                    strAge += string.Format("{0}天", intDay.ToString());
                }
            }
            return strAge;
        }
        /// <summary>
        /// 计算当前日期对应的农历年份
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static int ReckonYear(DateTime datetime)
        {
            int year = ChineseCalendar.GetYear(datetime);
            return year;
        }
        /// <summary>
        /// 计算当前日期对应的农历月份
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static int ReckonMonth(DateTime datetime)
        {
            int year = ChineseCalendar.GetYear(datetime);
            int month = ChineseCalendar.GetMonth(datetime);
            int day = ChineseCalendar.GetDayOfMonth(datetime);
            //获取闰月， 0 则表示没有闰月
            int leapMonth = ChineseCalendar.GetLeapMonth(year);
            if (leapMonth > 0)
            {
                leapMonth--;    //此时值为当前年份的闰月
                if (month - 1 == leapMonth)
                {
                    //闰月
                    month--;
                }
                else if (month > leapMonth)
                {
                    month--;
                }
                return month;
            }
            return month;
        }
        /// <summary>
        /// 计算当前日期对应年份的闰月
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static int ReckonLeapMonth(DateTime datetime)
        {
            int year = ChineseCalendar.GetYear(datetime);
            //获取闰月， 0 则表示没有闰月
            int leapMonth = ChineseCalendar.GetLeapMonth(year);
            return leapMonth == 0 ? 0 : leapMonth + 1;
        }
        /// <summary>
        /// 计算当前日期对应的农历日期Day
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static int ReckonDay(DateTime datetime)
        {
            int day = ChineseCalendar.GetDayOfMonth(datetime);
            return day;
        }
    }
}
