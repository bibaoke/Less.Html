//bibaoke.com

using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Less.Html.SelectorParamParser
{
    internal abstract class ReaderBase
    {
        internal Context Context
        {
            get;
            set;
        }

        internal int Position
        {
            get
            {
                return this.Context.Position;
            }
            private set
            {
                this.Context.Position = value;
            }
        }

        internal string Param
        {
            get
            {
                return this.Context.Param;
            }
        }

        internal string CurrentSymbol
        {
            get
            {
                return this.Context.CurrentSymbol;
            }
            set
            {
                this.Context.CurrentSymbol = value;
            }
        }

        internal bool PreSpace
        {
            get
            {
                return this.Context.PreSpace;
            }
            set
            {
                this.Context.PreSpace = value;
            }
        }

        internal List<ElementFilter> FilterList
        {
            get
            {
                return this.Context.FilterList;
            }
        }

        internal ElementFilter CurrentFilter
        {
            get
            {
                return this.Context.CurrentFilter;
            }
            set
            {
                this.Context.CurrentFilter = value;
            }
        }

        internal void SetNext(ElementFilter next)
        {
            if (this.CurrentFilter.IsNull())
            {
                this.FilterList.Add(next);
            }
            else
            {
                this.CurrentFilter.Next = next;
            }

            this.CurrentFilter = next;
        }

        internal void SetChild(ElementFilter child)
        {
            if (this.CurrentFilter.IsNull())
            {
                this.FilterList.Add(child);
            }
            else
            {
                this.CurrentFilter.Child = child;
            }

            this.CurrentFilter = child;
        }

        internal abstract ReaderBase Read();

        protected void Ascend(Capture capture)
        {
            this.Position = capture.Index + capture.Length;
        }

        protected T Pass<T>() where T : ReaderBase, new()
        {
            T t = new T();

            t.Context = this.Context;

            return t;
        }

        protected void AddFilter(ElementFilter filter, bool next)
        {
            if (next)
            {
                this.SetNext(filter);
            }
            else
            {
                this.SetChild(filter);
            }
        }
    }
}
