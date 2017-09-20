//bibaoke.com

using System.Linq;
using Less.Text;
using System.Collections.Generic;

namespace Less.Html
{
    internal class FilterByClass : ElementFilter
    {
        internal string Class
        {
            get;
            private set;
        }

        internal FilterByClass(string className)
        {
            this.Class = className;
        }

        protected override IEnumerable<Element> EvalThis(Document document)
        {
            return document.all.GetElementsByClassName(this.Class);
        }

        protected override IEnumerable<Element> EvalThis(Document document, IEnumerable<Element> source)
        {
            if (document.IsNotNull())
            {
                Element[] elements = document.all.GetElementsByClassName(this.Class);

                return source.SelectMany(i => elements.Where(j => i.Contains(j)));
            }
            else
            {
                return source.SelectMany(i => i.EnumerateAllElements().Where(
                    j =>
                    j.className.IsNotNull() &&
                    j.className.SplitByWhiteSpace().Any(k => k == this.Class)));
            }
        }
    }
}
