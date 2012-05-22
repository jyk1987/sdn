using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;

namespace SDN2.DB.DAO
{
    /// <summary>
    /// 存储过程帮助类
    /// </summary>
    public static class SPHelper
    {

        /// <summary>
        /// 创建一个操作存储过程的Command
        /// </summary>
        /// <returns></returns>
        private static DbCommand CreateCommand()
        {
            DbCommand cmd = DbHelper.CreateCommand(DbHelper.CreateConnection(true));
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
        }

        /// <summary>
        /// 根据存储过程名称，创建一个操作这个存储过程的Command
        /// </summary>
        /// <param name="SPName"></param>
        /// <returns></returns>
        private static DbCommand CreateCommand(string SPName)
        {
            DbCommand cmd = CreateCommand();
            cmd.CommandText = SPName;
            return cmd;
        }

        /// <summary>
        /// 根据存储过程名称和存储过程需要的参数，创建一个操作这个存储过程的Command
        /// </summary>
        /// <param name="SPName"></param>
        /// <param name="parameterBox"></param>
        /// <returns></returns>
        private static DbCommand CreateCommand(string SPName, ParameterBox parameterBox)
        {
            DbCommand cmd = CreateCommand(SPName);
            foreach (DbParameter param in parameterBox.Parameters)
                cmd.Parameters.Add(param);
            return cmd;
        }

        /// <summary>
        /// 根据存储过程名称，执行非查询的存储过程
        /// </summary>
        /// <param name="SPName"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string SPName)
        {
            return CreateCommand(SPName).ExecuteNonQuery();
        }

        /// <summary>
        /// 根据存储过程名称和存储过程需要的参数，执行非查询的存储过程
        /// </summary>
        /// <param name="SPName"></param>
        /// <param name="parameterBox"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string SPName, ParameterBox parameterBox)
        {
            return CreateCommand(SPName, parameterBox).ExecuteNonQuery();
        }

        /// <summary>
        /// 根据存储过程名称，创建一个数据读取器。使用完读取器请关闭。
        /// </summary>
        /// <param name="SPName"></param>
        /// <returns></returns>
        public static DbDataReader CreateDataReader(string SPName)
        {
            return DbHelper.CreateDataReader(CreateCommand(SPName));
        }

        /// <summary>
        /// 根据存储过程名称和存储过程需要的参数，创建一个数据读取器。使用完读取器请关闭。
        /// </summary>
        /// <param name="SPName"></param>
        /// <param name="parameterBox"></param>
        /// <returns></returns>
        public static DbDataReader CreateDataReader(string SPName, ParameterBox parameterBox)
        {
            return DbHelper.CreateDataReader(SPName, parameterBox);
        }

        /// <summary>
        /// 根据存储过程创建一个DataTable
        /// </summary>
        /// <param name="SPName"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(string SPName)
        {
            DataTable table = new DataTable();
            DbDataAdapter adapter = DbHelper.CreateDataAdapter(CreateCommand(SPName));
            adapter.Fill(table);
            DbHelper.CloseDataAdapter(adapter);
            return table;
        }

        /// <summary>
        /// 根据存储过程名称和存储过程需要的参数，创建一个DataTable
        /// </summary>
        /// <param name="SPName"></param>
        /// <param name="parameterBox"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(string SPName, ParameterBox parameterBox)
        {
            DataTable table = new DataTable();
            DbDataAdapter adapter = DbHelper.CreateDataAdapter(CreateCommand(SPName, parameterBox));
            adapter.Fill(table);
            DbHelper.CloseDataAdapter(adapter);
            return table;
        }

        /// <summary>
        /// 执行只返回一行一列的存储过程
        /// </summary>
        /// <param name="SPName"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string SPName)
        {
            object result = null;
            using (DbCommand cmd = CreateCommand(SPName))
            {
                result = cmd.ExecuteScalar();
                DbHelper.CloseCommand(cmd);
            }
            return result;
        }

        /// <summary>
        /// 执行只返回一行一列的存储过程
        /// </summary>
        /// <param name="SPName"></param>
        /// <param name="parameterBox"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string SPName, ParameterBox parameterBox)
        {
            object result = null;
            using (DbCommand cmd = CreateCommand(SPName, parameterBox))
            {
                result = cmd.ExecuteScalar();
                DbHelper.CloseCommand(cmd);
            }
            return result;
        }

    }
}
