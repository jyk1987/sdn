using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDN2.Common.Cache.MemCached;
using System.Web;

namespace SDN2.Common.Cache
{
    /// <summary>
    /// 缓存类型
    /// </summary>
    internal enum CacheType
    {
        SystemCache//.Net内置的Cache
            , Memcached//memcached服务器
    }

    /// <summary>
    /// 缓存客户端
    /// </summary>
    internal class CacheClient
    {

        /// <summary>
        /// 系统使用缓存类型
        /// </summary>
        internal static CacheType CacheType
        {
            get { return MemcachedClient.ExistConfig ? CacheType.Memcached : CacheType.SystemCache; }
        }

        /// <summary>
        /// .Net 内置Cache
        /// </summary>
        internal static System.Web.Caching.Cache SystemCache { get { return HttpRuntime.Cache; } }

        /// <summary>
        /// Memcached
        /// </summary>
        private static MemcachedClient memcached;

        /// <summary>
        /// 创建Memcached
        /// </summary>
        /// <returns></returns>
        private static MemcachedClient ResetMemcached()
        {
            if (memcached != null) { memcached = null; }
            memcached = MemcachedClient.GetInstance();
            memcached.SendReceiveTimeout = 5000;
            memcached.MaxPoolSize = 1000;
            memcached.MinPoolSize = 10;
            return memcached;
        }

        /// <summary>
        /// MemcachedClient 的访问属性
        /// </summary>
        internal static MemcachedClient MemCache
        {
            get
            {
                return memcached ?? ResetMemcached();
            }
        }
    }
}
