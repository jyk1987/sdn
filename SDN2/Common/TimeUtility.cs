using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Threading;

namespace SDN2.Common
{
    #region 时间类型枚举
    /// <summary>
    /// 时间类型
    /// </summary>
    public enum TimeType
    {
        /// <summary>
        /// 毫秒
        /// </summary>
        Millisecond,
        /// <summary>
        /// 秒
        /// </summary>
        Second,
        /// <summary>
        /// 分钟
        /// </summary>
        Minute,
        /// <summary>
        /// 小时
        /// </summary>
        Hour,
        /// <summary>
        /// 天
        /// </summary>
        Day,
        /// <summary>
        /// 周
        /// </summary>
        Week
    }
    #endregion

    /// <summary>
    /// 时间工具类
    /// </summary>
    public static class TimeUtility
    {
        /// <summary>
        /// 用于计时器记录开始时间
        /// </summary>
        private static IDictionary<int, DateTime> cacheTimes = new Dictionary<int, DateTime>();

        /// <summary>
        /// 得到相应毫秒数后的日期对象
        /// </summary>
        /// <param name="millisecond">毫秒数</param>
        /// <returns>日期对象</returns>
        public static DateTime GetNextTime(long millisecond)
        {
            return DateTime.Now.AddMilliseconds(millisecond);
        }

        /// <summary>
        /// 得到相应毫秒数后的时间间隔对象
        /// </summary>
        /// <param name="millisecond">毫秒数</param>
        /// <returns>时间间隔对象</returns>
        public static TimeSpan GetTimeSpan(long millisecond)
        {
            return GetNextTime(millisecond).Subtract(DateTime.Now);
        }

        /// <summary>
        /// 秒转毫秒
        /// </summary>
        /// <param name="second">秒数</param>
        /// <returns>相应毫秒数</returns>
        public static long SecondToMillisecond(long second)
        {
            return second * 1000;
        }

        /// <summary>
        /// 分钟转毫秒
        /// </summary>
        /// <param name="minute">分钟数</param>
        /// <returns>相应毫秒数</returns>
        public static long MinuteToMillisecond(long minute)
        {
            return SecondToMillisecond(minute * 60);
        }
        /// <summary>
        /// 小时转毫秒
        /// </summary>
        /// <param name="hour">小时数</param>
        /// <returns>相应毫秒数</returns>
        public static long HourToMillisecond(long hour)
        {
            return MinuteToMillisecond(hour * 60);
        }

        /// <summary>
        /// 天转毫秒
        /// </summary>
        /// <param name="day">天数</param>
        /// <returns>相应毫秒数</returns>
        public static long DayToMillisecond(long day)
        {
            return HourToMillisecond(day * 24);
        }

        /// <summary>
        /// 周转毫秒
        /// </summary>
        /// <param name="week">周数</param>
        /// <returns>相应毫秒数</returns>
        public static long WeekToMillisecond(long week)
        {
            return DayToMillisecond(week * 7);
        }

        /// <summary>
        /// 开始计时
        /// </summary>
        public static void BeginTimer()
        {
            cacheTimes[Thread.CurrentThread.ManagedThreadId] = DateTime.Now;
        }

        /// <summary>
        /// 得到计时器已执行时间
        /// </summary>
        /// <returns>TimeSpan对象</returns>
        public static TimeSpan GetTimerSpan()
        {
            return DateTime.Now.Subtract(cacheTimes[Thread.CurrentThread.ManagedThreadId]);
        }

        /// <summary>
        /// 停止计时
        /// </summary>
        /// <returns>TimeSpan对象</returns>
        public static TimeSpan EndTimer()
        {
            TimeSpan ts = DateTime.Now.Subtract(cacheTimes[Thread.CurrentThread.ManagedThreadId]);
            cacheTimes.Remove(Thread.CurrentThread.ManagedThreadId);
            return ts;
        }
    }
}
