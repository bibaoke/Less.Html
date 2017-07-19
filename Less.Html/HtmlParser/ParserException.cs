using System;

namespace Less.Html
{
    /// <summary>
    /// 解析器异常
    /// </summary>
    public class ParserException : Exception
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
        public ParserException(string content) : this("解析器异常", content)
        {
            //
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public ParserException(string message, string content) : base(message)
        {
            this.Content = content;
        }
    }
}
