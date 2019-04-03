//bibaoke.com

namespace Less.Html
{
    /// <summary>
    /// 属性
    /// </summary>
    public class Property
    {
        /// <summary>
        /// 属性名
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// 属性值
        /// </summary>
        public string Value
        {
            get;
            private set;
        }

        internal Property(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }
    }
}
