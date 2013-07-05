using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using SDN2.DB.DAO;

namespace SDN2.DB.Utility
{
    /// <summary>
    /// 数据库工具类
    /// </summary>
    public static class DBUtility
    {
        /// <summary>
        /// 没有查询结果的查询语句
        /// </summary>
        private const string NonDataSql = "SELECT * FROM {0} WHERE 1=2";

        /// <summary>
        /// 获取指定表的全部列名
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public static string[] GetTableColumnNames(string tableName)
        {
            DbDataReader reader = SqlHelper.GetDataReader(string.Format(NonDataSql, tableName));
            string[] columnNames = GetTableColumnNames(reader);
            reader.Close();
            return columnNames;
        }

        /// <summary>
        /// 得到DataReader中的所有列名
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static string[] GetTableColumnNames(DbDataReader reader)
        {
            string[] columnNames = null;
            int columnCount = reader.VisibleFieldCount;
            columnNames = new string[columnCount];
            for (int i = 0; i < columnCount; i++)
            {
                columnNames[i] = reader.GetName(i).ToLower();
            }
            return columnNames;
        }

        /// <summary>
        /// 得到DataTable中的所有列名
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string[] GetTableColumnNames(DataTable table)
        {
            DataColumnCollection dcc = table.Columns;
            string[] columnNames = new string[dcc.Count];
            for (int i = 0; i < dcc.Count; i++)
            {
                columnNames[i] = dcc[i].ColumnName;
            }
            return columnNames;
        }

        /// <summary>
        /// 根据表名得到表信息对象
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public static TableInfo GetTableInfo(string tableName)
        {
            TableInfo ti = new TableInfo(tableName);
            string sql = string.Format(NonDataSql, tableName);
            DbDataReader reader = DbHelper.GetSchemaDataReader(sql);
            ti.ColumnNames = GetTableColumnNames(reader);
            DataTable table = reader.GetSchemaTable();
            DataColumnCollection dcc = table.Columns;
            foreach (DataRow row in table.Rows)
            {
                ColumnInfo ci = new ColumnInfo();
                foreach (DataColumn dc in dcc)
                {
                    ci[dc.ColumnName] = row[dc.ColumnName].ToString();
                }
                ti[ci.ColumnName] = ci;
            }
            reader.Close();
            return ti;
        }
    }
}
