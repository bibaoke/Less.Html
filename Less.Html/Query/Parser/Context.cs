//bibaoke.com

using System.Collections.Generic;

namespace Less.Html.SelectorParamParser
{
    internal class Context
    {
        internal string Param
        {
            get;
            private set;
        }

        internal int Position
        {
            get;
            set;
        }

        internal string CurrentSymbol
        {
            get;
            set;
        }

        internal bool PreSpace
        {
            get;
            set;
        }

        internal List<ElementFilter> FilterList
        {
            get;
            private set;
        }

        internal ElementFilter CurrentFilter
        {
            get;
            set;
        }

        internal Context(string param)
        {
            this.Param = param;

            this.FilterList = new List<ElementFilter>();
        }
    }
}
