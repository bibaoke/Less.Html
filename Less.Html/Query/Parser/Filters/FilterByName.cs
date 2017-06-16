//bibaoke.com

using System.Linq;
using Less.Text;
using System.Collections.Generic;

namespace Less.Html
{
    internal class FilterByName : ElementFilter
    {
        internal string Name
        {
            get;
            private set;
        }

        internal FilterByName(string name)
        {
            this.Name = name;
        }

        protected override IEnumerable<Element> EvalThis(IEnumerable<Element> source)
        {
            return source.SelectMany(i => i.GetAllElements().Where(j => j.nodeName.CompareIgnoreCase(this.Name)));
        }
    }
}
