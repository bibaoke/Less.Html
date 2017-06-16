//bibaoke.com

namespace Less.Windows
{
    /// <summary>
    /// 退出程序
    /// </summary>
    public class Exit : Function
    {
        public override string Description
        {
            get
            {
                return "退出";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public override bool Execute(params string[] args)
        {
            return false;
        }
    }
}
