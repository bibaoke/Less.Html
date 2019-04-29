//bibaoke.com

using Less.Text;
using System;
using System.Collections.Generic;

namespace Less.Html
{
    /// <summary>
    /// 样式
    /// </summary>
    public class Style : CssInfo
    {
        private Dictionary<string, Func<Property, Property>> Handlers
        {
            get;
            set;
        }

        internal int SelectorEnd
        {
            get;
            set;
        }

        /// <summary>
        /// 选择器
        /// </summary>
        public string Selector
        {
            get
            {
                return this.OwnerCss.Content.SubstringUnsafe(this.Begin, this.SelectorEnd - this.Begin + 1);
            }
        }

        /// <summary>
        /// 属性集合
        /// </summary>
        public PropertyCollection Properties
        {
            get;
            private set;
        }

        /// <summary>
        /// 背景
        /// </summary>
        public Background Background
        {
            get;
            private set;
        }

        /// <summary>
        /// 背景图片
        /// </summary>
        public BackgroundImage BackgroundImage
        {
            get;
            private set;
        }

        /// <summary>
        /// 源文件
        /// </summary>
        public Src Src
        {
            get;
            private set;
        }

        internal Style(Css ownerCss, int begin, int selectorEnd) : this(ownerCss, begin)
        {
            this.SelectorEnd = selectorEnd;
        }

        internal Style(Css ownerCss, int begin) : base(ownerCss, begin)
        {
            this.Handlers = new Dictionary<string, Func<Property, Property>>(StringComparer.OrdinalIgnoreCase);

            this.Handlers.Add("background", property => this.Background = new Background(property));
            this.Handlers.Add("background-image", property => this.BackgroundImage = new BackgroundImage(property));
            this.Handlers.Add("src", property => this.Src = new Src(property));

            this.Properties = new PropertyCollection();
        }

        internal void Add(Property property)
        {
            Func<Property, Property> func;

            if (this.Handlers.TryGetValue(property.Name, out func))
            {
                this.Properties.Add(func(property));
            }
            else
            {
                this.Properties.Add(property);
            }
        }
    }
}
