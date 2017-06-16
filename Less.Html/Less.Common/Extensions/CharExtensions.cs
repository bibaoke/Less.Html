//bibaoke.com

using Less.Collection;
using Less.Text;
using System.Collections.Generic;

namespace Less
{
    /// <summary>
    /// 字符类型的扩展方法
    /// </summary>
    public static class CharExtensions
    {
        private static Dictionary<char, char> NumberMap
        {
            get;
            set;
        }

        static CharExtensions()
        {
            CharExtensions.NumberMap = new Dictionary<char, char>();

            CharExtensions.NumberMap.Add('0', '零');
            CharExtensions.NumberMap.Add('1', '一');
            CharExtensions.NumberMap.Add('2', '二');
            CharExtensions.NumberMap.Add('3', '三');
            CharExtensions.NumberMap.Add('4', '四');
            CharExtensions.NumberMap.Add('5', '五');
            CharExtensions.NumberMap.Add('6', '六');
            CharExtensions.NumberMap.Add('7', '七');
            CharExtensions.NumberMap.Add('8', '八');
            CharExtensions.NumberMap.Add('9', '九');
        }

        /// <summary>
        /// 是否 unicode 字符
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsUnicode(this char c)
        {
            return MyEncoding.GB2312.GetByteCount(c.ConstructArray()) >= 2;
        }

        /// <summary>
        /// 转换为大写
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static char ToUpper(this char c)
        {
            return char.ToUpper(c);
        }

        /// <summary>
        /// 转换为小写
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static char ToLower(this char c)
        {
            return char.ToLower(c);
        }

        /// <summary>
        /// 是否英文字符
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsEnglish(this char c)
        {
            if (c >= 65 && c <= 90)
                return true;

            if (c >= 97 && c <= 122)
                return true;

            return false;
        }

        /// <summary>
        /// 转换为中文数字字符
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static char ToChineseNumber(this char c)
        {
            return CharExtensions.NumberMap[c];
        }
    }
}
