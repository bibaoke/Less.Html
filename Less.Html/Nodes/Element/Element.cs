//bibaoke.com

using System;
using System.Collections.Generic;
using System.Web;
using Less.Text;
using Less.Collection;

namespace Less.Html
{
    /// <summary>
    /// 元素
    /// </summary>
    public class Element : Node
    {
        private static HashSet<string> TableElements
        {
            get;
            set;
        }

        private static HashSet<string> EnumElements
        {
            get;
            set;
        }

        private static HashSet<string> SingleElements
        {
            get;
            set;
        }

        private int AllElementsCount
        {
            get;
            set;
        }

        internal string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// 是否表格型元素
        /// </summary>
        internal bool IsTable
        {
            get;
            private set;
        }

        /// <summary>
        /// 是否枚举型元素
        /// </summary>
        internal bool IsEnum
        {
            get;
            private set;
        }

        /// <summary>
        /// 是否单标签元素
        /// </summary>
        internal bool IsSingle
        {
            get;
            private set;
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

        internal new int Index
        {
            get;
            set;
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
                IEnumerable<Node> nodes = this.EnumerateAllNodes();

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
                    int length = this.InnerEnd - this.InnerBegin + 1;

                    return this.ownerDocument.Content.SubstringUnsafe(this.InnerBegin, length);
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
            Element.TableElements = new HashSet<string>(new string[]
            {
                "tr", "td"
            }, StringComparer.OrdinalIgnoreCase);

            Element.EnumElements = new HashSet<string>(new string[]
            {
                "option", "li"
            }, StringComparer.OrdinalIgnoreCase);

            Element.SingleElements = new HashSet<string>(new string[]
            {
                "!doctype", "meta", "base", "link", "img", "input", "br"
            }, StringComparer.OrdinalIgnoreCase);
        }

        internal Element(string name)
        {
            this.Index = -1;

            this.AllElementsCount = 1;

            this.attributes = new NamedNodeMap<Attr>(this);

            this.Name = name;

            if (Element.TableElements.Contains(name))
            {
                this.IsTable = true;
            }

            if (Element.EnumElements.Contains(name))
            {
                this.IsEnum = true;
            }

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
            if (deep)
            {
                Document document = this.ownerDocument.Parse(this.Content);

                return document.firstChild;
            }
            else
            {
                string openTag = this.ownerDocument.Content.SubstringUnsafe(this.Begin, this.InnerBegin - this.Begin);

                string closeTag = this.ownerDocument.Content.SubstringUnsafe(this.InnerEnd + 1, this.End - this.InnerEnd);

                string content = openTag + closeTag;

                Document document = this.ownerDocument.Parse(content);

                return document.firstChild;
            }
        }

        internal override Node Clone(Node parent)
        {
            Element clone = new Element(this.Name);

            clone.parentNode = parent;

            clone.ChildIndex = this.ChildIndex;

            parent.ChildNodeList.Add(clone);

            ((Node)clone).Index = base.Index;

            parent.ownerDocument.AllNodes.Add(clone);

            clone.ownerDocument = parent.ownerDocument;

            clone.Begin = this.Begin;
            clone.End = this.End;

            clone.InnerBegin = this.InnerBegin;
            clone.InnerEnd = this.InnerEnd;

            clone.AllNodesCount = this.AllNodesCount;
            clone.AllElementsCount = this.AllElementsCount;

            foreach (Attr i in this.attributes)
            {
                Attr attr = (Attr)i.Clone(clone);

                attr.Element = clone;

                clone.attributes.AddItem(attr);
            }

            clone.Index = this.Index;

            parent.ownerDocument.all.Add(clone);

            clone.ChildNodeList.Capacity = this.ChildNodeList.Capacity;

            foreach (Node i in this.ChildNodeList)
            {
                Node child = i.Clone(clone);
            }

            return clone;
        }

        internal bool Contains(Element element)
        {
            if (this.ownerDocument == element.ownerDocument)
            {
                return element.Index >= this.Index && element.Index < this.Index + this.AllElementsCount;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 枚举本元素和所有的后代元素
        /// </summary>
        /// <returns></returns>
        internal IEnumerable<Element> EnumerateAllElements()
        {
            return this.ownerDocument.all.GetEnumerator(this.Index, this.AllElementsCount);
        }

        /// <summary>
        /// 获取本元素和所有的后代元素
        /// </summary>
        /// <returns></returns>
        internal Element[] GetAllElements()
        {
            Element[] elements = new Element[this.AllElementsCount];

            this.ownerDocument.all.CopyTo(this.Index, elements, this.AllElementsCount);

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

        /// <summary>
        /// 获取子节点插入索引
        /// </summary>
        /// <returns></returns>
        protected override int GetAppendIndex()
        {
            if (this.IsSingle)
            {
                return this.End + 1;
            }
            else
            {
                return this.InnerEnd + 1;
            }
        }

        /// <summary>
        /// 在移除子节点之后调用 此时子节点会属于一个新的文档
        /// </summary>
        /// <param name="document">移除节点的所属文档</param>
        /// <param name="nodes">已经移除的节点</param>
        protected override void OnRemovedChild(Document document, Node[] nodes)
        {
            IEnumerable<Element> elements = nodes.GetElements();

            elements.Each((index, item) =>
            {
                item.Index = document.all.Count + index;
            });

            document.all.AddRange(elements);
        }

        /// <summary>
        /// 在移除子节点时执行
        /// </summary>
        /// <param name="node"></param>
        protected override void OnRemoveChild(Node node)
        {
            //移除文档 all 集合中的元素
            if (node is Element)
            {
                Element element = (Element)node;

                this.ownerDocument.all.RemoveRange(element.Index, element.AllElementsCount);

                foreach (Element i in this.ownerDocument.all.GetEnumerator(element.Index))
                {
                    i.Index = i.Index - element.AllElementsCount;
                };

                this.AlterAllChildElementsCount(-element.AllElementsCount);
            }
        }

        /// <summary>
        /// 在插入子节点时执行
        /// </summary>
        /// <param name="newItem"></param>
        /// <param name="existingItem"></param>
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
                    Element[] elements = element.GetAllElements();

                    this.ownerDocument.all.InsertRange(nextElement.Index, elements);

                    elements.Each((index, item) =>
                    {
                        item.Index = nextElement.Index + index;
                    });

                    foreach (Element i in this.ownerDocument.all.GetEnumerator(nextElement.Index + elements.Length))
                    {
                        i.Index += elements.Length;
                    };
                }
                else
                {
                    if (element.ownerDocument.IsNotNull())
                    {
                        Element[] elements = element.GetAllElements();

                        elements.Each((index, item) =>
                        {
                            item.Index = this.ownerDocument.all.Count + index;
                        });

                        this.ownerDocument.all.AddRange(elements);
                    }
                    else
                    {
                        element.Index = this.ownerDocument.all.Count;

                        this.ownerDocument.all.Add(element);
                    }
                }

                this.AlterAllChildElementsCount(element.AllElementsCount);
            }
        }

        /// <summary>
        /// 在添加子节点时执行
        /// </summary>
        /// <param name="node"></param>
        protected override void OnAppendChild(Node node)
        {
            //把元素添加到文档的 all 集合
            if (node is Element)
            {
                Element element = (Element)node;

                Element nextElement = this.GetNextElement();

                if (nextElement.IsNotNull())
                {
                    Element[] elements = element.GetAllElements();

                    this.ownerDocument.all.InsertRange(nextElement.Index, elements);

                    elements.Each((index, item) =>
                    {
                        item.Index = nextElement.Index + index;
                    });

                    foreach (Element i in this.ownerDocument.all.GetEnumerator(nextElement.Index + elements.Length))
                    {
                        i.Index += elements.Length;
                    };
                }
                else
                {
                    if (element.ownerDocument.IsNotNull())
                    {
                        Element[] elements = element.GetAllElements();

                        elements.Each((index, item) =>
                        {
                            item.Index = this.ownerDocument.all.Count + index;
                        });

                        this.ownerDocument.all.AddRange(elements);
                    }
                    else
                    {
                        element.Index = this.ownerDocument.all.Count;

                        this.ownerDocument.all.Add(element);
                    }
                }

                this.AlterAllChildElementsCount(element.AllElementsCount);
            }
        }

        /// <summary>
        /// 自检函数
        /// </summary>
        protected override void OnSelfCheck()
        {
            int collectionIndex = this.ownerDocument.all.IndexOf(this);

            if (this.Index != collectionIndex)
            {
                throw new SelfCheckingException();
            }

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
            this.AllElementsCount += difference;

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
            int index = this.Index + this.AllElementsCount;

            if (this.ownerDocument.all.Count > index)
            {
                return this.ownerDocument.all[index];
            }
            else
            {
                return null;
            }
        }

        private Element GetNextElement(Node node)
        {
            int index = node.Index + node.AllNodesCount;

            while (node.ownerDocument.AllNodes.Count > index)
            {
                Node test = node.ownerDocument.AllNodes[index];

                if (test is Element)
                {
                    return (Element)test;
                }

                index++;
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
