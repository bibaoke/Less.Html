//bibaoke.com

using Less.Text;
using System.Text.RegularExpressions;

namespace Less.Html
{
    /// <summary>
    /// style 解析器
    /// </summary>
    public static class StyleParser
    {
        private static Regex Pattern
        {
            get;
            set;
        }

        static StyleParser()
        {
            StyleParser.Pattern = @"(?<name>[\w-]+):\s*(?<value>.+?)\s*(;|$)".ToRegex(
                RegexOptions.Singleline |
                RegexOptions.Compiled |
                RegexOptions.ExplicitCapture);
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static Style Parse(string content)
        {
            Css css = new Css(content);

            Style style = new Style(css, 0);

            int position = 0;

            Match match = StyleParser.Pattern.Match(content, position);

            while (match.Success)
            {
                Group name = match.Groups["name"];
                Group value = match.Groups["value"];

                Property property = new Property(css,
                    name.Index,
                    name.Index + name.Length - 1,
                    value.Index,
                    value.Index + value.Length - 1,
                    match.Index + match.Length - 1);

                style.Add(property);

                position = match.Index + match.Length;

                match = StyleParser.Pattern.Match(content, position);
            }

            style.End = css.Content.Length - 1;

            css.Styles.Add(style);

            return style;
        }
    }
}
