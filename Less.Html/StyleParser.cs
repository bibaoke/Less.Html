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
            Style style = new Style();

            int position = 0;

            Match match = StyleParser.Pattern.Match(content, position);

            while (match.Success)
            {
                position = match.Index + match.Length;

                string name = match.GetValue("name");
                string value = match.GetValue("value");

                Property property = new Property(name, value);

                style.Add(property);

                match = StyleParser.Pattern.Match(content, position);
            }

            return style;
        }
    }
}
