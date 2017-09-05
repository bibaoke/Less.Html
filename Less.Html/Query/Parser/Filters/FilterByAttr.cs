//bibaoke.com

using System.Linq;
using Less.Text;
using System.Collections.Generic;

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

        protected override IEnumerable<Element> EvalThis(Document document, IEnumerable<Element> source)
        {
            if (this.Name.CompareIgnoreCase("name"))
            {
                Element[] elements = document.all.GetElementsByName(this.Value);

                return source.SelectMany(i => elements.Where(j => j == i || j.IsParent(i)));
            }
            else
            {
                return source.SelectMany(i => i.GetAllElements().Where(j =>
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
}
