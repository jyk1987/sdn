using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using SDN2.VO;

namespace WebTest
{
    /// <summary>
    /// JSON 的摘要说明
    /// </summary>
    public class JSON : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/json";
            GObject obj = new GObject();
            obj.Add("name", "任肖").Add("age", "19").Add("desc", "你好爱上飞Y&*^*(67Y*(OIH*^_(*\"\n\tdd3").Add("sex", true).Add("day", DateTime.Now); ;
            GObjectList list = new GObjectList();
            for (int i = 0; i < 10; i++)
            {
                GObject obj1 = new GObject();
                obj1["name"] = "名字1";
                obj1["sex"] = false;
                obj1["day"] = DateTime.Now;
                list.Add(obj1);
            }
            obj["list"] = list;
            context.Response.Write(obj.GetJSON());
        }

        public bool IsReusable
        {
            get{ return false;}
        }


        private const string NonEncodingChats = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789`!@#$%^&*()_+|-=\\,./;'[]{}:<>?";

        public static string StringToWChar(string str)
        {
            StringBuilder outs = new StringBuilder();
            foreach (char item in str)
            {
                if (IsEncoding(item))//判断是否编码
                {
                    outs.Append(string.Format("\\u{0:x4}", (int)item));
                }
                else//不是中文
                {
                    outs.Append(item);
                }
            }
            return outs.ToString();
        }

        private static bool IsEncoding(char cha)
        {
            return NonEncodingChats.IndexOf(cha) == -1;
        }
    }
}