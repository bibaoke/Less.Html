//bibaoke.com

using Less.Text;

namespace Less.Html
{
    /// <summary>
    /// 样式
    /// </summary>
    public class Style
    {
        /// <summary>
        /// 选择器
        /// </summary>
        public string Selector
        {
            get;
            internal set;
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

        internal Style(string selector)
        {
            this.Properties = new PropertyCollection();

            this.Selector = selector;
        }

        internal void Add(Property property)
        {
            this.Properties.Add(property);

            if (property.Name.CompareIgnoreCase("background"))
            {
                this.Background = new Background(property.Value);
            }
        }
    }
}
