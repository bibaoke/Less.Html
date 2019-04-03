//bibaoke.com

namespace Less.Html
{
    /// <summary>
    /// 样式块
    /// </summary>
    public class Block
    {
        /// <summary>
        /// 样式块名称
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// 样式集合
        /// </summary>
        public StyleCollection Styles
        {
            get;
            private set;
        }

        internal Block(string name)
        {
            this.Styles = new StyleCollection();

            this.Name = name;
        }
    }
}
