//bibaoke.com

using System;
using System.Collections.Generic;

namespace Less.Html.HtmlInternal
{
    /// <summary>
    /// Html 阅读器上下文
    /// </summary>
    internal class Context
    {
        /// <summary>
        /// 当前正在读取的节点
        /// </summary>
        internal Node CurrentNode
        {
            get;
            set;
        }

        /// <summary>
        /// 整个 html 文档元素
        /// </summary>
        internal Document Document
        {
            get;
            private set;
        }

        /// <summary>
        /// 标签标记栈 只包括开标签
        /// </summary>
        internal Stack<TagMark> MarkStack
        {
            get;
            private set;
        }

        /// <summary>
        /// 上一个标签标记 包括开标签和闭标签
        /// </summary>
        internal TagMark Previous
        {
            get;
            set;
        }

        /// <summary>
        /// 当前正在读取的位置
        /// </summary>
        internal int Position
        {
            get;
            set;
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="content"></param>
        /// <param name="parse"></param>
        internal Context(string content, Func<string, Document> parse)
        {
            this.MarkStack = new Stack<TagMark>();

            this.Document = new Document(content, parse);

            this.Previous = new TagMark(this.Document.nodeName, 0);

            this.CurrentNode = this.Document;
        }
    }
}
