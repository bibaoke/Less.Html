using System;

namespace Less.Html
{
    /// <summary>
    /// 文档异常
    /// </summary>
    public class DocumentException : Exception
    {
        /// <summary>
        /// 出错的文档内容
        /// </summary>
        public string Content
        {
            get;
            private set;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public DocumentException(string content) : this("文档异常", content)
        {
            //
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public DocumentException(string message, string content) : base(message)
        {
            this.Content = content;
        }
    }
}
