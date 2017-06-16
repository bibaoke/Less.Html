//bibaoke.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Less.Collection;

namespace Less.Text
{
    /// <summary>
    /// 转换
    /// </summary>
    public static class ConvertExtensions
    {
        private static Regex[] Regexs
        {
            get;
            set;
        }

        /// <summary>
        /// 转换成正则表达式实例
        /// </summary>
        /// <param name="s"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static Regex ToRegex(this string s, RegexOptions options)
        {
            return new Regex(s, options);
        }

        /// <summary>
        /// 转换成正则表达式实例
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Regex ToRegex(this string s)
        {
            return new Regex(s);
        }

        /// <summary>
        /// 转换成字节数组
        /// </summary>
        /// <param name="s"></param>
        /// <param name="encoding">指定的编码</param>
        /// <returns></returns>
        public static byte[] ToByteArray(this string s, Encoding encoding)
        {
            return encoding.GetBytes(s);
        }

        /// <summary>
        /// 转换成时间
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string s)
        {
            DateTime result;

            DateTime.TryParse(s, out result);

            return result;
        }

        /// <summary>
        /// 转换成 Guid
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Guid ToGuid(this string s)
        {
            Guid result;

            Guid.TryParse(s, out result);

            return result;
        }

        /// <summary>
        /// 转换成 Guid
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Guid[] ToGuidArray(this string[] s)
        {
            Guid[] result = new Guid[s.Length];

            s.Each(delegate (int index, string i)
            {
                result[index] = i.ToGuid();
            });

            return result;
        }

        /// <summary>
        /// 转换成十进制数字型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string s)
        {
            decimal result;

            decimal.TryParse(s, out result);

            return result;
        }

        /// <summary>
        /// 转换成可空十进制数字型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static decimal? ToDecimalNullable(this string s)
        {
            decimal? nullable = null;

            decimal result;

            if (decimal.TryParse(s, out result))
                nullable = result;

            return nullable;
        }

        /// <summary>
        /// 转换成字节
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte ToByte(this string s)
        {
            byte result;

            byte.TryParse(s, out result);

            return result;
        }

        /// <summary>
        /// 转换成可空字节
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte? ToByteNullable(this string s)
        {
            byte? nullable = null;

            byte result;

            if (byte.TryParse(s, out result))
                nullable = result;

            return nullable;
        }

        /// <summary>
        /// 转换成短整型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static short ToShort(this string s)
        {
            short result;

            short.TryParse(s, out result);

            return result;
        }

        /// <summary>
        /// 转换成可空短整型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static short? ToShortNullable(this string s)
        {
            short? nullable = null;

            short result;

            if (short.TryParse(s, out result))
                nullable = result;

            return nullable;
        }

        /// <summary>
        /// 转换成整型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int ToInt(this string s)
        {
            int result;

            int.TryParse(s, out result);

            return result;
        }

        /// <summary>
        /// 转换成可空整型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int? ToIntNullable(this string s)
        {
            int? nullable = null;

            int result;

            if (int.TryParse(s, out result))
                nullable = result;

            return nullable;
        }

        /// <summary>
        /// 转换成整型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int[] ToIntArray(this string[] s)
        {
            int[] result = new int[s.Length];

            s.Each(delegate (int index, string i)
            {
                result[index] = i.ToInt();
            });

            return result;
        }

        /// <summary>
        /// 转换成长整型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static long ToLong(this string s)
        {
            long result;

            long.TryParse(s, out result);

            return result;
        }

        /// <summary>
        /// 转换成可空长整型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static long? ToLongNullable(this string s)
        {
            long? nullable = null;

            long result;

            if (long.TryParse(s, out result))
                nullable = result;

            return nullable;
        }

        /// <summary>
        /// 转换成布尔值
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool ToBool(this string s)
        {
            bool result;

            bool.TryParse(s, out result);

            return result;
        }

        /// <summary>
        /// 转换成可空布尔值
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool? ToBoolNullable(this string s)
        {
            bool? nullable = null;

            bool result;

            if (bool.TryParse(s, out result))
                nullable = result;

            return nullable;
        }
    }
}