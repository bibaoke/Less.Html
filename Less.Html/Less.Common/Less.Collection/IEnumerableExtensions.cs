//bibaoke.com

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Less.Collection
{
    /// <summary>
    /// 可迭代对象的扩展方法
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="items"></param>
        public static void Dispose(this IEnumerable<IDisposable> items)
        {
            foreach (IDisposable i in items)
                i.Dispose();
        }

        /// <summary>
        /// 获取指定类型的项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static IEnumerable<T> Select<T>(this IEnumerable<object> items)
        {
            return items.Where(i => i is T).Cast<T>();
        }

        /// <summary>
        /// 是否非空白
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static bool IsNotEmpty(this IEnumerable items)
        {
            return !items.IsEmpty();
        }

        /// <summary>
        /// 是否空白
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static bool IsEmpty(this IEnumerable items)
        {
            if (items.IsNotNull())
            {
                //能进入循环即非空白
                foreach (object i in items)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// 降序排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns>降序排列的一组元素</returns>
        public static IEnumerable<T> SortDescending<T>(this IEnumerable<T> items)
        {
            return items.OrderByDescending(i => i);
        }

        /// <summary>
        /// 升序排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns>升序排列的一组元素</returns>
        public static IEnumerable<T> Sort<T>(this IEnumerable<T> items)
        {
            return items.OrderBy(i => i);
        }

        /// <summary>
        /// 迭代对象
        /// 在处理委托中同时得到当前的实例、计数
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="e">可迭代的对象</param>
        /// <param name="action">迭代处理的委托</param>
        public static void Each<T>(this IEnumerable<T> e, Action<int, T> action)
        {
            e.Each((index, item) =>
            {
                action(index, item);

                return true;
            });
        }

        /// <summary>
        /// 迭代对象
        /// 在处理委托中同时得到当前的实例、计数
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="e">可迭代的对象</param>
        /// <param name="func">迭代处理的委托</param>
        public static void Each<T>(this IEnumerable<T> e, Func<int, T, bool> func)
        {
            int index = 0;

            foreach (T item in e)
            {
                if (!func(index++, item))
                    break;
            }
        }

        /// <summary>
        /// 迭代对象
        /// 在处理委托中同时得到当前的实例、计数和扩展信息
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="e">可迭代的对象</param>
        /// <param name="action">迭代处理的委托</param>
        public static void Each<T>(this IEnumerable<T> e, Action<int, T, EnumInfo> action)
        {
            e.Each((index, item, info) =>
            {
                action(index, item, info);

                return true;
            });
        }

        /// <summary>
        /// 迭代对象
        /// 在处理委托中同时得到当前的实例、计数和扩展信息
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="e">可迭代的对象</param>
        /// <param name="func">迭代处理的委托</param>
        public static void Each<T>(this IEnumerable<T> e, Func<int, T, EnumInfo, bool> func)
        {
            int index = 0;

            int count = e.Count();

            foreach (T item in e)
            {
                EnumInfo info = new EnumInfo();

                if (index == 0)
                    info.IsFirst = true;

                if (index == count - 1)
                    info.IsLast = true;

                if (!func(index++, item, info))
                    break;
            }
        }
    }
}
