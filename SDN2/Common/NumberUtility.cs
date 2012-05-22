using System;
using System.Collections.Generic;
using System.Text;

namespace SDN2.Common
{
    /// <summary>
    /// 数字工具类
    /// </summary>
    public static class NumberUtility
    {
        private static Random r = new Random();

        /// <summary>
        /// 所对象
        /// </summary>
        private static object _lock = new object();

        /// <summary>
        /// 得到一组随机数(无重复值)
        /// </summary>
        /// <param name="beginNum">开始数值</param>
        /// <param name="endNum">结束数值</param>
        /// <param name="count">需要的随机数个数</param>
        /// <returns></returns>
        public static IList<int> CreateRandom(int beginNum, int endNum, int count)
        {
            List<int> result = new List<int>();
            List<int> fullNumber = new List<int>();
            for (int i = beginNum; i <= endNum; i++)
            {
                fullNumber.Add(i);
            }
            for (int i = 0; i < count; )
            {
                int index = 0;
                if (fullNumber.Count == 1)
                {
                    result.Add(fullNumber[index]);
                    break;
                }
                lock (_lock)
                {
                    index = r.Next(fullNumber.Count);
                }
                result.Add(fullNumber[index]);
                fullNumber.RemoveAt(index);
                i++;
            }
            return result;
        }

        /// <summary>
        /// 创建一个随机数
        /// </summary>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        /// <returns></returns>
        public static int CreateRandomNumber(int minValue, int maxValue)
        {
            int result = 0;
            lock (_lock)
            {
                result = r.Next(minValue, maxValue);
            }
            return result;
        }

    }
}
