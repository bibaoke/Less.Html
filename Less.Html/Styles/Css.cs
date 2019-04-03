//bibaoke.com

namespace Less.Html
{
    /// <summary>
    /// css 文档
    /// </summary>
    public class Css
    {
        internal string Content
        {
            get;
            set;
        }

        /// <summary>
        /// 样式集合
        /// </summary>
        public StyleCollection Styles
        {
            get;
            private set;
        }

        /// <summary>
        /// 样式块集合
        /// </summary>
        public BlockCollection Blocks
        {
            get;
            private set;
        }

        internal Css(string content)
        {
            this.Content = content;

            this.Styles = new StyleCollection();
            this.Blocks = new BlockCollection();
        }
    }
}
