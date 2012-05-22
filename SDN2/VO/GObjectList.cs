using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading.Tasks;
using SDN2.Common.Web;

namespace SDN2.VO
{
    [Serializable]
    public class GObjectList : IEnumerable<GObject>
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
        /// 实体名属性
        /// </summary>
        public string EntityName
        {
            get { return this.entityName; }
            set { this.entityName = value.ToLower(); }
        }

        /// <summary>
        /// 集合数据
        /// </summary>
        List<GObject> listData = new List<GObject>(0);

        /// <summary>
        /// 索引数据对象
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>数据对象，如果不存在则返回null</returns>
        public GObject this[int index]
        {
            get
            {
                return index > this.listData.Count ? null : this.listData[index]; 
            }
            set
            {
                lock (this._lock)
                {
                    this.listData[index] = value;
                }
            }
        }

        /// <summary>
        /// 构建GObjectList对象
        /// </summary>
        public GObjectList() { }


        /// <summary>
        /// 构建GObjectList对象
        /// </summary>
        /// <param name="EntityName">实体名</param>
        public GObjectList(string EntityName)
        {
            this.EntityName = EntityName;
        }

        public GObjectList(string entityName, IList<GObject> listData)
        {
            //this.EntityName = EntityName;
            this.listData.AddRange(listData);
            this.SetEntityNameAndSync(entityName);
        }

        public GObjectList( IList<GObject> listData)
        {
            this.listData.AddRange(listData);
        }

        public GObjectList(string entityName, List<Dictionary<string, object>> listData)
        {
            foreach (Dictionary<string, object> item in listData)
            {
                this.Add(new GObject(item));
            }
            this.SetEntityNameAndSync(entityName);
        }

        public GObjectList(List<Dictionary<string, object>> listData)
        {
            foreach (Dictionary<string, object> item in listData)
            {
                this.Add(new GObject(item));
            }
        }






        /// <summary>
        /// 增加数据对象
        /// </summary>
        /// <param name="obj">数据对象</param>
        /// <returns>已经增加数据的集合</returns>
        public GObjectList Add(GObject obj)
        {
            lock (this._lock)
            {
                this.listData.Add(obj);
            }
            return this;
        }

        /// <summary>
        /// 增加数据对象集合
        /// </summary>
        /// <param name="list">数据对象集合</param>
        /// <returns>已经增加数据的集合</returns>
        public GObjectList Add(GObjectList list)
        {
            lock (this._lock)
            {
                this.listData.AddRange(list);
            }
            return this;
        }


        /// <summary>
        /// 移除数据对象
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>已经移除数据的集合</returns>
        public GObjectList Remove(int index)
        {
            lock (this._lock)
            {
                this.listData.RemoveAt(index);
            }
            return this;
        }

        /// <summary>
        /// 移除数据对象
        /// </summary>
        /// <param name="obj">数据对象</param>
        /// <returns>已经移除数据的集合</returns>
        public GObjectList Remove(GObject obj)
        {
            lock (this._lock)
            {
                this.listData.Remove(obj);
            }
            return this;
        }

        /// <summary>
        /// 设置实体名
        /// </summary>
        /// <param name="entityName">实体名</param>
        /// <returns>修改数据后的集合</returns>
        public GObjectList SetEntityName(string entityName)
        {
            this.EntityName = entityName;
            return this;
        }

        /// <summary>
        /// 设置实体名并同步到每一个数据对象上
        /// </summary>
        /// <param name="entityName">实体名</param>
        /// <returns>修改数据后的集合</returns>
        public GObjectList SetEntityNameAndSync(string entityName)
        {
            this.EntityName = entityName;
            lock (_lock)
            {
                Parallel.For(0, this.listData.Count, i =>
                {
                    this.listData[i].EntityName = this.EntityName;
                });
            }
            return this;
        }

        /// <summary>
        /// 集合大小(只读)
        /// </summary>
        public int Count
        {
            get { return this.listData.Count; }
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



        #region IEnumerable<GObject> 成员

        public IEnumerator<GObject> GetEnumerator()
        {
            return this.listData.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.listData.GetEnumerator();
        }

        #endregion

        public static GObjectList Load(string sql)
        {
            return null;
        }

    }
}
