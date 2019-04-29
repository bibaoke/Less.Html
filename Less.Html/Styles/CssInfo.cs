//bibaoke.com

using Less.Text;
using System;
using System.Text.RegularExpressions;

namespace Less.Html
{
    /// <summary>
    /// css 信息
    /// </summary>
    public abstract class CssInfo
    {
        /// <summary>
        /// 获取以空白字符分开的属性值的正则表达式
        /// </summary>
        protected static Regex SpaceValuePattern
        {
            get;
            set;
        }

        /// <summary>
        /// 获取以逗号分开的属性值的正则表达式
        /// </summary>
        protected static Regex CommaValuePattern
        {
            get;
            set;
        }

        /// <summary>
        /// 获取链接值的正则表达式
        /// </summary>
        protected static Regex UrlPattern
        {
            get;
            set;
        }

        /// <summary>
        /// 所属文档
        /// </summary>
        internal Css OwnerCss
        {
            get;
            set;
        }

        internal int Begin
        {
            get;
            set;
        }

        internal int End
        {
            get;
            set;
        }

        static CssInfo()
        {
            CssInfo.SpaceValuePattern = @"\S+".ToRegex(
                RegexOptions.IgnorePatternWhitespace |
                RegexOptions.Compiled);

            CssInfo.CommaValuePattern = "[^,]+".ToRegex(
                RegexOptions.IgnorePatternWhitespace |
                RegexOptions.Compiled);

            CssInfo.UrlPattern = @"
                url\((?<mark>[""'])(?<url>.*?)\k<mark>\)|
                url\((?<url>.*?)\)
                ".ToRegex(
                RegexOptions.IgnorePatternWhitespace |
                RegexOptions.IgnoreCase |
                RegexOptions.Compiled |
                RegexOptions.ExplicitCapture);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="ownerCss"></param>
        protected CssInfo(Css ownerCss) : this(ownerCss, -1)
        {
            //
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="ownerCss"></param>
        /// <param name="begin"></param>
        protected CssInfo(Css ownerCss, int begin) : this(ownerCss, begin, -1)
        {
            //
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="ownerCss"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        protected CssInfo(Css ownerCss, int begin, int end)
        {
            this.OwnerCss = ownerCss;
            this.Begin = begin;
            this.End = end;
        }

        /// <summary>
        /// 字面量
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.OwnerCss.Content.SubstringUnsafe(this.Begin, this.End - this.Begin + 1);
        }

        /// <summary>
        /// 枚举以逗号分隔的内容
        /// </summary>
        /// <param name="content"></param>
        /// <param name="action"></param>
        protected void EachCommaValue(string content, Action<Match> action)
        {
            int position = 0;

            Match match = CssInfo.CommaValuePattern.Match(content, position);

            while (match.Success)
            {
                action(match);

                position += match.Length;

                match = CssInfo.CommaValuePattern.Match(content, position);
            }
        }

        /// <summary>
        /// 枚举以逗号分隔的内容
        /// </summary>
        /// <param name="content"></param>
        /// <param name="action"></param>
        protected void EachSpaceValue(string content, Action<Match> action)
        {
            int position = 0;

            Match match = CssInfo.SpaceValuePattern.Match(content, position);

            while (match.Success)
            {
                action(match);

                position += match.Length;

                match = CssInfo.SpaceValuePattern.Match(content, position);
            }
        }
    }
}
