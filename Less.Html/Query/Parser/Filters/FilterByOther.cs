//bibaoke.com

using Less.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Less.Html
{
    internal class FilterByOther : ElementFilter
    {
        internal string Condition
        {
            get;
            private set;
        }

        internal int Index
        {
            get;
            private set;
        }

        internal FilterByOther(string condition, int index)
        {
            this.Condition = condition;
            this.Index = index;
        }

        protected override IEnumerable<Element> EvalThis(IEnumerable<Element> source)
        {
            switch (this.Condition.ToLower())
            {
                case "first":
                    return source.Take(1);
                case "last":
                    return source.Reverse().Take(1);
                default:
                    throw new SelectorParamException(this.Index, this.Condition);
            }
        }
    }
}
