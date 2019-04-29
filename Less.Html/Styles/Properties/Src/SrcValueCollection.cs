//bibaoke.com

using System.Collections;
using System.Collections.Generic;

namespace Less.Html
{
    /// <summary>
    /// 源文件值集合
    /// </summary>
    public class SrcValueCollection : IEnumerable<SrcValue>
    {
        private List<SrcValue> List
        {
            get;
            set;
        }

        /// <summary>
        /// 集合计数
        /// </summary>
        public int Count
        {
            get
            {
                return this.List.Count;
            }
        }

        /// <summary>
        /// 获取指定索引的源文件值
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public SrcValue this[int index]
        {
            get
            {
                return this.List[index];
            }
        }

        internal SrcValueCollection()
        {
            this.List = new List<SrcValue>();
        }

        internal void Add(SrcValue value)
        {
            this.List.Add(value);
        }

        /// <summary>
        /// 获取迭代器
        /// </summary>
        /// <returns></returns>
        public IEnumerator<SrcValue> GetEnumerator()
        {
            return this.List.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.List.GetEnumerator();
        }
    }
}
