//bibaoke.com

using Less.Collection;

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
            //处理没有关闭的标签
            this.MarkStack.Count.Each(() =>
            {
                Element element = (Element)this.CurrentNode;

                //设置元素结束位置
                element.End = this.Position - 1;

                element.InnerEnd = element.End;

                //设置当前节点为上一级节点
                this.CurrentNode = this.CurrentNode.parentNode;
            });

            //文档结束索引
            int end = this.Content.Length - 1;

            //截取文档最后的文本
            if (end >= this.Previous.Position)
                this.CurrentNode.appendChild(new Text(this.Previous.Position, end));

            return null;
        }
    }
}
