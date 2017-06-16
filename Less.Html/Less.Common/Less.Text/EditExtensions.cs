//bibaoke.com

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Less.Collection;

namespace Less.Text
{
    /// <summary>
    /// 编辑
    /// </summary>
    public static class EditExtensions
    {
        /// <summary>
        /// 替换
        /// </summary>
        /// <param name="s"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string FormatString(this string s, params object[] args)
        {
            return string.Format(s, args);
        }

        /// <summary>
        /// 省略文本
        /// </summary>
        /// <param name="s"></param>
        /// <param name="keepUnicodeCharCount">要保留的 Unicode 字符数</param>
        /// <returns></returns>
        public static string Ellipsis(this string s, int keepUnicodeCharCount)
        {
            string result = s.Trim(keepUnicodeCharCount);

            return result.Length == s.Length ? result : result.Combine("…");
        }

        /// <summary>
        /// 根据 Unicode 字符数截取字符串
        /// </summary>
        /// <param name="s">要进行截取的字符串</param>
        /// <param name="keepUnicodeCharCount">要保留的 Unicode 字符数</param>
        /// <returns>截取结果</returns>
        public static string Trim(this string s, int keepUnicodeCharCount)
        {
            List<char> result = new List<char>();

            decimal count = 0;

            int index = 0;

            while (count.Ceiling() < keepUnicodeCharCount && index < s.Length)
            {
                result.Add(s[index]);

                if (s[index].IsUnicode())
                    count += 1M;
                else
                    count += 0.5M;

                index++;
            }

            return result.Join();
        }

        /// <summary>
        /// 清除头部字符串
        /// </summary>
        /// <param name="s"></param>
        /// <param name="startString"></param>
        /// <param name="stringComparison"></param>
        /// <returns></returns>
        public static string TrimStart(this string s, string startString, StringComparison stringComparison)
        {
            if (s.StartsWith(startString, stringComparison))
                return s.Substring(startString.Length);

            return s;
        }

        /// <summary>
        /// 清除头部字符串
        /// 大小写敏感
        /// </summary>
        /// <param name="s"></param>
        /// <param name="startString"></param>
        /// <returns></returns>
        public static string TrimStart(this string s, string startString)
        {
            return s.TrimStart(startString, StringComparison.Ordinal);
        }

        /// <summary>
        /// 清除头部空白
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string TrimStart(this string s)
        {
            return s.TrimStart(null);
        }

        /// <summary>
        /// 清除尾部字符串
        /// </summary>
        /// <param name="s"></param>
        /// <param name="endString"></param>
        /// <param name="stringComparison"></param>
        /// <returns></returns>
        public static string TrimEnd(this string s, string endString, StringComparison stringComparison)
        {
            if (s.EndsWith(endString, stringComparison))
                return s.Substring(0, s.Length - endString.Length);

            return s;
        }

        /// <summary>
        /// 清除尾部字符串
        /// 大小写敏感
        /// </summary>
        /// <param name="s"></param>
        /// <param name="endString"></param>
        /// <returns></returns>
        public static string TrimEnd(this string s, string endString)
        {
            return s.TrimEnd(endString, StringComparison.Ordinal);
        }

        /// <summary>
        /// 清除尾部空白
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string TrimEnd(this string s)
        {
            return s.TrimEnd(null);
        }

        /// <summary>
        /// 清除指定的匹配
        /// </summary>
        /// <param name="s"></param>
        /// <param name="regex"></param>
        /// <returns></returns>
        public static string Clear(this string s, Regex regex)
        {
            return regex.Replace(s, string.Empty);
        }

        /// <summary>
        /// 清除字符串中的指定字符串
        /// 区分大小写
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="stringToClear">指定字符串</param>
        /// <returns></returns>
        public static string Clear(this string s, string stringToClear)
        {
            return s.Replace(stringToClear, string.Empty);
        }

        /// <summary>
        /// 清除字符串中的指定字符串
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="stringToClear">指定字符串</param>
        /// <param name="caseOption">大小写选项</param>
        /// <returns></returns>
        public static string Clear(this string s, string stringToClear, CaseOptions caseOption)
        {
            return s.Replace(stringToClear, string.Empty, caseOption);
        }

        /// <summary>
        /// 替换文本中的字符串
        /// </summary>
        /// <param name="s">文本</param>
        /// <param name="oldValue">要替换的字符串</param>
        /// <param name="newValue">取而代之的字符串</param>
        /// <param name="caseOption">大小写选项</param>
        /// <returns>替换之后的文本</returns>
        public static string Replace(this string s, string oldValue, string newValue, CaseOptions caseOption)
        {
            //根据大小写选项决定字符串查找选项
            StringComparison comparison = caseOption.ToStringComparison();

            //查找起始索引
            int startIndex = 0;

            //找到的旧值索引
            int oldValueIndex = 0;

            //替换处理之后的结果
            StringBuilder result = new StringBuilder(s.Length);

            //在文本中查找要替换的字符串
            while ((oldValueIndex = s.IndexOf(oldValue, startIndex, comparison)) >= 0)
            {
                //把旧值索引前的内容追加到结果中
                (oldValueIndex - startIndex).Each(index =>
                {
                    result.Append(s[startIndex + index]);
                });

                //再追加新值
                result.Append(newValue);

                //更新起始索引
                startIndex = oldValueIndex + oldValue.Length;
            }

            //把剩余的内容追加到结果中
            (s.Length - startIndex).Each(index =>
            {
                result.Append(s[startIndex + index]);
            });

            return result.ToString();
        }
    }
}
