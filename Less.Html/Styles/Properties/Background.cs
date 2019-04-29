//bibaoke.com

using Less.Text;
using System.Text.RegularExpressions;

namespace Less.Html
{
    /// <summary>
    /// 背景
    /// </summary>
    public class Background : Property
    {
        private int UrlBegin
        {
            get;
            set;
        }

        private int UrlEnd
        {
            get;
            set;
        }

        private bool HasUrl
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

        internal Background(Property property) : base(property)
        {
            this.EachSpaceValue(property.Value, match =>
            {
                Match matchUrl = CssInfo.UrlPattern.Match(match.Value);

                if (matchUrl.Success)
                {
                    Group url = matchUrl.Groups["url"];

                    this.UrlBegin = property.ValueBegin + match.Index + url.Index;

                    this.UrlEnd = this.UrlBegin + url.Length - 1;

                    this.HasUrl = true;
                }
            });
        }

        /// <summary>
        /// 偏移发生了内容变动的属性值的索引
        /// </summary>
        /// <param name="index"></param>
        /// <param name="offset"></param>
        protected override void ShiftInsideValue(int index, int offset)
        {
            if (HasUrl)
            {
                if (this.UrlBegin > index)
                {
                    this.ShiftAfterValue(offset);
                }
                else
                {
                    if (this.UrlEnd >= index)
                    {
                        this.UrlEnd += offset;
                    }
                }
            }
        }

        /// <summary>
        /// 偏移文档变动位置后面的属性值的索引
        /// </summary>
        /// <param name="offset"></param>
        protected override void ShiftAfterValue(int offset)
        {
            if (HasUrl)
            {
                this.UrlBegin += offset;
                this.UrlEnd += offset;
            }
        }
    }
}
