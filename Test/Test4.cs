using Less.Windows;
using System;

namespace Test
{
    public class Test4 : Function
    {
        public override string Description
        {
            get
            {
                return "示例四：与 WebBrowser 的配合使用，以抓取京东手机价格为例 http://bibaoke.com/post/78";
            }
        }

        public override bool Execute(params string[] args)
        {
            Cmd.Exec(Application.SetupDir.CombinePath("Test4.exe"), (s) =>
            {
                Console.WriteLine(s);
            });

            return true;
        }
    }
}