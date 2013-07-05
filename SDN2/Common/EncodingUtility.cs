using System;
using System.Collections.Generic;
using System.Text;

namespace SDN2.Common
{
    public static class EncodingUtility
    {
        public static string GetGB2312Coding(string str)
        {
            byte[] coding = Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding("GB2312"), Encoding.UTF8.GetBytes(str));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < coding.Length; i++)
            {
                sb.Append("%" + Convert.ToString(coding[i], 16));
            }
            return sb.ToString();
        }
    }
}
