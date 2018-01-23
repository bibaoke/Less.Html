//bibaoke.com

using System;
using System.Collections.Generic;
using Less.Encrypt;
using System.Text;

namespace Less.Html
{
    /// <summary>
    /// html 解析器
    /// </summary>
    public static class HtmlParser
    {
        private static Dictionary<string, Document> Cache
        {
            get;
            set;
        }

        static HtmlParser()
        {
            HtmlParser.Cache = new Dictionary<string, Document>();
        }

        /// <summary>
        /// 解析 html
        /// 返回选择器
        /// </summary>
        /// <param name="content">要解析的 html</param>
        /// <param name="cache">是否使用缓存</param>
        /// <returns>jQuery 风格的 css 选择器</returns>
        public static Qfun Query(string content, bool cache)
        {
            Document document = HtmlParser.Parse(content, cache);

            return Selector.Bind(document);
        }

        /// <summary>
        /// 解析 html
        /// 返回选择器
        /// </summary>
        /// <param name="content">要解析的 html</param>
        /// <returns>jQuery 风格的 css 选择器</returns>
        public static Qfun Query(string content)
        {
            Document document = HtmlParser.Parse(content);

            return Selector.Bind(document);
        }

        /// <summary>
        /// 解析 html
        /// 返回文档
        /// </summary>
        /// <param name="content">要解析的 html</param>
        /// <param name="cache">是否使用缓存</param>
        /// <returns>DOM 文档</returns>
        /// <exception cref="ArgumentNullException">content 不能为 null</exception>
        public static Document Parse(string content, bool cache)
        {
            if (cache)
            {
                Document document;

                if (HtmlParser.Cache.TryGetValue(content, out document))
                {
                    Document clone = (Document)document.cloneNode(true);

                    return clone;
                }
                else
                {
                    document = HtmlParser.Parse(content);

                    Document clone = (Document)document.cloneNode(true);

                    try
                    {
                        HtmlParser.Cache.Add(content, clone);
                    }
                    catch (ArgumentException)
                    {
                        //
                    }

                    return document;
                }
            }
            else
            {
                return HtmlParser.Parse(content);
            }
        }

        /// <summary>
        /// 解析 html
        /// 返回文档
        /// </summary>
        /// <param name="content">要解析的 html</param>
        /// <returns>DOM 文档</returns>
        public static Document Parse(string content)
        {
            //创建标签阅读器
            ReaderBase reader = new TagReader();

            //阅读器上下文
            Context context = new Context(content, c => HtmlParser.Parse(c));

            //设置上下文
            reader.Context = context;

            //读取所有内容
            while (true)
            {
                //执行读取 返回下一个阅读器
                reader = reader.Read();

                //不返回阅读器 读取完毕 跳出
                if (reader.IsNull())
                {
                    break;
                }
            }

            //返回文档元素
            return context.Document;
        }
    }
}
