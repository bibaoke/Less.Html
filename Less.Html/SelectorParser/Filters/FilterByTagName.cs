//bibaoke.com

using System.Linq;
using System.Collections.Generic;
using Less.Text;

namespace Less.Html.SelectorParser
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
            if (document.IsNotNull())
            {
                Element[] elements = document.getElementsByTagName(this.Name);

                return source.SelectMany(i => elements.Where(j => i.Contains(j)));
            }
            else
            {
                return source.SelectMany(i => i.EnumerateAllElements().Where(
                    j =>
                    j.nodeName.CompareIgnoreCase(this.Name)));
            }
        }
    }
}
