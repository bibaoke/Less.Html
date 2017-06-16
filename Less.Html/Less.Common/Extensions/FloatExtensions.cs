//bibaoke.com

using System;

namespace Less
{
    /// <summary>
    /// float 扩展方法
    /// </summary>
    public static class FloatExtensions
    {
        /// <summary>
        /// 转换为整型
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public static int ToInt(this float f)
        {
            return Convert.ToInt32(f);
        }
    }
}
