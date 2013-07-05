using System;
using System.Collections.Generic;
using System.Text;

namespace SDN2.DB.DAO
{
    /// <summary>
    /// 列信息
    /// </summary>
    public class ColumnInfo
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName
        {
            get { return this.infos[ColumnInfoKeys.ColumnName]; }
        }

        private IDictionary<string, string> infos = new Dictionary<string, string>();

        /// <summary>
        /// 列类型信息
        /// </summary>
        /// <param name="typeName">类型名</param>
        /// <returns></returns>
        public string this[string typeName]
        {
            get { return this.infos[typeName]; }
            set { this.infos[typeName] = value; }
        }

        /// <summary>
        /// 是否主键
        /// </summary>
        public bool IsPrimaryKey
        {
            get { return this[ColumnInfoKeys.IsKey] == bool.TrueString; }
        }

        /// <summary>
        /// 是否自增
        /// </summary>
        public bool IsAutoIncrement
        {
            get { return this[ColumnInfoKeys.IsAutoIncrement] == bool.TrueString; }
        }

    }
    /// <summary>
    /// 取出列信息的key
    /// </summary>
    public static class ColumnInfoKeys
    {
        #region 常量key
        /// <summary>
        /// 列名
        /// </summary>
        public const String ColumnName = "ColumnName";

        /// <summary>
        /// 列顺序
        /// </summary>
        public const String ColumnOrdinal = "ColumnOrdinal";

        /// <summary>
        /// 列大小
        /// </summary>
        public const String ColumnSize = "ColumnSize";

        /// <summary>
        /// 数值精度
        /// </summary>
        public const String NumericPrecision = "NumericPrecision";

        /// <summary>
        /// 小数部份的位数
        /// </summary>
        public const String NumericScale = "NumericScale";

        /// <summary>
        /// 是为唯一约束
        /// </summary>
        public const String IsUnique = "IsUnique";

        /// <summary>
        /// 数否主键
        /// </summary>
        public const String IsKey = "IsKey";

        /// <summary>
        /// 基础服务器名称
        /// </summary>
        public const String BaseServerName = "BaseServerName";

        /// <summary>
        /// 基础目录名
        /// </summary>
        public const String BaseCatalogName = "BaseCatalogName";

        /// <summary>
        /// 基础列名
        /// </summary>
        public const String BaseColumnName = "BaseColumnName";

        /// <summary>
        /// 基础结构名
        /// </summary>
        public const String BaseSchemaName = "BaseSchemaName";

        /// <summary>
        /// 基础表明
        /// </summary>
        public const String BaseTableName = "BaseTableName";

        /// <summary>
        /// 数据类型
        /// </summary>
        public const String DataType = "DataType";

        /// <summary>
        /// 是否允许空
        /// </summary>
        public const String AllowDBNull = "AllowDBNull";

        /// <summary>
        /// 提供程序类型
        /// </summary>
        public const String ProviderType = "ProviderType";

        /// <summary>
        /// 是否有别名
        /// </summary>
        public const String IsAliased = "IsAliased";

        /// <summary>
        /// 是否值来自于计算出的表达式
        /// </summary>
        public const String IsExpression = "IsExpression";

        /// <summary>
        /// 是否标示
        /// </summary>
        public const String IsIdentity = "IsIdentity";

        /// <summary>
        /// 是否自增
        /// </summary>
        public const String IsAutoIncrement = "IsAutoIncrement";

        /// <summary>
        /// 是否包含行版本信息
        /// </summary>
        public const String IsRowVersion = "IsRowVersion";

        /// <summary>
        /// 是否隐藏
        /// </summary>
        public const String IsHidden = "IsHidden";

        /// <summary>
        /// IsLong
        /// </summary>
        public const String IsLong = "IsLong";

        /// <summary>
        /// 是否只读
        /// </summary>
        public const String IsReadOnly = "IsReadOnly";

        /// <summary>
        /// 对应的数据类
        /// </summary>
        public const String ProviderSpecificDataType = "ProviderSpecificDataType";

        /// <summary>
        /// 数据库的数据类型
        /// </summary>
        public const String DataTypeName = "DataTypeName";

        /// <summary>
        /// 此 XML 实例的架构集合所在的数据库的 String 形式的名称
        /// </summary>
        public const String XmlSchemaCollectionDatabase = "XmlSchemaCollectionDatabase";

        /// <summary>
        /// String 形式的此 XML 实例的架构集合所在的所属关系架构。
        /// </summary>
        public const String XmlSchemaCollectionOwningSchema = "XmlSchemaCollectionOwningSchema";

        /// <summary>
        /// 此 XML 实例的架构集合的 String 形式的名称。
        /// </summary>
        public const String XmlSchemaCollectionName = "XmlSchemaCollectionName";

        /// <summary>
        /// UdtAssemblyQualifiedName
        /// </summary>
        public const String UdtAssemblyQualifiedName = "UdtAssemblyQualifiedName";

        /// <summary>
        /// NonVersionedProviderType
        /// </summary>
        public const String NonVersionedProviderType = "NonVersionedProviderType";

        /// <summary>
        /// IsColumnSet
        /// </summary>
        public const String IsColumnSet = "IsColumnSet";
        #endregion
    }
}
