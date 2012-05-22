using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Collections;

namespace SDN2.DB.DAO
{
    /// <summary>
    /// sql辅助类
    /// </summary>
    public static class SqlHelper
    {
        /// <summary>
        /// 执行增删改的sql语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql)
        {
            int result = -1;
            if (TransactionTransfer.ExistTransaction)
            {
                result = TransactionTransfer.ExecuteSql(sql);
            }
            else
                using (DbCommand cmd = DbHelper.CreateCommand(sql))
                {
                    result = cmd.ExecuteNonQuery();
                    DbHelper.CloseCommand(cmd);
                }
            return result;
        }

        /// <summary>
        /// 执行增删改的预编译sql语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameterBox">参数信息</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql, ParameterBox parameterBox)
        {
            int result = -1;
            if (TransactionTransfer.ExistTransaction)
            {
                result = TransactionTransfer.ExecuteSql(sql, parameterBox);
            }
            else
                using (DbCommand cmd = DbHelper.CreateCommand(sql, parameterBox))
                {
                    result = cmd.ExecuteNonQuery();
                    DbHelper.CloseCommand(cmd);
                }
            return result;
        }

        /// <summary>
        /// 执行sql语句集合(已经开启事务)
        /// </summary>
        /// <param name="sqlArray">sql语句集合</param>
        /// <returns>执行是否成功</returns>
        public static bool ExecuteNonQuery(IList<string> sqlArray)
        {
            TransactionFactory tf = TransactionTransfer.ExistTransaction
                ? TransactionTransfer.CurrentTransactionFactory
                : new TransactionFactory();
            foreach (string sql in sqlArray)
                tf.ExecuteSql(sql);
            return tf.Commit();
        }

        /// <summary>
        /// 根据执行sql语句集合和参数信息集合进行批处理(已经开启事务)
        /// </summary>
        /// <param name="sqlArray"></param>
        /// <param name="pbs"></param>
        /// <returns></returns>
        public static bool ExecuteNonQuery(IList<string> sqlArray, IList<ParameterBox> pbs)
        {
            TransactionFactory tf = TransactionTransfer.ExistTransaction
                ? TransactionTransfer.CurrentTransactionFactory
                : new TransactionFactory();
            bool result = false;
            try
            {
                for (int i = 0; i < sqlArray.Count; i++)
                    result = tf.ExecuteSql(sqlArray[i], pbs[i]) > 0;
                tf.Commit();
            }
            catch (Exception)
            {
                tf.Rollback();
            }

            return result;
        }

        /// <summary>
        /// 执行同一sql语句和不同参数信息(已经开启事务)
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="pbs">参数信息集合</param>
        /// <returns></returns>
        public static bool ExecuteNonQuery(string sql, IList<ParameterBox> pbs)
        {
            TransactionFactory tf = TransactionTransfer.ExistTransaction
                ? TransactionTransfer.CurrentTransactionFactory
                : new TransactionFactory();
            tf.ExecuteSql(sql, pbs);
            return tf.Commit();
        }

        /// <summary>
        /// 执行返回单结果的sql语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>执行结果</returns>
        public static object ExecuteScalar(string sql)
        {
            object obj = null;
            if (TransactionTransfer.ExistTransaction)
            {
                obj = TransactionTransfer.ExecuteSql(sql);
            }
            else
                using (DbCommand cmd = DbHelper.CreateCommand(sql))
                {
                    obj = cmd.ExecuteScalar();
                    DbHelper.CloseCommand(cmd);
                }
            return obj;
        }

        /// <summary>
        /// 执行返回单结果的sql语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameterBox">参数信息</param>
        /// <returns>执行结果</returns>
        public static object ExecuteScalar(string sql, ParameterBox parameterBox)
        {
            object obj = null;
            if (TransactionTransfer.ExistTransaction)
            {
                obj = TransactionTransfer.ExecuteSql(sql, parameterBox);
            }
            else
                using (DbCommand cmd = DbHelper.CreateCommand(sql, parameterBox))
                {
                    obj = cmd.ExecuteScalar();
                    DbHelper.CloseCommand(cmd);
                }
            return obj;
        }

        /// <summary>
        /// 根据sql语句填充DataTable
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns>DataTable</returns>
        public static DataTable GetDataTable(string sql)
        {
            DataTable table = new DataTable();
            DbDataAdapter adapter = DbHelper.CreateDataAdapter(sql);
            adapter.Fill(table);
            DbHelper.CloseDataAdapter(adapter);
            return table;
        }

        /// <summary>
        /// 根据sql居于填充DataSet
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns></returns>
        public static DataSet GetDataSet(string sql)
        {
            DataSet ds = new DataSet();
            DbDataAdapter adapter = DbHelper.CreateDataAdapter(sql);
            adapter.Fill(ds);
            DbHelper.CloseDataAdapter(adapter);
            return ds;
        }

        /// <summary>
        /// 根据sql语句和参数信息填充DataTable
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameterBox">参数信息</param>
        /// <returns>DataTable</returns>
        public static DataTable GetDataTable(string sql, ParameterBox parameterBox)
        {
            DataTable table = new DataTable();
            DbDataAdapter adapter = DbHelper.CreateDataAdapter(sql, parameterBox);
            adapter.Fill(table);
            DbHelper.CloseDataAdapter(adapter);
            return table;
        }

        /// <summary>
        /// 根据sql语句和参数信息填充DataSet
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameterBox">参数信息</param>
        /// <returns>DataSet</returns>
        public static DataSet GetDataSet(string sql, ParameterBox parameterBox)
        {
            DataSet ds = new DataSet();
            DbDataAdapter adapter = DbHelper.CreateDataAdapter(sql, parameterBox);
            adapter.Fill(ds);
            DbHelper.CloseDataAdapter(adapter);
            return ds;
        }

        /// <summary>
        /// 保存对DataTable的修改
        /// </summary>
        /// <param name="table"></param>
        /// <returns>操作影响了几条数据</returns>
        //private static int UpdateTable(DataTable table)
        //{
        //    int result = -1;
        //    DbCommandBuilder cb = DbHelper.CreateCommandBuilder(true);
        //    result = cb.DataAdapter.Update(table);
        //    cb.DataAdapter.Dispose();
        //    cb.Dispose();
        //    return result;
        //}

        /// <summary>
        /// 根据sql语句产生数据读取器。
        /// 使用完请调用Close()方法关闭读取器。
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>数据读取器</returns>
        public static DbDataReader GetDataReader(string sql)
        {
            return DbHelper.CreateDataReader(sql);
        }

        /// <summary>
        /// 根据sql语句产生数据读取器(表结构信息)。
        /// 使用完请调用Close()方法关闭读取器。
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>数据读取器</returns>
        public static DbDataReader GetDataReaderWithInfo(string sql)
        {
            return DbHelper.GetDataReaderWithInfo(sql);
        }

        /// <summary>
        /// 根据sql语句和参数信息产生数据读取器(表结构信息)。
        /// 使用完请调用Close()方法关闭读取器。
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="pb">参数信息</param>
        /// <returns>参数信息</returns>
        public static DbDataReader GetDataReaderWithInfo(string sql, ParameterBox pb)
        {
            return DbHelper.GetDataReaderWithInfo(sql, pb);
        }

        /// <summary>
        /// 根据sql语句和参数信息产生数据读取器。
        /// 使用完请调用Close()方法关闭读取器。
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameterBox">参数信息</param>
        /// <returns>数据读取器</returns>
        public static DbDataReader GetDataReader(string sql, ParameterBox parameterBox)
        {
            return DbHelper.CreateDataReader(sql, parameterBox);
        }

        /// <summary>
        /// 关闭DataReader
        /// </summary>
        /// <param name="reader"></param>
        public static void CloseDataReader(DbDataReader reader)
        {
            DbHelper.CloseDataReader(reader);
        }
    }
}
