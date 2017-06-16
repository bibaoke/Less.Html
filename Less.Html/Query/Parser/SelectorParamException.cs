//bibaoke.com

using System;

namespace Less.Html
{
    /// <summary>
    /// 选择器参数错误
    /// </summary>
    public class SelectorParamException : Exception
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index"></param>
        /// <param name="near"></param>
        public SelectorParamException(int index, string near) :
            base(string.Format("选择器参数错误，在位置{0}，“{1}”附近", index, near))
        {

        }
    }
}
