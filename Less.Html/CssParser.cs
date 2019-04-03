//bibaoke.com

using Less.Html.CssInternal;

namespace Less.Html
{
    /// <summary>
    /// css 解析器
    /// </summary>
    public static class CssParser
    {
        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static Css Parse(string content)
        {
            ReaderBase reader = new SelectorReader();

            Context context = new Context(content);

            reader.Context = context;

            while (true)
            {
                reader = reader.Read();

                if (reader.IsNull())
                {
                    break;
                }
            }

            return context.Css;
        }
    }
}
