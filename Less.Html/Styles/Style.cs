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
            private set;
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

        internal Style(string selector) : this()
        {
            this.Selector = selector;
        }

        internal Style()
        {
            this.Properties = new PropertyCollection();
        }

        internal void Add(Property property)
        {
            this.Properties.Add(property);

            if (property.Name.CompareIgnoreCase("background"))
            {
                this.Background = new Background(property.Value);
            }
            else if (property.Name.CompareIgnoreCase("background-image"))
            {
                this.BackgroundImage = new BackgroundImage(property.Value);
            }
            else if (property.Name.CompareIgnoreCase("src"))
            {
                this.Src = new Src(property.Value);
            }
        }
    }
}
