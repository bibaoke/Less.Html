//bibaoke.com

using System.Collections.Generic;
using System.Net;
using System.Web;

namespace Less.Html
{
    /// <summary>
    /// 元素
    /// </summary>
    public class Element : Node
    {
        private static HashSet<string> SingleElements
        {
            get;
            set;
        }

        private int AllChildElementsCount
        {
            get;
            set;
        }

        private string OriginalName
        {
            get;
            set;
        }

        internal string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 是否单标签元素
        /// </summary>
        internal bool IsSingle
        {
            get;
            set;
        }

        internal int InnerBegin
        {
            get;
            set;
        }

        internal int InnerEnd
        {
            get;
            set;
        }

        internal int Index
        {
            get
            {
                return this.ownerDocument.all.IndexOf(this);
            }
        }

        /// <summary>
        /// 元素名称
        /// </summary>
        public override string nodeName
        {
            get
            {
                return this.Name.ToUpper();
            }
        }

        /// <summary>
        /// style 属性
        /// </summary>
        public string style
        {
            get
            {
                return this.getAttribute("style");
            }
            set
            {
                this.setAttribute("style", value);
            }
        }

        /// <summary>
        /// 元素 id
        /// </summary>
        public string id
        {
            get
            {
                return this.getAttribute("id");
            }
            set
            {
                this.setAttribute("id", value);
            }
        }

        /// <summary>
        /// class 属性
        /// </summary>
        public string className
        {
            get
            {
                return this.getAttribute("class");
            }
            set
            {
                this.setAttribute("class", value);
            }
        }

        /// <summary>
        /// 设置或返回节点及其后代的文本内容
        /// </summary>
        public string textContent
        {
            get
            {
                Node[] nodes = this.GetAllNodes();

                string text = "";

                foreach (Node i in nodes)
                {
                    if (i is Text)
                    {
                        text += i.nodeValue;
                    }
                }

                return text;
            }
            set
            {
                this.innerHTML = HttpUtility.HtmlEncode(value);
            }
        }

        /// <summary>
        /// 元素内容
        /// </summary>
        public string innerHTML
        {
            get
            {
                if (!this.IsSingle)
                {
                    return this.ownerDocument.Content.Substring(this.InnerBegin, this.InnerEnd - this.InnerBegin + 1);
                }

                return null;
            }
            set
            {
                this.childNodes = this.ownerDocument.Parse(value).childNodes;
            }
        }

        /// <summary>
        /// 属性集合
        /// </summary>
        public NamedNodeMap<Attr> attributes
        {
            get;
            protected set;
        }

        static Element()
        {
            Element.SingleElements = new HashSet<string>(new string[]
            {
                "!doctype", "meta", "link", "img"
            });
        }

        internal Element(string name)
        {
            this.AllChildElementsCount = 1;

            this.attributes = new NamedNodeMap<Attr>(this);

            this.OriginalName = name;
            this.Name = name.ToLower();

            if (Element.SingleElements.Contains(name))
            {
                this.IsSingle = true;
            }
        }

        /// <summary>
        /// 元素是否拥有属性
        /// </summary>
        /// <returns></returns>
        public bool hasAttributes()
        {
            return this.attributes.Count > 0;
        }

        /// <summary>
        /// 返回元素节点的指定属性值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string getAttribute(string name)
        {
            Attr attr = this.getAttributeNode(name);

            return attr.IsNotNull() ? attr.value : null;
        }

        /// <summary>
        /// 返回指定的属性节点
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Attr getAttributeNode(string name)
        {
            return this.attributes.getNamedItem(name);
        }

        /// <summary>
        /// 把指定属性设置或更改为指定值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void setAttribute(string name, string value)
        {
            this.setAttributeNode(new Attr(name, value, this.ownerDocument.Parse));
        }

        /// <summary>
        /// 设置或更改指定属性节点
        /// </summary>
        /// <param name="attr"></param>
        /// <returns></returns>
        public Attr setAttributeNode(Attr attr)
        {
            return this.attributes.setNamedItem(attr);
        }

        /// <summary>
        /// 从元素中移除指定属性
        /// </summary>
        /// <param name="name"></param>
        public void removeAttribute(string name)
        {
            this.attributes.removeNamedItem(name);
        }

        /// <summary>
        /// 移除指定的属性节点
        /// </summary>
        /// <param name="attr"></param>
        /// <returns></returns>
        public Attr removeAttributeNode(Attr attr)
        {
            return this.attributes.removeNamedItem(attr.name);
        }

        /// <summary>
        /// 克隆节点
        /// </summary>
        /// <param name="deep"></param>
        /// <returns></returns>
        public override Node cloneNode(bool deep)
        {
            Document document = this.ownerDocument.Parse(this.Content);

            Node element = document.firstChild;

            if (!deep)
            {
                foreach (Node i in element.childNodes)
                {
                    element.removeChild(i);
                }
            }

            return element;
        }

        internal bool IsParent(Element element)
        {
            if (this.parentNode.IsNotNull())
            {
                if (this.parentNode is Element)
                {
                    Element parent = (Element)this.parentNode;

                    if (parent == element)
                    {
                        return true;
                    }
                    else
                    {
                        return parent.IsParent(element);
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 获取所有的元素
        /// </summary>
        /// <returns></returns>
        internal Element[] GetAllElements()
        {
            Element[] elements = new Element[this.AllChildElementsCount];

            this.ownerDocument.all.CopyTo(this.Index, elements, this.AllChildElementsCount);

            return elements;
        }

        internal void OnAttributesChange(int offset, bool inOpenTag)
        {
            this.End += offset;

            if (!this.IsSingle && inOpenTag)
            {
                this.InnerBegin += offset;
                this.InnerEnd += offset;
            }

            foreach (Node i in this.ChildNodeList)
            {
                i.SetIndex(offset);
            }

            this.ShiftNext(offset);

            this.ShiftParent(offset);
        }

        protected override int GetAppendIndex()
        {
            return this.InnerEnd + 1;
        }

        protected override void OnRemoveChild(Node node)
        {
            //移除文档 all 集合中的元素
            if (node is Element)
            {
                Element element = (Element)node;

                this.ownerDocument.all.RemoveRange(element.Index, element.AllChildElementsCount);

                this.AlterAllChildElementsCount(-element.AllChildElementsCount);
            }
        }

        protected override void OnInsertBefore(Node newItem, Node existingItem)
        {
            //把元素添加到文档的 all 集合
            if (newItem is Element)
            {
                Element element = (Element)newItem;

                Element nextElement = null;

                if (existingItem is Element)
                {
                    nextElement = (Element)existingItem;
                }
                else
                {
                    nextElement = this.GetNextElement(existingItem);
                }

                if (nextElement.IsNotNull())
                {
                    this.ownerDocument.all.InsertRange(nextElement.Index, element.GetAllElements());
                }
                else
                {
                    if (element.ownerDocument.IsNotNull())
                    {
                        this.ownerDocument.all.AddRange(element.GetAllElements());
                    }
                    else
                    {
                        this.ownerDocument.all.Add(element);
                    }
                }

                this.AlterAllChildElementsCount(element.AllChildElementsCount);
            }
        }

        protected override void OnAppendChild(Node node)
        {
            //把元素添加到文档的 all 集合
            if (node is Element)
            {
                Element element = (Element)node;

                Element nextElement = this.GetNextElement();

                if (nextElement.IsNotNull())
                {
                    this.ownerDocument.all.InsertRange(nextElement.Index, element.GetAllElements());
                }
                else
                {
                    if (element.ownerDocument.IsNotNull())
                    {
                        this.ownerDocument.all.AddRange(element.GetAllElements());
                    }
                    else
                    {
                        this.ownerDocument.all.Add(element);
                    }
                }

                this.AlterAllChildElementsCount(element.AllChildElementsCount);
            }
        }

        /// <summary>
        /// 自检函数
        /// </summary>
        protected override void OnSelfCheck()
        {
            foreach (Attr i in this.attributes)
            {
                i.SelfCheck();
            }
        }

        /// <summary>
        /// 设置新的索引
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        protected override void OnSetIndex(int offset)
        {
            if (!this.IsSingle)
            {
                this.InnerBegin += offset;
                this.InnerEnd += offset;
            }

            foreach (Attr i in this.attributes)
            {
                i.SetIndex(offset);
            }
        }

        /// <summary>
        /// 作为下一个节点偏移索引
        /// </summary>
        /// <param name="offset"></param>
        protected override void OnShiftNext(int offset)
        {
            if (!this.IsSingle)
            {
                this.InnerBegin += offset;
                this.InnerEnd += offset;
            }

            if (this.hasAttributes())
            {
                this.attributes[0].Shift(offset);
            }

            //偏移本实例的子节点
            foreach (Node i in this.ChildNodeList)
            {
                i.SetIndex(offset);
            }
        }

        /// <summary>
        /// 作为父节点偏移索引
        /// </summary>
        /// <param name="offset"></param>
        protected override void OnShiftParent(int offset)
        {
            if (!this.IsSingle)
            {
                this.InnerEnd += offset;
            }

            this.ShiftAttributesInCloseTag(offset);
        }

        /// <summary>
        /// 设置元素的所属文档
        /// </summary>
        /// <param name="document"></param>
        protected override void OnSetOwnerDocument(Document document)
        {
            foreach (Attr i in this.attributes)
            {
                i.ownerDocument = document;
            }
        }

        private void AlterAllChildElementsCount(int difference)
        {
            this.AllChildElementsCount += difference;

            if (this.parentNode.IsNotNull())
            {
                if (this.parentNode is Element)
                {
                    ((Element)this.parentNode).AlterAllChildElementsCount(difference);
                }
            }
        }

        private Element GetNextElement()
        {
            return this.GetNextElement(this);
        }

        private Element GetNextElement(Node node)
        {
            Node testNode = node.nextSibling;

            while (testNode.IsNotNull())
            {
                if (testNode is Element)
                {
                    return (Element)testNode;
                }

                if (testNode.nextSibling.IsNotNull())
                {
                    testNode = testNode.nextSibling;
                }
                else if (testNode.parentNode.IsNotNull())
                {
                    testNode = testNode.parentNode.nextSibling;
                }
            }

            return null;
        }

        private void ShiftAttributesInCloseTag(int offset)
        {
            foreach (Attr i in this.attributes)
            {
                if (!i.InOpenTag)
                {
                    i.Shift(offset);

                    break;
                }
            }
        }
    }
}
