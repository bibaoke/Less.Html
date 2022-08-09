//bibaoke.com

using Less.Log;
using Less.Text;
using System;
using System.Text;
using System.Linq;
using System.Diagnostics;

namespace Less.Html
{
    /// <summary>
    /// 文档内容
    /// </summary>
    internal class DocumentContent
    {
        private StringBuilder ValueBuilder
        {
            get;
            set;
        }

        private string ValueCache
        {
            get;
            set;
        }

        private bool SelfChecking
        {
            get;
            set;
        }

        private string Value
        {
            get
            {
                if (this.ValueBuilder.IsNotNull())
                {
                    this.ValueCache = this.ValueBuilder.ToString();

#if DEBUG
                    if (!SelfChecking)
                    {
                        AppLog.Write(new
                        {
                            this.ValueCache,
                            StackTrace = new StackTrace().GetFrames().Select(i => i.GetMethod().Name)
                        }, false);
                    }
#endif

                    this.ValueBuilder = null;
                }

                return this.ValueCache;
            }
            set
            {
                this.ValueCache = value;
            }
        }

        private DocumentContent(string value)
        {
            this.Value = value;
        }

        /// <summary>
        /// 文档长度
        /// </summary>
        public int Length
        {
            get
            {
                if (this.ValueBuilder.IsNull())
                {
                    return this.Value.Length;
                }
                else
                {
                    return this.ValueBuilder.Length;
                }
            }
        }

        /// <summary>
        /// 获取指定索引的字符
        /// </summary>
        /// <param name="index">指定索引</param>
        /// <returns></returns>
        public char this[int index]
        {
            get
            {
                return this.Value[index];
            }
        }

        /// <summary>
        /// 从 string 到 DocumentContent 的隐式转换
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator DocumentContent(string value)
        {
            return value.IsNotNull() ? new DocumentContent(value) : null;
        }

        /// <summary>
        /// 从 DocumentContent 到 string 的隐式转换
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator string(DocumentContent value)
        {
            return value.IsNotNull() ? value.ToString() : null;
        }

        /// <summary>
        /// 输出字面量
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Value;
        }

        /// <summary>
        /// 在自检状态下执行
        /// </summary>
        /// <param name="action">在自检状态下执行的方法</param>
        public void ExecInSelfCheck(Action action)
        {
            this.SelfChecking = true;

            action();

            this.SelfChecking = false;
        }

        /// <summary>
        /// 将指定范围的字符从此实例中移除
        /// </summary>
        /// <param name="startIndex">起始索引</param>
        /// <param name="count">长度</param>
        public void Remove(int startIndex, int count)
        {
            this.GetValueBuilder().Remove(startIndex, count);
        }

        /// <summary>
        /// 将字符串插入到此实例中的指定字符位置
        /// </summary>
        /// <param name="startIndex">起始索引</param>
        /// <param name="value">插入值</param>
        public void Insert(int startIndex, string value)
        {
            this.GetValueBuilder().Insert(startIndex, value);
        }

        /// <summary>
        /// 获取引用此实例地址的子字符串
        /// </summary>
        /// <param name="startIndex">起始索引</param>
        /// <param name="length">长度</param>
        /// <returns>返回引用此实例地址的子字符串</returns>
        public string SubstringUnsafe(int startIndex, int length)
        {
            return this.Value.SubstringUnsafe(startIndex, length);
        }

        private StringBuilder GetValueBuilder()
        {
            if (this.ValueBuilder.IsNull())
            {
                this.ValueBuilder = new StringBuilder(this.Value);
            }

            return this.ValueBuilder;
        }
    }
}
