using System.Collections.Generic;

namespace Less.Html
{
    /// <summary>
    /// 元素 class 索引
    /// </summary>
    internal class IndexOnName
    {
        private Dictionary<string, List<Element>> Dictionary
        {
            get;
            set;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        internal IndexOnName()
        {
            this.Dictionary = new Dictionary<string, List<Element>>();
        }

        internal void Remove(string name)
        {
            this.Dictionary.Remove(name);
        }

        internal Element[] Get(string name)
        {
            if (this.Dictionary.ContainsKey(name))
            {
                return this.Dictionary[name].ToArray();
            }
            else
            {
                return new Element[0];
            }
        }

        internal void Add(string name, Element element)
        {
            if (this.Dictionary.ContainsKey(name))
            {
                this.Dictionary[name].Add(element);
            }
            else
            {
                List<Element> list = new List<Element>();

                list.Add(element);

                this.Dictionary.Add(name, list);
            }
        }
    }
}
