//bibaoke.com

using System.Text.RegularExpressions;

namespace Less.Html.CssInternal
{
    internal abstract class ReaderBase
    {
        internal Context Context
        {
            get;
            set;
        }

        internal Css Css
        {
            get
            {
                return this.Context.Css;
            }
        }

        internal string Content
        {
            get
            {
                return this.Css.Content;
            }
        }

        internal int Position
        {
            get
            {
                return this.Context.Position;
            }
            set
            {
                this.Context.Position = value;
            }
        }

        internal Style CurrentStyle
        {
            get
            {
                return this.Context.CurrentStyle;
            }
            set
            {
                this.Context.CurrentStyle = value;
            }
        }

        internal Block CurrentBlock
        {
            get
            {
                return this.Context.CurrentBlock;
            }
            set
            {
                this.Context.CurrentBlock = value;
            }
        }

        internal StyleCollection Styles
        {
            get
            {
                return this.Css.Styles;
            }
        }

        internal BlockCollection Blocks
        {
            get
            {
                return this.Css.Blocks;
            }
        }

        internal abstract ReaderBase Read();

        protected T Pass<T>() where T : ReaderBase, new()
        {
            T t = new T();

            t.Context = this.Context;

            return t;
        }

        protected void Ascend(Match match)
        {
            this.Position = match.Index + match.Length;
        }
    }
}
