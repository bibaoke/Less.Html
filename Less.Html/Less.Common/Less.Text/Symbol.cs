//bibaoke.com

namespace Less.Text
{
    /// <summary>
    /// 提供常用符号
    /// </summary>
    public static class Symbol
    {
        /// <summary>
        /// 为内容添加全角小括号
        /// </summary>
        /// <param name="s">要添加全角小括号的内容</param>
        /// <returns>添加后的字符串</returns>
        public static string AddFullWidthParentheses(this string s)
        {
            return string.Concat("（", s, "）");
        }

        /// <summary>
        /// 为内容添加半角小括号
        /// </summary>
        /// <param name="s">要添加半角小括号的内容</param>
        /// <returns>添加后的字符串</returns>
        public static string AddParentheses(this string s)
        {
            return string.Concat("(", s, ")");
        }

        /// <summary>
        /// 为内容添加全角双引号
        /// </summary>
        /// <param name="s">要添加全角双引号的内容</param>
        /// <returns>添加后的字符串</returns>
        public static string AddFullWidthDoubleQuotes(this string s)
        {
            return string.Concat("“", s, "”");
        }

        /// <summary>
        /// 为内容添加半角双引号
        /// </summary>
        /// <param name="s">要添加半角双引号的内容</param>
        /// <returns>添加后的字符串</returns>
        public static string AddDoubleQuotes(this string s)
        {
            return string.Concat("\"", s, "\"");
        }

        /// <summary>
        /// 为内容添加半角单引号
        /// </summary>
        /// <param name="s">要添加半角单引号的内容</param>
        /// <returns>添加后的字符串</returns>
        public static string AddSingleQuotes(this string s)
        {
            return string.Concat("'", s, "'");
        }

        /// <summary>
        /// 为内容添加 N + 半角单引号
        /// 用于 SQL Server Unicode 字符串参数格式
        /// </summary>
        /// <param name="s">要添加 N + 半角单引号的内容</param>
        /// <returns>添加后的字符串</returns>
        public static string AddNSingleQuotes(this string s)
        {
            return string.Concat("N", "'", s, "'");
        }

        /// <summary>
        /// 半角空格
        /// </summary>
        public static string Space
        {
            get { return " "; }
        }

        /// <summary>
        /// 全角空格
        /// </summary>
        public static string FullWidthSpace
        {
            get { return "　"; }
        }

        /// <summary>
        /// 制表符
        /// </summary>
        public static string Tab
        {
            get { return "  "; }
        }

        /// <summary>
        /// 换车符加换行符
        /// </summary>
        public static string NewLine
        {
            get { return "\r\n"; }
        }

        /// <summary>
        /// 回车符
        /// </summary>
        public static char EnterChar
        {
            get { return '\r'; }
        }

        /// <summary>
        /// 换行符
        /// </summary>
        public static char NewLineChar
        {
            get { return '\n'; }
        }
    }
}
