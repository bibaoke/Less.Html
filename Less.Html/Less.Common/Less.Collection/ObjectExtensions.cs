//bibaoke.com

using System;

namespace Less.Collection
{
    /// <summary>
    /// object 扩展方法
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// 把元素转换成数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T[] ConstructArray<T>(this T t)
        {
            return new T[] { t };
        }

        /// <summary>
        /// 连接元素成为数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public static T[] ConstructArray<T>(this T t, params T[] array)
        {
            T[] result = new T[array.Length + 1];

            result[0] = t;

            Array.Copy(array, 0, result, 1, array.Length);

            return result;
        }
    }
}
