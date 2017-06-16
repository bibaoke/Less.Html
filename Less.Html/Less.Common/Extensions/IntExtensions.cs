//bibaoke.com

using Less.Text;
using System;

namespace Less
{
    /// <summary>
    /// 32位整型的扩展方法
    /// </summary>
    public static class IntExtensions
    {
        private static Random MyRandom
        {
            get;
            set;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        static IntExtensions()
        {
            IntExtensions.MyRandom = new Random();
        }

        /// <summary>
        /// 是否与数组中的任意一项相等
        /// </summary>
        /// <param name="i"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public static bool In(this int i, params int[] array)
        {
            foreach (int item in array)
            {
                if (i == item)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// 是否偶数
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static bool IsEvenNumber(this int i)
        {
            return i != 0 && i % 2 == 0;
        }

        /// <summary>
        /// 以此实例为最大值
        /// 生成随机数
        /// 不包括最大值
        /// </summary>
        /// <returns></returns>
        public static int Random(this int i)
        {
            return MyRandom.Next(i);
        }

        /// <summary>
        /// 输出指定长度的字符串
        /// 数字长度不足在前面补零
        /// </summary>
        /// <param name="i"></param>
        /// <param name="length">字符串长度</param>
        /// <returns></returns>
        public static string ToString(this int i, int length)
        {
            string original = i.ToString();

            return string.Concat("0".Repeat(length - original.Length), original);
        }

        /// <summary>
        /// 是否在指定的范围之间
        /// </summary>
        /// <param name="i"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static bool Between(this int i, int begin, int end)
        {
            return i >= begin && i <= end;
        }
    }
}
