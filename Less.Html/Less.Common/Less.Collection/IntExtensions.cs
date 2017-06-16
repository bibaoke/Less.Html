//bibaoke.com

using System;

namespace Less.Collection
{
    /// <summary>
    /// int 扩展方法
    /// </summary>
    public static class IntExtensions
    {
        /// <summary>
        /// 按次数迭代
        /// 在处理委托中得到当前的计数
        /// </summary>
        /// <param name="count">迭代次数</param>
        /// <param name="action">迭代处理的委托</param>
        public static void Each(this int count, Action<int> action)
        {
            for (int i = 0; i < count; i++)
                action(i);
        }

        /// <summary>
        /// 按次数倒序迭代
        /// 在处理委托中得到当前的计数
        /// </summary>
        /// <param name="count">迭代次数</param>
        /// <param name="action">迭代处理的委托</param>
        public static void EachDesc(this int count, Action<int> action)
        {
            for (int i = count - 1; i >= 0; i--)
                action(i);
        }

        /// <summary>
        /// 按次数迭代
        /// 在处理委托中得到当前的计数
        /// </summary>
        /// <param name="count">迭代次数</param>
        /// <param name="func">迭代处理的委托</param>
        public static void Each(this int count, Func<int, bool> func)
        {
            for (int i = 0; i < count; i++)
            {
                if (!func(i))
                    break;
            }
        }

        /// <summary>
        /// 按次数倒序迭代
        /// 在处理委托中得到当前的计数
        /// </summary>
        /// <param name="count">迭代次数</param>
        /// <param name="func">迭代处理的委托</param>
        public static void EachDesc(this int count, Func<int, bool> func)
        {
            for (int i = count - 1; i >= 0; i--)
            {
                if (!func(i))
                    break;
            }
        }
    }
}
