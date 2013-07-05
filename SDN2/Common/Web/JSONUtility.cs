using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDN2.VO;
using System.Web.Script.Serialization;

namespace SDN2.Common.Web
{
    /// <summary>
    /// JSON工具类
    /// </summary>
    public class JSONUtility
    {
        /// <summary>
        /// 将数据对象转换成json
        /// </summary>
        /// <param name="obj">数据对象</param>
        /// <param name="type">编码类型</param>
        /// <returns>json字符串</returns>
        public static string ToJSON(GObject obj,JSONEncodingType type)
        {
            JSONObject jobj = new JSONObject(obj);
            return jobj.GetJSON(type);
        }

        /// <summary>
        /// 将数据对象集合转化成json
        /// </summary>
        /// <param name="list">数据对象集合</param>
        /// <param name="type">编码类型</param>
        /// <returns>json字符串</returns>
        public static string ToJSON(GObjectList list, JSONEncodingType type)
        {
            JSONObject jobj = new JSONObject(list);
            return jobj.GetJSON(type);
        }

        /// <summary>
        /// 属性值编码
        /// </summary>
        /// <param name="str">属性值</param>
        /// <param name="type">编码类型</param>
        /// <returns>编码后的内容</returns>
        private static string ValueEncoding(string str, JSONEncodingType type)
        {
            switch (type)
            {
                case JSONEncodingType.None:
                    return StringUtility.JSONEncoding(str);
                case JSONEncodingType.Chniese:
                    return StringUtility.JSONEncodingWithChinese(str);
                default:
                    return string.Empty;
            }
        }


        /// <summary>
        /// 属性对象转换成字符串
        /// </summary>
        /// <param name="GObjValue">属性对象</param>
        /// <param name="type">编码类型</param>
        /// <returns>属性对应的字符串</returns>
        internal static string ValueToString(object GObjValue, JSONEncodingType type)
        {
            if (GObjValue is GObject)
            {
                return JSONUtility.ToJSON(GObjValue as GObject, type);
            }
            if (GObjValue is GObjectList)
            {
                return JSONUtility.ToJSON(GObjValue as GObjectList, type);
            }
            if (GObjValue is DateTime)
            {
                return string.Format("\"{0}\"", ((DateTime)GObjValue).ToString("yyyy-MM-dd HH:mm:ss"));
            }
            int temp;
            if (int.TryParse(GObjValue.ToString(), out temp))
            {
                return GObjValue.ToString();
            }
            if (GObjValue is bool)
            {
                return GObjValue.ToString().ToLower();
            }
            if (GObjValue is string)
            {
                return string.Format("\"{0}\"", ValueEncoding(GObjValue.ToString(), type));
            }
            return string.Format("\"{0}\"", GObjValue);
        }

    }
}
