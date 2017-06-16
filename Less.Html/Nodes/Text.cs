//bibaoke.com

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
                return this.Decode(this.ownerDocument.Content.Substring(this.Begin, this.End - this.Begin + 1));
            }
        }

        internal Text(int begin, int end) : base(begin, end)
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
