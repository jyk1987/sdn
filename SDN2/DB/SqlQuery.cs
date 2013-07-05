using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDN2.DB.DAO;

namespace SDN2.DB
{

    /// <summary>
    /// sql查询器
    /// 2011/4/7
    /// jyk
    /// </summary>
    public class SqlQuery
    {
        /// <summary>
        /// 表名
        /// </summary>
        private string tableName = string.Empty;


        ParameterBox pb = new ParameterBox();

        /// <summary>
        /// 设置从那张表查询
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public SqlQuery From(string tableName)
        {
            this.tableName = tableName;
            return this;
        }


        public SqlQuery Where()
        {
            return this;
        }

        public SqlQuery And()
        {
            return this;
        }

        //public SqlQuery 

    }
}
