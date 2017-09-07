﻿using System.Collections.Generic;

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
            List<Element> list;

            if (this.Dictionary.TryGetValue(name, out list))
            {
                return list.ToArray();
            }
            else
            {
                return new Element[0];
            }
        }

        internal void Add(string name, Element element)
        {
            List<Element> list;

            if (this.Dictionary.TryGetValue(name, out list))
            {
                list.Add(element);
            }
            else
            {
                list = new List<Element>();

                list.Add(element);

                this.Dictionary.Add(name, list);
            }
        }
    }
}