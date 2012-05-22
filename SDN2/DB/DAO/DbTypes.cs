using System;
using System.Collections.Generic;
using System.Text;

namespace SDN2.DB.DAO
{
    /// <summary>
    /// 数据库类型
    /// </summary>
    //public static class DbTypes
    //{
    //    /// <summary>
    //    /// MS SqlServer
    //    /// </summary>
    //    public const string MSSQL = "MSSQL";

    //    /// <summary>
    //    /// MySql
    //    /// </summary>
    //    public const string MYSQL = "MYSQL";

    //    /// <summary>
    //    /// Oracle
    //    /// </summary>
    //    public const string ORACLE = "ORACLE";

    //    /// <summary>
    //    /// OlEDB
    //    /// </summary>
    //    public const string OlEDB = "OlEDB";

    //    /// <summary>
    //    /// SQLite
    //    /// </summary>
    //    public const string SQLITE = "SQLITE";
    //}

    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum SDNDbType
    {
        MsSQL = 1, MySQL = 2, Oracle = 3, OleDB = 4, SQLite = 5
    }
}
