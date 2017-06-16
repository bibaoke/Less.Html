//bibaoke.com

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Less.Text
{
    /// <summary>
    /// 分隔
    /// </summary>
    public static class SplitExtensions
    {
        private static Regex WhiteSpacePattern
        {
            get;
            set;
        }

        static SplitExtensions()
        {
            SplitExtensions.WhiteSpacePattern = @"\s+".ToRegex(RegexOptions.Compiled | RegexOptions.Singleline);
        }

        /// <summary>
        /// 根据空白字符分隔字符串
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string[] SplitByWhiteSpace(this string s)
        {
            int index = 0;

            List<string> result = new List<string>();

            while (true)
            {
                Tuple<int, int> tuple = s.IndexOfWhiteSpace(index, s.Length - index);

                if (tuple.IsNotNull())
                {
                    result.Add(s.Substring(index, tuple.Item1 - index));

                    index = tuple.Item1 + tuple.Item2;
                }
                else
                {
                    int length = s.Length - index;

                    if (length > 0)
                        result.Add(s.Substring(index, length));

                    break;
                }
            }

            return result.ToArray();
        }

        /// <summary>
        /// 空白字符的索引
        /// </summary>
        /// <param name="s"></param>
        /// <returns>第一个分量是索引 第二个分量是长度</returns>
        public static Tuple<int, int> IndexOfWhiteSpace(this string s)
        {
            return s.IndexOfWhiteSpace(0);
        }

        /// <summary>
        /// 空白字符的索引
        /// </summary>
        /// <param name="s"></param>
        /// <param name="startIndex"></param>
        /// <returns>第一个分量是索引 第二个分量是长度</returns>
        public static Tuple<int, int> IndexOfWhiteSpace(this string s, int startIndex)
        {
            return s.IndexOfWhiteSpace(startIndex, s.Length - startIndex);
        }

        /// <summary>
        /// 空白字符的索引
        /// </summary>
        /// <param name="s"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns>第一个分量是索引 第二个分量是长度</returns>
        public static Tuple<int, int> IndexOfWhiteSpace(this string s, int startIndex, int count)
        {
            Match match = SplitExtensions.WhiteSpacePattern.Match(s, startIndex, count);

            if (match.Success)
                return new Tuple<int, int>(match.Index, match.Length);

            return null;
        }

        /// <summary>
        /// 分隔字符串
        /// </summary>
        /// <param name="s">要分隔的字符串</param>
        /// <param name="separator">分隔符</param>
        /// <param name="caseOption">大小写选项</param>
        /// <returns>分隔后的字符串数组</returns>
        public static string[] Split(this string s, string separator, CaseOptions caseOption)
        {
            return s.Split(separator, caseOption, SeparatorOptions.NoSeparator);
        }

        /// <summary>
        /// 分隔字符串
        /// </summary>
        /// <param name="s">要分隔的字符串</param>
        /// <param name="separator">分隔符</param>
        /// <param name="caseOption">大小写选项</param>
        /// <param name="separatorOption">分隔符选项</param>
        /// <returns>分隔后的字符串数组</returns>
        public static string[] Split(this string s, string separator, CaseOptions caseOption, SeparatorOptions separatorOption)
        {
            //根据大小写选项决定字符串查找选项
            StringComparison comparison = caseOption.ToStringComparison();

            //查找起始索引
            int startIndex = 0;

            //找到的分隔符索引
            int separatorIndex = 0;

            //分隔出来的字符串列表
            List<string> result = new List<string>();

            //在文本中查找分隔符
            while ((separatorIndex = s.IndexOf(separator, startIndex, comparison)) >= 0)
            {
                //字符串截取起始索引
                int substringStartIndex = startIndex;

                //字符串截取长度
                int substringCount = separatorIndex - startIndex;

                //根据分隔符选项决定截取起始索引和截取长度
                switch (separatorOption)
                {
                    case SeparatorOptions.NoSeparator:
                        break;
                    case SeparatorOptions.PostSeparator:
                        substringCount += separator.Length;
                        break;
                    case SeparatorOptions.PreSeparator:
                        substringStartIndex -= separator.Length;
                        substringCount += separator.Length;
                        break;
                }

                //如果分隔符不在位置0
                //把分隔符前的内容加入结果队列
                if (separatorIndex > 0)
                    result.Add(s.Substring(substringStartIndex, substringCount));

                //更新起始索引
                startIndex = separatorIndex + separator.Length;
            }

            //剩余长度
            int restCount = s.Length - startIndex;

            //如果还有剩余内容
            //把剩余的内容加入结果队列
            if (restCount > 0)
                result.Add(s.Substring(startIndex, restCount));

            return result.ToArray();
        }
    }
}
