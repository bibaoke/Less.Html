//bibaoke.com

using Less.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Less.Html
{
    /// <summary>
    /// 属性集合
    /// </summary>
    public class PropertyCollection : IEnumerable<Property>
    {
        private List<Property> List
        {
            get;
            set;
        }

        /// <summary>
        /// 属性计数
        /// </summary>
        public int Count
        {
            get
            {
                return this.List.Count;
            }
        }

        /// <summary>
        /// 获取指定索引的属性
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Property this[int index]
        {
            get
            {
                return this.List[index];
            }
        }

        /// <summary>
        /// 获取指定名称的属性
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Property[] this[string name]
        {
            get
            {
                return this.List.Where(i => i.Name.CompareIgnoreCase(name)).ToArray();
            }
        }

        internal PropertyCollection()
        {
            this.List = new List<Property>();
        }

        internal void Add(Property property)
        {
            this.List.Add(property);
        }

        /// <summary>
        /// 获取迭代器
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Property> GetEnumerator()
        {
            return this.List.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.List.GetEnumerator();
        }
    }
}
