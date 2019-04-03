//bibaoke.com

using System.Collections;
using System.Collections.Generic;

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
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }
}
