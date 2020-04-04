//bibaoke.com

using System.Collections.Generic;

namespace Less.Html
{
    /// <summary>
    /// 查询器
    /// </summary>
    public partial class Query : IEnumerable<Element>
    {
        /// <summary>
        /// 获取元素的 id 属性
        /// </summary>
        /// <returns></returns>
        public string id()
        {
            return this.attr("id");
        }

        /// <summary>
        /// 设置元素的 id 属性
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Query id(object value)
        {
            this.id(value.ToString());

            return this;
        }

        /// <summary>
        /// 设置元素的 id 属性
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Query id(string value)
        {
            this.attr("id", value);

            return this;
        }

        /// <summary>
        /// 获取元素的 selected 属性
        /// </summary>
        /// <returns></returns>
        public string selected()
        {
            return this.attr("selected");
        }

        /// <summary>
        /// 设置元素的 selected 属性
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Query selected(string value)
        {
            this.attr("selected", value);

            return this;
        }

        /// <summary>
        /// 获取元素的 checked 属性
        /// </summary>
        /// <returns></returns>
        public string chked()
        {
            return this.attr("checked");
        }

        /// <summary>
        /// 设置元素的 checked 属性
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Query chked(string value)
        {
            this.attr("checked", value);

            return this;
        }

        /// <summary>
        /// 获取元素的 title 属性
        /// </summary>
        /// <returns></returns>
        public string title()
        {
            return this.attr("title");
        }

        /// <summary>
        /// 设置元素的 title 属性
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Query title(string value)
        {
            return this.attr("title", value);
        }

        /// <summary>
        /// 获取元素的 src 属性
        /// </summary>
        /// <returns></returns>
        /// <exception cref="SelectorParamException">选择器参数错误</exception>
        public string src()
        {
            return this.attr("src");
        }

        /// <summary>
        /// 设置元素的 src 属性
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="SelectorParamException">选择器参数错误</exception>
        public Query src(string value)
        {
            return this.attr("src", value);
        }

        /// <summary>
        /// 获取元素的 href 属性
        /// </summary>
        /// <returns></returns>
        /// <exception cref="SelectorParamException">选择器参数错误</exception>
        public string href()
        {
            return this.attr("href");
        }

        /// <summary>
        /// 设置元素的 href 属性
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="SelectorParamException">选择器参数错误</exception>
        public Query href(string value)
        {
            return this.attr("href", value);
        }
    }
}
