//bibaoke.com

using System.Net;

namespace Less.Text
{
    /// <summary>
    /// 编码
    /// </summary>
    public static class EncodeExtensions
    {
        /// <summary>
        /// html 解码
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string HtmlDecode(this string s)
        {
            return WebUtility.HtmlDecode(s);
        }

        /// <summary>
        /// html 编码
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string HtmlEncode(this string s)
        {
            return WebUtility.HtmlEncode(s);
        }
    }
}
