//bibaoke.com

using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Less.Collection;
using Less.Text;

namespace Less.Html.SelectorParser
{
    internal class FilterByOther : ElementFilter
    {
        private static Regex EqPattern
        {
            get;
            set;
        }

        private static Regex GtPattern
        {
            get;
            set;
        }

        private static Regex LtPattern
        {
            get;
            set;
        }

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

        static FilterByOther()
        {
            FilterByOther.EqPattern = @"eq\((?<index>\d+)\)".ToRegex(
                RegexOptions.Compiled |
                RegexOptions.IgnoreCase);

            FilterByOther.GtPattern = @"gt\((?<index>\d+)\)".ToRegex(
                RegexOptions.Compiled |
                RegexOptions.IgnoreCase);

            FilterByOther.LtPattern = @"lt\((?<index>\d+)\)".ToRegex(
                RegexOptions.Compiled |
                RegexOptions.IgnoreCase);
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
            if (this.Condition.CompareIgnoreCase("first"))
            {
                return source.Take(1);
            }
            else if (this.Condition.CompareIgnoreCase("last"))
            {
                Element last = source.LastOrDefault();

                if (last.IsNull())
                {
                    return new Element[0];
                }
                else
                {
                    return last.ConstructArray();
                }
            }
            else if (this.Condition.CompareIgnoreCase("checkbox"))
            {
                return source.SelectMany(i => i.EnumerateAllElements().Where(
                    j =>
                    j.Name.CompareIgnoreCase("input") &&
                    j.getAttribute("type").CompareIgnoreCase("checkbox")));
            }
            else
            {
                Match eq = FilterByOther.EqPattern.Match(this.Condition);

                if (eq.Success)
                {
                    int? index = eq.GetValue("index").ToInt();

                    if (index.IsNotNull())
                    {
                        return source.SelectMany(i => i.EnumerateAllElements()).Skip(index.Value).Take(1);
                    }
                    else
                    {
                        throw new SelectorParamException(this.Index, this.Condition);
                    }
                }
                else
                {
                    Match gt = FilterByOther.GtPattern.Match(this.Condition);

                    if (gt.Success)
                    {
                        int? index = gt.GetValue("index").ToInt();

                        if (index.IsNotNull())
                        {
                            return source.SelectMany(i => i.EnumerateAllElements()).Skip(index.Value + 1);
                        }
                        else
                        {
                            throw new SelectorParamException(this.Index, this.Condition);
                        }
                    }
                    else
                    {
                        Match lt = FilterByOther.LtPattern.Match(this.Condition);

                        if (lt.Success)
                        {
                            int? index = lt.GetValue("index").ToInt();

                            if (index.IsNotNull())
                            {
                                return source.SelectMany(i => i.EnumerateAllElements()).Take(index.Value);
                            }
                            else
                            {
                                throw new SelectorParamException(this.Index, this.Condition);
                            }
                        }
                        else
                        {
                            throw new SelectorParamException(this.Index, this.Condition);
                        }
                    }
                }
            }
        }
    }
}
