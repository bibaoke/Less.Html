//bibaoke.com

using System.Linq;
using System;
using System.Collections.Generic;
using Less.Text;
using Less.Collection;

namespace Less.Html
{
    /// <summary>
    /// 文档
    /// </summary>
    public class Document : Node
    {
        internal override int Begin
        {
            get
            {
                return 0;
            }
            set
            {
                //不能设置文档的索引
            }
        }

        internal override int End
        {
            get
            {
                return this.Content.Length - 1;
            }
            set
            {
                //不能设置文档的索引
            }
        }

        /// <summary>
        /// 文档内容
        /// </summary>
        internal new DocumentContent Content
        {
            get;
            set;
        }

        /// <summary>
        /// html 解析委托
        /// </summary>
        internal Func<string, Document> Parse
        {
            get;
            private set;
        }

        internal List<Node> AllNodes
        {
            get;
            private set;
        }

        /// <summary>
        /// 节点名称
        /// </summary>
        public override string nodeName
        {
            get
            {
                return "#DOCUMENT";
            }
        }

        /// <summary>
        /// 返回对文档中所有 area 和 a 元素
        /// </summary>
        public Element[] links
        {
            get
            {
                Element[] elements = this.getElementsByTagName("a").ExtArray(this.getElementsByTagName("area"));

                return elements.Where(i => i.attributes["href"].IsNotNull()).ToArray();
            }
        }

        /// <summary>
        /// 所有元素
        /// </summary>
        public ElementCollection all
        {
            get;
            private set;
        }

        private Document(string content, Func<string, Document> parse, ElementCollection all)
        {
            this.AllNodes = new List<Node>();

            this.Index = 0;

            this.AllNodes.Add(this);

            this.all = all;

            //设置文档内容
            this.Content = content;

            //文档的文档元素是本身
            this.ownerDocument = this;

            this.Parse = parse;
        }

        internal Document(string content, Func<string, Document> parse) : this(content, parse, new ElementCollection())
        {
            //
        }

        /// <summary>
        /// 输出字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Content;
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
                return this.Clone(null);
            }
            else
            {
                return new Document("", this.Parse);
            }
        }

        /// <summary>
        /// 返回对拥有指定 id 的第一个元素
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Element getElementById(string id)
        {
            return this.all.GetElementById(id);
        }

        /// <summary>
        /// 返回带有指定名称的元素
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Element[] getElementsByName(string name)
        {
            return this.all.GetElementsByName(name);
        }

        /// <summary>
        /// 返回带有指定标签名的元素
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public Element[] getElementsByTagName(string tagName)
        {
            return this.all.GetElementsByTagName(tagName);
        }

        /// <summary>
        /// 创建元素
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Element createElement(string name)
        {
            return new Element(name);
        }

        internal override Node Clone(Node parent)
        {
            Document clone = new Document(this.Content, this.Parse, this.all.Clone());

            clone.AllNodesCount = this.AllNodesCount;

            clone.AllNodes.Capacity = this.AllNodes.Capacity;

            clone.ChildNodeList.Capacity = this.ChildNodeList.Capacity;

            foreach (Node i in this.ChildNodeList)
            {
                Node child = i.Clone(clone);
            }

            return clone;
        }

        /// <summary>
        /// 添加子节点时执行
        /// </summary>
        /// <param name="node"></param>
        protected override void OnAppendChild(Node node)
        {
            //把元素添加到文档的 all 集合
            if (node is Element)
            {
                Element element = (Element)node;

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
        }

        /// <summary>
        /// 获取文档的子节点插入索引
        /// </summary>
        /// <returns></returns>
        protected override int GetAppendIndex()
        {
            return this.End + 1;
        }
    }
}
