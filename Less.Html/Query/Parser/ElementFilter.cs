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

        internal IEnumerable<Element> Eval(Document document, IEnumerable<Element> source)
        {
            IEnumerable<Element> elements = this.EvalThis(document, source);

            if (this.Next.IsNotNull())
            {
                elements = this.Next.Eval(document, elements);
            }
            else
            {
                if (this.Child.IsNotNull())
                {
                    elements = this.Child.Eval(document, elements.GetChildElements());
                }
            }

            return elements;
        }

        protected abstract IEnumerable<Element> EvalThis(Document document, IEnumerable<Element> source);
    }
}
