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

        internal void Remove(string tagName)
        {
            this.Dictionary.Remove(tagName);
        }

        internal Element[] Get(string tagName)
        {
            tagName = tagName.ToLower();

            if (this.Dictionary.ContainsKey(tagName))
            {
                return this.Dictionary[tagName].ToArray();
            }
            else
            {
                return new Element[0];
            }
        }

        internal void Add(string tagName, Element element)
        {
            if (this.Dictionary.ContainsKey(tagName))
            {
                this.Dictionary[tagName].Add(element);
            }
            else
            {
                List<Element> list = new List<Element>();

                list.Add(element);

                this.Dictionary.Add(tagName, list);
            }
        }
    }
}
