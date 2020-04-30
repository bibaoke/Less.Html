using Less.Html;
using Less.Windows;
using System.Net;
using System.Text;

namespace Test
{
    public class Test3 : Function
    {
        public override string Description
        {
            get
            {
                return "示例三：与 WebClient 的配合使用，以抓取 CSDN 论坛内容为例 http://bibaoke.com/post/77";
            }
        }

        public override bool Execute(params string[] args)
        {
            WebClient client = new WebClient();

            client.Encoding = Encoding.UTF8;

            string aspDotNet = client.DownloadString(
                "http://bbs.csdn.net/forums/ASPDotNET");

            var q = HtmlParser.Query(aspDotNet);

            var title = q(".forums_title");

            foreach (Element i in title)
            {
                this.WriteLine(i.textContent);
            }

            return true;
        }
    }
}