//bibaoke.com

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Less.Text;

namespace Less.Html
{
    /// <summary>
    /// 样式集合
    /// </summary>
    public class StyleCollection : IEnumerable<Style>
    {
        private List<Style> List
        {
            get;
            set;
        }

        /// <summary>
        /// 样式计数
        /// </summary>
        public int Count
        {
            get
            {
                return this.List.Count;
            }
        }

        /// <summary>
        /// 获取指定索引的样式
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Style this[int index]
        {
            get
            {
                return this.List[index];
            }
        }

        /// <summary>
        /// 获取指定选择器的样式
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public Style[] this[string selector]
        {
            get
            {
                return this.List.Where(i => i.Selector.CompareIgnoreCase(selector)).ToArray();
            }
        }

        internal StyleCollection()
        {
            this.List = new List<Style>();
        }

        internal bool Contains(Style style)
        {
            return this.List.Contains(style);
        }

        internal void Add(Style style)
        {
            this.List.Add(style);
        }

        /// <summary>
        /// 获取迭代器
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Style> GetEnumerator()
        {
            return this.List.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.List.GetEnumerator();
        }
    }
}
