//bibaoke.com

using System;
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

        internal void Remove(string className, Element element)
        {
            List<Element> list;

            if (this.Dictionary.TryGetValue(className, out list))
            {
                list.Remove(element);
            }
        }

        internal Element[] Get(string className)
        {
            List<Element> list;

            if (this.Dictionary.TryGetValue(className, out list))
            {
                return list.ToArray();
            }
            else
            {
                return new Element[0];
            }
        }

        internal void Add(string className, Element element)
        {
            List<Element> list;

            if (this.Dictionary.TryGetValue(className, out list))
            {
                list.Add(element);
            }
            else
            {
                list = new List<Element>();

                list.Add(element);

                this.Dictionary.Add(className, list);
            }
        }
    }
}
