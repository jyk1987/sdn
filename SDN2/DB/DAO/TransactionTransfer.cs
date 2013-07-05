using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Threading;

namespace SDN2.DB.DAO
{
    /// <summary>
    /// 事务中转中心
    /// 为了方便不同数据库操作方法中的事物实现
    /// 请先调用CreateTransaction方法创建一个事务，创建事务的时候需要输入事务名称，
    /// 在其他方法中需要访问同一事务的时候要输入相同事务名称。
    /// </summary>
    public class TransactionTransfer
    {

        private TransactionTransfer() { }

        /// <summary>
        /// 用于存在跨多个业务的事物工厂
        /// </summary>
        private static IDictionary<string, TransactionFactory> nameTF = new Dictionary<string, TransactionFactory>();

        /// <summary>
        /// 用于存储同一线程内使用事物工厂
        /// </summary>
        private static IDictionary<int, TransactionFactory> threadTF = new Dictionary<int, TransactionFactory>();

        /// <summary>
        /// 当前线程使用事务工厂
        /// </summary>
        internal static TransactionFactory CurrentTransactionFactory
        {
            get { return ExistTransaction ? threadTF[Thread.CurrentThread.ManagedThreadId] : null; }
        }

        /// <summary>
        /// 根据事物名称创建一个事物，名称自定义，只要保证不和别的名称冲突即可
        /// </summary>
        /// <param name="transactionName">事务名称</param>
        public static void CreateTransaction(string transactionName)
        {
            nameTF[transactionName] = new TransactionFactory();
        }

        /// <summary>
        /// 根据线程创建一个事物
        /// </summary>
        public static void CreateTransaction()
        {
            threadTF[Thread.CurrentThread.ManagedThreadId] = new TransactionFactory();
        }

        /// <summary>
        /// 当前线程是否存在事物
        /// </summary>
        public static bool ExistTransaction
        {
            get
            {
                TransactionFactory temp;
                return threadTF.TryGetValue(Thread.CurrentThread.ManagedThreadId, out temp);
            }
        }

        /// <summary>
        /// 使用当前线程内的事物执行sql
        /// </summary>
        /// <param name="sql"></param>
        internal static int ExecuteSql(string sql)
        {
            return threadTF[Thread.CurrentThread.ManagedThreadId].ExecuteSql(sql);
        }

        /// <summary>
        /// 执行只返回一行一列的sql语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        internal static object ExecuteScalar(string sql)
        {
            return threadTF[Thread.CurrentThread.ManagedThreadId].ExecuteScalar(sql);
        }
        /// <summary>
        /// 使用当前线程内的事物执行sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameterBox"></param>
        internal static int ExecuteSql(string sql, ParameterBox parameterBox)
        {
            return threadTF[Thread.CurrentThread.ManagedThreadId].ExecuteSql(sql, parameterBox);
        }


        /// <summary>
        /// 执行只返回一行一列的sql语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameterBox">参数信息</param>
        /// <returns></returns>
        internal static object ExecuteScalar(string sql, ParameterBox parameterBox)
        {
            return threadTF[Thread.CurrentThread.ManagedThreadId].ExecuteScalar(sql, parameterBox);
        }
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="transactionName">事务名称</param>
        /// <param name="sql">sql语句</param>
        internal static int ExecuteSql(string transactionName, string sql)
        {
            return nameTF[transactionName].ExecuteSql(sql);
        }

        /// <summary>
        /// 执行带参数的sql语句
        /// </summary>
        /// <param name="transactionName">事务名称</param>
        /// <param name="sql">sql语句</param>
        /// <param name="parameterBox">参数信息</param>
        internal static int ExecuteSql(string transactionName, string sql, ParameterBox parameterBox)
        {
            return nameTF[transactionName].ExecuteSql(sql, parameterBox);
        }

        /// <summary>
        /// 提交事务,事务在提交之后会被移除事务中转中心
        /// </summary>
        /// <param name="transactionName">事务名称</param>
        public static bool Commit(string transactionName)
        {
            bool result = false;
            result = nameTF[transactionName].Commit();
            nameTF.Remove(transactionName);
            return result;
        }

        /// <summary>
        /// 根据事务名称进行事物回滚
        /// </summary>
        /// <param name="transactionName"></param>
        /// <returns></returns>
        public static void Rollback(string transactionName)
        {
            nameTF[transactionName].Rollback();
            nameTF.Remove(transactionName);
        }

        /// <summary>
        /// 提交当前线程内的事物
        /// </summary>
        public static bool Commit()
        {
            bool result = false;
            result = threadTF[Thread.CurrentThread.ManagedThreadId].Commit();
            threadTF.Remove(Thread.CurrentThread.ManagedThreadId);
            return result;
        }

        /// <summary>
        /// 事务回滚
        /// </summary>
        public static void Rollback()
        {
            threadTF[Thread.CurrentThread.ManagedThreadId].Rollback();
            threadTF.Remove(Thread.CurrentThread.ManagedThreadId);
        }
    }
}
