//bibaoke.com

using System;
using System.Collections.Generic;
using Less.Html.SelectorParser;
using System.Linq;

namespace Less.Html
{
    /// <summary>
    /// 选择器委托
    /// </summary>
    /// <param name="param"></param>
    /// <returns></returns>
    public delegate Query Qfun(SelectorParam param);

    /// <summary>
    /// 选择器
    /// </summary>
    public class Selector
    {
        internal Document Document
        {
            get;
            set;
        }

        internal SelectorParam Param
        {
            get;
            set;
        }

        internal List<Func<IEnumerable<Element>, IEnumerable<Element>>> ExtFilterList
        {
            get;
            private set;
        }

        private Selector(Document document, SelectorParam selectorParam)
        {
            this.Document = document;
            this.Param = selectorParam;

            this.ExtFilterList = new List<Func<IEnumerable<Element>, IEnumerable<Element>>>();
        }

        /// <summary>
        /// 绑定文档
        /// </summary>
        /// <param name="document">文档</param>
        /// <returns>jQuery 风格的 css 选择器</returns>
        /// <exception cref="ArgumentNullException">document 不能为 null</exception>
        public static Qfun Bind(Document document)
        {
            if (document.IsNull())
            {
                throw new ArgumentNullException("document", "document 不能为 null");
            }

            return selectorParam => new Query(new Selector(document, selectorParam));
        }

        /// <summary>
        /// 重新绑定同一个文档 以取得一个新的查询器
        /// </summary>
        /// <returns></returns>
        public Qfun Rebind()
        {
            return Selector.Bind(this.Document);
        }

        /// <summary>
        /// 选择元素
        /// </summary>
        /// <returns></returns>
        internal IEnumerable<Element> Select()
        {
            if (this.Param.IsNotNull())
            {
                if (this.Param.StringValue.IsNotNull())
                {
                    List<Node> nodes = this.Document.Parse(this.Param.StringValue).ChildNodeList;

                    if (nodes.Any(i => i is Element))
                    {
                        this.Param.NodesValue = nodes.ToArray();

                        this.Param.StringValue = null;
                    }
                }

                IEnumerable<Element> selected;

                if (this.Param.NodesValue.IsNull())
                {
                    if (this.Param.StringValue.IsNull())
                    {
                        selected = this.Param.QueryValue.Select();
                    }
                    else
                    {
                        selected = this.Select(this.Document, this.Param.StringValue);
                    }
                }
                else
                {
                    IEnumerable<Element> source = this.Param.NodesValue.GetElements();

                    if (this.Param.StringValue.IsNull())
                    {
                        selected = source;
                    }
                    else
                    {
                        Document document = source.GetOwnerDocument();

                        selected = this.Select(document, source, this.Param.StringValue);
                    }
                }

                foreach (Func<IEnumerable<Element>, IEnumerable<Element>> i in this.ExtFilterList)
                {
                    selected = i(selected);
                }

                return selected;
            }
            else
            {
                return new Element[0];
            }
        }

        /// <summary>
        /// 查询元素
        /// </summary>
        /// <param name="document"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        internal IEnumerable<Element> Select(Document document, string param)
        {
            return ParamParser.Parse(param).SelectMany(i => i.Eval(document)).Distinct().OrderBy(i => i.Index);
        }

        /// <summary>
        /// 查询元素
        /// </summary>
        /// <param name="document"></param>
        /// <param name="source"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        internal IEnumerable<Element> Select(Document document, IEnumerable<Element> source, string param)
        {
            ElementFilter[] filters = ParamParser.Parse(param);

            if (filters.Length > 1)
            {
                return filters.SelectMany(i => i.Eval(document, source)).Distinct().OrderBy(i => i.Index);
            }
            else
            {
                return filters[0].Eval(document, source).ToArray();
            }
        }
    }
}
