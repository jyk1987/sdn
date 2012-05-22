using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;

namespace SDN2.DB.DAO
{
    /// <summary>
    /// 参数信息
    /// </summary>
    public class ParameterBox
    {
        /// <summary>
        /// 存储数据参数的map
        /// </summary>
        private IDictionary<string, DbParameter> parameters = new Dictionary<string, DbParameter>();

        /// <summary>
        /// 创建一个参数信息对象
        /// </summary>
        public ParameterBox() { }

        /// <summary>
        /// 传入一组参数的构造方法
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="value">参数值</param>
        public ParameterBox(string parameterName, object value)
        {
            this.parameters[parameterName] = CreateParameter(parameterName, value);
        }

        /// <summary>
        /// 传入一组参数的口早方法,可以设置参数的ParameterDirection，用在存储过程上
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="value">参数值</param>
        /// <param name="direction">方向</param>
        public ParameterBox(string parameterName, object value, ParameterDirection direction)
        {
            this.parameters[parameterName] = CreateParameter(parameterName, value);
            this.parameters[parameterName].Direction = direction;
        }
        /// <summary>
        /// 增加参数
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="value">参数值</param>
        /// <returns></returns>
        public ParameterBox Add(string parameterName, object value)
        {
            this.parameters[parameterName] = CreateParameter(parameterName, value);
            return this;
        }

        /// <summary>
        /// 增加参数
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="value">参数值</param>
        /// <param name="direction">方向</param>
        /// <returns></returns>
        public ParameterBox Add(string parameterName, object value, ParameterDirection direction)
        {
            this.parameters[parameterName] = CreateParameter(parameterName, value);
            this.parameters[parameterName].Direction = direction;
            return this;
        }

        /// <summary>
        /// 增加参数
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="value">参数值</param>
        /// <param name="direction">方向</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public ParameterBox Add(string parameterName, object value, ParameterDirection direction, DbType type)
        {
            this.parameters[parameterName] = CreateParameter(parameterName, value);
            this.parameters[parameterName].Direction = direction;
            this.parameters[parameterName].DbType = type;
            return this;

        }

        /// <summary>
        /// 增加参数
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="value">参数值</param>
        /// <param name="direction">方向</param>
        /// <param name="type">类型</param>
        /// <param name="size">大小</param>
        /// <returns></returns>
        public ParameterBox Add(string parameterName, object value, ParameterDirection direction, DbType type, int size)
        {
            this.parameters[parameterName] = CreateParameter(parameterName, value);
            this.parameters[parameterName].Direction = direction;
            this.parameters[parameterName].DbType = type;
            this.parameters[parameterName].Size = size;
            return this;

        }




        /// <summary>
        /// 取出存储的Parameter
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <returns></returns>
        public DbParameter GetParameter(string parameterName)
        {
            DbParameter dp = null;
            this.parameters.TryGetValue(parameterName, out dp);
            return dp;
        }

        /// <summary>
        /// 清空所有参数
        /// </summary>
        public void Clear() { this.parameters.Clear(); }

        /// <summary>
        /// 删除参数信息
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <returns></returns>
        public ParameterBox Remove(string parameterName)
        {
            this.parameters.Remove(parameterName);
            return this;
        }

        /// <summary>
        /// 创建数据参数对象
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private DbParameter CreateParameter(string parameterName, object value)
        {
            value = value is DateTime ? ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss") : (value is int || value is uint || value is long || value is short ? value.ToString() : value);
            switch (DbInfoUtility.DbType)
            {
                case SDNDbType.MsSQL:
                    return new SqlParameter(parameterName, value);
                //case DbTypes.SQLITE:
                //    return new SQLiteParameter(parameterName, value);
                //case DbTypes.MYSQL:
                //    return new MySqlParameter(parameterName, value);
                default:
                    return new OleDbParameter(parameterName, value);
            }
        }

        /// <summary>
        /// 所有参数名
        /// </summary>
        public ICollection<string> ParameterNames { get { return this.parameters.Keys; } }

        /// <summary>
        /// 所有参数对象
        /// </summary>
        public ICollection<DbParameter> Parameters { get { return this.parameters.Values; } }
    }
}
