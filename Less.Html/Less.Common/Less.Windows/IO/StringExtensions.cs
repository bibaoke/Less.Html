//bibaoke.com

using System.Linq;
using System.IO;
using Less.Collection;

namespace Less.Windows
{
    /// <summary>
    /// String 扩展方法
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 连接文件路径
        /// </summary>
        /// <param name="s"></param>
        /// <param name="paths"></param>
        /// <returns></returns>
        public static string CombinePath(this string s, params object[] paths)
        {
            return Path.Combine(s.ConstructArray(paths.Select(i => i.ToString()).ToArray()));
        }

        /// <summary>
        /// 连接文件路径
        /// </summary>
        /// <param name="s"></param>
        /// <param name="paths"></param>
        /// <returns></returns>
        public static string CombinePath(this string s, params string[] paths)
        {
            return Path.Combine(s.ConstructArray(paths));
        }
    }
}
