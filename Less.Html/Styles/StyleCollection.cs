//bibaoke.com

using System.Collections;
using System.Collections.Generic;

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
