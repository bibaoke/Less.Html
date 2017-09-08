//bibaoke.com

using System.Linq;
using System.Collections.Generic;
using System;

namespace Less.Html
{
    internal class FilterByTagName : ElementFilter
    {
        internal string Name
        {
            get;
            private set;
        }

        internal FilterByTagName(string name)
        {
            this.Name = name;
        }

        protected override IEnumerable<Element> EvalThis(Document document)
        {
            return document.getElementsByTagName(this.Name);
        }

        protected override IEnumerable<Element> EvalThis(Document document, IEnumerable<Element> source)
        {
            Element[] elements = document.getElementsByTagName(this.Name);

            return source.SelectMany(i => elements.Where(j => i.GetAllElements().Contains(j)));
        }
    }
}
