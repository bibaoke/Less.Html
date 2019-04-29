//bibaoke.com

using Less.Text;

namespace Less.Html
{
    /// <summary>
    /// 样式块
    /// </summary>
    public class Block : CssInfo
    {
        internal int NameEnd
        {
            get;
            set;
        }

        /// <summary>
        /// 样式块名称
        /// </summary>
        public string Name
        {
            get
            {
                return this.OwnerCss.Content.SubstringUnsafe(this.Begin, this.NameEnd - this.Begin + 1);
            }
        }

        /// <summary>
        /// 样式集合
        /// </summary>
        public StyleCollection Styles
        {
            get;
            private set;
        }

        internal Block(Css ownerCss, int begin, int nameEnd) : base(ownerCss, begin)
        {
            this.Styles = new StyleCollection();

            this.NameEnd = nameEnd;
        }
    }
}
