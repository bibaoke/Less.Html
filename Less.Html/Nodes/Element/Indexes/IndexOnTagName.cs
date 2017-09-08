//bibaoke.com

using System.Collections.Generic;

namespace Less.Html
{
    /// <summary>
    /// 元素 class 索引
    /// </summary>
    internal class IndexOnTagName
    {
        private Dictionary<string, List<Element>> Dictionary
        {
            get;
            set;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        internal IndexOnTagName()
        {
            this.Dictionary = new Dictionary<string, List<Element>>();
        }

        internal void Remove(string tagName, Element element)
        {
            List<Element> list;

            if (this.Dictionary.TryGetValue(tagName, out list))
            {
                list.Remove(element);
            }
        }

        internal Element[] Get(string tagName)
        {
            tagName = tagName.ToLower();

            List<Element> list;

            if (this.Dictionary.TryGetValue(tagName, out list))
            {
                return list.ToArray();
            }
            else
            {
                return new Element[0];
            }
        }

        internal void Add(string tagName, Element element)
        {
            List<Element> list;

            if (this.Dictionary.TryGetValue(tagName, out list))
            {
                list.Add(element);
            }
            else
            {
                list = new List<Element>();

                list.Add(element);

                this.Dictionary.Add(tagName, list);
            }
        }
    }
}
