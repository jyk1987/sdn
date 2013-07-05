using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;

namespace SDN2.DB.DAO
{
    /// <summary>
    /// 数据库操作对象辅助类
    /// </summary>
    public static class DbHelper
    {
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        /// <param name="conn">数据库连接</param>
        public static void CloseConnection(DbConnection conn)
        {
            if (conn == null)
                return;
            try { conn.Close(); }
            catch (Exception) { }
            finally { conn = null; }
        }

        /// <summary>
        /// 关闭Command
        /// </summary>
        /// <param name="cmd">Command</param>
        public static void CloseCommand(DbCommand cmd)
        {
            if (cmd == null || cmd.Connection == null)
            {
                return;
            }
            CloseConnection(cmd.Connection);
            cmd.Dispose();
        }

        /// <summary>
        /// 创建一个数据库连接
        /// </summary>
        /// <returns></returns>
        public static DbConnection CreateConnection()
        {
            switch (DbInfoUtility.DbType)
            {
                case SDNDbType.MsSQL:
                    return new SqlConnection(DbInfoUtility.DbUrl);
                //case DbTypes.SQLITE:
                //    return new SQLiteConnection(DbInfoUtility.DbUrl);
                //case DbTypes.MYSQL:
                //    return new MySqlConnection(DbInfoUtility.DbUrl);
                default:
                    return new OleDbConnection(DbInfoUtility.DbUrl);
            }
        }

        /// <summary>
        /// 创建一个打开的数据库连接
        /// </summary>
        /// <returns></returns>
        public static DbConnection CreateConnection(bool isOpen)
        {
            DbConnection conn = CreateConnection();
            if (isOpen)
                conn.Open();
            return conn;
        }

        /// <summary>
        /// 创建一个Command
        /// </summary>
        /// <returns></returns>
        public static DbCommand CreateCommand()
        {
            switch (DbInfoUtility.DbType)
            {
                case SDNDbType.MsSQL:
                    return new SqlCommand();
                //case DbTypes.SQLITE:
                //    return new SQLiteCommand();
                //case DbTypes.MYSQL:
                //    return new MySqlCommand();
                default:
                    return new OleDbCommand();
            }
        }

        /// <summary>
        /// 用数据库连接创建一个Command
        /// </summary>
        /// <param name="conn">数据库连接</param>
        /// <returns>创建好的Command</returns>
        public static DbCommand CreateCommand(DbConnection conn)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            DbCommand cmd = CreateCommand();
            cmd.Connection = conn;
            return cmd;
        }

        /// <summary>
        /// 根据数据库连接和sql语句创建Command
        /// </summary>
        /// <param name="conn">数据库连接</param>
        /// <param name="sql">sql语句</param>
        /// <returns>创建好的Command</returns>
        public static DbCommand CreateCommand(DbConnection conn, string sql)
        {
            DbCommand cmd = CreateCommand(conn);
            cmd.CommandText = sql;
            return cmd;
        }

        /// <summary>
        /// 根据sql语句创建Command
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>创建好的Command</returns>
        public static DbCommand CreateCommand(string sql)
        {
            return CreateCommand(CreateConnection(true), sql);
        }

        /// <summary>
        /// 根据数据库连接、sql语句和参数对象创建Command
        /// </summary>
        /// <param name="conn">数据库连接</param>
        /// <param name="sql">sql语句</param>
        /// <param name="parameterBox">参数信息</param>
        /// <returns>创建好的Command</returns>
        public static DbCommand CreateCommand(DbConnection conn, string sql, ParameterBox parameterBox)
        {
            DbCommand cmd = CreateCommand(conn, sql);
            foreach (DbParameter param in parameterBox.Parameters)
                cmd.Parameters.Add(param);
            return cmd;
        }

        /// <summary>
        /// 根据sql语句和参数信息穿件Command
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameterBox">参数信息</param>
        /// <returns>创建好的Command</returns>
        public static DbCommand CreateCommand(string sql, ParameterBox parameterBox)
        {
            return CreateCommand(CreateConnection(true), sql, parameterBox);
        }

        /// <summary>
        /// 创建数据填充器
        /// </summary>
        /// <returns>数据填充器</returns>
        internal static DbDataAdapter CreateDataAdapter()
        {
            switch (DbInfoUtility.DbType)
            {
                case SDNDbType.MsSQL:
                    return new SqlDataAdapter();
                //case DbTypes.SQLITE:
                //    return new SQLiteDataAdapter();
                //case DbTypes.MYSQL:
                //    return new MySqlDataAdapter();
                default:
                    return new OleDbDataAdapter();
            }
        }

        /// <summary>
        /// 根据Command对象穿件数据填充器
        /// </summary>
        /// <param name="cmd">Command对象</param>
        /// <returns>数据填充器</returns>
        public static DbDataAdapter CreateDataAdapter(DbCommand cmd)
        {
            DbDataAdapter adapter = CreateDataAdapter();
            adapter.SelectCommand = cmd;
            return adapter;
        }

        /// <summary>
        /// 根据sql语句创建数据填充器
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>数据填充器</returns>
        public static DbDataAdapter CreateDataAdapter(string sql)
        {
            return CreateDataAdapter(CreateCommand(sql));
        }

        /// <summary>
        /// 根据sql语句和参数信息创建数据填充器
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameterBox">参数信息</param>
        /// <returns>数据填充器</returns>
        public static DbDataAdapter CreateDataAdapter(string sql, ParameterBox parameterBox)
        {
            return CreateDataAdapter(CreateCommand(sql, parameterBox));
        }

        /// <summary>
        /// 根据数据库类型创建一个CommandBuilder
        /// </summary>
        /// <returns></returns>
        internal static DbCommandBuilder CreateCommandBuilder()
        {
            switch (DbInfoUtility.DbType)
            {
                case SDNDbType.MsSQL:
                    return new SqlCommandBuilder();
                //case DbTypes.SQLITE:
                //    return new SQLiteCommandBuilder();
                //case DbTypes.MYSQL:
                //    return new MySqlCommandBuilder();
                default:
                    return new OleDbCommandBuilder();
            }
        }

        /// <summary>
        /// 根据数据库类型创建一个CommandBuilder
        /// </summary>
        /// <param name="isHaveAdapter">是否包含填充器</param>
        /// <returns></returns>
        //private static DbCommandBuilder CreateCommandBuilder(bool isHaveAdapter)
        //{
        //    DbCommandBuilder cb = CreateCommandBuilder();
        //    if (isHaveAdapter)
        //        cb.DataAdapter = CreateDataAdapter(CreateCommand(CreateConnection()));
        //    return cb;
        //}

        /// <summary>
        /// 根据Command对象产生数据读取器。
        /// 使用完请调用Close()方法关闭读取器。
        /// </summary>
        /// <param name="cmd">Command对象</param>
        /// <returns>数据读取器</returns>
        public static DbDataReader CreateDataReader(DbCommand cmd)
        {
            return CreateDataReader(cmd, CommandBehavior.CloseConnection);
        }

        /// <summary>
        /// 根据Command对象和读取行为创建数据读取器。
        /// 使用完请调用Close()方法关闭读取器。
        /// </summary>
        /// <param name="cmd">Command对象</param>
        /// <param name="behavior">读取行为</param>
        /// <returns>数据读取器</returns>
        private static DbDataReader CreateDataReader(DbCommand cmd, CommandBehavior behavior)
        {
            return cmd.ExecuteReader(behavior);
        }

        /// <summary>
        /// 根据sql语句产生数据读取器。
        /// 使用完请调用Close()方法关闭读取器。
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>数据读取器</returns>
        public static DbDataReader CreateDataReader(string sql)
        {
            return CreateDataReader(CreateCommand(sql));
        }

        /// <summary>
        /// 根据sql语句和参数信息产生数据读取器。
        /// 使用完请调用Close()方法关闭读取器。
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameterBox">参数信息</param>
        /// <returns>数据读取器</returns>
        public static DbDataReader CreateDataReader(String sql, ParameterBox parameterBox)
        {
            return CreateDataReader(CreateCommand(sql, parameterBox));
        }

        /// <summary>
        /// 根据sql语句产生表信构造信息读取器。
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>数据读取器</returns>
        public static DbDataReader GetSchemaDataReader(string sql)
        {
            return CreateDataReader(CreateCommand(sql), CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo | CommandBehavior.CloseConnection);
        }

        /// <summary>
        /// 根据sql语句产生包含表信息的数据读取器
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>数据读取器</returns>
        public static DbDataReader GetDataReaderWithInfo(string sql)
        {
            return CreateDataReader(CreateCommand(sql), CommandBehavior.KeyInfo | CommandBehavior.CloseConnection);
        }

        /// <summary>
        /// 根据sql语句和参数信息产生包含表信息的数据读取器
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="pb">参数信息</param>
        /// <returns>数据读取器</returns>
        public static DbDataReader GetDataReaderWithInfo(string sql, ParameterBox pb)
        {
            return CreateDataReader(CreateCommand(sql, pb), CommandBehavior.KeyInfo | CommandBehavior.CloseConnection);
        }
        /// <summary>
        /// 关闭DataReader
        /// </summary>
        /// <param name="reader"></param>
        public static void CloseDataReader(DbDataReader reader)
        {
            if (reader == null) { return; }
            try { if (!reader.IsClosed) { reader.Close(); reader.Dispose(); } }
            catch (Exception) { }
            reader = null;
        }

        /// <summary>
        /// 关闭数据填充器
        /// </summary>
        /// <param name="adapter"></param>
        public static void CloseDataAdapter(DbDataAdapter adapter)
        {
            if (adapter == null)
                return;
            CloseCommand(adapter.SelectCommand);
            CloseCommand(adapter.UpdateCommand);
            CloseCommand(adapter.DeleteCommand);
            CloseCommand(adapter.InsertCommand);
            adapter.Dispose();
        }
    }
}