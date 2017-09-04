//bibaoke.com

using System.Linq;
using System.Collections.Generic;

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

        protected override IEnumerable<Element> EvalThis(Document document, IEnumerable<Element> source)
        {
            Element[] elements = document.getElementsByTagName(this.Name);

            if (elements.IsNotNull())
            {
                return source.SelectMany(i => elements.Where(j => j == i || j.IsParent(i)));
            }

            return new Element[0];
        }
    }
}
