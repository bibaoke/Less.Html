//bibaoke.com

namespace Less.Html.HtmlInternal
{
    /// <summary>
    /// 标签标记
    /// </summary>
    internal class TagMark
    {
        /// <summary>
        /// 标签名
        /// </summary>
        internal string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// 位置 标签结束时 解析器扫描到的位置 等于元素的 End + 1
        /// </summary>
        internal int Position
        {
            get;
            private set;
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="name"></param>
        /// <param name="position"></param>
        internal TagMark(string name, int position)
        {
            this.Name = name;
            this.Position = position;
        }
    }
}
