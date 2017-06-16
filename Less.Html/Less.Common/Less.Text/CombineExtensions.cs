//bibaoke.com

using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using Less.Collection;
using System.Linq;

namespace Less.Text
{
    /// <summary>
    /// 拼接
    /// </summary>
    public static class CombineExtensions
    {
        /// <summary>
        /// 连接 Url 参数
        /// </summary>
        /// <param name="s"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string CombineUrlQuery(this string s, params string[] values)
        {
            DynamicString result = new DynamicString(s);

            if (s.Contains("?"))
            {
                foreach (string i in values)
                    result.Append(i.Replace('?', '&'));
            }
            else
            {
                foreach (string i in values)
                    result.Append(i);
            }

            return result;
        }

        /// <summary>
        /// 连接 Url 路径
        /// </summary>
        /// <param name="s"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string CombineUrlPath(this string s, params object[] values)
        {
            return s.CombineUrlPath(values.Select(i => i.ToString()));
        }

        /// <summary>
        /// 连接 Url 路径
        /// </summary>
        /// <param name="s"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string CombineUrlPath(this string s, params string[] values)
        {
            DynamicString result = new DynamicString(s.TrimEnd('/'));

            foreach (string i in values)
            {
                if (i.StartsWith("/"))
                    result.Append(i.TrimEnd('/'));
                else
                    result.Append("/").Append(i.TrimEnd('/'));
            }

            return result;
        }

        /// <summary>
        /// 连接对象字面量
        /// </summary>
        /// <param name="s"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string Combine(this string s, params object[] values)
        {
            return string.Concat(s.ConstructArray(values));
        }

        /// <summary>
        /// 连接字符串
        /// </summary>
        /// <param name="s"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string Combine(this string s, params string[] values)
        {
            return string.Concat(s.ConstructArray(values));
        }

        /// <summary>
        /// 连接字符串
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static string Join<T>(this IEnumerable<T> items)
        {
            return items.Join(string.Empty);
        }

        /// <summary>
        /// 用指定的分隔符连接字符串
        /// </summary>
        /// <param name="items"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string Join<T>(this IEnumerable<T> items, string separator)
        {
            return string.Join(separator, items);
        }

        /// <summary>
        /// 用文字表示并列项
        /// 用顿号分隔
        /// </summary>
        /// <param name="list">并列文字项</param>
        /// <returns>处理结果</returns>
        public static string List(this string[] list)
        {
            return list.Join("、");
        }

        /// <summary>
        /// 用文字表示键值集合
        /// 用逗号分隔
        /// </summary>
        /// <param name="list">键值集合</param>
        /// <returns></returns>
        public static string List(this NameValueCollection list)
        {
            return list.List(",");
        }

        /// <summary>
        /// 用文字表示键值集合
        /// </summary>
        /// <param name="list">键值集合</param>
        /// <param name="separator">分隔符</param>
        /// <returns></returns>
        public static string List(this NameValueCollection list, string separator)
        {
            //可变字符串
            //保存结果
            StringBuilder b = new StringBuilder();

            //拼接键值集合
            foreach (string i in list.AllKeys)
                b.Append(i).Append("=").Append(list[i]).Append(separator);

            //移除最后一个分隔符
            if (b.Length > 0)
                b.Remove(b.Length - 1, 1);

            return b.ToString();
        }

        /// <summary>
        /// 重复字符串
        /// </summary>
        /// <param name="s"></param>
        /// <param name="count">重复次数</param>
        /// <returns>重复后的字符串</returns>
        public static string Repeat(this string s, int count)
        {
            string[] result = new string[count];

            count.Each(delegate (int index)
            {
                result[index] = s;
            });

            return string.Concat(result);
        }
    }
}
