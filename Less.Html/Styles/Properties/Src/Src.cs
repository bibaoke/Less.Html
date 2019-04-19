//bibaoke.com

namespace Less.Html
{
    /// <summary>
    /// 源文件
    /// </summary>
    public class Src
    {
        /// <summary>
        /// 值集合
        /// </summary>
        public SrcValueCollection Values
        {
            get;
            private set;
        }

        internal Src(string value)
        {
            this.Values = new SrcValueCollection();

            string[] values = value.Split(',');

            foreach (string i in values)
            {
                this.Values.Add(new SrcValue(i));
            }
        }
    }
}
