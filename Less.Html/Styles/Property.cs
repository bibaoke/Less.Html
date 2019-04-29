//bibaoke.com

using Less.Text;
using System.Text.RegularExpressions;

namespace Less.Html
{
    /// <summary>
    /// 属性
    /// </summary>
    public class Property : CssInfo
    {
        internal int NameEnd
        {
            get;
            set;
        }

        internal int ValueBegin
        {
            get;
            set;
        }

        internal int ValueEnd
        {
            get;
            set;
        }

        /// <summary>
        /// 属性名
        /// </summary>
        public string Name
        {
            get
            {
                return this.OwnerCss.Content.SubstringUnsafe(this.Begin, this.NameEnd - this.Begin + 1);
            }
        }

        /// <summary>
        /// 属性值
        /// </summary>
        public string Value
        {
            get
            {
                return this.OwnerCss.Content.SubstringUnsafe(this.ValueBegin, this.ValueEnd - this.ValueBegin + 1);
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="property"></param>
        protected Property(Property property) : this(
            property.OwnerCss, property.Begin, property.NameEnd, property.ValueBegin, property.ValueEnd, property.End)
        {
            //
        }

        internal Property(Css ownerCss, int begin, int nameEnd, int valueBegin, int valueEnd, int end) : base(ownerCss, begin, end)
        {
            this.NameEnd = nameEnd;
            this.ValueBegin = valueBegin;
            this.ValueEnd = valueEnd;
        }

        /// <summary>
        /// 偏移发生了内容变动的属性的索引
        /// </summary>
        /// <param name="index"></param>
        /// <param name="offset"></param>
        internal void ShiftInsideProperty(int index, int offset)
        {
            this.End += offset;

            if (this.ValueEnd >= index)
            {
                this.ValueEnd += offset;

                if (this.ValueBegin > index)
                {
                    this.ValueBegin += offset;
                }

                if (this.NameEnd >= index)
                {
                    this.NameEnd += offset;
                }
            }

            this.ShiftInsideValue(index, offset);
        }

        /// <summary>
        /// 偏移发生了内容变动的属性值的索引
        /// </summary>
        /// <param name="index"></param>
        /// <param name="offset"></param>
        protected virtual void ShiftInsideValue(int index, int offset)
        {
            //
        }

        /// <summary>
        /// 偏移文档变动位置后面的属性的索引
        /// </summary>
        /// <param name="offset"></param>
        internal void ShiftAfterProperty(int offset)
        {
            this.Begin += offset;
            this.NameEnd += offset;
            this.ValueBegin += offset;
            this.ValueEnd += offset;
            this.End += offset;

            this.ShiftAfterValue(offset);
        }

        /// <summary>
        /// 偏移文档变动位置后面的属性值的索引
        /// </summary>
        /// <param name="offset"></param>
        protected virtual void ShiftAfterValue(int offset)
        {
            //
        }
    }
}
