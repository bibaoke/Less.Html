//bibaoke.com

namespace Less.Html
{
    /// <summary>
    /// 注释
    /// </summary>
    public class Comment : Node
    {
        /// <summary>
        /// 节点名称
        /// </summary>
        internal static string NodeName
        {
            get { return "#comment"; }
        }

        /// <summary>
        /// 节点名称
        /// </summary>
        public override string nodeName
        {
            get
            {
                return Comment.NodeName.ToUpper();
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
                
                return this.ownerDocument.Content.Substring(this.Begin, length);
            }
        }

        internal Comment(int begin, int end) : base(begin, end)
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
            Comment clone = new Comment(this.Begin, this.End);

            clone.parentNode = parent;

            parent.ChildNodeList.Add(clone);

            parent.ownerDocument.AllNodes.Add(clone);

            clone.ownerDocument = parent.ownerDocument;

            return clone;
        }
    }
}
