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

        internal FilterByClass(string cls)
        {
            this.Class = cls;
        }

        protected override IEnumerable<Element> EvalThis(IEnumerable<Element> source)
        {
            return source.SelectMany(i => i.GetAllElements().Where(j =>
                  j.className.IsNotNull() &&
                  j.className.SplitByWhiteSpace().Any(k => k.CompareIgnoreCase(this.Class))));
        }
    }
}
