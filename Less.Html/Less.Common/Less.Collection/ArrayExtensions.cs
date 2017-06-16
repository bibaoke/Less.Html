//bibaoke.com

using System;
using System.Collections.Generic;

namespace Less.Collection
{
    /// <summary>
    /// 数组扩展方法
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// 倒叙迭代
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="action"></param>
        public static void EachDesc<T>(this T[] array, Action<T> action)
        {
            array.EachDesc((index, item) =>
            {
                action(item);
            });
        }

        /// <summary>
        /// 倒叙迭代
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="action"></param>
        public static void EachDesc<T>(this T[] array, Action<int, T> action)
        {
            array.EachDesc((index, item) =>
            {
                action(index, item);

                return true;
            });
        }

        /// <summary>
        /// 倒叙迭代
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="func"></param>
        public static void EachDesc<T>(this T[] array, Func<int, T, bool> func)
        {
            array.Length.EachDesc(i =>
            {
                return func(i, array[i]);
            });
        }

        /// <summary>
        /// 降序排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns>降序数组</returns>
        public static T[] SortDescending<T>(this T[] array)
        {
            List<T> list = new List<T>(array);

            list.Sort();

            list.Reverse();

            return list.ToArray();
        }

        /// <summary>
        /// 升序排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns>升序数组</returns>
        public static T[] Sort<T>(this T[] array)
        {
            List<T> list = new List<T>(array);

            list.Sort();

            return list.ToArray();
        }

        /// <summary>
        /// 扩展数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="extension">扩展元素</param>
        /// <returns></returns>
        public static T[] ExtArray<T>(this T[] array, params T[] extension)
        {
            T[] result = new T[array.Length + extension.Length];

            Array.Copy(array, result, array.Length);

            Array.Copy(extension, 0, result, array.Length, extension.Length);

            return result;
        }

        /// <summary>
        /// 扩展数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="length">新长度</param>
        /// <returns></returns>
        public static T[] ExtArray<T>(this T[] array, int length)
        {
            T[] result = new T[length];

            Array.Copy(array, result, array.Length);

            return result;
        }

        /// <summary>
        /// 获取子数组
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="array">父数组</param>
        /// <param name="startIndex">起始索引</param>
        /// <returns>子数组</returns>
        public static T[] SubArray<T>(this T[] array, int startIndex)
        {
            T[] result = new T[array.Length - startIndex];

            Array.Copy(array, startIndex, result, 0, result.Length);

            return result;
        }

        /// <summary>
        /// 获取子数组
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="array">父数组</param>
        /// <param name="startIndex">起始索引</param>
        /// <param name="count">元素个数</param>
        /// <returns>子数组</returns>
        public static T[] SubArray<T>(this T[] array, int startIndex, int count)
        {
            T[] result = new T[count];

            Array.Copy(array, startIndex, result, 0, count);

            return result;
        }
    }
}
