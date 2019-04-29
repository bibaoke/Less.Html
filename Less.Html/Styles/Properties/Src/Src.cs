//bibaoke.com

using System.Text.RegularExpressions;

namespace Less.Html
{
    /// <summary>
    /// 源文件
    /// </summary>
    public class Src : Property
    {
        /// <summary>
        /// 值集合
        /// </summary>
        public SrcValueCollection Values
        {
            get;
            private set;
        }

        internal Src(Property property) : base(property)
        {
            this.Values = new SrcValueCollection();

            this.EachCommaValue(property.Value, match =>
            {
                int begin = this.ValueBegin + match.Index;

                int end = begin + match.Length - 1;

                this.Values.Add(new SrcValue(this.OwnerCss, begin, end));
            });
        }

        /// <summary>
        /// 偏移发生了内容变动的属性值的索引
        /// </summary>
        /// <param name="index"></param>
        /// <param name="offset"></param>
        protected override void ShiftInsideValue(int index, int offset)
        {
            for (int i = this.Values.Count - 1; i >= 0; i--)
            {
                SrcValue value = this.Values[i];

                if (value.HasUrl)
                {
                    if (value.UrlEnd >= index)
                    {
                        value.UrlEnd += offset;

                        if (value.UrlBegin > index)
                        {
                            value.UrlBegin += offset;
                        }
                    }
                    else
                    {
                        break;
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
            foreach (SrcValue i in this.Values)
            {
                if (i.HasUrl)
                {
                    i.UrlBegin += offset;
                    i.UrlEnd += offset;
                }
            }
        }
    }
}
