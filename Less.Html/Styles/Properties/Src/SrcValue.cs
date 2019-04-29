//bibaoke.com

using Less.Text;
using System.Text.RegularExpressions;

namespace Less.Html
{
    /// <summary>
    /// 源文件值
    /// </summary>
    public class SrcValue : CssInfo
    {
        internal int UrlBegin
        {
            get;
            set;
        }

        internal int UrlEnd
        {
            get;
            set;
        }

        internal bool HasUrl
        {
            get;
            set;
        }

        /// <summary>
        /// url
        /// </summary>
        public string Url
        {
            get
            {
                if (HasUrl)
                {
                    return this.OwnerCss.Content.SubstringUnsafe(this.UrlBegin, this.UrlEnd - this.UrlBegin + 1);
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (HasUrl)
                {
                    this.OwnerCss.Replace(this.UrlBegin, this.UrlEnd - this.UrlBegin + 1, value);
                }
                else
                {
                    //
                }
            }
        }

        internal SrcValue(Css ownerCss, int begin, int end) : base(ownerCss, begin, end)
        {
            this.EachSpaceValue(this.ToString(), match =>
            {
                Match matchUrl = Property.UrlPattern.Match(match.Value);

                if (matchUrl.Success)
                {
                    Group url = matchUrl.Groups["url"];

                    this.UrlBegin = this.Begin + match.Index + url.Index;

                    this.UrlEnd = this.UrlBegin + url.Length - 1;

                    this.HasUrl = true;
                }
            });
        }
    }
}
