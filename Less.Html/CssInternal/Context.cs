//bibaoke.com

namespace Less.Html.CssInternal
{
    internal class Context
    {
        internal Css Css
        {
            get;
            private set;
        }

        internal int Position
        {
            get;
            set;
        }

        internal Style CurrentStyle
        {
            get;
            set;
        }

        internal Block CurrentBlock
        {
            get;
            set;
        }

        internal Context(string content)
        {
            this.Css = new Css(content);
        }
    }
}
