using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDN2.Common.Cache.MemCached;
using System.Web;

namespace SDN2.Common.Cache
{
    /// <summary>
    /// 缓存类
    /// </summary>
    public static class SDNCache
    {

        /// <summary>
        /// 得到缓存个数
        /// </summary>
        public static int ItemCount
        {
            get
            {
                int result = 0;
                switch (CacheClient.CacheType)
                {
                    case CacheType.Memcached:
                        Dictionary<string, Dictionary<string, string>> stats = CacheClient.MemCache.Stats();
                        foreach (string key in stats.Keys)
                        { result += int.Parse(stats[key]["curr_items"].ToString()); }
                        break;
                    default:
                        result = CacheClient.SystemCache.Count;
                        break;
                }
                return result;
            }
        }
        /// <summary>
        /// 根据key得到value
        /// </summary>
        /// <param name="key">缓存的key</param>
        /// <returns>key所对应的value，如果不存在返回null</returns>
        public static object Get(string key)
        {
            switch (CacheClient.CacheType)
            {
                case CacheType.Memcached:
                    return CacheClient.MemCache.Get(key);
                default:
                    return CacheClient.SystemCache.Get(key);
            }
        }

        /// <summary>
        /// 判断是否有相应key缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Exist(string key) { return Get(key) != null; }

        /// <summary>
        /// 尝试取出缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">输出缓存</param>
        /// <returns>是否存在缓存</returns>
        public static bool TryGetValue(string key, out object value)
        {
            return (value = Get(key)) != null;
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key"></param>
        public static void Delete(string key)
        {
            switch (CacheClient.CacheType)
            {
                case CacheType.Memcached:
                    CacheClient.MemCache.Delete(key); break;
                default:
                    CacheClient.SystemCache.Remove(key); break;
            }
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存对象</param>
        public static void Set(string key, object value)
        {
            switch (CacheClient.CacheType)
            {
                case CacheType.Memcached:
                    CacheClient.MemCache.Set(key, value); break;
                default:
                    CacheClient.SystemCache.Insert(key, value); break;
            }
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存对象</param>
        /// <param name="time">时间</param>
        /// <param name="tt">时间类型</param>
        public static void Set(string key, object value, long time, TimeType tt)
        {
            long millisecond = 0;
            switch (tt)
            {
                case TimeType.Second:
                    millisecond = TimeUtility.SecondToMillisecond(time);
                    break;
                case TimeType.Minute:
                    millisecond = TimeUtility.MinuteToMillisecond(time);
                    break;
                case TimeType.Hour:
                    millisecond = TimeUtility.HourToMillisecond(time);
                    break;
                case TimeType.Day:
                    millisecond = TimeUtility.DayToMillisecond(time);
                    break;
                case TimeType.Week:
                    millisecond = TimeUtility.WeekToMillisecond(time);
                    break;
                default:
                    millisecond = time;
                    break;
            }
            Set(key, value, millisecond);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存对象</param>
        /// <param name="millisecond">缓存毫秒数</param>
        public static void Set(string key, object value, long millisecond)
        {
            DateTime dt = TimeUtility.GetNextTime(millisecond);
            switch (CacheClient.CacheType)
            {
                case CacheType.Memcached:
                    CacheClient.MemCache.Set(key, value, dt); break;
                default:
                    CacheClient.SystemCache.Insert(key, value, null, dt, TimeSpan.Zero); break;
            }
        }
    }
}
