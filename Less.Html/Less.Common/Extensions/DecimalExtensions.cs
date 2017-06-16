//bibaoke.com

using Less.Text;
using System;

namespace Less
{
    /// <summary>
    /// decimal 扩展方法
    /// </summary>
    public static class DecimalExtensions
    {
        /// <summary>
        /// 向下取整
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static int Floor(this decimal d)
        {
            return Math.Floor(d).ToInt();
        }

        /// <summary>
        /// 向上取整
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static int Ceiling(this decimal d)
        {
            return Math.Ceiling(d).ToInt();
        }

        /// <summary>
        /// 输出长整型
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static long ToLong(this decimal d)
        {
            return (long)d;
        }

        /// <summary>
        /// 输出整型
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static int ToInt(this decimal d)
        {
            return Convert.ToInt32(d);
        }

        /// <summary>
        /// 输出字符串
        /// </summary>
        /// <param name="d"></param>
        /// <param name="keep">保留小数位</param>
        /// <returns></returns>
        public static string ToString(this decimal d, int keep)
        {
            string format = "0";

            if (keep > 0)
                format.Combine(".", "#".Repeat(keep));

            return d.ToString(format);
        }
    }
}
