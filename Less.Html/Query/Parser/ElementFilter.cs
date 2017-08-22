//bibaoke.com

using System.Collections.Generic;

namespace Less.Html
{
    internal abstract class ElementFilter
    {
        internal ElementFilter Next
        {
            get;
            set;
        }

        internal ElementFilter Child
        {
            get;
            set;
        }

        internal IEnumerable<Element> Eval(IEnumerable<Element> source)
        {
            IEnumerable<Element> elements = this.EvalThis(source);

            if (this.Next.IsNotNull())
            {
                elements = this.Next.Eval(elements);
            }
            else
            {
                if (this.Child.IsNotNull())
                {
                    elements = this.Child.Eval(elements.GetChildElements());
                }
            }

            return elements;
        }

        protected abstract IEnumerable<Element> EvalThis(IEnumerable<Element> source);
    }
}
