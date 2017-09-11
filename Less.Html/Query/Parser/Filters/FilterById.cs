//bibaoke.com

using System.Linq;
using System.Collections.Generic;
using Less.Collection;
using System;
using Less.Text;

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

        protected override IEnumerable<Element> EvalThis(Document document)
        {
            Element element = document.getElementById(this.Id);

            if (element.IsNotNull())
            {
                return element.ConstructArray();
            }
            else
            {
                return new Element[0];
            }
        }

        protected override IEnumerable<Element> EvalThis(Document document, IEnumerable<Element> source)
        {
            if (document.IsNotNull())
            {
                Element element = document.getElementById(this.Id);

                if (element.IsNotNull())
                {
                    if (source.Any(i => i.EnumerateAllElements().Contains(element)))
                    {
                        return element.ConstructArray();
                    }
                }

                return new Element[0];
            }
            else
            {
                return source.SelectMany(i => i.EnumerateAllElements().Where(
                    j =>
                    j.id.IsNotNull() && j.id.CompareIgnoreCase(this.Id)));
            }
        }
    }
}
