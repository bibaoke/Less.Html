//bibaoke.com

using System.Linq;
using Less.Text;
using System.Collections.Generic;
using System;

namespace Less.Html
{
    internal class FilterByAttr : ElementFilter
    {
        internal string Name
        {
            get;
            private set;
        }

        internal string Value
        {
            get;
            private set;
        }

        internal FilterByAttr(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        protected override IEnumerable<Element> EvalThis(Document document)
        {
            if (this.Name.CompareIgnoreCase("name"))
            {
                return document.getElementsByName(this.Value);
            }
            else
            {
                return this.GetElementsByAttr(document.ChildNodeList.GetElements());
            }
        }

        protected override IEnumerable<Element> EvalThis(Document document, IEnumerable<Element> source)
        {
            if (this.Name.CompareIgnoreCase("name"))
            {
                Element[] elements = document.getElementsByName(this.Value);

                return source.SelectMany(i => elements.Where(j => i.EnumerateAllElements().Contains(j)));
            }
            else
            {
                return this.GetElementsByAttr(source);
            }
        }

        private IEnumerable<Element> GetElementsByAttr(IEnumerable<Element> source)
        {
            return source.SelectMany(i => i.EnumerateAllElements().Where(j =>
            {
                if (this.Value.IsNull())
                {
                    return j.attributes[this.Name].IsNotNull();
                }
                else
                {
                    Attr attr = j.attributes[this.Name];

                    if (attr.IsNull())
                    {
                        return false;
                    }
                    else
                    {
                        return attr.value.CompareIgnoreCase(this.Value);
                    }
                }
            }));
        }
    }
}
