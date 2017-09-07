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

        internal IEnumerable<Element> Eval(Document document)
        {
            IEnumerable<Element> elements = this.EvalThis(document);

            return this.EvalOthers(document, elements);
        }

        internal IEnumerable<Element> Eval(Document document, IEnumerable<Element> source)
        {
            IEnumerable<Element> elements = this.EvalThis(document, source);

            return this.EvalOthers(document, elements);
        }

        protected abstract IEnumerable<Element> EvalThis(Document document);

        protected abstract IEnumerable<Element> EvalThis(Document document, IEnumerable<Element> source);

        private IEnumerable<Element> EvalOthers(Document document, IEnumerable<Element> elements)
        {
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
    }
}
