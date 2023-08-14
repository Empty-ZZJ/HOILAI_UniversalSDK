namespace HL_Time
{
    public static class HL_Time
    {
        /// <summary>
        /// 时间转为13位时间戳
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static long ConvertToTimeStamp (DateTime time)
        {

            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0, 0));

            long t = (time.Ticks - startTime.Ticks) / 10000;

            return t;
        }

        /// <summary>
        /// 当前时间转为13位时间戳
        /// </summary>
        /// <returns></returns>
        public static long ConvertToTimeStamp ()
        {
            return ConvertToTimeStamp(DateTime.Now);

        }

        /// <summary>        
        /// 时间戳转为C#格式时间        
        /// </summary>        
        /// <param name=”timeStamp”></param>        
        /// <returns></returns>        
        public static string TimeStampToDateTime (string timeStamp)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0, 0));

            long t = long.Parse(timeStamp + "0000");

            TimeSpan timeSpan = new TimeSpan(t);
            DateTime nowTime = startTime.Add(timeSpan);

            string strTime = nowTime.ToString("yyyy-MM-dd HH:mm:ss");
            return strTime;
        }



        /// <summary>
        /// 取十三位时间戳 
        /// </summary>
        /// <param name="nowTime">给的时间值</param>
        /// <returns>返回13位时间戳</returns>
        public static string GetUnixTime (DateTime nowTime)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));

            long unixTime = (long)Math.Round((nowTime - startTime).TotalMilliseconds, MidpointRounding.AwayFromZero);

            return unixTime.ToString();
        }

        /// <summary>
        /// 取13位时间戳
        /// </summary>
        /// <returns>返回13位时间戳</returns>
        public static string GetUnixTime ()
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));

            DateTime nowTime = DateTime.Now;

            long unixTime = (long)Math.Round((nowTime - startTime).TotalMilliseconds, MidpointRounding.AwayFromZero);

            return unixTime.ToString();
        }


        /// <summary>
        /// 根据时间 取现在时间戳 10位数
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long GetUnixTime10 ()
        {
            TimeSpan ts = DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1);//ToUniversalTime()转换为标准时区的时间,去掉的话直接就用北京时间
            //return (long)ts.TotalMilliseconds; //精确到毫秒
            return (long)ts.TotalSeconds;//获取10位
        }

        /// <summary>
        /// 根据时间 取现在时间戳 10位数
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long GetUnixTime10 (DateTime dateTime)
        {
            TimeSpan ts = dateTime.ToUniversalTime() - new DateTime(1970, 1, 1);//ToUniversalTime()转换为标准时区的时间,去掉的话直接就用北京时间
            //return (long)ts.TotalMilliseconds; //精确到毫秒
            return (long)ts.TotalSeconds;//获取10位
        }








        /// <summary>
        /// 把秒转换成分钟
        /// </summary>
        /// <returns></returns>
        public static int SecondToMinute (int Second)
        {
            decimal mm = (decimal)((decimal)Second / (decimal)60);
            return Convert.ToInt32(Math.Ceiling(mm));
        }

        #region 返回某年某月最后一天
        /// <summary>
        /// 返回某年某月最后一天
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>日</returns>
        public static int GetMonthLastDate (int year, int month)
        {
            DateTime lastDay = new DateTime(year, month, new System.Globalization.GregorianCalendar().GetDaysInMonth(year, month));
            int Day = lastDay.Day;
            return Day;
        }
        #endregion

        #region 返回时间差
        /// <summary>
        /// 比较两个时间 差 返回 时间 差
        /// </summary>
        /// <param name="DateTime1"></param>
        /// <param name="DateTime2"></param>
        /// <returns></returns>
        public static string DateDiff (DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = null;
            try
            {
                //TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
                //TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
                //TimeSpan ts = ts1.Subtract(ts2).Duration();
                TimeSpan ts = DateTime2 - DateTime1;
                if (ts.Days >= 1)
                {
                    dateDiff = DateTime1.Month.ToString() + "月" + DateTime1.Day.ToString() + "日";
                }
                else
                {
                    if (ts.Hours > 1)
                    {
                        dateDiff = ts.Hours.ToString() + "小时前";
                    }
                    else
                    {
                        dateDiff = ts.Minutes.ToString() + "分钟前";
                    }
                }
            }
            catch
            { }
            return dateDiff;
        }
        #endregion


    }
}
