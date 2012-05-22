using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SDN2.Common
{
    /// <summary>
    /// 字符串工具类
    /// </summary>
    public class StringUtility
    {

        private const string jsonEncodingChars = "\\\r\n\t\"";
        private const string chineseNonEncodinChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789`!@#$%^&*()_+|-=,./;'[]{}:<>?";
        /// <summary>
        /// 过滤字符串中的JSON无法识别特殊字符
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>转换后的字符串</returns>
        public static string JSONEncoding(string str)
        {
            bool encoding = false;
            //检测是否需要编码
            foreach (char item in jsonEncodingChars)
            {
                if (str.IndexOf(item) > -1)
                {
                    encoding = true;
                    break;
                }
            }
            //不需要编码
            if (!encoding)
            {
                return str;
            }
            StringBuilder result = new StringBuilder(str);
            //编码
            foreach (char item in jsonEncodingChars)
            {
                result.Replace(item.ToString(), WCharEncoding(item));
            }
            return result.ToString();
        }

        /// <summary>
        /// 宽字符编码字符串中的JSON无法识别特殊字符和中文
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string JSONEncodingWithChinese(string str)
        {
            StringBuilder result = new StringBuilder(0);

            foreach (char item in str)
            {
                result.Append((chineseNonEncodinChars.IndexOf(item) == -1) ? item.ToString() : WCharEncoding(item));
            }

            return result.ToString();
        }

        /// <summary>
        /// 将字符转换成宽字符字符串
        /// </summary>
        /// <param name="c">需要转换的字符</param>
        /// <returns>转换后的字符串</returns>
        public static string WCharEncoding(char c)
        {
            return string.Format("\\u{0:x4}", (int)c);
        }

        /// <summary>
        /// 将字符串转换成宽字符字符创
        /// </summary>
        /// <param name="str">需要转换的字符串</param>
        /// <returns>转换后的字符串</returns>
        public static string WCharEncoding(string str)
        {
            if (str.Length == 0)
            {
                return string.Empty;
            }
            if (str.Length == 1)
            {
                return string.Format("\\u{0:x4}", (int)str[0]);
            }
            StringBuilder result = new StringBuilder(0);
            foreach (char item in str)
            {
                result.Append(string.Format("\\u{0:x4}", (int)item));
            }
            return result.ToString();
        }

        /// <summary>
        /// 将字符串数组输出成字符串
        /// </summary>
        /// <param name="strArray">字符串数组</param>
        /// <returns></returns>
        public static string StringArrayToString(string[] strArray)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < strArray.Length; i++)
            {
                sb.Append(strArray[i]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 将简单数据类型集合输出成字符串
        /// </summary>
        /// <param name="strArray">简单数据类型集合</param>
        /// <returns></returns>
        public static string StringArrayToString(IList strArray)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < strArray.Count; i++)
            {
                sb.Append(strArray[i]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 将字符串数组输出成字符串
        /// </summary>
        /// <param name="strArray">字符串数组</param>
        /// <param name="str">分隔符</param>
        /// <returns></returns>
        public static string StringArrayToString(string[] strArray, string str)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(strArray[0]);
            for (int i = 1; i < strArray.Length; i++)
            {
                sb.Append(str);
                sb.Append(strArray[i]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 将整型数组拼接成一个字符串
        /// </summary>
        /// <param name="objs">整型数组</param>
        /// <returns></returns>
        public static string ArrayToString(int[] objs)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < objs.Length; i++)
                sb.Append(objs[i].ToString());
            return sb.ToString();
        }

        /// <summary>
        /// 将整型数组拼接成一个字符串
        /// </summary>
        /// <param name="objs">整型数组</param>
        /// <param name="str">每个元素之间的分隔符</param>
        /// <returns></returns>
        public static string ArrayToString(int[] objs, string str)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(objs[0].ToString());
            for (int i = 1; i < objs.Length; i++)
            {
                sb.Append(str);
                sb.Append(objs[i].ToString());
            }
            return sb.ToString();
        }

        /// <summary>
        /// 截断字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="length">长度(字节长度)</param>
        /// <returns></returns>
        public static string CutString(string str, int length)
        {
            char[] chars = str.ToCharArray();
            int count = 0;
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < chars.Length; i++)
            {
                result.Append(chars[i]);
                count += System.Text.Encoding.Default.GetByteCount(new char[] { chars[i] });
                if (count == length) break;
                if (count > length) { result.Remove(result.Length - 1, 1); break; }

            }
            return result.ToString();
        }
    }
}
