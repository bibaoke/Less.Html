//bibaoke.com

using System;
using Less.Collection;
using System.Collections.Generic;
using System.Linq;
using Less.Text;
using System.Collections;

namespace Less.Html
{
    /// <summary>
    /// 查询器
    /// </summary>
    public class Query : IEnumerable<Element>
    {
        private Selector Selector
        {
            get;
            set;
        }

        internal Query(Selector selector)
        {
            this.Selector = selector;
        }

        /// <summary>
        /// 获取枚举器
        /// </summary>
        /// <returns></returns>
        /// <exception cref="DocumentException">文档异常</exception>
        /// <exception cref="SelectorParamException">选择器参数错误</exception>
        public IEnumerator<Element> GetEnumerator()
        {
            return this.Select().GetEnumerator();
        }

        /// <summary>
        /// 获取枚举器
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Select().GetEnumerator();
        }

        /// <summary>
        /// 获取指定索引的元素
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="DocumentException">文档异常</exception>
        /// <exception cref="SelectorParamException">选择器参数错误</exception>
        public Element this[int index]
        {
            get
            {
                IEnumerable<Element> elements = this.Select();

                return elements.ToArray()[index];
            }
        }

        /// <summary>
        /// 查询到的元素数
        /// </summary>
        /// <exception cref="DocumentException">文档异常</exception>
        /// <exception cref="SelectorParamException">选择器参数错误</exception>
        public int length
        {
            get
            {
                return this.Select().ToArray().Length;
            }
        }

        /// <summary>
        /// 是否查询到元素
        /// </summary>
        /// <exception cref="DocumentException">文档异常</exception>
        /// <exception cref="SelectorParamException">选择器参数错误</exception>
        public bool hasElement
        {
            get
            {
                return this.length > 0;
            }
        }

        /// <summary>
        /// 向被选元素添加一个或多个类
        /// </summary>
        /// <param name="classes"></param>
        /// <returns></returns>
        /// <exception cref="DocumentException">文档异常</exception>
        /// <exception cref="SelectorParamException">选择器参数错误</exception>
        public Query addClass(string classes)
        {
            IEnumerable<Element> elements = this.Select();

            string[] addArray = classes.SplitByWhiteSpace();

            foreach (Element i in elements)
            {
                string[] oriArray = i.className.SplitByWhiteSpace();

                DynamicString className = new DynamicString(i.className).Append(Symbol.Space);

                foreach (string j in addArray)
                {
                    if (!oriArray.Any(k => k.CompareIgnoreCase(j)))
                        className.Append(j).Append(Symbol.Space);
                }

                i.className = className.ToString().TrimEnd();
            }

            return this;
        }

        /// <summary>
        /// 返回被选元素的直接父元素
        /// </summary>
        /// <returns></returns>
        public Query parent()
        {
            var parent = this.Copy();

            parent.Selector.ExtFilterList.Add(selected => selected.Select(i => i.parentNode).GetElements());

            return parent;
        }

        /// <summary>
        /// 创建匹配元素集合的副本
        /// </summary>
        /// <returns></returns>
        public Query clone()
        {
            IEnumerable<Element> elements = this.Select();

            List<Node> clone = new List<Node>();

            foreach (Element i in elements)
                clone.Add(i.cloneNode(true));

            var q = this.Selector.Rebind();

            return q(clone.ToArray());
        }

        /// <summary>
        /// 移除所有匹配的元素
        /// </summary>
        /// <exception cref="DocumentException">文档异常</exception>
        /// <exception cref="SelectorParamException">选择器参数错误</exception>
        public Query remove()
        {
            IEnumerable<Element> elements = this.Select();

            List<Node> list = new List<Node>();

            foreach (Element i in elements)
            {
                list.Add(i.parentNode.removeChild(i));
            }

            var q = this.Selector.Rebind();

            return q(list.ToArray());
        }

        /// <summary>
        /// 设置元素集合的 textContent
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        /// <exception cref="DocumentException">文档异常</exception>
        /// <exception cref="SelectorParamException">选择器参数错误</exception>
        public Query text(string content)
        {
            IEnumerable<Element> elements = this.Select();

            foreach (Element i in elements)
                i.textContent = content;

            return this;
        }

        /// <summary>
        /// 返回元素集合的 textContent 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="DocumentException">文档异常</exception>
        /// <exception cref="SelectorParamException">选择器参数错误</exception>
        public string text()
        {
            IEnumerable<Element> elements = this.Select();

            DynamicString text = new DynamicString();

            foreach (Element i in elements)
                text.Append(i.textContent);

            return text;
        }

        /// <summary>
        /// 设置 value 属性
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="DocumentException">文档异常</exception>
        /// <exception cref="SelectorParamException">选择器参数错误</exception>
        public Query val(object value)
        {
            this.val(value.ToString());

            return this;
        }

        /// <summary>
        /// 设置 value 属性
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="DocumentException">文档异常</exception>
        /// <exception cref="SelectorParamException">选择器参数错误</exception>
        public Query val(string value)
        {
            this.attr("value", value);

            return this;
        }

        /// <summary>
        /// 设置 src 属性
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="DocumentException">文档异常</exception>
        /// <exception cref="SelectorParamException">选择器参数错误</exception>
        public Query src(string value)
        {
            return this.attr("src", value);
        }

        /// <summary>
        /// 获取 src 属性
        /// </summary>
        /// <returns></returns>
        /// <exception cref="DocumentException">文档异常</exception>
        /// <exception cref="SelectorParamException">选择器参数错误</exception>
        public string src()
        {
            return this.attr("src");
        }

        /// <summary>
        /// 获取 href 属性
        /// </summary>
        /// <returns></returns>
        /// <exception cref="DocumentException">文档异常</exception>
        /// <exception cref="SelectorParamException">选择器参数错误</exception>
        public string href()
        {
            return this.attr("href");
        }

        /// <summary>
        /// 设置 href 属性
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="DocumentException">文档异常</exception>
        /// <exception cref="SelectorParamException">选择器参数错误</exception>
        public Query href(string value)
        {
            return this.attr("href", value);
        }

        /// <summary>
        /// 获取属性
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="DocumentException">文档异常</exception>
        /// <exception cref="SelectorParamException">选择器参数错误</exception>
        public string attr(string name)
        {
            IEnumerable<Element> elements = this.Select();

            foreach (Element i in elements)
                return i.getAttribute(name);

            return null;
        }

        /// <summary>
        /// 设置属性
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="DocumentException">文档异常</exception>
        /// <exception cref="SelectorParamException">选择器参数错误</exception>
        public Query attr(string name, object value)
        {
            return this.attr(name, value.IsNull() ? "" : value.ToString());
        }

        /// <summary>
        /// 设置属性
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="DocumentException">文档异常</exception>
        /// <exception cref="SelectorParamException">选择器参数错误</exception>
        public Query attr(string name, string value)
        {
            IEnumerable<Element> elements = this.Select();

            foreach (Element i in elements)
            {
                i.setAttribute(name, value);
            }

            return this;
        }

        /// <summary>
        /// 根据参数查找元素
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public Query find(string param)
        {
            var find = this.Copy();

            find.Selector.ExtFilterList.Add(
                selected => find.Selector.Select(selected.GetChildElements(), param));

            return find;
        }

        /// <summary>
        /// 在匹配的元素之后插入内容
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <exception cref="DocumentException">文档异常</exception>
        /// <exception cref="SelectorParamException">选择器参数错误</exception>
        public Query after(params SelectorParam[] parameters)
        {
            foreach (SelectorParam i in parameters)
            {
                this.Operate(i,
                    (element, nodes) =>
                    {
                        Node after = element.nextSibling;

                        if (after.IsNotNull())
                        {
                            foreach (Node j in nodes)
                            {
                                element.parentNode.insertBefore(j, after);
                            }
                        }
                        else
                        {
                            foreach (Node j in nodes)
                            {
                                element.parentNode.appendChild(j);
                            }
                        }
                    },

                    (element, html) =>
                    {
                        Node after = element.nextSibling;

                        Node[] nodes = element.ownerDocument.Parse(html).childNodes;

                        if (after.IsNotNull())
                        {
                            foreach (Node j in nodes)
                            {
                                element.parentNode.insertBefore(j, after);
                            }
                        }
                        else
                        {
                            foreach (Node j in nodes)
                            {
                                element.parentNode.appendChild(j);
                            }
                        }
                    },

                    (element, query) =>
                    {
                        Node after = element.nextSibling;

                        IEnumerable<Element> elements = query.Select();

                        if (after.IsNotNull())
                        {
                            foreach (Element j in elements)
                            {
                                element.parentNode.insertBefore(j, after);
                            }
                        }
                        else
                        {
                            foreach (Element j in elements)
                            {
                                element.parentNode.appendChild(j);
                            }
                        }
                    });
            }

            return this;
        }

        /// <summary>
        /// 在每个匹配的元素之前插入内容
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <exception cref="DocumentException">文档异常</exception>
        /// <exception cref="SelectorParamException">选择器参数错误</exception>
        public Query before(params SelectorParam[] parameters)
        {
            foreach (SelectorParam i in parameters)
            {
                this.Operate(i,
                    (element, nodes) =>
                    {
                        foreach (Node j in nodes)
                        {
                            element.parentNode.insertBefore(j, element);
                        }
                    },

                    (element, html) =>
                    {
                        foreach (Node j in element.ownerDocument.Parse(html).childNodes)
                        {
                            element.parentNode.insertBefore(j, element);
                        }
                    },

                    (element, query) =>
                    {
                        IEnumerable<Element> elements = query.Select();

                        foreach (Element j in elements)
                        {
                            element.parentNode.insertBefore(j, element);
                        }
                    });
            }

            return this;
        }

        /// <summary>
        /// 在被选元素的开头插入内容
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <exception cref="DocumentException">文档异常</exception>
        /// <exception cref="SelectorParamException">选择器参数错误</exception>
        public Query prepend(params SelectorParam[] parameters)
        {
            foreach (SelectorParam i in parameters)
            {
                this.Operate(i,
                    (element, nodes) =>
                    {
                        nodes.EachDesc(j =>
                        {
                            if (element.hasChildNodes())
                            {
                                element.insertBefore(j, element.childNodes[0]);
                            }
                            else
                            {
                                element.appendChild(j);
                            }
                        });
                    },

                    (element, html) =>
                    {
                        element.innerHTML = element.innerHTML.Insert(0, html);
                    },

                    (element, query) =>
                    {
                        IEnumerable<Element> elements = query.Select();

                        foreach (Element j in elements.Reverse())
                        {
                            if (element.hasChildNodes())
                            {
                                element.insertBefore(j, element.childNodes[0]);
                            }
                            else
                            {
                                element.appendChild(j);
                            }
                        }
                    });
            }

            return this;
        }

        /// <summary>
        /// 在被选元素的结尾插入内容
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <exception cref="DocumentException">文档异常</exception>
        /// <exception cref="SelectorParamException">选择器参数错误</exception>
        public Query append(params SelectorParam[] parameters)
        {
            foreach (SelectorParam i in parameters)
            {
                this.Operate(i,
                    (element, nodes) =>
                    {
                        foreach (Node j in nodes)
                        {
                            element.appendChild(j);
                        }
                    },

                    (element, html) =>
                    {
                        element.innerHTML += html;
                    },

                    (element, query) =>
                    {
                        IEnumerable<Element> elements = query.Select();

                        foreach (Element j in elements)
                        {
                            element.appendChild(j);
                        }
                    });
            }

            return this;
        }

        /// <summary>
        /// 删除子元素
        /// </summary>
        /// <returns></returns>
        /// <exception cref="DocumentException">文档异常</exception>
        /// <exception cref="SelectorParamException">选择器参数错误</exception>
        public Query empty()
        {
            return this.html("");
        }

        /// <summary>
        /// 设置元素集合的 innerHTML
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        /// <exception cref="DocumentException">文档异常</exception>
        /// <exception cref="SelectorParamException">选择器参数错误</exception>
        public Query html(object html)
        {
            return this.html(html.IsNull() ? "" : html.ToString());
        }

        /// <summary>
        /// 设置元素集合的 innerHTML
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        /// <exception cref="DocumentException">文档异常</exception>
        /// <exception cref="SelectorParamException">选择器参数错误</exception>
        public Query html(SelectorParam param)
        {
            this.Operate(param,
                (element, nodes) =>
                {
                    element.childNodes = nodes;
                },

                (element, html) =>
                {
                    element.innerHTML = html;
                },

                (element, query) =>
                {
                    IEnumerable<Element> elements = query.Select();

                    element.childNodes = elements.ToArray();
                });

            return this;
        }

        /// <summary>
        /// 返回元素集合的 innerHTML
        /// </summary>
        /// <returns></returns>
        /// <exception cref="DocumentException">文档异常</exception>
        /// <exception cref="SelectorParamException">选择器参数错误</exception>
        public string html()
        {
            IEnumerable<Element> elements = this.Select();

            DynamicString html = new DynamicString();

            foreach (Element i in elements)
                html.Append(i.innerHTML);

            return html;
        }

        internal IEnumerable<Element> Select()
        {
            return this.Selector.Select();
        }

        private Query Copy()
        {
            var q = this.Selector.Rebind();

            var copy = q(this.Selector.Param);

            copy.Selector.ExtFilterList.AddRange(this.Selector.ExtFilterList);

            return copy;
        }

        private void Operate(
            SelectorParam param,
            Action<Element, Node[]> nodesAction, Action<Element, string> stringAction, Action<Element, Query> queryAction)
        {
            if (param.IsNotNull())
            {
                IEnumerable<Element> elements = this.Select();

                if (param.NodesValue.IsNotNull())
                {
                    foreach (Element i in elements)
                    {
                        nodesAction(i, param.NodesValue);
                    }
                }
                else if (param.StringValue.IsNotNull())
                {
                    foreach (Element i in elements)
                    {
                        stringAction(i, param.StringValue);
                    }
                }
                else
                {
                    foreach (Element i in elements)
                    {
                        queryAction(i, param.QueryValue);
                    }
                }
            }
        }
    }
}
