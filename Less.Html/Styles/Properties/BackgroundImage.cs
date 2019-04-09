//bibaoke.com

using Less.Text;
using System.Text.RegularExpressions;

namespace Less.Html
{
    /// <summary>
    /// 背景图片
    /// </summary>
    public class BackgroundImage
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

        static BackgroundImage()
        {
            BackgroundImage.UrlPattern = @"
                url\((?<mark>[""'])(?<url>.*?)\k<mark>\)|
                url\((?<url>.*?)\)
                ".ToRegex(
                RegexOptions.IgnorePatternWhitespace |
                RegexOptions.IgnoreCase |
                RegexOptions.Compiled |
                RegexOptions.ExplicitCapture);
        }

        internal BackgroundImage(string value)
        {
            string[] values = value.SplitByWhiteSpace();

            foreach (string i in values)
            {
                Match matchUrl = BackgroundImage.UrlPattern.Match(i);

                if (matchUrl.Success)
                {
                    this.Url = matchUrl.GetValue("url");
                }
            }
        }
    }
}
