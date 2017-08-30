//bibaoke.com

using System.Collections;
using System.Collections.Generic;

namespace Less.Html
{
    /// <summary>
    /// 元素集合
    /// </summary>
    public class ElementCollection : IEnumerable<Element>
    {
        private List<Element> List
        {
            get;
            set;
        }

        /// <summary>
        /// 指定索引的元素
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Element this[int index]
        {
            get
            {
                return this.List[index];
            }
            set
            {
                this.List[index] = value;
            }
        }

        internal void AddRange(IEnumerable<Element> elements)
        {
            this.List.AddRange(elements);
        }

        internal void InsertRange(int index, IEnumerable<Element> elements)
        {
            this.List.InsertRange(index, elements);
        }

        internal void RemoveRange(int index, int count)
        {
            this.List.RemoveRange(index, count);
        }

        internal int IndexOf(Element element)
        {
            return this.List.IndexOf(element);
        }

        internal ElementCollection()
        {
            this.List = new List<Element>();
        }

        /// <summary>
        /// 从 ElementCollection 到 Element[] 的隐式转换
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Element[] (ElementCollection value)
        {
            return value.List.ToArray();
        }

        /// <summary>
        /// 迭代器
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Element> GetEnumerator()
        {
            return this.List.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.List.GetEnumerator();
        }
    }
}
