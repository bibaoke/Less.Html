//bibaoke.com

using System.Text;

namespace Less.Text
{
    /// <summary>
    /// 常用编码
    /// </summary>
    public static class MyEncoding
    {
        /// <summary>
        /// GB2312 编码
        /// </summary>
        public static Encoding GB2312
        {
            get;
            set;
        }

        static MyEncoding()
        {
            MyEncoding.GB2312 = Encoding.GetEncoding("GB2312");
        }
    }
}
