using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace SDN2.DB.DAO
{
    /// <summary>
    /// 数据库信息存储中心，请调用Init(string db_url, string db_type)
    /// 方法初始化信息，如果未初始化，在使用时会发生异常
    /// 如果程序不初始化数据库信息可以在config文件中增加以下配置
    /// &lt;connectionStrings&gt;
    /// &lt;!--默认数据库（会被系统自动加载，目前只支持mssql）--&gt;
    ///     &lt;add name="defaultConnection" connectionString="Data Source=localhost;Initial Catalog=mydb;Persist Security Info=True;User ID=sa;Password=123456"/&gt;
    ///     &lt;/connectionStrings&gt;
    /// </summary>
    public class DbInfo
    {
        /// <summary>
        /// 系统数据库名称
        /// </summary>
        public static string SystemDataBase = "SystemDataBase_001";

        private static bool isload = false;

        /// <summary>
        /// 存储数据库信息对象
        /// </summary>
        private static DbInfo dbInfo = null;

        /// <summary>
        /// 存储次数据库信息
        /// </summary>
        private static Dictionary<string, DbInfo> otherDbInfo = new Dictionary<string, DbInfo>();

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private string db_url;

        /// <summary>
        /// 数据库类型
        /// </summary>
        private SDNDbType db_type;

        /// <summary>
        /// 初始化数据库连接字符串和数据库类型的构造方法
        /// </summary>
        /// <param name="db_url">数据库连接字符串</param>
        /// <param name="db_type">数据库类型</param>
        private DbInfo(string db_url, SDNDbType db_type)
        { this.db_url = db_url; this.db_type = db_type; }

        /// <summary>
        /// 检测数据库信息是否被初始化，如果没有加载则抛出异常
        /// </summary>
        private static void checkDbInfo()
        {
            if (!isload) Init();
            if (dbInfo == null) throw new Exception("默认数据库信息未初始化!");
        }

        /// <summary>
        /// 检查相应标示的数据库是否加载，如果没有加载则抛出异常
        /// </summary>
        /// <param name="dbname">数据库信息表示</param>
        private static void checkDbInfo(string dbname)
        {
            DbInfo di = null;
            if (!otherDbInfo.TryGetValue(dbname, out di))
                throw new Exception(dbname + "数据库信息未加载!");
        }

        /// <summary>
        /// 判断数据库信息是否已经加载
        /// </summary>
        /// <returns></returns>
        public static bool IsInit
        {
            get { if (dbInfo == null) return false; return true; }
        }

        /// <summary>
        /// 数据库信息初始化方法
        /// </summary>
        /// <param name="db_url">数据库连接字符串</param>
        /// <param name="db_type">数据库类型</param>
        public static void Init(string db_url, SDNDbType db_type)
        { if (dbInfo == null) dbInfo = new DbInfo(db_url, db_type); isload = true; }

        /// <summary>
        /// 加载次数据库
        /// </summary>
        /// <param name="db_url">数据库连接字符串</param>
        /// <param name="db_type">数据库类型</param>
        /// <param name="dbname">数据库信息标示</param>
        public static void Init(string db_url, SDNDbType db_type, string dbname)
        { otherDbInfo[dbname] = new DbInfo(db_url, db_type); }

        /// <summary>
        /// 加载配置文件connectionStrings节点下名称为defaultConnection的数据库为默认数据库
        /// </summary>
        private static void Init()
        {
            ConnectionStringSettings css = ConfigurationManager.ConnectionStrings["defaultConnection"];
            Init(css.ConnectionString, SDNDbType.MsSQL);
            //try
            //{
            //    string location = ConfigurationManager.AppSettings["sysdblocation"];
            //    string sysdb = ConfigurationManager.AppSettings["sysdb"];
            //    string uname = ConfigurationManager.AppSettings["sysdbuname"];
            //    string upass = ConfigurationManager.AppSettings["sysdbupass"];
            //    string url = "Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}";
            //    int count = url.Length;
            //    url = string.Format(url, location, sysdb, uname, upass);
            //    if (url.Length > count)
            //    {
            //        Init(url, DbTypes.MSSQL);
            //    }
            //}
            //catch (Exception) { }

            /*
                <appSettings>
                    <add key="sysdblocation" value="localhost"/>
                    <add key="sysdb" value="mydb"/>
                    <add key="sysdbuname" value="sa"/>
                    <add key="sysdbupass" value="123456"/>
                </appSettings>
             */
        }


        /// <summary>
        /// 得到主数据库连接URL
        /// </summary>
        /// <returns></returns>
        public static string GetDbUrl()
        {
            checkDbInfo(); return dbInfo.db_url;
        }

        /// <summary>
        /// 根据数据库信息标示，得到数据库URL
        /// </summary>
        /// <param name="dbname">数据库信息标示</param>
        /// <returns></returns>
        public static string GetDbUrl(string dbname)
        {
            checkDbInfo(dbname);
            return otherDbInfo[dbname].db_url;
        }

        /// <summary>
        /// 得到主数据类型
        /// </summary>
        /// <returns></returns>
        public static SDNDbType GetDbType()
        {
            checkDbInfo();
            return dbInfo.db_type;
        }

        /// <summary>
        /// /// 根据数据库信息标示，得到数据库类型
        /// </summary>
        /// <param name="dbname">数据库信息标示</param>
        /// <returns></returns>
        public static SDNDbType GetDbType(string dbname)
        {
            checkDbInfo(dbname);
            return otherDbInfo[dbname].db_type;
        }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private static string DbUrl { get { checkDbInfo(); return dbInfo.db_url; } }

        /// <summary>
        /// 数据库类型
        /// </summary>
        private static SDNDbType DbType { get { checkDbInfo(); return dbInfo.db_type; } }
    }
}
