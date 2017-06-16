//bibaoke.com

namespace Less.Text
{
    /// <summary>
    /// 字符串枚举
    /// </summary>
    public abstract class StringEnum
    {
        /// <summary>
        /// 字符串值
        /// </summary>
        protected string Value
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        protected StringEnum(string value)
        {
            this.Value = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
    }
}
