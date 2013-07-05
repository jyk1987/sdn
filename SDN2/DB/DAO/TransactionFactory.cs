using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Collections;

namespace SDN2.DB.DAO
{
    /// <summary>
    /// 事务执行工厂
    /// </summary>
    public class TransactionFactory
    {
        /// <summary>
        /// 存储数据库连接
        /// </summary>
        private DbConnection conn = null;

        /// <summary>
        /// 存储事务
        /// </summary>
        private DbTransaction ts = null;

        /// <summary>
        /// 存储Command对象
        /// </summary>
        private DbCommand cmd = null;

        /// <summary>
        /// 对象在构造是，会产生一个打开的数据库连接并打开事务,Command对象也会同时产生
        /// </summary>
        public TransactionFactory()
        {
            this.conn = DbHelper.CreateConnection(true);
            this.cmd = DbHelper.CreateCommand(conn);
            this.ts = conn.BeginTransaction();
            this.cmd.Connection = this.conn;
            this.cmd.Transaction = ts;
        }

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        public int ExecuteSql(string sql)
        {
            this.cmd.CommandText = sql;
            return cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// 执行带参数的sql语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameterBox">参数信息</param>
        public int ExecuteSql(string sql, ParameterBox parameterBox)
        {
            int result = 0;
            this.cmd.CommandText = sql;
            foreach (DbParameter param in parameterBox.Parameters)
                this.cmd.Parameters.Add(param);
            result = this.cmd.ExecuteNonQuery();
            this.cmd.Parameters.Clear();
            return result;
        }

        /// <summary>
        /// 执行只返回一行一列的sql
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public object ExecuteScalar(string sql)
        {
            object result = null;
            this.cmd.CommandText = sql;
            result = this.cmd.ExecuteScalar();
            return result;
        }


        /// <summary>
        /// 执行只返回一行一列的sql语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameterBox">参数信息</param>
        /// <returns></returns>
        public object ExecuteScalar(string sql, ParameterBox parameterBox)
        {
            object result = null;
            this.cmd.CommandText = sql;
            foreach (DbParameter param in parameterBox.Parameters)
                this.cmd.Parameters.Add(param);
            result = cmd.ExecuteScalar();
            this.cmd.Parameters.Clear();
            return result;
        }

        /// <summary>
        /// 执行同一sql语句和不同参数信息
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="pbs">参数信息集合</param>
        public int ExecuteSql(string sql, IList<ParameterBox> pbs)
        {
            int result = 0;
            foreach (ParameterBox pb in pbs)
                result += this.ExecuteSql(sql, pb);
            return result;
        }

        /// <summary>
        /// 提交事务并释放数据库连接
        /// 提交失败后自动回滚
        /// </summary>
        /// <returns></returns>
        public bool Commit()
        {
            bool result;
            try { this.ts.Commit(); result = true; }
            catch (Exception) { try { this.ts.Rollback(); } catch (Exception) { } result = false; }
            finally { DbHelper.CloseCommand(cmd); }
            return result;
        }

        /// <summary>
        /// 事务回滚
        /// </summary>
        public void Rollback()
        {
            this.ts.Rollback();
            DbHelper.CloseCommand(cmd);
        }
    }
}
