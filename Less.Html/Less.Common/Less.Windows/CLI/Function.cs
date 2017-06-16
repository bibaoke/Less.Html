//bibaoke.com

using System;

namespace Less.Windows
{
    /// <summary>
    /// 功能
    /// </summary>
    public abstract class Function
    {
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name
        {
            get
            {
                return this.GetType().Name.ToLower();
            }
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public abstract bool Execute(params string[] args);

        /// <summary>
        /// 输出一行
        /// </summary>
        protected void WriteLine()
        {
            Console.WriteLine();
        }

        /// <summary>
        /// 输出一行
        /// </summary>
        /// <param name="content"></param>
        protected void WriteLine(object content)
        {
            Console.WriteLine(content);
        }

        /// <summary>
        /// 输出一行
        /// </summary>
        /// <param name="content"></param>
        /// <param name="parameters"></param>
        protected void WriteLine(string content, params string[] parameters)
        {
            Console.WriteLine(string.Format(content, parameters));
        }

        /// <summary>
        /// 输出一行
        /// </summary>
        /// <param name="content"></param>
        protected void WriteLine(string content)
        {
            Console.WriteLine(content);
        }
    }
}
