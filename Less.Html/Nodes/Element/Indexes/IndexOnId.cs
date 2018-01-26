//bibaoke.com

using System.Collections.Generic;

namespace Less.Html
{
    /// <summary>
    /// 元素 class 索引
    /// </summary>
    internal class IndexOnId
    {
        private Dictionary<string, Element> Dictionary
        {
            get;
            set;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        internal IndexOnId()
        {
            this.Dictionary = new Dictionary<string, Element>();
        }

        internal void Remove(string id)
        {
            this.Dictionary.Remove(id);
        }

        internal Element Get(string id)
        {
            Element element;

            if (this.Dictionary.TryGetValue(id, out element))
            {
                return element;
            }
            else
            {
                return null;
            }
        }

        internal void Add(string id, Element element)
        {
            if (this.Dictionary.ContainsKey(id))
            {
                this.Dictionary[id] = element;
            }
            else
            {
                this.Dictionary.Add(id, element);
            }
        }
    }
}
