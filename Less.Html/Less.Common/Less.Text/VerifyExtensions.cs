//bibaoke.com

namespace Less.Text
{
    /// <summary>
    /// 验证
    /// </summary>
    public static class VerifyExtensions
    {
        /// <summary>
        /// 不是空字符串 不是空白字符
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNotWhiteSpace(this string s)
        {
            return !s.IsWhiteSpace();
        }

        /// <summary>
        /// 是空字符串 或空白字符
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsWhiteSpace(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }

        /// <summary>
        /// 是否非空字符串
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNotEmpty(this string s)
        {
            return !s.IsEmpty();
        }

        /// <summary>
        /// 是否空字符串
        /// </summary>
        /// <param name="s"></param>
        /// <param name="instead">替代字符串</param>
        /// <returns></returns>
        public static string IsEmpty(this string s, string instead)
        {
            return s.IsEmpty() ? instead : s;
        }

        /// <summary>
        /// 是否空字符串
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        /// <summary>
        /// 字符串是否全部为空
        /// </summary>
        /// <param name="s">待判断的字符串</param>
        /// <returns></returns>
        public static bool IsAllEmpty(this string[] s)
        {
            foreach (string i in s)
            {
                if (!i.IsEmpty())
                    return false;
            }

            return true;
        }

        /// <summary>
        /// 字符串是否全部为 null
        /// </summary>
        /// <param name="s">待判断的字符串</param>
        /// <returns></returns>
        public static bool IsAllNull(this string[] s)
        {
            foreach (string i in s)
            {
                if (i != null)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// 字符串是否有一个为 null
        /// </summary>
        /// <param name="s">待判断的字符串</param>
        /// <returns></returns>
        public static bool IsAnyNull(this string[] s)
        {
            foreach (string i in s)
            {
                if (i == null)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// 字符串是否全部不为 null
        /// </summary>
        /// <param name="s">待判断的字符串</param>
        /// <returns></returns>
        public static bool IsNotNull(this string[] s)
        {
            return !IsAnyNull(s);
        }
    }
}
