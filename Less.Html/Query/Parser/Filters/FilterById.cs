//bibaoke.com

using System.Linq;
using System.Collections.Generic;
using Less.Collection;

namespace Less.Html
{
    internal class FilterById : ElementFilter
    {
        internal string Id
        {
            get;
            private set;
        }

        internal FilterById(string id)
        {
            this.Id = id;
        }

        protected override IEnumerable<Element> EvalThis(Document document, IEnumerable<Element> source)
        {
            Element element = document.getElementById(this.Id);

            if (element.IsNotNull())
            {
                if (source.Any(i => element == i || element.IsParent(i)))
                {
                    return element.ConstructArray();
                }
            }

            return new Element[0];
        }
    }
}
