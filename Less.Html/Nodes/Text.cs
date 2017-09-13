//bibaoke.com

using Less.Text;

namespace Less.Html
{
    /// <summary>
    /// 文本
    /// </summary>
    public class Text : Node
    {
        /// <summary>
        /// 元素名称
        /// </summary>
        public override string nodeName
        {
            get
            {
                return "#text".ToUpper();
            }
        }

        /// <summary>
        /// 节点值
        /// </summary>
        public override string nodeValue
        {
            get
            {
                int length = this.End - this.Begin + 1;

                string content = this.ownerDocument.Content.SubstringUnsafe(this.Begin, length);

                return this.Decode(content);
            }
        }

        internal Text(int begin, int end) : base(begin, end)
        {
            //
        }

        /// <summary>
        /// 克隆节点
        /// </summary>
        /// <param name="deep"></param>
        /// <returns></returns>
        public override Node cloneNode(bool deep)
        {
            return this.ownerDocument.Parse(this.Content).firstChild;
        }

        internal override Node Clone(Node parent)
        {
            Text clone = new Text(this.Begin, this.End);

            clone.parentNode = parent;

            parent.ChildNodeList.Add(clone);

            parent.ownerDocument.AllNodes.Add(clone);

            clone.ownerDocument = parent.ownerDocument;

            return clone;
        }
    }
}
