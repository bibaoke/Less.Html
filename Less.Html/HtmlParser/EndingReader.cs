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
            //当前要处理的标签
            Node node = this.CurrentNode;

            //根据标记栈确定没有关闭标签的个数
            this.MarkStack.Count.Each(() =>
            {
                //节点一定是元素
                Element element = (Element)node;

                //设置元素结束位置
                if (element.hasChildNodes())
                {
                    element.End = element.firstChild.Begin - 1;
                }
                else
                {
                    element.End = this.Position - 1;
                }

                element.InnerEnd = element.End;

                //设置当前处理的标签为上一级节点
                node = node.parentNode;
            });

            //文档结束索引
            int end = this.Content.Length - 1;

            //截取文档最后的文本
            if (end >= this.Previous.Position)
            {
                if (this.Document.all.Count > 0)
                {
                    Element last = this.Document.all[this.Document.all.Count - 1];

                    if (last.End == 0)
                    {
                        this.CurrentNode.DiscardNode(last);

                        return null;
                    }
                }

                node.appendChild(new Text(this.Previous.Position, end));
            }

            return null;
        }
    }
}
