using System;
using System.Collections.Generic;
using System.Text;

namespace SDN2.DB.DAO
{
    /// <summary>
    /// 数据库表信息
    /// </summary>
    public class TableInfo
    {
        /// <summary>
        /// 根据表明得到表信息对象
        /// </summary>
        /// <param name="tableName"></param>
        public TableInfo(string tableName)
        {
            this.tableName = tableName.ToLower();
        }

        private string tableName;

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName.ToLower(); }
        }

        private IDictionary<string, ColumnInfo> columnInfos = new Dictionary<string, ColumnInfo>();

        /// <summary>
        /// 索引列信息
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <returns></returns>
        public ColumnInfo this[string columnName]
        {
            get
            {
                ColumnInfo ci;
                return this.columnInfos.TryGetValue(columnName.ToLower(), out ci) ? ci : null;
            }
            set { this.columnInfos[columnName.ToLower()] = value; }
        }

        private string[] columnNames;

        /// <summary>
        /// 列名
        /// </summary>
        public string[] ColumnNames
        {
            get { return columnNames; }
            set { columnNames = value; }
        }

        private IList<string> primaryKeys = new List<string>();
        /// <summary>
        /// 主键
        /// </summary>
        public IList<string> PrimaryKeys
        {
            get { InitPrimaryKey(); return primaryKeys; }
        }

        /// <summary>
        /// 从列信息中加载主键
        /// </summary>
        private void InitPrimaryKey()
        {
            if (primaryKeys.Count > 0) return;
            for (int i = 0; i < columnNames.Length; i++)
                if (columnInfos[columnNames[i]].IsPrimaryKey)
                    primaryKeys.Add(columnNames[i]);
        }

        /// <summary>
        /// 判断表信息对象中是否包含相应列
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <returns></returns>
        public bool ExistColumn(string columnName)
        {
            ColumnInfo ci;
            return this.columnInfos.TryGetValue(columnName.ToLower(), out ci);
        }

    }
}
