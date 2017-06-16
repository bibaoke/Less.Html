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
                return this.ownerDocument.Content.Substring(this.Begin, this.End - this.Begin + 1);
            }
        }

        internal Comment(int begin, int end) : base(begin, end)
        {

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
    }
}
