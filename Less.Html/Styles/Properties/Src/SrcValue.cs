//bibaoke.com

using Less.Text;
using System.Text.RegularExpressions;

namespace Less.Html
{
    /// <summary>
    /// 源文件值
    /// </summary>
    public class SrcValue
    {
        private static Regex UrlPattern
        {
            get;
            set;
        }

        /// <summary>
        /// url
        /// </summary>
        public string Url
        {
            get;
            private set;
        }

        static SrcValue()
        {
            SrcValue.UrlPattern = @"
                url\((?<mark>[""'])(?<url>.*?)\k<mark>\)|
                url\((?<url>.*?)\)
                ".ToRegex(
                RegexOptions.IgnorePatternWhitespace |
                RegexOptions.IgnoreCase |
                RegexOptions.Compiled |
                RegexOptions.ExplicitCapture);
        }

        internal SrcValue(string value)
        {
            string[] values = value.SplitByWhiteSpace();

            foreach (string i in values)
            {
                Match matchUrl = SrcValue.UrlPattern.Match(i);

                if (matchUrl.Success)
                {
                    this.Url = matchUrl.GetValue("url");
                }
            }
        }
    }
}
