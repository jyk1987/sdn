using System;
using System.Collections.Generic;
using System.Text;
using SDN2.DB.Utility;

namespace SDN2.DB.DAO
{
    /// <summary>
    /// 数据库资源
    /// </summary>
    public class DBResource
    {
        /// <summary>
        /// 缓存表信息
        /// </summary>
        private static IDictionary<string, TableInfo> tableInfos = new Dictionary<string, TableInfo>();

        /// <summary>
        /// 从缓存中得到表信息对象
        /// </summary>
        /// <param name="tableName">表明</param>
        /// <returns></returns>
        public static TableInfo GetTableInfo(string tableName)
        {
            TableInfo ti = null;
            if (!tableInfos.TryGetValue(tableName, out ti))
                ti = tableInfos[tableName] = DBUtility.GetTableInfo(tableName);
            return ti;
        }

    }
}
