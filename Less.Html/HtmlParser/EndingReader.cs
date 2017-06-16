//bibaoke.com

namespace Less.Html
{
    /// <summary>
    /// 文档结束阅读器
    /// </summary>
    internal class EndingReader : ReaderBase
    {
        /// <summary>
        /// 阅读
        /// </summary>
        /// <returns></returns>
        internal override ReaderBase Read()
        {
            //文档结束索引
            int end = this.Content.Length - 1;

            //截取文档最后的文本
            if (end > this.Previous.Position)
                this.CurrentNode.appendChild(new Text(this.Previous.Position, end));

            return null;
        }
    }
}
