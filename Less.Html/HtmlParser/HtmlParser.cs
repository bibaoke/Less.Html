//bibaoke.com

using System;

namespace Less.Html
{
    /// <summary>
    /// html 解析器
    /// </summary>
    public static class HtmlParser
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="content">要查询的文档</param>
        /// <returns></returns>
        public static Func<SelectorParam, Query> Query(string content)
        {
            return Selector.Bind(HtmlParser.Parse(content));
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="content">要解析的文档</param>
        /// <returns></returns>
        public static Document Parse(string content)
        {
            //创建标签阅读器
            ReaderBase reader = new TagReader();

            //阅读器上下文
            Context context = new Context(content, HtmlParser.Parse);

            //设置上下文
            reader.Context = context;

            //读取所有内容
            while (true)
            {
                //执行读取 返回下一个阅读器
                reader = reader.Read();

                //不返回阅读器 读取完毕 跳出
                if (reader.IsNull())
                    break;
            }

            //返回文档元素
            return context.Document;
        }
    }
}
