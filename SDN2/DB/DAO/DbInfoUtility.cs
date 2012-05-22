using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Threading;

namespace SDN2.DB.DAO
{
    /// <summary>
    /// 数据库信息辅助类
    /// </summary>
    public class DbInfoUtility
    {
        /// <summary>
        /// 缓存被被加载的其他的数据的名称
        /// </summary>
        private static IDictionary<int, string> usingdb = new Dictionary<int, string>();

        /// <summary>
        /// 锁
        /// </summary>
        private static object lockobj = new object();

        /// <summary>
        /// 根据数据库信息名进行数据库加载
        /// </summary>
        /// <param name="dbname"></param>
        public static void LoadOhterDB(string dbname)
        {
            lock (usingdb)
            {
                usingdb[Thread.CurrentThread.ManagedThreadId] = dbname;
            }
        }

        /// <summary>
        /// 根据数据库信息名进行数据库加载
        /// </summary>
        /// <param name="dnname"></param>
        public static void LoadOtherDB(string dnname)
        {
            LoadOhterDB(dnname);
        }

        /// <summary>
        /// 根据数据库信息名进行数据库卸载
        /// </summary>
        /// <param name="dbname"></param>
        public static void UnLoadOhterDB(string dbname)
        {
            lock (lockobj)
            {
                usingdb.Remove(Thread.CurrentThread.ManagedThreadId);
            }
        }

        /// <summary>
        /// 卸载当权关在的其他数据库
        /// </summary>
        public static void UnLoadOhterDB()
        {

            if (usingdb.ContainsKey(Thread.CurrentThread.ManagedThreadId))
            {
                lock (lockobj)
                {
                    usingdb.Remove(Thread.CurrentThread.ManagedThreadId);
                }
            }
        }

        public static void UnLoadOtherDB()
        {
            UnLoadOhterDB();
        }

        /// <summary>
        /// 得到已经加载的其他数据库的URL
        /// </summary>
        internal static string DbUrl
        {
            get
            {

                if (usingdb.ContainsKey(Thread.CurrentThread.ManagedThreadId))
                    return DbInfo.GetDbUrl(usingdb[Thread.CurrentThread.ManagedThreadId]);
                return DbInfo.GetDbUrl();

            }
        }

        /// <summary>
        /// 得到已经加载的其他数据库的类型
        /// </summary>
        internal static SDNDbType DbType
        {
            get
            {
                if (usingdb.ContainsKey(Thread.CurrentThread.ManagedThreadId))
                    return DbInfo.GetDbType(usingdb[Thread.CurrentThread.ManagedThreadId]);
                return DbInfo.GetDbType();

            }
        }

    }
}
