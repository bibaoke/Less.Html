using System;

namespace Less.Html
{
    /// <summary>
    /// html 解析错误
    /// </summary>
    public class ParseException : Exception
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
        public ParseException(string content) : this("html 解析错误", content)
        {
            //
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public ParseException(string message, string content) : base(message)
        {
            this.Content = content;
        }
    }
}
