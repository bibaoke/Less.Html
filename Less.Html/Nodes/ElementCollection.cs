//bibaoke.com

using System.Collections;
using System.Collections.Generic;
using Less.Text;

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

        private IndexOnId IndexOnId
        {
            get;
            set;
        }

        private IndexOnClass IndexOnClass
        {
            get;
            set;
        }

        private IndexOnTagName IndexOnTagName
        {
            get;
            set;
        }

        private IndexOnName IndexOnName
        {
            get;
            set;
        }

        internal int Count
        {
            get
            {
                return this.List.Count;
            }
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

        internal ElementCollection()
        {
            this.List = new List<Element>();

            this.IndexOnId = new IndexOnId();
            this.IndexOnClass = new IndexOnClass();
            this.IndexOnTagName = new IndexOnTagName();
            this.IndexOnName = new IndexOnName();
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

        internal ElementCollection Clone()
        {
            ElementCollection clone = new ElementCollection();

            clone.List.Capacity = this.List.Capacity;

            return clone;
        }

        internal Element GetElementById(string id)
        {
            return this.IndexOnId.Get(id);
        }

        internal Element[] GetElementsByClassName(string className)
        {
            return this.IndexOnClass.Get(className);
        }

        internal Element[] GetElementsByTagName(string tagName)
        {
            return this.IndexOnTagName.Get(tagName);
        }

        internal Element[] GetElementsByName(string name)
        {
            return this.IndexOnName.Get(name);
        }

        internal void CopyTo(int index, Element[] array, int count)
        {
            this.List.CopyTo(index, array, 0, count);
        }

        internal void Add(Element element)
        {
            this.List.Add(element);

            this.AddIndex(element);
        }

        internal void AddRange(IEnumerable<Element> elements)
        {
            this.List.AddRange(elements);

            foreach (Element i in elements)
            {
                this.AddIndex(i);
            }
        }

        internal void Insert(int index, Element element)
        {
            this.List.Insert(index, element);

            this.AddIndex(element);
        }

        internal void InsertRange(int index, IEnumerable<Element> elements)
        {
            this.List.InsertRange(index, elements);

            foreach (Element i in elements)
            {
                this.AddIndex(i);
            }
        }

        internal void RemoveRange(int index, int count)
        {
            Element[] elements = new Element[count];

            this.CopyTo(index, elements, count);

            foreach (Element i in elements)
            {
                this.RemoveIndex(i);
            }

            this.List.RemoveRange(index, count);
        }

        internal int IndexOf(Element element)
        {
            return this.List.IndexOf(element);
        }

        internal void RemoveIndex(Attr attr)
        {
            switch (attr.name)
            {
                case "id":
                    this.IndexOnId.Remove(attr.value);
                    break;
                case "class":
                    this.RemoveIndexOnClass(attr.value, attr.Element);
                    break;
                case "name":
                    this.IndexOnName.Remove(attr.value, attr.Element);
                    break;
            }
        }

        internal void AddIndex(Attr attr)
        {
            switch (attr.name)
            {
                case "id":
                    this.IndexOnId.Add(attr.value, attr.Element);
                    break;
                case "class":
                    this.AddIndexOnClass(attr.value, attr.Element);
                    break;
                case "name":
                    this.IndexOnName.Add(attr.value, attr.Element);
                    break;
            }
        }

        private void RemoveIndex(Element element)
        {
            if (element.id.IsNotNull())
            {
                this.IndexOnId.Remove(element.id);
            }

            this.RemoveIndexOnClass(element.className, element);

            string name = element.getAttribute("name");

            if (name.IsNotNull())
            {
                this.IndexOnName.Remove(name, element);
            }

            this.IndexOnTagName.Remove(element.Name, element);
        }

        private void AddIndex(Element element)
        {
            if (element.id.IsNotNull())
            {
                this.IndexOnId.Add(element.id, element);
            }

            this.AddIndexOnClass(element.className, element);

            string name = element.getAttribute("name");

            if (name.IsNotNull())
            {
                this.IndexOnName.Add(name, element);
            }

            this.IndexOnTagName.Add(element.Name, element);
        }

        private void RemoveIndexOnClass(string className, Element element)
        {
            if (className.IsNotWhiteSpace())
            {
                string[] classes = className.SplitByWhiteSpace();

                foreach (string i in classes)
                {
                    this.IndexOnClass.Remove(i, element);
                }
            }
        }

        private void AddIndexOnClass(string className, Element element)
        {
            if (className.IsNotWhiteSpace())
            {
                string[] classes = className.SplitByWhiteSpace();

                foreach (string i in classes)
                {
                    this.IndexOnClass.Add(i, element);
                }
            }
        }
    }
}
