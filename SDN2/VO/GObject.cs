using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using SDN2.Exceptions;
using SDN2.Common.Web;

namespace SDN2.VO
{
    /// <summary>
    /// 通用数据对象
    /// </summary>
    [Serializable]
    public class GObject : IEnumerable
    {
        /// <summary>
        /// 实体名
        /// </summary>
        private string entityName = string.Empty;

        /// <summary>
        /// 数据操作锁
        /// </summary>
        private object _lock = new object();

        /// <summary>
        /// 实体名属性,实体名在这支的时候会被自动转换成小写
        /// </summary>
        public string EntityName
        {
            get
            {
                return this.entityName;
            }
            set
            {
                this.entityName = value.ToLower();
            }
        }

        /// <summary>
        /// 属性索引器
        /// </summary>
        /// <param name="attribute">属性名</param>
        /// <returns>属性值</returns>
        public object this[string attribute]
        {
            get
            {
                attribute = attribute.ToLower();
                object tempValue = null;
                if (this.objectData.TryGetValue(attribute, out tempValue))
                {
                    return tempValue;
                }
                throw new AttributeNotFindException(attribute);
            }
            set
            {
                lock (_lock)
                {
                    this.objectData[attribute.ToLower()] = value;
                }
            }
        }


        /// <summary>
        /// 对象数据
        /// </summary>
        internal Dictionary<string, object> objectData = new Dictionary<string, object>(0);

        /// <summary>
        /// 构建GObject对象
        /// </summary>
        public GObject() { }

        /// <summary>
        /// 构建包含实体名的GObject对象
        /// </summary>
        /// <param name="entityName">实体名</param>
        public GObject(string entityName)
        {
            this.EntityName = entityName;
        }

        /// <summary>
        /// 构建包含实体名和数据的GObject对象
        /// </summary>
        /// <param name="entityName">实体名</param>
        /// <param name="data">对象数据</param>
        public GObject(string entityName,Dictionary<string,object> data)
        {
            this.EntityName = entityName;
            Add(data);
        }

        /// <summary>
        /// 构建包含数据的GObject对象
        /// </summary>
        /// <param name="data">对象数据</param>
        public GObject(Dictionary<string, object> data)
        {
            Add(data);
        }


        /// <summary>
        /// 迭代所有的属性
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return this.objectData.Keys.GetEnumerator();
        }

        /// <summary>
        /// 增加新属性
        /// </summary>
        /// <param name="attribute">属性名</param>
        /// <param name="value">属性值</param>
        /// <returns></returns>
        public GObject Add(string attribute, object value)
        {
            this[attribute] = value;
            return this;
        }

        /// <summary>
        /// 增加属性
        /// </summary>
        /// <param name="data">属性数据</param>
        /// <returns>已修改数据数据对象</returns>
        public GObject Add(Dictionary<string, object> data)
        {
            lock (_lock)
            {
                foreach (string attribute in data.Keys)
                {
                    this[attribute.ToLower()] = data[attribute];
                }
            }
            return this;
        }

        /// <summary>
        /// 移除属性
        /// </summary>
        /// <param name="attribute">属性名</param>
        /// <returns>已修改数据数据对象</returns>
        public object Remove(string attribute)
        {
            try
            {
                lock (_lock)
                {
                    this.objectData.Remove(attribute);
                }
            }
            catch (Exception) { }
            return this;
        }

        /// <summary>
        /// 取出属性
        /// </summary>
        /// <param name="attribute">属性名</param>
        /// <returns>属性值</returns>
        public object Get(string attribute)
        {
            return this[attribute];
        }

        /// <summary>
        /// 取出属性
        /// </summary>
        /// <typeparam name="T">返回属性的类型</typeparam>
        /// <param name="attribute">属性名</param>
        /// <returns>属性值</returns>
        public T Get<T>(string attribute)
        {
            return (T)this[attribute];
        }

        /// <summary>
        /// 尝试取出属性
        /// </summary>
        /// <param name="attribute">属性名</param>
        /// <param name="value">属性值</param>
        /// <returns>是否取出成功</returns>
        public bool TryGetValue(string attribute, out object value)
        {
            return this.objectData.TryGetValue(attribute, out value);
        }

        /// <summary>
        /// 得到当前数据对象的json字符串
        /// </summary>
        /// <returns>json字符串</returns>
        public string GetJSON()
        {
            return JSONUtility.ToJSON(this, JSONEncodingType.None);
        }

        /// <summary>
        /// 得到当前数据对象的json字符串
        /// </summary>
        /// <param name="type">编码类型</param>
        /// <returns>json字符串</returns>
        public string GetJSON(JSONEncodingType type)
        {
            return JSONUtility.ToJSON(this, type);
        }

        #region 数据持久化方法

        public bool Save()
        {

            return false;
        }

        public bool Update()
        {
            return false;
        }

        public bool Delete()
        {
            return false;
        }
        #endregion



    }
}
