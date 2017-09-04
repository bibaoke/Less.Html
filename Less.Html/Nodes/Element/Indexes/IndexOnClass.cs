using System.Collections.Generic;

namespace Less.Html
{
    /// <summary>
    /// 元素 class 索引
    /// </summary>
    internal class IndexOnClass
    {
        private Dictionary<string, List<Element>> Dictionary
        {
            get;
            set;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        internal IndexOnClass()
        {
            this.Dictionary = new Dictionary<string, List<Element>>();
        }

        internal void Remove(string className)
        {
            this.Dictionary.Remove(className);
        }

        internal Element[] Get(string className)
        {
            if (this.Dictionary.ContainsKey(className))
            {
                return this.Dictionary[className].ToArray();
            }
            else
            {
                return null;
            }
        }

        internal void Add(string className, Element element)
        {
            if (this.Dictionary.ContainsKey(className))
            {
                this.Dictionary[className].Add(element);
            }
            else
            {
                List<Element> list = new List<Element>();

                list.Add(element);

                this.Dictionary.Add(className, list);
            }
        }
    }
}
