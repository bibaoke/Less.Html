//bibaoke.com

using System;

namespace Less.Windows
{
    /// <summary>
    /// 应用程序相关
    /// </summary>
    public static class Application
    {
        /// <summary>
        /// 安装目录
        /// </summary>
        public static string SetupDir
        {
            get
            {
                return AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            }
        }
    }
}
