//bibaoke.com

using Less.Text;
using System;
using Less.Collection;
using System.Web;

namespace Less.Html
{
    /// <summary>
    /// 属性
    /// </summary>
    public class Attr : Node
    {
        /// <summary>
        /// 属性名开始索引
        /// </summary>
        private int NameBegin
        {
            get;
            set;
        }

        /// <summary>
        /// 属性名长度
        /// </summary>
        private int NameLength
        {
            get;
            set;
        }

        /// <summary>
        /// 属性值开始索引
        /// </summary>
        private int ValueBegin
        {
            get;
            set;
        }

        /// <summary>
        /// 属性值长度
        /// </summary>
        private int ValueLength
        {
            get;
            set;
        }

        internal bool InOpenTag
        {
            get
            {
                if (this.NameLength > -1)
                    return this.NameBegin < this.Element.InnerBegin;
                else
                    return this.ValueBegin < this.Element.InnerBegin;
            }
        }

        internal Element Element
        {
            get;
            set;
        }

        /// <summary>
        /// 节点名称
        /// </summary>
        public override string nodeName
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// 属性名称
        /// </summary>
        public string name
        {
            get
            {
                if (this.NameLength > -1)
                    return this.ownerDocument.Content.Substring(this.NameBegin, this.NameLength).ToLower();

                return null;
            }
        }

        /// <summary>
        /// 属性值
        /// </summary>
        public string value
        {
            get
            {
                if (this.ValueLength > -1)
                    return HttpUtility.HtmlDecode(this.ownerDocument.Content.Substring(this.ValueBegin, this.ValueLength));

                return null;
            }
        }

        /// <summary>
        /// 克隆属性
        /// </summary>
        /// <param name="deep"></param>
        /// <returns></returns>
        public override Node cloneNode(bool deep)
        {
            Attr attr = new Attr();

            attr.Begin = this.Begin;
            attr.End = this.End;
            attr.NameBegin = this.NameBegin;
            attr.NameLength = this.NameBegin;
            attr.ValueBegin = this.NameBegin;
            attr.ValueLength = this.NameBegin;

            attr.ownerDocument = new Document(this.Content, this.ownerDocument.Parse);

            attr.SetIndex(-attr.Begin);

            return attr;
        }

        internal override void OnChangeNamedItem(Node reference, Node replace)
        {
            Element element = (Element)reference;
            Attr attr = (Attr)replace;

            int stringIndex = attr.Begin;
            int collectionIndex = element.attributes.IndexOf(attr);
            bool inOpenTag = attr.InOpenTag;

            attr.OnRemoveNamedItem();

            element.attributes.Value.Insert(collectionIndex, this);

            string postfix = Symbol.Space;

            if (element.ownerDocument.Content[stringIndex + 1] == Symbol.Space[0])
                postfix = "";

            element.ownerDocument.Content = element.ownerDocument.Content.Insert(stringIndex, this.Content + postfix);

            int length = this.End - this.Begin + 1 + postfix.Length;

            int index = element.attributes.IndexOf(this);

            int next = index + 1;

            if (element.attributes.Count > next)
            {
                element.attributes[next].Shift(length);
            }

            element.OnAttributesChange(length, inOpenTag);

            this.Element = element;

            this.ownerDocument = element.ownerDocument;

            this.SetIndex(stringIndex);

            this.ownerDocument.SelfCheck();
        }

        internal override void OnAddNamedItem(Node reference)
        {
            Element element = (Element)reference;

            if (this.Element.IsNotNull())
            {
                element.attributes.Value.Add(this);
            }
            else
            {
                int position = 0;

                element.attributes.Each((idx, item) =>
                {
                    position = idx;

                    if (!item.InOpenTag)
                    {
                        return false;
                    }

                    return true;
                });

                element.attributes.Value.Insert(position, this);

                int begin;

                string prefix = Symbol.Space;
                string postfix = Symbol.Space;

                if (element.IsSingle)
                {
                    begin = element.End - 1;

                    if (element.ownerDocument.Content[begin - 1] == '/')
                    {
                        begin--;
                    }
                    else
                    {
                        postfix = "";
                    }
                }
                else
                {
                    begin = element.InnerBegin - 1;

                    postfix = "";
                }

                if (element.ownerDocument.Content[begin - 1] == Symbol.Space[0])
                {
                    prefix = "";
                }

                element.ownerDocument.Content = element.ownerDocument.Content.Insert(begin, prefix + this.Content + postfix);

                int length = prefix.Length + this.End - this.Begin + 1 + postfix.Length;

                int index = element.attributes.IndexOf(this);

                int next = index + 1;

                if (element.attributes.Count > next)
                {
                    element.attributes[next].Shift(length);
                }

                element.OnAttributesChange(length, true);

                this.Element = element;

                this.ownerDocument = element.ownerDocument;

                this.SetIndex(begin);

                this.ownerDocument.SelfCheck();
            }
        }

        internal override void OnRemoveNamedItem()
        {
            //关联了元素的属性
            if (this.Element.IsNotNull())
            {
                string content = this.Content;

                int length = this.End - this.Begin + 1;

                int removeLength = this.ownerDocument.Content[this.End + 1] == Symbol.Space[0] ? length + 1 : length;

                this.ownerDocument.Content = this.ownerDocument.Content.Remove(this.Begin, removeLength);

                int index = this.Element.attributes.IndexOf(this);

                int next = index + 1;

                int offset = -removeLength;

                if (this.Element.attributes.Count > next)
                    this.Element.attributes[next].Shift(offset);

                this.Element.OnAttributesChange(offset, this.InOpenTag);

                this.Element.attributes.Value.Remove(this);

                //删除关联元素
                this.Element = null;

                //新建一个文档存放原属性值
                this.ownerDocument = new Document(content, this.ownerDocument.Parse);

                //偏移量
                offset = -this.Begin;

                //设置属性索引
                this.Begin += offset;
                this.End += offset;

                if (this.NameLength > -1)
                    this.NameBegin += offset;

                if (this.ValueLength > -1)
                    this.ValueBegin += offset;
            }
        }

        internal void Shift(int offset)
        {
            this.Begin += offset;
            this.End += offset;

            if (this.NameLength > -1)
                this.NameBegin += offset;

            if (this.ValueLength > -1)
                this.ValueBegin += offset;

            int index = this.Element.attributes.IndexOf(this);

            if (index >= 0)
            {
                int next = index + 1;

                if (next < this.Element.attributes.Count)
                    this.Element.attributes[next].Shift(offset);
            }
        }

        private Attr()
        {

        }

        internal Attr(string name, string value, Func<string, Document> parse)
        {
            Document document = new Document("{0}=\"{1}\"".FormatString(name, value), parse);

            this.ownerDocument = document;

            this.Begin = 0;
            this.End = document.End;

            this.NameBegin = 0;
            this.NameLength = name.Length;

            this.ValueBegin = name.Length + 2;
            this.ValueLength = value.Length;
        }

        internal Attr(Element element, int begin, int end, int nameBegin, int nameLength, int valueBegin, int valueLength) : base(begin, end)
        {
            this.Element = element;

            this.ownerDocument = element.ownerDocument;

            this.NameBegin = nameBegin;
            this.NameLength = nameLength;
            this.ValueBegin = valueBegin;
            this.ValueLength = valueLength;
        }

        /// <summary>
        /// 设置新的索引
        /// </summary>
        /// <param name="offset"></param>
        protected override void OnSetIndex(int offset)
        {
            if (this.NameLength > -1)
                this.NameBegin += offset;

            if (this.ValueLength > -1)
                this.ValueBegin += offset;
        }
    }
}
