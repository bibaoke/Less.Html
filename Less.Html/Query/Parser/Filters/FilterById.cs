//bibaoke.com

using System.Linq;
using Less.Text;
using System.Collections.Generic;

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

        protected override IEnumerable<Element> EvalThis(IEnumerable<Element> source)
        {
            return source.SelectMany(i => i.GetAllElements().Where(j => j.id.IsNotNull() && j.id.CompareIgnoreCase(this.Id)));
        }
    }
}
