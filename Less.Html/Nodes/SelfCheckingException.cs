//bibaoke.com

using System;

namespace Less.Html
{
    /// <summary>
    /// 自检错误
    /// </summary>
    public class SelfCheckingException : Exception
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="innerException"></param>
        public SelfCheckingException(Exception innerException) : base("自检错误", innerException)
        {

        }
    }
}
