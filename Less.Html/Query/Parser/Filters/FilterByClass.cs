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

        protected override IEnumerable<Element> EvalThis(Document document, IEnumerable<Element> source)
        {
            Element[] elements = document.all.GetElementsByClassName(this.Class);

            return source.SelectMany(i => elements.Where(j => j == i || j.IsParent(i)));
        }
    }
}
