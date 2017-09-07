//bibaoke.com

using System;
using System.Collections.Generic;
using System.Linq;
using Less.Collection;

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

        protected override IEnumerable<Element> EvalThis(Document document)
        {
            return this.EvalThis(document, document.ChildNodeList.GetElements());
        }

        protected override IEnumerable<Element> EvalThis(Document document, IEnumerable<Element> source)
        {
            switch (this.Condition.ToLower())
            {
                case "first":
                    return source.Take(1);
                case "last":
                    Element last = source.LastOrDefault();

                    if (last.IsNotNull())
                    {
                        return last.ConstructArray();
                    }
                    else
                    {
                        return new Element[0];
                    }
                default:
                    throw new SelectorParamException(this.Index, this.Condition);
            }
        }
    }
}
