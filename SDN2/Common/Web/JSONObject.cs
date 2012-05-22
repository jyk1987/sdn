using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDN2.VO;

namespace SDN2.Common.Web
{
    public enum JSONEncodingType { None, Chniese }

    /// <summary>
    /// json对象必须有GObject或者GObjectList构建
    /// </summary>
    public class JSONObject
    {
        /// <summary>
        /// 存储json字符串内容
        /// </summary>
        StringBuilder jsonContent = null;

        /// <summary>
        /// 构建数据
        /// </summary>
        object data = null;
        private JSONObject() { }

        /// <summary>
        /// 用GObject构建json对象
        /// </summary>
        /// <param name="obj"></param>
        public JSONObject(GObject obj)
        {
            this.data = obj;
        }

        /// <summary>
        /// 用GObjectList构建json对象
        /// </summary>
        /// <param name="list"></param>
        public JSONObject(GObjectList list)
        {
            this.data = list;
        }

        /// <summary>
        /// 输出json，只会编码json中禁用的特殊字符
        /// </summary>
        /// <returns>json字符串</returns>
        public string GetJSON()
        {
            return this.GetJSON(JSONEncodingType.None);
        }

        /// <summary>
        /// 输出json
        /// </summary>
        /// <param name="type">编码类型</param>
        /// <returns>json字符串</returns>
        public string GetJSON(JSONEncodingType type)
        {
            if (data == null)
            {
                return string.Empty;
            }
            if (data is GObject)
            {
                return GObjectToJSON(type);
            }
            if (data is GObjectList)
            {
                return GObjectListToJSON(type);
            }
            return string.Empty;
        }

        /// <summary>
        /// 输出数据对象的json字符串
        /// </summary>
        /// <param name="type">加密类型</param>
        /// <returns>json字符串</returns>
        private string GObjectToJSON(JSONEncodingType type)
        {
            jsonContent = new StringBuilder("{");
            GObject obj = this.data as GObject;
            foreach (string attribute in obj)
            {
                jsonContent.Append(string.Format("\"{0}\":{1},", attribute, JSONUtility.ValueToString(obj.Get(attribute), type)));
            }
            return jsonContent.Replace(',', '}', jsonContent.Length - 1, 1).ToString();
        }

        /// <summary>
        /// 输出数据对象集合的json字符串
        /// </summary>
        /// <param name="type">编码类型</param>
        /// <returns>json字符串</returns>
        private string GObjectListToJSON(JSONEncodingType type)
        {
            jsonContent = new StringBuilder("[");
            GObjectList list = this.data as GObjectList;
            for (int i = 0; i < list.Count; i++)
            {
                jsonContent.Append(list[i].GetJSON(type));
                jsonContent.Append(",");
            }
            jsonContent.Replace(',', ']', jsonContent.Length - 1, 1);
            return jsonContent.ToString();
        }

    }
}
